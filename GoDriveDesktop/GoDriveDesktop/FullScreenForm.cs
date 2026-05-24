using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace GoDriveDesktop
{
    public class FullScreenForm : Form
    {
        private bool isFullScreen;
        private FormBorderStyle previousBorderStyle;
        private FormWindowState previousWindowState;
        private Rectangle previousBounds;

        public FullScreenForm()
        {
            StartPosition = FormStartPosition.CenterScreen;
            KeyPreview = true;
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            if (DesignMode || LicenseManager.UsageMode == LicenseUsageMode.Designtime)
            {
                return;
            }

            EnterFullScreen();
        }

        protected override bool ProcessCmdKey(ref Message message, Keys keyData)
        {
            if (keyData == Keys.F10)
            {
                ToggleFullScreen();
                return true;
            }

            return base.ProcessCmdKey(ref message, keyData);
        }

        private void ToggleFullScreen()
        {
            if (isFullScreen)
            {
                ExitFullScreen();
            }
            else
            {
                EnterFullScreen();
            }
        }

        private void EnterFullScreen()
        {
            if (isFullScreen)
            {
                return;
            }

            previousBorderStyle = FormBorderStyle;
            previousWindowState = WindowState;
            previousBounds = Bounds;

            FormBorderStyle = FormBorderStyle.None;
            WindowState = FormWindowState.Maximized;

            isFullScreen = true;
        }

        private void ExitFullScreen()
        {
            if (!isFullScreen)
            {
                return;
            }

            WindowState = FormWindowState.Normal;
            FormBorderStyle = previousBorderStyle;
            Bounds = previousBounds;
            WindowState = previousWindowState;

            isFullScreen = false;
        }
    }
}