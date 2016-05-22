using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication2
{
    public partial class MainForm : Form
    {
        [DllImport("user32.dll")]
        static extern bool GetCursorPos(ref Point lpPoint);

        [DllImport("gdi32.dll", CharSet = CharSet.Auto, SetLastError = true, ExactSpelling = true)]
        public static extern int BitBlt(IntPtr hDC, int x, int y, int nWidth, int nHeight, IntPtr hSrcDC, int xSrc, int ySrc, int dwRop);

        Color Pixel;

        int CoordX =-443;
        int CoordY = 923;
        int OverPointX = -279;
        int OverPointY = 451;


        Random rnd = new Random();

        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        public static extern void mouse_event(long dwFlags, long dx, long dy, long cButtons, long dwExtraInfo);
        private const int MOUSEEVENTF_LEFTDOWN = 0x02;
        private const int MOUSEEVENTF_LEFTUP = 0x04;
        private const int MOUSEEVENTF_RIGHTDOWN = 0x08;
        private const int MOUSEEVENTF_RIGHTUP = 0x10;

        public MainForm()
        {
            InitializeComponent();
            Pixel = GetColorAt(CoordX, CoordY);
            timerGetCursorPos.Start(); //старт таймера для получения координат курсора
        }

        private void button1_Click(object sender, EventArgs e)  //Нажатие кнопки
        {
            for (int i = 1; i < 500; i++)
            {
                GOGO();                   //Запуск метода
            }
        }

         public void GOGO()
        {
            //int random = rnd.Next(230, 1234);
            Thread.Sleep(4000);                     //ждем 2000 мс
            int OverPointX = 816;
            int OverPointY = 647;
            label2.Text = "3r423423";
            Cursor.Position = new Point(OverPointX, OverPointY);
            mouse_event(MOUSEEVENTF_LEFTDOWN | MOUSEEVENTF_LEFTUP, OverPointX, OverPointY, 0, 0);
           Thread.Sleep(3000);
            OverPointX = 522;
            OverPointY = 517;
            Cursor.Position = new Point(OverPointX, OverPointY);
            mouse_event(MOUSEEVENTF_LEFTDOWN | MOUSEEVENTF_LEFTUP, OverPointX, OverPointY, 0, 0);
            Thread.Sleep(3000);
            OverPointX = 212;
            OverPointY = 698;
            Cursor.Position = new Point(OverPointX, OverPointY);
            mouse_event(MOUSEEVENTF_LEFTDOWN | MOUSEEVENTF_LEFTUP, OverPointX, OverPointY, 0, 0);
            
            //SendKeys.Send("d");                //эмуляция нажатия ALT+F4
        }
        
        private void timerGetCursorPos_Tick(object sender, EventArgs e)
        {
            int x = Cursor.Position.X;
            int y = Cursor.Position.Y;
            label1.Text =  x + "  " + y; //получаем координаты курсора
            var c = GetColorAt(CoordX, CoordY);
            //this.BackColor = c;
            label2.Text = Pixel.ToString();
           /*if (c != Pixel)
            {
                timerGetCursorPos.Stop();
                Cursor.Position = new Point(OverPointX, OverPointY);  //перемещение курсора
                for (int l = 1; l < 45;l++)
                {
                  //  GOGO();
                }
                

            }*/
        }
        
        //получить цвет пикселя
        Bitmap screenPixel = new Bitmap(1, 1, PixelFormat.Format32bppArgb);
        public Color GetColorAt(int x, int y)
        {
            using (Graphics gdest = Graphics.FromImage(screenPixel))
            {
                using (Graphics gsrc = Graphics.FromHwnd(IntPtr.Zero))
                {
                    IntPtr hSrcDC = gsrc.GetHdc();
                    IntPtr hDC = gdest.GetHdc();
                    int retval = BitBlt(hDC, 0, 0, 1, 1, hSrcDC, x, y, (int)CopyPixelOperation.SourceCopy);
                    gdest.ReleaseHdc();
                    gsrc.ReleaseHdc();
                }
            }
            
            return screenPixel.GetPixel(0, 0);

        }
    }
}
