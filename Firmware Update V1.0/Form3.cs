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

        private void Form3_Load(object sender, EventArgs e)
        {
            textBox1.Text = ((Form1.DEVEUI[0] << 8) + Form1.DEVEUI[1]).ToString("X2").PadLeft(4, '0');
            textBox2.Text = Form1.SendPeriod.ToString();
        }

        private void button1_Click(object sender, EventArgs e)//确认设置
        {
            byte[] SendBytes = new byte[10];
            byte TerminalTypeNum = 0x01, TransmissionWayNum = 0x01;

            if (textBox1.TextLength < textBox1.MaxLength)
            {
                MessageBox.Show("DevEUI填写长度有误，请检查！", "警告");
            }
            else if(textBox2.Text == "")
            {
                MessageBox.Show("上传周期填写有误，请检查！", "警告");
            }
            else
            {
                DialogResult result = MessageBox.Show("请确保终端信息设置正确！", "提示", MessageBoxButtons.OKCancel);
                if (result == DialogResult.OK)
                {
                    switch(comboBox1.SelectedIndex)
                    {
                        case 0: TerminalTypeNum = 0x01; break;
                        case 1: TerminalTypeNum = 0x02; break;
                        case 2: TerminalTypeNum = 0x03; break;
                        case 3: TerminalTypeNum = 0x04; break;
                        case 4: TerminalTypeNum = 0x05; break;
                        case 5: TerminalTypeNum = 0x06; break;
                        case 6: TerminalTypeNum = 0x07; break;
                        case 7: TerminalTypeNum = 0x08; break;
                        case 8: TerminalTypeNum = 0x09; break;

                        case 9: TerminalTypeNum = 0x10; break;
                        case 10: TerminalTypeNum = 0x11; break;
                        case 11: TerminalTypeNum = 0x12; break;
                        case 12: TerminalTypeNum = 0x13; break;
                        case 13: TerminalTypeNum = 0x14; break;

                        case 14: TerminalTypeNum = 0x0A; break;
                        case 15: TerminalTypeNum = 0x0B; break;
                        case 16: TerminalTypeNum = 0x55; break;
                        default: TerminalTypeNum = 0x01; break;
                    }
                    switch(comboBox2.SelectedIndex)
                    {
                        case 0: TransmissionWayNum = 0x01; break;
                        case 1: TransmissionWayNum = 0x07; break;
                        case 2: TransmissionWayNum = 0x02; break;
                        case 3: TransmissionWayNum = 0x03; break;
                        case 4: TransmissionWayNum = 0x04; break;
                        case 5: TransmissionWayNum = 0x05; break;
                        case 6: TransmissionWayNum = 0x06; break;
                        default: TransmissionWayNum = 0x01; break;
                    }
                    try
                    {
                        SendBytes[0] = 0x0D;
                        SendBytes[1] = 0xFA;
                        //SendBytes[2] = TerminalTypeNum;
                        //SendBytes[3] = TransmissionWayNum;
                        SendBytes[2] = GloabValue.TerminalTypeValue;
                        SendBytes[3] = GloabValue.TransmissionTypeValue;
                        SendBytes[4] = byte.Parse(textBox1.Text.Substring(0, 2), System.Globalization.NumberStyles.HexNumber);
                        SendBytes[5] = byte.Parse(textBox1.Text.Substring(2, 2), System.Globalization.NumberStyles.HexNumber);
                        SendBytes[6] = (byte)int.Parse(textBox2.Text);
                        SendBytes[7] = 0x00;
                        SendBytes[8] = 0x00;
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

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            int v = 0;
            if (textBox2.Text != null && textBox2.Text != "")
                v = int.Parse(textBox2.Text);
            if (v > 255)
                textBox2.Text = "255";
            else if(v < 1)
                textBox2.Text = "1";
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

    }
}
