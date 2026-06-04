using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace ColorPicker
{
    public partial class Main : Form
    {
        /// <summary>
        /// build:2014-02-27
        /// update:2014-04-16
        /// update:2016-03-10
        /// update 2017-05-01
        /// </summary>
        public Main()
        {
            InitializeComponent();
        }
        private void Main_Load(object sender, EventArgs e)
        {
            btnStart.Text = "Start(&p)";
            btnStop.Text = "Stop(&e)";
        }

        Color c;
        string color;
        bool canChangeColor = false;
        const int InvalidColor = -1;

        [DllImport("user32.dll")]
        private static extern IntPtr GetDC(IntPtr hWnd);

        [DllImport("user32.dll")]
        private static extern int ReleaseDC(IntPtr hWnd, IntPtr hDC);

        [DllImport("gdi32.dll")]
        private static extern int GetPixel(IntPtr hdc, int nXPos, int nYPos);

        private void txtColor_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            Clipboard.SetDataObject(txtColor.Text.Trim());
        }

        private void txtR_DoubleClick(object sender, EventArgs e)
        {
            Clipboard.SetDataObject(txtR.Text.Trim());
        }

        private void txtG_DoubleClick(object sender, EventArgs e)
        {
            Clipboard.SetDataObject(txtG.Text.Trim());
        }

        private void txtB_DoubleClick(object sender, EventArgs e)
        {
            Clipboard.SetDataObject(txtB.Text.Trim());
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            canChangeColor = false;
            timer1.Enabled = true;
            timer1.Interval = 50;
            timer1.Start();
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            timer1.Stop();
            timer1.Enabled = false;
            canChangeColor = true;
        }

        private void Main_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.P)
            {
                btnStart_Click(sender, e);
            }
            if (e.KeyCode == Keys.E)
            {
                btnStop_Click(sender, e);
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            Color pickedColor = GetColor();
            if (pickedColor.IsEmpty)
            {
                return;
            }

            UpdateColorDisplay(pickedColor, true);
        }

        private void txtR_TextChanged(object sender, EventArgs e)
        {
            if (canChangeColor)
            {
                SetColor();
            }
        }

        private void txtG_TextChanged(object sender, EventArgs e)
        {
            if (canChangeColor)
            {
                SetColor();
            }
        }

        private void txtB_TextChanged(object sender, EventArgs e)
        {
            if (canChangeColor)
            {
                SetColor();
            }
        }

        #region 内部方法
        /*屏幕取色*/
        public Color GetColor()
        {
            Point p = Control.MousePosition;    //得到当前鼠标坐标 
            return GetScrPixel(p);              //取色方法，传参p 当前坐标
        }
        /// <summary>
        /// 取色方法
        /// </summary>
        /// <param name="pt">坐标</param>
        /// <returns>返回颜色</returns>
        private static Color GetScrPixel(Point pt)
        {
            IntPtr desktopDc = GetDC(IntPtr.Zero);
            if (desktopDc == IntPtr.Zero)
            {
                return Color.Empty;
            }

            try
            {
                int colorRef = GetPixel(desktopDc, pt.X, pt.Y);
                if (colorRef == InvalidColor)
                {
                    return Color.Empty;
                }

                int r = colorRef & 0x000000FF;
                int g = (colorRef & 0x0000FF00) >> 8;
                int b = (colorRef & 0x00FF0000) >> 16;
                return Color.FromArgb(r, g, b);
            }
            finally
            {
                ReleaseDC(IntPtr.Zero, desktopDc);
            }
        }


        private void SetColor()
        {
            int r, g, b;
            if (!TryReadColorComponent(txtR, out r) ||
                !TryReadColorComponent(txtG, out g) ||
                !TryReadColorComponent(txtB, out b))
            {
                return;
            }

            UpdateColorDisplay(Color.FromArgb(r, g, b), false);
        }

        private static bool TryReadColorComponent(TextBox textBox, out int value)
        {
            string text = textBox.Text.Trim();
            return int.TryParse(text, out value) && value >= 0 && value <= 255;
        }

        private void UpdateColorDisplay(Color selectedColor, bool updateRgbText)
        {
            c = selectedColor;
            color = ColorTranslator.ToHtml(c).ToUpperInvariant();

            pbColor.BackColor = c;
            txtColor.Text = color;

            if (updateRgbText)
            {
                txtR.Text = c.R.ToString();
                txtG.Text = c.G.ToString();
                txtB.Text = c.B.ToString();
            }
        }

        #endregion

    }
}
