using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Firmware_Update_V1._0
{
    public class GloabValue     //全局变量
    {
        public static byte TerminalTypeValue = 0;
        public static byte TransmissionTypeValue = 0;
    }
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            Form1 view = new Form1();

            IController controller = new IController(view);
            Application.Run(view);
        }
    }
}
