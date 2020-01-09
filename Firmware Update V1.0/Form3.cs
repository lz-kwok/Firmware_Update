using DevExpress.Spreadsheet;
using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Office.Interop.Excel;
using Excel = Microsoft.Office.Interop.Excel;

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
                string filename = "测试报告";
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

        public void DPSP1000_Parameter_Display(byte vol_h_data, byte vol_l_data, byte cur_h_data, byte cur_l_data)
        {
            if (vol_l_data < 10)
            {
                this.textBox2.Text = vol_h_data.ToString() + ".0" + vol_l_data.ToString();
            }
            else {
                this.textBox2.Text = vol_h_data.ToString() + "." + vol_l_data.ToString();
            }

            if (cur_l_data < 10)
            {
                this.textBox1.Text = cur_h_data.ToString() + ".0" + cur_l_data.ToString();
            }
            else
            {
                this.textBox1.Text = cur_h_data.ToString() + "." + cur_l_data.ToString();
            }
            
        }

        public void Spreadsheet_Content_Show(byte[] data)
        {
            Worksheet worksheet = spreadsheetControl1.ActiveWorksheet;
            if(data[2] == 0x01){
                int cur_in_x100 = (int)data[3]<<8 + data[4];
                float cur_in = ((float)cur_in_x100)/100;
                worksheet.Range["J98:K98"].Value = cur_in;

                int vol_out_x100 = (int)data[5]<<8 + data[6];
                float vol_out = ((float)vol_out_x100)/100;
                worksheet.Range["L98"].Value = vol_out;

                int cur_out_x100 = (int)data[7]<<8 + data[8];
                float cur_out = ((float)cur_out_x100)/100;
                worksheet.Range["M98"].Value = cur_out;
            }else if(data[2] == 0x02){
                int freq_x100 = (int)data[3]<<8 + data[4];
                float freq = ((float)freq_x100)/100;
                worksheet.Range["N98"].Value = freq;

                int deltaV_x100 = (int)data[5]<<8 + data[6];
                float deltaV = ((float)deltaV_x100)/100;
                worksheet.Range["P98"].Value = deltaV;
            }if(data[2] == 0x03){
                int cur_in_x100 = (int)data[3]<<8 + data[4];
                float cur_in = ((float)cur_in_x100)/100;
                worksheet.Range["J99:K99"].Value = cur_in;

                int vol_out_x100 = (int)data[5]<<8 + data[6];
                float vol_out = ((float)vol_out_x100)/100;
                worksheet.Range["L99"].Value = vol_out;

                int cur_out_x100 = (int)data[7]<<8 + data[8];
                float cur_out = ((float)cur_out_x100)/100;
                worksheet.Range["M99"].Value = cur_out;
            }else if(data[2] == 0x04){
                int freq_x100 = (int)data[3]<<8 + data[4];
                float freq = ((float)freq_x100)/100;
                worksheet.Range["N99"].Value = freq;

                int deltaV_x100 = (int)data[5]<<8 + data[6];
                float deltaV = ((float)deltaV_x100)/100;
                worksheet.Range["P99"].Value = deltaV;
            }if(data[2] == 0x05){
                int cur_in_x100 = (int)data[3]<<8 + data[4];
                float cur_in = ((float)cur_in_x100)/100;
                worksheet.Range["J100:K100"].Value = cur_in;

                int vol_out_x100 = (int)data[5]<<8 + data[6];
                float vol_out = ((float)vol_out_x100)/100;
                worksheet.Range["L100"].Value = vol_out;

                int cur_out_x100 = (int)data[7]<<8 + data[8];
                float cur_out = ((float)cur_out_x100)/100;
                worksheet.Range["M100"].Value = cur_out;
            }else if(data[2] == 0x06){
                int freq_x100 = (int)data[3]<<8 + data[4];
                float freq = ((float)freq_x100)/100;
                worksheet.Range["N100"].Value = freq;

                int deltaV_x100 = (int)data[5]<<8 + data[6];
                float deltaV = ((float)deltaV_x100)/100;
                worksheet.Range["P100"].Value = deltaV;

                int HIE_x100 = (int)data[7]<<8 + data[8];
                float HIE = ((float)HIE_x100)/100;
                worksheet.Range["O100"].Value = HIE;
            }

            else if(data[2] == 0x11){
                int vol_in_x100 = (int)data[3]<<8 + data[4];
                float vol_in = ((float)vol_in_x100)/100;
                worksheet.Range["E102:H102"].Value = vol_in;

                int cur_in_x100 = (int)data[5]<<8 + data[6];
                float cur_in = ((float)cur_in_x100)/100;
                worksheet.Range["I102:K102"].Value = cur_in;

                int vol_out_x100 = (int)data[7]<<8 + data[8];
                float vol_out = ((float)vol_out_x100)/100;
                worksheet.Range["L102:M102"].Value = vol_out;

                int cur_out_x100 = (int)data[9]<<8 + data[10];
                float cur_out = ((float)cur_out_x100)/100;
                worksheet.Range["N102"].Value = cur_out;

                worksheet.Range["P102"].Value = (float)((cur_out*vol_out*100)/(vol_in*cur_in));
            }

            else if(data[2] == 0x21){
                int cur_in_x100 = (int)data[3]<<8 + data[4];
                float cur_in = ((float)cur_in_x100)/100;
                worksheet.Range["I104:K104"].Value = cur_in;

                int vol_out_x100 = (int)data[5]<<8 + data[6];
                float vol_out = ((float)vol_out_x100)/100;
                worksheet.Range["L104:M104"].Value = vol_out;

                int cur_out_x100 = (int)data[7]<<8 + data[8];
                float cur_out = ((float)cur_out_x100)/100;
                worksheet.Range["N104:O104"].Value = cur_out;

                int deltaV_x100 = (int)data[9]<<8 + data[10];
                float deltaV = ((float)deltaV_x100)/100;
                worksheet.Range["P104"].Value = deltaV;
            }else if(data[2] == 0x22){
                int cur_in_x100 = (int)data[3]<<8 + data[4];
                float cur_in = ((float)cur_in_x100)/100;
                worksheet.Range["I105:K105"].Value = cur_in;

                int vol_out_x100 = (int)data[5]<<8 + data[6];
                float vol_out = ((float)vol_out_x100)/100;
                worksheet.Range["L105:M105"].Value = vol_out;

                int cur_out_x100 = (int)data[7]<<8 + data[8];
                float cur_out = ((float)cur_out_x100)/100;
                worksheet.Range["N105:O105"].Value = cur_out;

                int deltaV_x100 = (int)data[9]<<8 + data[10];
                float deltaV = ((float)deltaV_x100)/100;
                worksheet.Range["P105"].Value = deltaV;
            }else if(data[2] == 0x23){
                int cur_in_x100 = (int)data[3]<<8 + data[4];
                float cur_in = ((float)cur_in_x100)/100;
                worksheet.Range["I106:K106"].Value = cur_in;

                int vol_out_x100 = (int)data[5]<<8 + data[6];
                float vol_out = ((float)vol_out_x100)/100;
                worksheet.Range["L106:M106"].Value = vol_out;

                int cur_out_x100 = (int)data[7]<<8 + data[8];
                float cur_out = ((float)cur_out_x100)/100;
                worksheet.Range["N106:O106"].Value = cur_out;

                int deltaV_x100 = (int)data[9]<<8 + data[10];
                float deltaV = ((float)deltaV_x100)/100;
                worksheet.Range["P106"].Value = deltaV;
            }

            else if(data[2] == 0x31){
                int Overvol_x100 = (int)data[3]<<8 + data[4];
                float Overvol = ((float)Overvol_x100)/100;
                worksheet.Range["I107:K108"].Value = Overvol;

                int err_code = (int)data[5]<<8 + data[6];
                worksheet.Range["N107:O108"].Value = err_code;
            }else if(data[2] == 0x32){
                int uvp_x100 = (int)data[3]<<8 + data[4];
                float uvp = ((float)uvp_x100)/100;
                worksheet.Range["I109:K110"].Value = uvp;

                int err_code = (int)data[5]<<8 + data[6];
                worksheet.Range["N109:O110"].Value = err_code;
            }

            else if(data[2] == 0x41){
                int cur_out_x100 = (int)data[3]<<8 + data[4];
                float cur_out = ((float)cur_out_x100)/100;
                worksheet.Range["O111"].Value = cur_out;

                int err_code = (int)data[5]<<8 + data[6];
                worksheet.Range["M111"].Value = err_code;
            }
            
            else if(data[2] == 0x51){
                int err_code = (int)data[3]<<8 + data[4];
                worksheet.Range["N113:O113"].Value = err_code;
            }

            // else if(data[2] == 0x61){
            //     int err_code = (int)data[3]<<8 + data[4];
            //     worksheet.Range["N103:O103"].Value = err_code;
            // }
        }

        public static int GetBit(byte b, int index) { return ((b & (1 << index)) > 0) ? 1: 0; }

        public static byte SetBit(byte b, int index) { return (byte)(b | (1 << index)); }

        public static byte ClearBit(byte b, int index) { return (byte)(b & (byte.MaxValue - (1 << index))); }

        public static byte ReverseBit(byte b, int index) { return (byte)(b ^ (byte)(1 << index)); }

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
            string filePath = FileDialogHelper.SaveExcel();
            if (!string.IsNullOrEmpty(filePath))
            {
                try
                {
                    IWorkbook workbook = spreadsheetControl1.Document;
                    workbook.SaveDocument(filePath);
                    MessageBox.Show("保存成功");
                }
                catch (Exception ex) {
                    MessageBox.Show(ex.Message);
                }
            }
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
                SendBytes[3] = 0x00;
                SendBytes[4] = 0x00;
                SendBytes[5] = 0x00;
                SendBytes[6] = 0x00;
                SendBytes[7] = 0x0D;
            }
            else
            {
                SendBytes[0] = 0x0D;
                SendBytes[1] = 0xFD;//查询
                SendBytes[2] = 0x04;
                SendBytes[3] = 0x00;
                SendBytes[4] = 0x00;
                SendBytes[5] = 0x00;
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
                SendBytes[3] = 0x00;
                SendBytes[4] = 0x00;
                SendBytes[5] = 0x00;
                SendBytes[6] = 0x00;
                SendBytes[7] = 0x0D;
            }
            else
            {
                MessageBox.Show("请确认手动关闭吸尘器", "警告");
                SendBytes[0] = 0x0D;
                SendBytes[1] = 0xFD;//查询
                SendBytes[2] = 0x08;
                SendBytes[3] = 0x00;
                SendBytes[4] = 0x00;
                SendBytes[5] = 0x00;
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
                SendBytes[3] = 0x00;
                SendBytes[4] = 0x00;
                SendBytes[5] = 0x00;
                SendBytes[6] = 0x00;
                SendBytes[7] = 0x0D;
            }
            else
            {
                SendBytes[0] = 0x0D;
                SendBytes[1] = 0xFD;//查询
                SendBytes[2] = 0x0A;
                SendBytes[3] = 0x00;
                SendBytes[4] = 0x00;
                SendBytes[5] = 0x00;
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
                SendBytes[3] = 0x00;
                SendBytes[4] = 0x00;
                SendBytes[5] = 0x00;
                SendBytes[6] = 0x00;
                SendBytes[7] = 0x0D;
            }
            else
            {
                SendBytes[0] = 0x0D;
                SendBytes[1] = 0xFD;//查询
                SendBytes[2] = 0x06;
                SendBytes[3] = 0x00;
                SendBytes[4] = 0x00;
                SendBytes[5] = 0x00;
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
                SendBytes[3] = 0x00;
                SendBytes[4] = 0x00;
                SendBytes[5] = 0x00;
                SendBytes[6] = 0x00;
                SendBytes[7] = 0x0D;
            }
            else
            {
                SendBytes[0] = 0x0D;
                SendBytes[1] = 0xFD;//查询
                SendBytes[2] = 0x0E;
                SendBytes[3] = 0x00;
                SendBytes[4] = 0x00;
                SendBytes[5] = 0x00;
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

        private void textEdit1_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void checkEdit1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkEdit1.Checked == true) {
                checkEdit2.Checked = false;
                checkEdit3.Checked = false;
                checkEdit4.Checked = false;
                checkEdit5.Checked = false;
            }
        }

        private void checkEdit2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkEdit2.Checked == true)
            {
                checkEdit1.Checked = false;
                checkEdit3.Checked = false;
                checkEdit4.Checked = false;
                checkEdit5.Checked = false;
            }
        }

        private void checkEdit3_CheckedChanged(object sender, EventArgs e)
        {
            if (checkEdit3.Checked == true)
            {
                checkEdit1.Checked = false;
                checkEdit2.Checked = false;
                checkEdit4.Checked = false;
                checkEdit5.Checked = false;
            }
        }

        private void checkEdit4_CheckedChanged(object sender, EventArgs e)
        {
            if (checkEdit4.Checked == true)
            {
                checkEdit1.Checked = false;
                checkEdit2.Checked = false;
                checkEdit3.Checked = false;
                checkEdit5.Checked = false;
            }
        }

        private void checkEdit5_CheckedChanged(object sender, EventArgs e)
        {
            if (checkEdit5.Checked == true)
            {
                checkEdit2.Checked = false;
                checkEdit3.Checked = false;
                checkEdit4.Checked = false;
                checkEdit1.Checked = false;
            }
        }

        private void simpleButton6_Click(object sender, EventArgs e)
        {
            byte[] SendBytes = new byte[8];
            if (this.simpleButton6.Text == "打开DPSP直流电源")
            {
                this.simpleButton6.Text = "关闭DPSP直流电源";
                SendBytes[0] = 0x0D;
                SendBytes[1] = 0xFB;//查询
                SendBytes[2] = 0x10;
                SendBytes[3] = 0x00;
                SendBytes[4] = 0x00;
                SendBytes[5] = 0x00;
                SendBytes[6] = 0x00;
                SendBytes[7] = 0x0D;
            }
            else
            {
                SendBytes[0] = 0x0D;
                SendBytes[1] = 0xFB;//查询
                SendBytes[2] = 0x0F;
                SendBytes[3] = 0x00;
                SendBytes[4] = 0x00;
                SendBytes[5] = 0x00;
                SendBytes[6] = 0x00;
                SendBytes[7] = 0x0D;
                this.simpleButton6.Text = "打开DPSP直流电源";
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

        private void simpleButton7_Click(object sender, EventArgs e)
        {
            byte[] SendBytes = new byte[8];
            if (this.simpleButton7.Text == "源效应电压设置")
            {
                SendBytes[0] = 0x0D;
                SendBytes[1] = 0xFA;//查询
                if (checkEdit1.Checked == true) {
                    SendBytes[2] = 0x02;
                }
                if (checkEdit2.Checked == true)
                {
                    SendBytes[2] = 0x03;
                }
                if (checkEdit3.Checked == true)
                {
                    SendBytes[2] = 0x04;
                }
                if (checkEdit4.Checked == true)
                {
                    SendBytes[2] = 0x01;
                }
                if (checkEdit5.Checked == true)
                {
                    SendBytes[2] = 0x05;
                }
                SendBytes[3] = 0x00;
                SendBytes[4] = 0x00;
                SendBytes[5] = 0x00;
                SendBytes[6] = 0x00;
                SendBytes[7] = 0x0D;
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

        private void textBox1_TextChanged_1(object sender, EventArgs e)
        {

        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            byte[] SendBytes = new byte[8];
            if (this.simpleButton2.Text == "自动化测试->开始")
            {
                this.simpleButton2.Text = "自动化测试->进行中";
                SendBytes[0] = 0x0D;
                SendBytes[1] = 0xE0;//查询
                SendBytes[2] = 0x01;
                SendBytes[3] = 0x00;
                SendBytes[4] = 0x00;
                SendBytes[5] = 0x00;
                SendBytes[6] = 0x00;
                SendBytes[7] = 0x0D;
            }
            else
            {
                SendBytes[0] = 0x0D;
                SendBytes[1] = 0xE0;//查询
                SendBytes[2] = 0x02;
                SendBytes[3] = 0x00;
                SendBytes[4] = 0x00;
                SendBytes[5] = 0x00;
                SendBytes[6] = 0x00;
                SendBytes[7] = 0x0D;
                this.simpleButton2.Text = "自动化测试->开始";
            }

            try
            {
                f1.TransmitData(SendBytes);
            }
            catch
            {
                MessageBox.Show("串口通讯错误", "错误");
            }
            Form4 frm = new Form4(this);//首先实例化
            frm.ShowDialog();
        }
    }
}
