using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ColorPicker
{
    public partial class Main : Form
    {
        /// <summary>
        /// build:2014-02-27
        /// update:2014-04-16
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
        private void txtColor_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            Clipboard.SetDataObject(txtColor.Text.Trim());
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            timer1.Enabled = true;
            timer1.Interval = 50;
            timer1.Start();
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            timer1.Stop();
            timer1.Enabled = false;
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
        Color c;
        string color;
        private void timer1_Tick(object sender, EventArgs e)
        {
            Dictionary<Color, string> dic_Color = GetColor();

            foreach (var item in dic_Color)
            {
                c = item.Key;
                color = item.Value;
            }
            pbColor.BackColor = c;
            //txtColor.Text = color.ToUpper();
            txtColor.Text = color;
        }
        #region 内部方法
        /*屏幕取色*/
        public Dictionary<Color, string> GetColor()
        {
            int r, g, b;
            Point p = Control.MousePosition;    //得到当前鼠标坐标 
            Color c = GetScrPixel(p);           //取色方法，传参p 当前坐标
            r = c.R;
            g = c.G;
            b = c.B;
            //这种方式不准确，使用系统自带的转换方式
            //string res = "#" + (Convert.ToString(r, 16) == "0" ? "00" : Convert.ToString(r, 16)) + (Convert.ToString(g, 16) == "0" ? "00" : Convert.ToString(g, 16)) + (Convert.ToString(r, 16) == "0" ? "00" : Convert.ToString(b, 16));  //rgb文本框写的内容
            string res = System.Drawing.ColorTranslator.ToHtml(c).ToUpper();
            Dictionary<Color, string> dic_Color = new Dictionary<Color, string>();
            dic_Color.Add(c, res);
            //System.GC.Collect();        //内存垃圾回收
            return dic_Color;
        }
        /// <summary>
        /// 取色方法
        /// </summary>
        /// <param name="pt">坐标</param>
        /// <returns>返回颜色</returns>
        private static Color GetScrPixel(Point pt)
        {
            var scrBound = Screen.PrimaryScreen.Bounds;
            using (var bmp = new Bitmap(scrBound.Width, scrBound.Height))
            {
                using (var g = Graphics.FromImage(bmp))
                {
                    g.CopyFromScreen(scrBound.Location, scrBound.Location, scrBound.Size);
                }
                System.GC.Collect();
                return bmp.GetPixel(pt.X, pt.Y);
            }
        }
        #endregion


    }
}
