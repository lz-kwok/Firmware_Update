using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace Firmware_Update_V1._0
{
    public partial class Form2 : Form
    {
        private Form1 f1;
        public Form2()
        {
            InitializeComponent();  
        }
        public Form2(Form1 frm1)
        {
            InitializeComponent();
            f1 = frm1;
            textBox1.Text = Form1.FirmwareVersionOld.ToString("000");
            textBox3.Text = Environment.CurrentDirectory;
        }

        public static bool ReadyForUpdateFlag = false;
        public static byte FirmwareVersionNew = 0xFF;
        string FileText = "";
        private void button1_Click(object sender, EventArgs e)
        {
            string FirmwareName, FileName, txtName;
            byte FirmwareNumber;
            openFileDialog1.Multiselect = false;
            openFileDialog1.Title = "请选择固件";
            openFileDialog1.AddExtension = true;
            openFileDialog1.CheckFileExists = true;
            openFileDialog1.CheckPathExists = true;
            openFileDialog1.Filter = @"txt文件|*.txt";
            DialogResult result = openFileDialog1.ShowDialog();
            FileName = this.openFileDialog1.FileName;
            txtName = Path.GetFileName(FileName);

            if (result == DialogResult.OK && txtName.Length == 15) 
            {//在对话框中选中文件并确认，且文件名正好为15位                
                if (txtName.Substring(0, 8) == "OneBox_v")//判断文件名
                {
                    try
                    {
                        using (FileStream fsRead = new FileStream(FileName, FileMode.OpenOrCreate, FileAccess.Read))
                        {//读取文件到FileText里
                            int fsLEN = (int)fsRead.Length;
                            byte[] buffer = new byte[fsLEN];
                            int r = fsRead.Read(buffer, 0, buffer.Length);
                            FileText = Encoding.Default.GetString(buffer, 0, r);
                        }
                        if (FileText.Substring(0, 5) == "@c400")
                        {//判断文件内容            
                            FirmwareName = txtName.Remove(0, txtName.Length - 7).Remove(3, 4);//提取固件字符串
                            FirmwareNumber = byte.Parse(FirmwareName, System.Globalization.NumberStyles.Integer);
                            //显示↓↓↓
                            textBox3.Text = FileName;//包含路径的文件名
                            textBox2.Text = FirmwareName;//版本号
                            MessageBox.Show("固件已加载！", "提示");
                            ReadyForUpdateFlag = true;
                            FirmwareVersionNew = FirmwareNumber;
                        }
                        else
                        {
                            FileText = "";
                            FirmwareName = "";
                            textBox3.Text = FileName;
                            textBox2.Text = "";
                            MessageBox.Show("请选择正确的固件，注意起始地址为C400", "提示");
                            ReadyForUpdateFlag = false;
                            FirmwareVersionNew = 0xFF;
                        }
                    }
                    catch
                    {
                        FileText = "";
                        FirmwareName = "";
                        textBox3.Text = FileName;
                        textBox2.Text = "";
                        MessageBox.Show("请选择正确的固件", "提示");
                        ReadyForUpdateFlag = false;
                    }
                }
                else
                {
                    textBox3.Text = FileName;
                    textBox2.Text = "";
                    MessageBox.Show("请选择正确的固件", "提示");
                    ReadyForUpdateFlag = false;
                }
            }
            else
            {
                if (result == DialogResult.OK && txtName.Length != 15)
                {
                    textBox3.Text = FileName;
                    textBox2.Text = "";
                    MessageBox.Show("请选择正确的固件", "提示");
                    ReadyForUpdateFlag = false;
                }
                FileName = "";
                txtName = "";
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            byte[] SendBytes = new byte[10];
            if (ReadyForUpdateFlag == false)
            {
                MessageBox.Show("请先加载固件!", "警告");
            }
            else
            {
                if (button2.Text == "开始升级")
                {
                    timer1.Enabled = true;//打开定时器
                    button1.Enabled = false;//“浏览”
                    button2.Text = "停止升级";
                    CMD_Reset = false;
                    textBox4.AppendText("正在复位终端...");
                    try
                    {
                        Delay(100);
                        SendBytes[0] = 0x0D;
                        SendBytes[1] = 0xEF;
                        SendBytes[2] = 0X00;
                        SendBytes[3] = 0X00;
                        SendBytes[4] = 0X00;
                        SendBytes[5] = 0X00;
                        SendBytes[6] = 0x00;
                        SendBytes[7] = 0x00;
                        SendBytes[8] = 0x00;
                        SendBytes[9] = 0x0D;
                        Form1.serialPort1.Write(SendBytes, 0, SendBytes.Length);
                        Delay(100);
                        f1.ResetMessages(1);//清除终端信息
                    }
                    catch
                    {
                        timer1.Enabled = false;//关闭定时器
                        button1.Enabled = true;//“浏览”
                        button2.Text = "开始升级";
                        CMD_Reset = true;
                        textBox4.AppendText("Error[01]\r\n");//指令发送失败
                        textBox4.AppendText("终端固件升级失败！\r\n");
                    } 
                }
                else if (button2.Text == "停止升级")
                {
                    MessageBox.Show("固件升级中,请稍等！","警告");
                }
            }
        }

        private void Form2_FormClosing(object sender, FormClosingEventArgs e)//关闭窗口前
        {
            ReadyForUpdateFlag = false;
            CMD_Reset = true;
            CMD_Firmware = true;
            CMD_EraseBegin = true;
            CMD_EraseEnd = true;
            CMD_Send = true;
            CMD_Done = true;
            timer1.Enabled = false;//关闭定时器

            
        }

        public static bool CMD_Reset = true, CMD_EraseEnd = true, CMD_Firmware = true;
        public static bool CMD_EraseBegin = true, CMD_Send = true, CMD_Done = true;
        byte[] SendTxtBytes = new byte[16];

        private void timer1_Tick(object sender, EventArgs e)
        {
            byte[] SendBytes = new byte[10];
            int SendCount = 0;
            string SendTxtLine = "";
            int PercentCount = 0;
    
            if (Form1.UpdateState == 1 && CMD_Reset == false)//【状态1】
            {
                CMD_Reset = true;
                CMD_Firmware = false;
                textBox4.AppendText("OK\r\n");
                textBox4.AppendText("正在校验固件版本号...");
                try
                {
                    Delay(100);
                    SendBytes[0] = 0x0D;
                    SendBytes[1] = 0xEF;
                    SendBytes[2] = FirmwareVersionNew;//新版本号
                    SendBytes[3] = 0X00;
                    SendBytes[4] = 0X00;
                    SendBytes[5] = 0X00;
                    SendBytes[6] = 0x00;
                    SendBytes[7] = 0x00;
                    SendBytes[8] = 0x00;
                    SendBytes[9] = 0x0D;
                    Form1.serialPort1.Write(SendBytes, 0, SendBytes.Length);
                    Delay(100);
                }
                catch
                {
                    timer1.Enabled = false;//关闭定时器
                    button1.Enabled = true;//“浏览”
                    CMD_Firmware = true;
                    button2.Text = "开始升级";
                    textBox4.AppendText("Error[01]\r\n");//指令发送失败
                    textBox4.AppendText("终端固件升级失败！\r\n");
                }
            }
            if (Form1.UpdateState == 2 && CMD_Firmware == false)//【状态2】
            {
                CMD_Firmware = true;
                CMD_EraseBegin = false;
                textBox4.AppendText("OK\r\n");
            }
            if (Form1.UpdateState == 11 && CMD_Firmware == false)//固件版本校验未通过
            {
                timer1.Enabled = false;//关闭定时器
                button1.Enabled = true;//"浏览"
                CMD_Firmware = true;
                textBox4.AppendText("Error[03]\r\n");//固件版本校验未通过
                button2.Text = "开始升级";
                textBox4.AppendText("终端固件升级失败！\r\n");
            }
            if (Form1.UpdateState == 3 && CMD_EraseBegin == false)//【状态3】
            {
                CMD_EraseBegin = true;
                CMD_EraseEnd = false;
                textBox4.AppendText("正在擦除FLASH...");
            }
            if (Form1.UpdateState == 4 && CMD_EraseEnd == false)//【状态4】
            {
                CMD_EraseEnd = true;
                CMD_Send = false;
                textBox4.AppendText("OK\r\n");
                textBox4.AppendText("发送固件...");
            }
            if (Form1.UpdateState == 5 && CMD_Send == false)//【状态5】
            {
                CMD_Send = true;
                CMD_Done = false;
                textBox4.AppendText("00%");
                try
                {
                    SendTxtLine = "";
                    PercentCount = 0;
                    for (SendCount = 0; SendCount < FileText.Length; SendCount++)
                    {
                        if (FileText[SendCount] != '\n')
                        {
                            if (FileText[SendCount] != '\r')//不计回车符
                                SendTxtLine += FileText[SendCount];
                        }
                        else
                        {
                            SendTxtLine += '\n';
                            Form1.serialPort1.Write(SendTxtLine);//发送固件
                            //Delay(50);
                            SendTxtLine = "";

                            if (PercentCount < (SendCount * 100 / FileText.Length))
                            {
                                PercentCount = SendCount * 100 / FileText.Length;
                                if (PercentCount > 99)
                                    PercentCount = 99;
                                textBox4.Text = textBox4.Text.Substring(0, textBox4.Text.Length - 3);
                                textBox4.AppendText(PercentCount.ToString("00") + "%");
                            }
                        }
                    }
                }
                catch
                {
                    timer1.Enabled = false;//关闭定时器
                    button1.Enabled = true;//“浏览”
                    CMD_Done = true;
                    button2.Text = "开始升级";
                    textBox4.AppendText("Error[04]\r\n");//固件发送失败
                    textBox4.AppendText("终端固件升级失败！\r\n");
                }
            }
            if (Form1.UpdateState == 6 && CMD_Done == false)//【状态6】
            {
                timer1.Enabled = false;//关闭定时器
                button1.Enabled = true;//“浏览”
                CMD_Done = true;
                button2.Text = "开始升级";
                textBox4.Text = textBox4.Text.Substring(0, textBox4.Text.Length - 3);
                textBox4.AppendText("100%");
                textBox4.AppendText("\r\n终端固件升级成功！\r\n");
            }
        }

        byte ASCIIToHex(byte cNum)
        {
            if (cNum >= '0' && cNum <= '9')
            {
                cNum -= 0x30;//0的ASCII值
            }
            else if (cNum >= 'A' && cNum <= 'F')
            {
                cNum -= 0x41;//A的ASCII值
                cNum += 10;
            }

            return cNum;
        }

        public static void Delay(int milliSecond)
        {
            int start = Environment.TickCount;
            while (Math.Abs(Environment.TickCount - start) < milliSecond)
            {
                Application.DoEvents();
            }
        }

        private void ratingControl1_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void separatorControl1_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
