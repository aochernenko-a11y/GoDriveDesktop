using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace GoDriveDesktop
{
    public class RoundedPanel : Panel
    {
        private int borderRadius = 20;
        private int borderThickness = 3;
        private Color borderColor = Color.FromArgb(60, 120, 140);

        public RoundedPanel()
        {
            SetStyle(
                ControlStyles.AllPaintingInWmPaint |
                ControlStyles.UserPaint |
                ControlStyles.OptimizedDoubleBuffer |
                ControlStyles.ResizeRedraw,
                true
            );

            BackColor = Color.FromArgb(145, 201, 214);
        }

        [Category("Appearance")]
        [Description("Радиус закругления углов панели")]
        [DefaultValue(20)]
        public int BorderRadius
        {
            get
            {
                return borderRadius;
            }
            set
            {
                borderRadius = value;
                Invalidate();
            }
        }

        [Category("Appearance")]
        [Description("Толщина рамки панели")]
        [DefaultValue(3)]
        public int BorderThickness
        {
            get
            {
                return borderThickness;
            }
            set
            {
                borderThickness = value;
                Invalidate();
            }
        }

        [Category("Appearance")]
        [Description("Цвет рамки панели")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public Color BorderColor
        {
            get
            {
                return borderColor;
            }
            set
            {
                borderColor = value;
                Invalidate();
            }
        }

        private bool ShouldSerializeBorderColor()
        {
            return borderColor != Color.FromArgb(60, 120, 140);
        }

        private void ResetBorderColor()
        {
            BorderColor = Color.FromArgb(60, 120, 140);
        }

        protected override void OnPaintBackground(PaintEventArgs e)
        {
            if (Parent != null)
            {
                e.Graphics.Clear(Parent.BackColor);
            }
            else
            {
                e.Graphics.Clear(SystemColors.Control);
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            e.Graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;

            Rectangle rect = new Rectangle(
                borderThickness,
                borderThickness,
                Width - borderThickness * 2 - 1,
                Height - borderThickness * 2 - 1
            );

            using GraphicsPath path = GetRoundedRectangle(rect, borderRadius);
            using SolidBrush brush = new SolidBrush(BackColor);
            using Pen pen = new Pen(borderColor, borderThickness);

            e.Graphics.FillPath(brush, path);
            e.Graphics.DrawPath(pen, path);
        }

        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);
            Invalidate();
        }

        private GraphicsPath GetRoundedRectangle(Rectangle rect, int radius)
        {
            GraphicsPath path = new GraphicsPath();

            int diameter = radius * 2;

            path.AddArc(rect.X, rect.Y, diameter, diameter, 180, 90);
            path.AddArc(rect.Right - diameter, rect.Y, diameter, diameter, 270, 90);
            path.AddArc(rect.Right - diameter, rect.Bottom - diameter, diameter, diameter, 0, 90);
            path.AddArc(rect.X, rect.Bottom - diameter, diameter, diameter, 90, 90);

            path.CloseFigure();

            return path;
        }
    }
}