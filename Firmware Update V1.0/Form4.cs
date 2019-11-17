using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Firmware_Update_V1._0
{
    public partial class Form4 : Form
    {
        private Form1 f1;
        public Form4()
        {
            InitializeComponent();

        }

        public Form4(Form1 frm1)
        {
            InitializeComponent();
            f1 = frm1;
        }

        private void Form4_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)//确定
        {
            if (true)
            { 
                byte[] SendBytes = new byte[10];

                if(true)
                {
                    DialogResult result = MessageBox.Show("请确保终端信息设置正确！", "提示", MessageBoxButtons.OKCancel);
                    if (result == DialogResult.OK)
                    {
                        try
                        {

                            Form1.serialPort1.Write(SendBytes, 0, SendBytes.Length);
                            f1.ResetMessages(1);//清除终端信息
                            MessageBox.Show("设置成功！", "提示");
                        }
                        catch
                        {
                            MessageBox.Show("串口通讯出错", "警告");
                        }
                    }
                }
            }
        }



        private void button2_Click(object sender, EventArgs e)//取消
        {
            this.Close();
        }

        private void checkEdit1_CheckedChanged(object sender, EventArgs e)
        {

        }

    }
}
