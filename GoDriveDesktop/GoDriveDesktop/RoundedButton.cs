using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace GoDriveDesktop
{
    public class RoundedButton : System.Windows.Forms.Button
    {
        private int borderRadius = 5;
        private int borderThickness = 1;

        private Color borderColor = Color.Silver;
        private Color normalColor = SystemColors.MenuHighlight;
        private Color hoverColor = Color.ForestGreen;

        public RoundedButton()
        {
            FlatStyle = FlatStyle.Flat;
            UseVisualStyleBackColor = false;
            TabStop = false;

            FlatAppearance.BorderSize = 0;

            BackColor = normalColor;
            ForeColor = Color.White;
            Cursor = Cursors.Hand;

            SetStyle(
                ControlStyles.AllPaintingInWmPaint |
                ControlStyles.UserPaint |
                ControlStyles.OptimizedDoubleBuffer |
                ControlStyles.ResizeRedraw,
                true
            );

            UpdateRegion();
        }

        [Category("Appearance")]
        [Description("Радиус закругления кнопки")]
        [DefaultValue(5)]
        public int BorderRadius
        {
            get => borderRadius;
            set
            {
                if (borderRadius != value)
                {
                    borderRadius = Math.Max(0, value);
                    UpdateRegion();
                    Invalidate();
                }
            }
        }

        [Category("Appearance")]
        [Description("Толщина рамки кнопки")]
        [DefaultValue(1)]
        public int BorderThickness
        {
            get => borderThickness;
            set
            {
                if (borderThickness != value)
                {
                    borderThickness = Math.Max(1, value);
                    Invalidate();
                }
            }
        }

        [Category("Appearance")]
        [Description("Цвет рамки кнопки")]
        public Color BorderColor
        {
            get => borderColor;
            set
            {
                borderColor = value;
                Invalidate();
            }
        }

        [Category("Appearance")]
        [Description("Обычный цвет кнопки")]
        public Color NormalColor
        {
            get => normalColor;
            set
            {
                normalColor = value;
                if (!DesignMode && ClientRectangle.Contains(PointToClient(Cursor.Position)))
                {
                    // Игнорируем BackColor при наведенной мыши
                }
                else
                {
                    BackColor = value;
                }
                Invalidate();
            }
        }

        [Category("Appearance")]
        [Description("Цвет кнопки при наведении")]
        public Color HoverColor
        {
            get => hoverColor;
            set
            {
                hoverColor = value;
                Invalidate();
            }
        }

        // Обновленные методы сброса под Silver и SystemColors
        private bool ShouldSerializeBorderColor() => borderColor != Color.Silver;
        private void ResetBorderColor() => BorderColor = Color.Silver;

        private bool ShouldSerializeNormalColor() => normalColor != SystemColors.MenuHighlight;
        private void ResetNormalColor() => NormalColor = SystemColors.MenuHighlight;

        private bool ShouldSerializeHoverColor() => hoverColor != Color.ForestGreen;
        private void ResetHoverColor() => HoverColor = Color.ForestGreen;

        protected override void OnMouseEnter(EventArgs e)
        {
            base.OnMouseEnter(e);
            BackColor = hoverColor;
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);
            BackColor = normalColor;
        }

        protected override void OnPaintBackground(PaintEventArgs e)
        {
            Color backgroundColor = SystemColors.MenuHighlight;
            Control parent = Parent;

            while (parent != null)
            {
                if (parent.BackColor != Color.Transparent && parent.BackColor != Color.Empty)
                {
                    backgroundColor = parent.BackColor;
                    break;
                }
                parent = parent.Parent;
            }

            using (SolidBrush brush = new SolidBrush(backgroundColor))
            {
                e.Graphics.FillRectangle(brush, ClientRectangle);
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            OnPaintBackground(e);

            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            e.Graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;

            RectangleF rectDraw = new RectangleF(0, 0, Width, Height);
            float halfThickness = borderThickness / 2f;
            rectDraw.Inflate(-halfThickness, -halfThickness);

            using (GraphicsPath path = GetRoundedRectangle(rectDraw, borderRadius))
            {
                // Заливка цветом Windows
                using (SolidBrush brush = new SolidBrush(BackColor))
                {
                    e.Graphics.FillPath(brush, path);
                }

                // Отрисовка серебряной рамки
                using (Pen pen = new Pen(borderColor, borderThickness))
                {
                    pen.Alignment = PenAlignment.Center;
                    e.Graphics.DrawPath(pen, path);
                }
            }

            TextRenderer.DrawText(
                e.Graphics,
                Text,
                Font,
                ClientRectangle,
                ForeColor,
                TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter | TextFormatFlags.EndEllipsis
            );
        }

        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);
            UpdateRegion();
        }

        private void UpdateRegion()
        {
            if (Width <= 0 || Height <= 0) return;

            RectangleF rect = new RectangleF(0, 0, Width, Height);
            using (GraphicsPath path = GetRoundedRectangle(rect, borderRadius))
            {
                Region oldRegion = Region;
                Region = new Region(path);
                oldRegion?.Dispose();
            }
        }

        private GraphicsPath GetRoundedRectangle(RectangleF rect, float radius)
        {
            GraphicsPath path = new GraphicsPath();
            float maxRadius = Math.Min(rect.Width, rect.Height) / 2f;
            radius = Math.Min(radius, maxRadius);

            if (radius <= 0)
            {
                path.AddRectangle(rect);
                return path;
            }

            float diameter = radius * 2f;

            path.AddArc(rect.X, rect.Y, diameter, diameter, 180, 90);
            path.AddArc(rect.Right - diameter, rect.Y, diameter, diameter, 270, 90);
            path.AddArc(rect.Right - diameter, rect.Bottom - diameter, diameter, diameter, 0, 90);
            path.AddArc(rect.X, rect.Bottom - diameter, diameter, diameter, 90, 90);
            path.CloseFigure();

            return path;
        }
    }
}
