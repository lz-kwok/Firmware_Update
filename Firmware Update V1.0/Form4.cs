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
            textBox1.Text = "00000";
            textBox2.Text = "00000";
        }

        public Form4(Form1 frm1)
        {
            InitializeComponent();
            textBox1.Text = ((Form1.PDNumber[0] << 8) + Form1.PDNumber[1]).ToString("00000");
            textBox2.Text = ((Form1.DeviceNumber[0] << 8) + Form1.DeviceNumber[1]).ToString("00000");
            f1 = frm1;
        }

        private void Form4_Load(object sender, EventArgs e)
        {

        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)//限数字
        {
            e.Handled = "0123456789\b".IndexOf(char.ToUpper(e.KeyChar)) < 0;
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)//限数字
        {
            e.Handled = "0123456789\b".IndexOf(char.ToUpper(e.KeyChar)) < 0;
        }

        byte AliSetupIndex = 0;

        private void button1_Click(object sender, EventArgs e)//确定
        {
            if (tabControl1.SelectedIndex == 0) { 
                byte[] SendBytes = new byte[10];
                DateTime Date = dateTimePicker1.Value;
                int tmp1, tmp2;
                tmp1 = Convert.ToInt32(textBox1.Text);
                tmp2 = Convert.ToInt32(textBox2.Text);

                if(Date.Year < 2000 || Date.Year > 2250)
                {
                    MessageBox.Show("出厂日期只允许在公元2000年~2250年之间！", "警告");
                }
                else
                {
                    DialogResult result = MessageBox.Show("请确保终端信息设置正确！", "提示", MessageBoxButtons.OKCancel);
                    if (result == DialogResult.OK)
                    {
                        try
                        {
                            SendBytes[0] = 0x0D;
                            SendBytes[1] = 0xFE;
                            SendBytes[2] = (byte)((Date.Year % 1000) & 0xFF);
                            SendBytes[3] = (byte)Date.Month;
                            SendBytes[4] = (byte)Date.Day;
                            SendBytes[5] = (byte)((tmp1 & 0xFF00) >> 8);//高八位
                            SendBytes[6] = (byte)(tmp1 & 0x00FF);//低八位
                            SendBytes[7] = (byte)((tmp2 & 0xFF00) >> 8);//高八位
                            SendBytes[8] = (byte)(tmp2 & 0x00FF);//低八位
                            SendBytes[9] = 0x0D;
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
            else if (tabControl1.SelectedIndex == 1)
            {
                int i = 0;
                AliSetupIndex++;
                byte[] SendBytes = new byte[100];
                string ProductKey = textBox3.Text;
                int ProductKeylen = textBox3.Text.Length;

                string DeviceName = textBox4.Text;
                int DeviceNamelen = textBox4.Text.Length;

                string DeviceSecret = textBox5.Text;
                int DeviceSecretlen = textBox5.Text.Length;

                int cmdLen = 0;
                DialogResult result = DialogResult.None;
                SendBytes[1] = AliSetupIndex;
                switch(AliSetupIndex)
                {
                    case 1:
                        for (i = 0; i < ProductKeylen; i++)
                        { 
                            SendBytes[3+i] = (byte)ProductKey[i];
                        }
                        SendBytes[2] = (byte)(ProductKeylen);
                        SendBytes[ProductKeylen+3] = 0x0D;
                        cmdLen = ProductKeylen + 4;
                        result = MessageBox.Show("请确保终ProductKey:"+ ProductKey +"输入正确！", "提示", MessageBoxButtons.OKCancel);
                        break;
                    case 2:
                        for (i = 0; i < DeviceNamelen; i++)
                        {
                            SendBytes[3 + i] = (byte)DeviceName[i];
                        }
                        SendBytes[2] = (byte)(DeviceNamelen);
                        SendBytes[DeviceNamelen + 3] = 0x0D;
                        cmdLen = DeviceNamelen + 4;
                        result = MessageBox.Show("请确保终DeviceName:" + DeviceName + "输入正确！", "提示", MessageBoxButtons.OKCancel);
                        break;
                    case 3:
                        AliSetupIndex = 0;
                        for (i = 0; i < DeviceSecretlen; i++)
                        {
                            SendBytes[3 + i] = (byte)DeviceSecret[i];
                        }
                        SendBytes[2] = (byte)(DeviceSecretlen);
                        SendBytes[DeviceSecretlen + 3] = 0x0D;
                        cmdLen = DeviceSecretlen + 4;
                        result = MessageBox.Show("请确保终DeviceSecret:" + DeviceSecret + "输入正确！", "提示", MessageBoxButtons.OKCancel);
                        break;
                }
                
                if (result == DialogResult.OK)
                {
                    try
                    {
                        SendBytes[0] = 0x0D;

                        Form1.serialPort1.Write(SendBytes, 0, cmdLen);
                        f1.ResetMessages(1);//清除终端信息

                        switch (SendBytes[1])
                        { 
                            case 1:
                                MessageBox.Show("设置ProductKey成功，继续点击‘确定’设置DeviceName！", "提示");
                                break;
                            case 2:
                                MessageBox.Show("设置DeviceName成功，继续点击‘确定’设置DeviceSecret！", "提示");
                                break;
                            case 3:
                                MessageBox.Show("设置DeviceSecret成功,完成AliIoT三参数配置！", "提示");
                                break;
                        }
                    }
                    catch
                    {
                        MessageBox.Show("串口通讯出错", "警告");
                    }
                }    
            }
            else if (tabControl1.SelectedIndex == 2) {
                byte[] SendBytes = new byte[27];
                double Longitude = double.Parse(textBox6.Text);
                double Latitude = double.Parse(textBox7.Text);
                double Altitude = double.Parse(textBox8.Text);

                byte[] LongitudeBuf = BitConverter.GetBytes(Longitude);
                byte[] LatitudeBuf = BitConverter.GetBytes(Latitude);
                byte[] AltitudeBuf = BitConverter.GetBytes(Altitude);

                DialogResult result = MessageBox.Show("请确保地理信息设置正确！", "提示", MessageBoxButtons.OKCancel);
                if (result == DialogResult.OK)
                {
                    try
                    {
                        SendBytes[0] = 0x0D;
                        SendBytes[1] = 0xD1;        //地理
                        for (int i = 0; i < 8; i++) {
                            SendBytes[i+2] = LongitudeBuf[i];
                        }
                        for (int i = 0; i < 8; i++)
                        {
                            SendBytes[i + 10] = LatitudeBuf[i];
                        }
                        for (int i = 0; i < 8; i++)
                        {
                            SendBytes[i + 18] = AltitudeBuf[i];
                        }

                        SendBytes[26] = 0x0D;
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

        private void textBox1_TextChanged(object sender, EventArgs e)//限幅
        {
            int v = 0;
            if (textBox1.Text != null && textBox1.Text != "")
                v = int.Parse(textBox1.Text);
            if (v > 65535)
                textBox1.Text = "65535";
        }

        private void textBox2_TextChanged(object sender, EventArgs e)//限幅
        {
            int v = 0;
            if (textBox2.Text != null && textBox2.Text != "")
                v = int.Parse(textBox2.Text);
            if (v > 65535)
                textBox2.Text = "65535";
        }

        private void button2_Click(object sender, EventArgs e)//取消
        {
            this.Close();
        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox7_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox8_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
