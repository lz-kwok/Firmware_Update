using DevExpress.Spreadsheet;
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
    public partial class Form3 : Form
    {
        private Form1 f1;
        public Form3(Form1 frm1)
        {
            InitializeComponent();
            f1 = frm1;
        }

        class FileDialogHelper
        {
            public static string OpenExcel()
            {
                OpenFileDialog fileDialog = new OpenFileDialog();
                fileDialog.Multiselect = true;
                fileDialog.Title = "请选择文件";
                fileDialog.Filter = "所有文件(*xls*)|*.xls*"; //设置要选择的文件的类型
                if (fileDialog.ShowDialog() == DialogResult.OK)
                {
                    return fileDialog.FileName;//返回文件的完整路径               
                }
                else
                {
                    return null;
                }

            }
            /// <summary>
            /// excel另存为选择路径
            /// </summary>
            /// <returns></returns>
            public static string SaveExcel()
            {
                string filename = "霸道";
                SaveFileDialog saveDialog = new SaveFileDialog();
                //设置默认文件扩展名。
                saveDialog.DefaultExt = "xls";
                //设置当前文件名筛选器字符串，该字符串决定对话框的“另存为文件类型”或“文件类型”框中出现的选择内容。
                saveDialog.Filter = "Excel文件|*.xls";

                //  用默认的所有者运行通用对话框。
                saveDialog.ShowDialog();
                //如果修改了文件名，用对话框中的文件名名重新赋值
                filename = saveDialog.FileName;
                //被点了取消
                if (filename.IndexOf(":") < 0) return null;
                else
                {
                    return saveDialog.FileName.ToString();
                }

            }

        }

        public void Output_Status_Show(byte dat)
        {
            if (dat == 0x01)
            {
                this.Q1_5.Properties.Appearance.BackColor = System.Drawing.Color.Green;
                this.Q1_6.Properties.Appearance.BackColor = System.Drawing.Color.Green;
            }
            else if (dat == 0x02)
            {
                this.Q1_5.Properties.Appearance.BackColor = System.Drawing.Color.Red;
                this.Q1_6.Properties.Appearance.BackColor = System.Drawing.Color.Red;
            }
            else if (dat == 0x03)
            {
                this.Q1_6.Properties.Appearance.BackColor = System.Drawing.Color.Green;
                this.Q1_8.Properties.Appearance.BackColor = System.Drawing.Color.Green;
            }
            else if (dat == 0x04)
            {
                this.Q1_6.Properties.Appearance.BackColor = System.Drawing.Color.Red;
                this.Q1_8.Properties.Appearance.BackColor = System.Drawing.Color.Red;
            }
            else if (dat == 0x05)
            {
                this.Q1_1.Properties.Appearance.BackColor = System.Drawing.Color.Green;
            }
            else if (dat == 0x06)
            {
                this.Q1_1.Properties.Appearance.BackColor = System.Drawing.Color.Red;
            }
            else if (dat == 0x07)
            {
                this.Q2_0.Properties.Appearance.BackColor = System.Drawing.Color.Green;
                this.Q1_4.Properties.Appearance.BackColor = System.Drawing.Color.Green;
                this.Q1_5.Properties.Appearance.BackColor = System.Drawing.Color.Green;
                this.Q1_6.Properties.Appearance.BackColor = System.Drawing.Color.Green;
                this.Q1_7.Properties.Appearance.BackColor = System.Drawing.Color.Green;
                this.Q1_8.Properties.Appearance.BackColor = System.Drawing.Color.Green;
            }
            else if (dat == 0x08)
            {
                this.Q2_0.Properties.Appearance.BackColor = System.Drawing.Color.Red;
                this.Q1_4.Properties.Appearance.BackColor = System.Drawing.Color.Red;
                this.Q1_5.Properties.Appearance.BackColor = System.Drawing.Color.Red;
                this.Q1_6.Properties.Appearance.BackColor = System.Drawing.Color.Red;
                this.Q1_7.Properties.Appearance.BackColor = System.Drawing.Color.Red;
                this.Q1_8.Properties.Appearance.BackColor = System.Drawing.Color.Red;
            }
            else if (dat == 0x09)
            {
                this.Q1_9.Properties.Appearance.BackColor = System.Drawing.Color.Green;
            }
            else if (dat == 0x0A)
            {
                this.Q1_9.Properties.Appearance.BackColor = System.Drawing.Color.Red;
            }
            else if (dat == 0x0B)
            {
                this.Q1_2.Properties.Appearance.BackColor = System.Drawing.Color.Green;
            }
            else if (dat == 0x0C)
            {
                this.Q1_2.Properties.Appearance.BackColor = System.Drawing.Color.Red;
            }
            else if (dat == 0x0D) {
                this.Q1_0.Properties.Appearance.BackColor = System.Drawing.Color.Green;
            }
            else if (dat == 0x0E) {
                this.Q1_0.Properties.Appearance.BackColor = System.Drawing.Color.Red;
            }
        }

        private void Form3_Load(object sender, EventArgs e)
        {
 
        }


        private void button2_Click(object sender, EventArgs e)//取消设置
        {
            this.Close();
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = "0123456789ABCDEF\b".IndexOf(char.ToUpper(e.KeyChar)) < 0;
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = "0123456789\b".IndexOf(char.ToUpper(e.KeyChar)) < 0;
        }



        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }




        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void comboBox2_SelectedIndexChanged_1(object sender, EventArgs e)
        {

        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {
            string filePath = FileDialogHelper.OpenExcel();
            if (!string.IsNullOrEmpty(filePath))
            {
                IWorkbook workbook = spreadsheetControl1.Document;
                workbook.LoadDocument(filePath);
            }
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {

        }

        private void simpleButton4_Click(object sender, EventArgs e)
        {
            this.spreadsheetControl1.ShowPrintPreview();
        }

        private void Power1_5Button_Click(object sender, EventArgs e)
        {
            byte[] SendBytes = new byte[8];
            if (this.Power1_5Button.Text == "1.5KW 功率测试")
            {
                this.Power1_5Button.Text = "1.5KW 功率测试中";
                SendBytes[0] = 0x0D;
                SendBytes[1] = 0xFD;//查询
                SendBytes[2] = 0x01;//负载1.5KW
                SendBytes[3] = 0x00;
                SendBytes[4] = 0x00;
                SendBytes[5] = 0x00;
                SendBytes[6] = 0x00;
                SendBytes[7] = 0x0D;
            }
            else if (this.Power1_5Button.Text == "1.5KW 功率测试中")
            {
                SendBytes[0] = 0x0D;
                SendBytes[1] = 0xFD;//查询
                SendBytes[2] = 0x02;//负载1.5KW
                SendBytes[3] = 0x00;
                SendBytes[4] = 0x00;
                SendBytes[5] = 0x00;
                SendBytes[6] = 0x00;
                SendBytes[7] = 0x0D;
                this.Power1_5Button.Text = "1.5KW 功率测试";
            }

            try
            {
                f1.TransmitData(SendBytes);
            }
            catch
            {
                MessageBox.Show("串口通讯错误", "错误");
            }
        }

        private void Q1_0_EditValueChanged(object sender, EventArgs e)
        {
            
        }

        private void Power3_0Button_Click(object sender, EventArgs e)
        {
            byte[] SendBytes = new byte[8];
            if (this.Power3_0Button.Text == "3KW 功率测试")
            {
                this.Power3_0Button.Text = "3KW 功率测试中";
                SendBytes[0] = 0x0D;
                SendBytes[1] = 0xFD;//查询
                SendBytes[2] = 0x03;
                SendBytes[3] = 0X00;
                SendBytes[4] = 0X00;
                SendBytes[5] = 0X00;
                SendBytes[6] = 0x00;
                SendBytes[7] = 0x0D;
            }
            else
            {
                SendBytes[0] = 0x0D;
                SendBytes[1] = 0xFD;//查询
                SendBytes[2] = 0x04;
                SendBytes[3] = 0X00;
                SendBytes[4] = 0X00;
                SendBytes[5] = 0X00;
                SendBytes[6] = 0x00;
                SendBytes[7] = 0x0D;
                this.Power3_0Button.Text = "3KW 功率测试";
            }

            try
            {
                f1.TransmitData(SendBytes);
            }
            catch
            {
                MessageBox.Show("串口通讯错误", "错误");
            }
        }

        private void OverLoad_Button_Click(object sender, EventArgs e)
        {
            byte[] SendBytes = new byte[8];
            if (this.OverLoad_Button.Text == "过载测试")
            {
                MessageBox.Show("请确认手动打开吸尘器", "警告");
                this.OverLoad_Button.Text = "过载测试中";
                SendBytes[0] = 0x0D;
                SendBytes[1] = 0xFD;//查询
                SendBytes[2] = 0x07;
                SendBytes[3] = 0X00;
                SendBytes[4] = 0X00;
                SendBytes[5] = 0X00;
                SendBytes[6] = 0x00;
                SendBytes[7] = 0x0D;
            }
            else
            {
                MessageBox.Show("请确认手动关闭吸尘器", "警告");
                SendBytes[0] = 0x0D;
                SendBytes[1] = 0xFD;//查询
                SendBytes[2] = 0x08;
                SendBytes[3] = 0X00;
                SendBytes[4] = 0X00;
                SendBytes[5] = 0X00;
                SendBytes[6] = 0x00;
                SendBytes[7] = 0x0D;
                this.OverLoad_Button.Text = "过载测试";
            }

            try
            {
                f1.TransmitData(SendBytes);
            }
            catch
            {
                MessageBox.Show("串口通讯错误", "错误");
            }
        }

        private void Load_Short_Circuit_Button_Click(object sender, EventArgs e)
        {
            byte[] SendBytes = new byte[8];
            if (this.Load_Short_Circuit_Button.Text == "短路测试")
            {
                this.Load_Short_Circuit_Button.Text = "短路测试中";
                SendBytes[0] = 0x0D;
                SendBytes[1] = 0xFD;//查询
                SendBytes[2] = 0x09;
                SendBytes[3] = 0X00;
                SendBytes[4] = 0X00;
                SendBytes[5] = 0X00;
                SendBytes[6] = 0x00;
                SendBytes[7] = 0x0D;
            }
            else
            {
                SendBytes[0] = 0x0D;
                SendBytes[1] = 0xFD;//查询
                SendBytes[2] = 0x0A;
                SendBytes[3] = 0X00;
                SendBytes[4] = 0X00;
                SendBytes[5] = 0X00;
                SendBytes[6] = 0x00;
                SendBytes[7] = 0x0D;
                this.Load_Short_Circuit_Button.Text = "短路测试";
            }

            try
            {
                f1.TransmitData(SendBytes);
            }
            catch
            {
                MessageBox.Show("串口通讯错误", "错误");
            }
        }

        private void Load_Reverse_Button_Click(object sender, EventArgs e)
        {
            byte[] SendBytes = new byte[8];
            if (this.Load_Reverse_Button.Text == "反接测试")
            {
                this.Load_Reverse_Button.Text = "反接测试中";
                SendBytes[0] = 0x0D;
                SendBytes[1] = 0xFD;//查询
                SendBytes[2] = 0x05;
                SendBytes[3] = 0X00;
                SendBytes[4] = 0X00;
                SendBytes[5] = 0X00;
                SendBytes[6] = 0x00;
                SendBytes[7] = 0x0D;
            }
            else
            {
                SendBytes[0] = 0x0D;
                SendBytes[1] = 0xFD;//查询
                SendBytes[2] = 0x06;
                SendBytes[3] = 0X00;
                SendBytes[4] = 0X00;
                SendBytes[5] = 0X00;
                SendBytes[6] = 0x00;
                SendBytes[7] = 0x0D;
                this.Load_Reverse_Button.Text = "反接测试";
            }

            try
            {
                f1.TransmitData(SendBytes);
            }
            catch
            {
                MessageBox.Show("串口通讯错误", "错误");
            }
        }

        private void Main_Button_Click(object sender, EventArgs e)
        {
            byte[] SendBytes = new byte[8];
            if (this.Main_Button.Text == "主接触器打开")
            {
                this.Main_Button.Text = "主接触器关闭";
                SendBytes[0] = 0x0D;
                SendBytes[1] = 0xFD;//查询
                SendBytes[2] = 0x0D;
                SendBytes[3] = 0X00;
                SendBytes[4] = 0X00;
                SendBytes[5] = 0X00;
                SendBytes[6] = 0x00;
                SendBytes[7] = 0x0D;
            }
            else
            {
                SendBytes[0] = 0x0D;
                SendBytes[1] = 0xFD;//查询
                SendBytes[2] = 0x0E;
                SendBytes[3] = 0X00;
                SendBytes[4] = 0X00;
                SendBytes[5] = 0X00;
                SendBytes[6] = 0x00;
                SendBytes[7] = 0x0D;
                this.Main_Button.Text = "主接触器打开";
            }

            try
            {
                f1.TransmitData(SendBytes);
            }
            catch
            {
                MessageBox.Show("串口通讯错误", "错误");
            }
        }
    }
}
