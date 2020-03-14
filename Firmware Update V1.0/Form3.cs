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
using System.Collections;

namespace Firmware_Update_V1._0
{
    public partial class Form3 : Form
    {
        private Form1 f1;
        public Form3(Form1 frm1)
        {
            InitializeComponent();
            f1 = frm1;

            int index = this.dataGridView1.Rows.Add();
            this.dataGridView1.Rows[index].Cells[0].Value = "负载效应测试";
            index = this.dataGridView1.Rows.Add();
            this.dataGridView1.Rows[index].Cells[0].Value = "效率测试";
            index = this.dataGridView1.Rows.Add();
            this.dataGridView1.Rows[index].Cells[0].Value = "源效应测试";
            index = this.dataGridView1.Rows.Add();
            this.dataGridView1.Rows[index].Cells[0].Value = "输入欠压保护测试";
            index = this.dataGridView1.Rows.Add();
            this.dataGridView1.Rows[index].Cells[0].Value = "输入过压保护测试";
            index = this.dataGridView1.Rows.Add();
            this.dataGridView1.Rows[index].Cells[0].Value = "输出过载保护测试";
            index = this.dataGridView1.Rows.Add();
            this.dataGridView1.Rows[index].Cells[0].Value = "输出过流保护测试";
            index = this.dataGridView1.Rows.Add();
            this.dataGridView1.Rows[index].Cells[0].Value = "输出短路试验测试";
            index = this.dataGridView1.Rows.Add();
            this.dataGridView1.Rows[index].Cells[0].Value = "输入反接保护测试";
            index = this.dataGridView1.Rows.Add();
            this.dataGridView1.Rows[index].Cells[0].Value = "启动时间测试";
            index = this.dataGridView1.Rows.Add();
            this.dataGridView1.Rows[index].Cells[0].Value = "带吸尘器启动";

            index = this.dataGridView3.Rows.Add();
            this.dataGridView3.Rows[index].Cells[2].Value = "50V";
            this.dataGridView3.Rows[index].Cells[5].Value = "step1";
            index = this.dataGridView3.Rows.Add();
            this.dataGridView3.Rows[index].Cells[2].Value = "60V";
            this.dataGridView3.Rows[index].Cells[5].Value = "step2";
            index = this.dataGridView3.Rows.Add();
            this.dataGridView3.Rows[index].Cells[2].Value = "70V";
            this.dataGridView3.Rows[index].Cells[5].Value = "step3";
            index = this.dataGridView3.Rows.Add();
            this.dataGridView3.Rows[index].Cells[2].Value = "80V";
            this.dataGridView3.Rows[index].Cells[5].Value = "step4";
            index = this.dataGridView3.Rows.Add();
            this.dataGridView3.Rows[index].Cells[2].Value = "90V";
            this.dataGridView3.Rows[index].Cells[5].Value = "step5";
            index = this.dataGridView3.Rows.Add();
            this.dataGridView3.Rows[index].Cells[2].Value = "100V";
            this.dataGridView3.Rows[index].Cells[5].Value = "step6";
            index = this.dataGridView3.Rows.Add();
            this.dataGridView3.Rows[index].Cells[2].Value = "110V";
            this.dataGridView3.Rows[index].Cells[5].Value = "step7";
            index = this.dataGridView3.Rows.Add();
            this.dataGridView3.Rows[index].Cells[2].Value = "120V";
            this.dataGridView3.Rows[index].Cells[5].Value = "step8";
            index = this.dataGridView3.Rows.Add();
            this.dataGridView3.Rows[index].Cells[2].Value = "130V";
            this.dataGridView3.Rows[index].Cells[5].Value = "step9";
            index = this.dataGridView3.Rows.Add();
            this.dataGridView3.Rows[index].Cells[2].Value = "140V";
            this.dataGridView3.Rows[index].Cells[5].Value = "step10";
            index = this.dataGridView3.Rows.Add();
            this.dataGridView3.Rows[index].Cells[2].Value = "150V";
            this.dataGridView3.Rows[index].Cells[5].Value = "step11";
            index = this.dataGridView3.Rows.Add();
            this.dataGridView3.Rows[index].Cells[2].Value = "结束标定";
            this.dataGridView3.Rows[index].Cells[5].Value = "结束标定";

            string path = System.Windows.Forms.Application.StartupPath;
            string xls_path = path +　"\\单相逆变器.xls";
            if (!string.IsNullOrEmpty(xls_path))
            {
                IWorkbook workbook = spreadsheetControl1.Document;
                workbook.LoadDocument(xls_path);
            }
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

        public byte get_bit(byte data, byte offset)
        {
            byte[] list = BitConverter.GetBytes(data);
            BitArray arr = new BitArray(list);
            bool bit = arr[offset];
            if (bit)
            {
                return 1;
            }
            else {
                return 0;
            }
        }
      
        private byte err_s = 0, nor_s = 0;

        public void calibration_process(byte[] dat)
        {
            int data_vol = ((int)dat[3]) * 256 + dat[4];
            int data_cur = ((int)dat[5]) * 256 + dat[6];
            switch (dat[2]) { 
                case 0x01:
                    this.dataGridView3.Rows[0].Cells[0].Value = data_vol;
                    break;
                case 0x02:
                    this.dataGridView3.Rows[1].Cells[0].Value = data_vol;
                    break;
                case 0x03:
                    this.dataGridView3.Rows[2].Cells[0].Value = data_vol;
                    break;
                case 0x04:
                    this.dataGridView3.Rows[3].Cells[0].Value = data_vol;
                    break;
                case 0x05:
                    this.dataGridView3.Rows[4].Cells[0].Value = data_vol;
                    break;
                case 0x06:
                    this.dataGridView3.Rows[5].Cells[0].Value = data_vol;
                    break;
                case 0x07:
                    this.dataGridView3.Rows[6].Cells[0].Value = data_vol;
                    break;
                case 0x08:
                    this.dataGridView3.Rows[7].Cells[0].Value = data_vol;
                    break;
                case 0x09:
                    this.dataGridView3.Rows[8].Cells[0].Value = data_vol;
                    break;
                case 0x0A:
                    this.dataGridView3.Rows[9].Cells[0].Value = data_vol;
                    break;
                case 11:
                    this.dataGridView3.Rows[10].Cells[0].Value = data_vol;
                    break;

                case 12:
                    this.dataGridView3.Rows[0].Cells[3].Value = data_cur;
                    break;
                case 13:
                    this.dataGridView3.Rows[1].Cells[3].Value = data_cur;
                    break;
                case 14:
                    this.dataGridView3.Rows[2].Cells[3].Value = data_cur;
                    break;
                case 15:
                    this.dataGridView3.Rows[3].Cells[3].Value = data_cur;
                    break;
                case 16:
                    this.dataGridView3.Rows[4].Cells[3].Value = data_cur;
                    break;
                case 17:
                    this.dataGridView3.Rows[5].Cells[3].Value = data_cur;
                    break;
                case 18:
                    this.dataGridView3.Rows[6].Cells[3].Value = data_cur;
                    break;
                case 19:
                    this.dataGridView3.Rows[7].Cells[3].Value = data_cur;
                    break;
                case 20:
                    this.dataGridView3.Rows[8].Cells[3].Value = data_cur;
                    break;
                case 21:
                    this.dataGridView3.Rows[9].Cells[3].Value = data_cur;
                    break;
                case 22:
                    this.dataGridView3.Rows[10].Cells[3].Value = data_cur;
                    break;

            }
        }

        public void Output_Status_Show(byte[] dat)
        {
            //if (dat == 0x01)
            if (get_bit(dat[2], 0) == 1)
            {
                this.Q1_0.Properties.Appearance.BackColor = System.Drawing.Color.Green;
            }
            else {
                this.Q1_0.Properties.Appearance.BackColor = System.Drawing.Color.Red;
            }

            if (get_bit(dat[2], 1) == 1)
            {
                this.Q1_1.Properties.Appearance.BackColor = System.Drawing.Color.Green;
            }
            else
            {
                this.Q1_1.Properties.Appearance.BackColor = System.Drawing.Color.Red;
            }

            if (get_bit(dat[2], 2) == 1)
            {
                this.Q1_2.Properties.Appearance.BackColor = System.Drawing.Color.Green;
            }
            else
            {
                this.Q1_2.Properties.Appearance.BackColor = System.Drawing.Color.Red;
            }

            if (get_bit(dat[2], 3) == 1)
            {
                this.Q1_3.Properties.Appearance.BackColor = System.Drawing.Color.Green;
            }
            else
            {
                this.Q1_3.Properties.Appearance.BackColor = System.Drawing.Color.Red;
            }

            if (get_bit(dat[2], 4) == 1)
            {
                this.Q1_4.Properties.Appearance.BackColor = System.Drawing.Color.Green;
            }
            else
            {
                this.Q1_4.Properties.Appearance.BackColor = System.Drawing.Color.Red;
            }

            if (get_bit(dat[2], 5) == 1)
            {
                this.Q1_5.Properties.Appearance.BackColor = System.Drawing.Color.Green;
            }
            else
            {
                this.Q1_5.Properties.Appearance.BackColor = System.Drawing.Color.Red;
            }

            if (get_bit(dat[2], 6) == 1)
            {
                this.Q1_6.Properties.Appearance.BackColor = System.Drawing.Color.Green;
            }
            else
            {
                this.Q1_6.Properties.Appearance.BackColor = System.Drawing.Color.Red;
            }

            if (get_bit(dat[2], 7) == 1)
            {
                this.Q1_7.Properties.Appearance.BackColor = System.Drawing.Color.Green;
            }
            else
            {
                this.Q1_7.Properties.Appearance.BackColor = System.Drawing.Color.Red;
            }

            if (get_bit(dat[3], 0) == 1)
            {
                this.Q1_8.Properties.Appearance.BackColor = System.Drawing.Color.Green;
            }
            else
            {
                this.Q1_8.Properties.Appearance.BackColor = System.Drawing.Color.Red;
            }

            if (get_bit(dat[3], 1) == 1)
            {
                this.Q1_9.Properties.Appearance.BackColor = System.Drawing.Color.Green;
            }
            else
            {
                this.Q1_9.Properties.Appearance.BackColor = System.Drawing.Color.Red;
            }

            if (get_bit(dat[3], 2) == 1)
            {
                this.Q2_0.Properties.Appearance.BackColor = System.Drawing.Color.Green;
            }
            else
            {
                this.Q2_0.Properties.Appearance.BackColor = System.Drawing.Color.Red;
            }

            if (get_bit(dat[3], 3) == 1)
            {
                this.Q2_1.Properties.Appearance.BackColor = System.Drawing.Color.Green;
            }
            else
            {
                this.Q2_1.Properties.Appearance.BackColor = System.Drawing.Color.Red;
            }

            if (get_bit(dat[3], 4) == 1)
            {
                this.Fault_1.Properties.Appearance.BackColor = System.Drawing.Color.Red;
                err_s = 0;
            }
            else
            {
                this.Fault_1.Properties.Appearance.BackColor = System.Drawing.Color.Green;
                err_s = 1;
            }

            if (get_bit(dat[3], 5) == 1)
            {
                this.Fault_2.Properties.Appearance.BackColor = System.Drawing.Color.Red;
                nor_s = 0;
            }
            else
            {
                this.Fault_2.Properties.Appearance.BackColor = System.Drawing.Color.Green;
                nor_s = 1;
            }

            if (get_bit(dat[3], 7) == 1)
            {
                this.S.Properties.Appearance.BackColor = System.Drawing.Color.Green;
            }
            else
            {
                this.S.Properties.Appearance.BackColor = System.Drawing.Color.Red;
            }
            
        }

        public void Note_Message(byte[] data)
        {
            DialogResult result = DialogResult.None;
            if(data[2] == 0x07){
                if (data[3] == 0x00) {
                    result = MessageBox.Show("请确认手动打开吸尘器", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                    if (result == DialogResult.Yes)
                    {
                        byte[] SendBytes = new byte[8];
                        SendBytes[0] = 0x0D;
                        SendBytes[1] = 0xF9;//
                        SendBytes[2] = 0x07;//
                        SendBytes[3] = 0x01;
                        SendBytes[4] = 0x00;
                        SendBytes[5] = 0x00;
                        SendBytes[6] = 0x00;
                        SendBytes[7] = 0x0D;

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
            
        }

        public void SingleReverse_Parameter_Display(byte vol_h_data, byte vol_l_data, byte cur_h_data, byte cur_l_data, byte type , byte err_code)
        {
            
        }

        private int check_start_index = 0;
        private byte check_index = 0;

        private void Test_process_show(byte err_code)
        {
            switch (err_code)
            { 
                case 0x01:
                    this.dataGridView1.Rows[check_start_index].Cells[2].Value = "输入过压";
                    break;
                case 0x02:
                    this.dataGridView1.Rows[check_start_index].Cells[2].Value = "输入欠压";
                    break;
                case 0x03:
                    this.dataGridView1.Rows[check_start_index].Cells[2].Value = "输出过压";
                    break;
                case 0x04:
                    this.dataGridView1.Rows[check_start_index].Cells[2].Value = "输出欠压";
                    break;
                case 0x05:
                    this.dataGridView1.Rows[check_start_index].Cells[2].Value = "输出过流";
                    break;
                case 0x06:
                    this.dataGridView1.Rows[check_start_index].Cells[2].Value = "输出过载";
                    break;
                case 0x07:
                    this.dataGridView1.Rows[check_start_index].Cells[2].Value = "输出过流";
                    break;
                case 0x0B:
                    this.dataGridView1.Rows[check_start_index].Cells[2].Value = "逆变故障";
                    break;
                case 0x0D:
                    this.dataGridView1.Rows[check_start_index].Cells[2].Value = "无启动信号";
                    break;
                default:
                    this.dataGridView1.Rows[check_start_index].Cells[2].Value = "完成";
                    break;
            }
        }

        public void Spreadsheet_Content_Show(byte[] data)
        {
            DialogResult result = DialogResult.None;
            Worksheet worksheet = spreadsheetControl1.ActiveWorksheet;
            this.errorCode.Text = data[15].ToString("X2");
            

            if (data[4] < 10)       //直流电压显示
            {
                this.dc_volt.Text = data[3].ToString() + ".0" + data[4].ToString();
            }
            else {
                this.dc_volt.Text = data[3].ToString() + "." + data[4].ToString();
            }

            if (data[6] < 10)       //直流电流显示
            {
                this.dc_current.Text = data[5].ToString() + ".0" + data[6].ToString();
            }
            else
            {
                this.dc_current.Text = data[5].ToString() + "." + data[6].ToString();
            }

            if (data[8] < 10)       //交流电压有效值显示
            {
                this.ac_volt.Text = data[7].ToString() + ".0" + data[8].ToString();
            }
            else
            {
                this.ac_volt.Text = data[7].ToString() + "." + data[8].ToString();
            }

            if (data[10] < 10)      //交流电流有效值显示
            {
                this.ac_current.Text = data[9].ToString() + ".0" + data[10].ToString();
            }
            else
            {
                this.ac_current.Text = data[9].ToString() + "." + data[10].ToString();
            }
            string delta_v;
            //电位差显示
            //if (data[7] > 220)
            {
                float ac_v = (float)data[7] + (float)data[8] / 100;
                float delta_ac_v = ac_v - 220;
                float del_percent = delta_ac_v / 220 * 100;
                delta_v = String.Format("{0:F}", del_percent);
            }
            //else {
            //    float ac_v = (float)data[7] + (float)data[8] / 100;
            //    float delta_ac_v = 220 - ac_v ;
            //    float del_percent = delta_ac_v / 220 * 100;
            //    delta_v = "-" + String.Format("{0:F}", del_percent);
            //}

            this.ac_freq.Text = data[12].ToString();

            if (data[14] < 10)      //输出效率显示
            {
                this.efficient.Text = data[13].ToString() + ".0" + data[14].ToString();
            }
            else
            {
                this.efficient.Text = data[13].ToString() + "." + data[14].ToString();
            }

            if(data[2] == 0x01){            //0kW测试
                worksheet.Range["J98:K98"].Value = this.dc_current.Text;
                worksheet.Range["L98"].Value = this.ac_volt.Text;
                worksheet.Range["M98"].Value = this.ac_current.Text;
                worksheet.Range["N98"].Value = this.ac_freq.Text;
                worksheet.Range["P98"].Value = delta_v;

                if (data[7] > 210)
                {
                    worksheet.Range["L115:P115"].Value = "启动信号正常";
                    if (nor_s == 1)
                    {
                        worksheet.Range["L118:P118"].Value = "触点正常";
                    }
                    else {
                        worksheet.Range["L118:P118"].Value = "触点异常";
                    }
                }
                else {
                    if( (data[15] == 0)&&(data[16] >= 9) ){
                        worksheet.Range["L115:P115"].Value = "启动信号异常";
                    }
                }
            }else if(data[2] == 0x02){      //1.5kW测试
                worksheet.Range["J99:K99"].Value = this.dc_current.Text;
                worksheet.Range["L99"].Value = this.ac_volt.Text;
                worksheet.Range["M99"].Value = this.ac_current.Text;
                worksheet.Range["N99"].Value = this.ac_freq.Text;
                worksheet.Range["P99"].Value = delta_v;
            }if(data[2] == 0x03){           //3kW测试
                worksheet.Range["J100:K100"].Value = this.dc_current.Text;
                worksheet.Range["L100"].Value = this.ac_volt.Text;
                worksheet.Range["M100"].Value = this.ac_current.Text;
                worksheet.Range["N100"].Value = this.ac_freq.Text;
                worksheet.Range["P100"].Value = delta_v;
                worksheet.Range["O100"].Value = "2.5";
                //worksheet.Range["O100"].Value = HIE;      //谐波
            }else if(data[2] == 0x04){      //效率测试
                worksheet.Range["E102:H102"].Value = this.dc_volt.Text;
                worksheet.Range["I102:K102"].Value = this.dc_current.Text;
                worksheet.Range["L102:M102"].Value = this.ac_volt.Text;
                worksheet.Range["N102"].Value = this.ac_current.Text;
                worksheet.Range["P102"].Value = this.efficient.Text;      //效率
            }if(data[2] == 0x05){       //源效应77V测试
                worksheet.Range["I104:K104"].Value = this.dc_current.Text;
                worksheet.Range["L104:M104"].Value = this.ac_volt.Text;
                worksheet.Range["N104:O104"].Value = this.ac_current.Text;
                worksheet.Range["P104"].Value = delta_v;
            }else if(data[2] == 0x06){  //源效应110V测试
                worksheet.Range["I105:K105"].Value = this.dc_current.Text;
                worksheet.Range["L105:M105"].Value = this.ac_volt.Text;
                worksheet.Range["N105:O105"].Value = this.ac_current.Text;
                worksheet.Range["P105"].Value = delta_v;
            }else if(data[2] == 0x07){  //源效应137.5V测试
                worksheet.Range["I106:K106"].Value = this.dc_current.Text;
                worksheet.Range["L106:M106"].Value = this.ac_volt.Text;
                worksheet.Range["N106:O106"].Value = this.ac_current.Text;
                worksheet.Range["P106"].Value = delta_v;
            }else if(data[2] == 0x08){   //输入欠压测试
                if(data[15] == 0x02){
                    worksheet.Range["I107:K108"].Value = this.dc_volt.Text;
                    worksheet.Range["N107:O108"].Value = this.errorCode.Text;
                }
            }else if(data[2] == 0x09){   //输入过压测试
                if(data[15] == 0x01){
                    worksheet.Range["I109:K110"].Value = this.dc_volt.Text;
                    worksheet.Range["N109:O110"].Value = this.errorCode.Text;
                    if (err_s == 1)
                    {
                        worksheet.Range["L119:P119"].Value = "触点正常";
                    }
                    else
                    {
                        worksheet.Range["L119:P119"].Value = "触点异常";
                    }
                }
            }else if(data[2] == 0x0A){   //输出过载测试
                if(data[15] == 0x06){
                    worksheet.Range["O111"].Value = this.ac_current.Text;
                    worksheet.Range["M111"].Value = this.errorCode.Text;
                    if (err_s == 1)
                    {
                        worksheet.Range["L119:P119"].Value = "触点正常";
                    }
                    else
                    {
                        worksheet.Range["L119:P119"].Value = "触点异常";
                    }
                }
            }else if(data[2] == 0x0B){   //输出过流测试
                if(data[15] == 0x05){
                    if (data[9] > 0)
                    {
                        worksheet.Range["O112"].Value = this.ac_current.Text;
                    }
                    else {
                        worksheet.Range["O112"].Value = "32A";
                    }
                    
                    worksheet.Range["M112"].Value = this.errorCode.Text;
                    if (data[16] == 3) {
                        MessageBox.Show("请确认手动关闭吸尘器", "提示");
                    }
                }
            }else if(data[2] == 0x0C){  //短路测试
                if(data[15] == 0x0B){
                    worksheet.Range["N113:O113"].Value = this.errorCode.Text;
                }
            }
            else if (data[2] == 20)   //反接测试&启动时间测试
            {
                if ((data[7] > 210) && (data[16] <= 14))
                {
                    worksheet.Range["L114:P114"].Value = "反接保护正常";
                }

                if (data[16] > 14) {
                    if (data[7] < 210)
                    {
                        worksheet.Range["L114:P114"].Value = "反接保护异常";
                    }
                }
            }
            else if (data[2] == 22)   //启动时间测试
            {
                if ((data[7] > 210) && (data[16] <= 15))
                {
                    worksheet.Range["L116:P116"].Value = "电源启动正常";
                }

                if (data[16] > 15)
                {
                    if (data[7] < 210)
                    {
                        worksheet.Range["L116:P116"].Value = "电源启动超时";
                    }
                }
            }
            else if(data[2] == 21){
                if (data[16] >= 14) {
                    if (data[7] > 210)
                    {
                        worksheet.Range["L117:P117"].Value = "正常";
                    }
                    else
                    {
                        worksheet.Range["L117:P117"].Value = "异常";
                    }
                }
             }

            if (data[18] == 1)
            {
                if ((data[16] == 10) && (check_start_index < 3))
                {
                    if (data[15] != 0x0)
                    {
                        byte[] SendBytes = new byte[8];
                        SendBytes[0] = 0x0D;
                        SendBytes[1] = 0xF8;//
                        SendBytes[2] = 0x00;//
                        SendBytes[3] = 0x00;
                        SendBytes[4] = 0x00;
                        SendBytes[5] = 0x00;
                        SendBytes[6] = 0x00;
                        SendBytes[7] = 0x0D;
                        this.启动测试.DefaultCellStyle.NullValue = "启动测试";
                        try
                        {
                            f1.TransmitData(SendBytes);
                        }
                        catch
                        {
                            MessageBox.Show("串口通讯错误", "错误");
                        }

                        MessageBox.Show("发生错误", this.errorCode.Text);
                    }


                    if (data[2] < 0x03)
                    {
                        ;
                    }
                    else if ((data[2] > 0x04) && (data[2] < 0x07))
                    {
                        ;
                    }
                    else
                    {
                        Test_process_show(data[15]);
                        this.dataGridView1.Rows[check_start_index].Cells[2].Value = "完成";
                        check_start_index += 1;
                        byte[] SendBytes = new byte[8];
                        SendBytes[0] = 0x0D;
                        SendBytes[1] = 0xF9;//
                        SendBytes[2] = check_index;//
                        SendBytes[3] = 0x00;
                        SendBytes[4] = 0x00;
                        SendBytes[5] = 0x00;
                        SendBytes[6] = 0x00;
                        SendBytes[7] = 0x0D;

                        for (int i = check_start_index; i < 11; i++)
                        {
                            bool x = Convert.ToBoolean(this.dataGridView1.Rows[i].Cells[1].Value);
                            if (x)
                            {
                                check_start_index = i;
                                SendBytes[2] = ((byte)(i + 1));
                                break;
                            }
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

                //截止到反接命令发出
                if ((data[15] >= 0x01) && (check_start_index < 8) && (data[16] == 3))
                {
                    Test_process_show(data[15]);
                    check_start_index += 1;
                    byte[] SendBytes = new byte[8];
                    SendBytes[0] = 0x0D;
                    SendBytes[1] = 0xF9;//
                    SendBytes[2] = check_index;//
                    SendBytes[3] = 0x00;
                    SendBytes[4] = 0x00;
                    SendBytes[5] = 0x00;
                    SendBytes[6] = 0x00;
                    SendBytes[7] = 0x0D;

                    for (int i = check_start_index; i < 11; i++)
                    {
                        bool x = Convert.ToBoolean(this.dataGridView1.Rows[i].Cells[1].Value);
                        if (x)
                        {
                            check_start_index = i;
                            SendBytes[2] = ((byte)(i + 1));

                            //if (check_start_index == 0x06)
                            //{  //过流
                            //    if (data[3] == 0x00)
                            //    {
                            //        check_start_index = 0x05;
                            //    }
                            //}

                            try
                            {
                                f1.TransmitData(SendBytes);
                            }
                            catch
                            {
                                MessageBox.Show("串口通讯错误", "错误");
                            }

                            break;
                        }
                    }
                }

                //判断反接回应数据
                if ((check_start_index == 8) || (check_start_index == 9))
                {
                    if ((data[16] >= 14) || (data[7] > 210))
                    {
                        //Test_process_show(data[15]);
                        this.dataGridView1.Rows[check_start_index].Cells[2].Value = "完成";
                        //this.dataGridView1.Rows[9].Cells[2].Value = "完成";
                        check_start_index++;
                        byte[] SendBytes = new byte[8];
                        SendBytes[0] = 0x0D;
                        SendBytes[1] = 0xF9;//
                        SendBytes[2] = check_index;//
                        SendBytes[3] = 0x00;
                        SendBytes[4] = 0x00;
                        SendBytes[5] = 0x00;
                        SendBytes[6] = 0x00;
                        SendBytes[7] = 0x0D;

                        for (int i = check_start_index; i < 11; i++)
                        {
                            bool x = Convert.ToBoolean(this.dataGridView1.Rows[i].Cells[1].Value);
                            if (x)
                            {
                                check_start_index = i;
                                SendBytes[2] = ((byte)(i + 1));

                                if (check_start_index == 10)
                                {  //过流
                                    MessageBox.Show("请确认手动打开吸尘器", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                                    //MessageBox.Show("请确认手动打开吸尘器", "警告");
                                    if (result == DialogResult.No)
                                    {
                                        break;
                                    }
                                }

                                try
                                {
                                    f1.TransmitData(SendBytes);
                                }
                                catch
                                {
                                    MessageBox.Show("串口通讯错误", "错误");
                                }

                                break;
                            }
                        }
                    }

                }
            }
        }

        

        private void Form3_Load(object sender, EventArgs e)
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


        private void errorCode_TextChanged(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void splitContainer1_Panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewColumn column = dataGridView2.Columns[e.ColumnIndex];
                if (column is DataGridViewButtonColumn)
                {
                    if (column.DefaultCellStyle.NullValue == "全选")
                    {
                        this.全选.DefaultCellStyle.NullValue = "全隐";
                        for (int i = 0; i < 11; i++) {
                            this.dataGridView1.Rows[i].Cells[1].Value = true;
                        }
                    }
                    else if (column.DefaultCellStyle.NullValue == "全隐")
                    {
                        this.全选.DefaultCellStyle.NullValue = "全选";
                        for (int i = 0; i < 11; i++)
                        {
                            this.dataGridView1.Rows[i].Cells[1].Value = false;
                        }
                    }

                    if (column.DefaultCellStyle.NullValue == "启动测试")
                    {
                        byte[] SendBytes = new byte[8];
                        SendBytes[0] = 0x0D;
                        SendBytes[1] = 0xF9;//
                        SendBytes[2] = 0x00;//
                        SendBytes[3] = 0x00;
                        SendBytes[4] = 0x00;
                        SendBytes[5] = 0x00;
                        SendBytes[6] = 0x00;
                        SendBytes[7] = 0x0D;
                        this.启动测试.DefaultCellStyle.NullValue = "结束测试";
                        for (int i = 0; i < 11; i++)
                        {
                            bool x =  Convert.ToBoolean(this.dataGridView1.Rows[i].Cells[1].Value);
                            if (x)
                            {
                                check_start_index = i;
                                SendBytes[2] = ((byte)(i+1));
                                break;
                            }
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
                    else if (column.DefaultCellStyle.NullValue == "结束测试")
                    {
                        byte[] SendBytes = new byte[8];
                        SendBytes[0] = 0x0D;
                        SendBytes[1] = 0xF8;//
                        SendBytes[2] = 0x00;//
                        SendBytes[3] = 0x00;
                        SendBytes[4] = 0x00;
                        SendBytes[5] = 0x00;
                        SendBytes[6] = 0x00;
                        SendBytes[7] = 0x0D;
                        this.启动测试.DefaultCellStyle.NullValue = "启动测试";
                        try
                        {
                            f1.TransmitData(SendBytes);
                        }
                        catch
                        {
                            MessageBox.Show("串口通讯错误", "错误");
                        }
                    }

                    if (column.DefaultCellStyle.NullValue == "打开主接触器")
                    {
                        byte[] SendBytes = new byte[8];
                        this.打开主接触器.DefaultCellStyle.NullValue = "关闭主接触器";
                        SendBytes[0] = 0x0D;
                        SendBytes[1] = 0xFD;//查询
                        SendBytes[2] = 0x0D;
                        SendBytes[3] = 0x00;
                        SendBytes[4] = 0x00;
                        SendBytes[5] = 0x00;
                        SendBytes[6] = 0x00;
                        SendBytes[7] = 0x0D;

                        try
                        {
                            f1.TransmitData(SendBytes);
                        }
                        catch
                        {
                            MessageBox.Show("串口通讯错误", "错误");
                        }
                    }
                    else if (column.DefaultCellStyle.NullValue == "关闭主接触器")
                    {
                        byte[] SendBytes = new byte[8];
                        SendBytes[0] = 0x0D;
                        SendBytes[1] = 0xFD;//查询
                        SendBytes[2] = 0x0E;
                        SendBytes[3] = 0x00;
                        SendBytes[4] = 0x00;
                        SendBytes[5] = 0x00;
                        SendBytes[6] = 0x00;
                        SendBytes[7] = 0x0D;
                        this.打开主接触器.DefaultCellStyle.NullValue = "打开主接触器";

                        try
                        {
                            f1.TransmitData(SendBytes);
                        }
                        catch
                        {
                            MessageBox.Show("串口通讯错误", "错误");
                        }
                    }



                    if (column.DefaultCellStyle.NullValue == "打开直流电源")
                    {
                        byte[] SendBytes = new byte[8];
                        this.打开直流电源.DefaultCellStyle.NullValue = "关闭直流电源";
                        SendBytes[0] = 0x0D;
                        SendBytes[1] = 0xFB;//查询
                        SendBytes[2] = 0x10;
                        SendBytes[3] = 0x00;
                        SendBytes[4] = 0x00;
                        SendBytes[5] = 0x00;
                        SendBytes[6] = 0x00;
                        SendBytes[7] = 0x0D;

                        try
                        {
                            f1.TransmitData(SendBytes);
                        }
                        catch
                        {
                            MessageBox.Show("串口通讯错误", "错误");
                        }

                    }
                    else if (column.DefaultCellStyle.NullValue == "关闭直流电源")
                    {
                        byte[] SendBytes = new byte[8];
                        this.打开直流电源.DefaultCellStyle.NullValue = "打开直流电源";
                        SendBytes[0] = 0x0D;
                        SendBytes[1] = 0xFB;//查询
                        SendBytes[2] = 0x0F;
                        SendBytes[3] = 0x00;
                        SendBytes[4] = 0x00;
                        SendBytes[5] = 0x00;
                        SendBytes[6] = 0x00;
                        SendBytes[7] = 0x0D;

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
            
        }

        private void dataGridView3_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            byte[] SendBytes = new byte[8];

            SendBytes[0] = 0x0D;
            SendBytes[1] = 0xF2;
            SendBytes[2] = (byte)(e.RowIndex);
            SendBytes[3] = (byte)(e.ColumnIndex);
            SendBytes[4] = 0x00;
            SendBytes[5] = 0x00;
            SendBytes[6] = 0x00;
            SendBytes[7] = 0x0D;

            try
            {
                f1.TransmitData(SendBytes);
            }
            catch
            {
                MessageBox.Show("串口通讯错误", "错误");
            }
        }

        private void tabPane1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("1.根据万用表读数，手动录入电压和电流值\r\n2.电流标定在step6之后，需通过吸尘器或其他方法改变电流大小", "标定说明by leonGuo");
        }
    }
}
