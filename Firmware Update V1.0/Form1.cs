using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO.Ports;

namespace Firmware_Update_V1._0
{
    public partial class Form1 : Form
    {
        public static SerialPort serialPort1 = new SerialPort();

        public static byte FirmwareVersionOld = 0x00;
        public static byte[] DeviceNumber = new byte[2];
        public static byte[] PDNumber = new byte[2];
        struct DateYMD
        {
            public byte Year;
            public byte Month;
            public byte Day;
        }
        DateYMD PDDate;
        public static byte TerminalType = 0xFF;
        public static byte TransmissionWay = 0x00;
        public static byte[] DEVEUI = new byte[2];
        public static byte SendPeriod;

        int TimerMScounter = 0;//计时器变量

         public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.comboBox2.SelectedIndex = 3;//波特率默认“115200”
            this.comboBox3.SelectedIndex = 0;//校验位默认“None”
            this.comboBox4.SelectedIndex = 3;//数据位默认“8”
            this.comboBox5.SelectedIndex = 0;//停止位默认“1”
        }

        private void button1_Click(object sender, EventArgs e)//“搜索”
        {
            comboBox1.Text = "";
            SearchAndAddSerialToComboBox(serialPort1, comboBox1);//调用扫描代码
        }
        private void SearchAndAddSerialToComboBox(SerialPort MyPort, ComboBox MyBox)//扫描代码
        {                                                               //将可用端口号添加到ComboBox
            string Buffer;                                              //缓存
            MyBox.Items.Clear();                                        //清空ComboBox内容
            for (int i = 1; i < 60; i++)                                //循环
            {
                try                                                     //核心原理是依靠try和catch完成遍历
                {
                    Buffer = "COM" + i.ToString();
                    MyPort.PortName = Buffer;
                    MyPort.Open();                                      //如果失败，后面的代码不会执行

                    MyBox.Items.Add(Buffer);                            //打开成功，添加至下拉列表
                    MyPort.Close();                                     //关闭
                }
                catch
                { }
            }
        }

        private void button2_Click(object sender, EventArgs e)//“打开串口”
        {
            if (serialPort1.IsOpen)//如果串口已经打开，按下此开关应关闭串口，并将此按钮显示为“打开串口”
            {
                try
                {
                    timer1.Enabled = false;//关闭定时器
                    serialPort1.Close();
                    button2.Text = "开始连接";
                    pictureBox1.Image = Properties.Resources.off;
                    comboBox1.Enabled = true;//“端口号”可选
                    comboBox2.Enabled = true;//“波特率”可选
                    comboBox3.Enabled = true;//“校验位”可选
                    comboBox4.Enabled = true;//“数据位”可选
                    comboBox5.Enabled = true;//“停止位”可选
                    button1.Enabled = true;//“搜索”可按
                    button4.Enabled = false;//“升级固件”禁按
                    button10.Enabled = false;//“复位终端”禁按
                    button7.Enabled = false;//“出厂设置”禁按
                    button6.Enabled = false;//“终端设置”禁按
                    button8.Enabled = false;//“时间同步”禁按
                    button9.Enabled = false;//“查询终端”禁按
                    serialPort1.DataReceived -= new System.IO.Ports.SerialDataReceivedEventHandler(this.serialPort1_DataReceived);
                }
                catch
                {
                    MessageBox.Show("串口断开失败", "错误");
                }
            }
            else
            {
                try
                {
                    serialPort1.PortName = comboBox1.Text;//1.端口号
                    serialPort1.BaudRate = Convert.ToInt32(comboBox2.Text);//2.波特率
                    switch (comboBox3.Text)//3.校验位
                    {
                        case "None （无）": serialPort1.Parity = Parity.None;break;
                        case "Odd  （奇）": serialPort1.Parity = Parity.Odd;break;
                        case "Even （偶）": serialPort1.Parity = Parity.Even;break;
                        case "Mark （=1）": serialPort1.Parity = Parity.Mark;break;
                        case "Space（=0）": serialPort1.Parity = Parity.Space;break;
                            default: serialPort1.Parity = Parity.None;break;
                    }
                    serialPort1.DataBits = Convert.ToInt32(comboBox4.Text);//4.数据位
                    switch (comboBox5.Text)//5.停止位
                    {
                        case   "1": serialPort1.StopBits = StopBits.One; break;
                        case "1.5": serialPort1.StopBits = StopBits.OnePointFive; break;
                        case   "2": serialPort1.StopBits = StopBits.Two; break;
                           default: serialPort1.StopBits = StopBits.One;break;
                    }
                    timer1.Enabled = true;//打开定时器
                    serialPort1.Open();//打开端口
                    button2.Text = "断开连接";
                    pictureBox1.Image = Properties.Resources.on;
                    comboBox1.Enabled = false;//“端口号”禁选
                    comboBox2.Enabled = false;//“波特率”禁选
                    comboBox3.Enabled = false;//“校验位”禁选
                    comboBox4.Enabled = false;//“数据位”禁选
                    comboBox5.Enabled = false;//“停止位”禁选
                    button1.Enabled = false;//“搜索”禁按
                    button4.Enabled = false;//“升级固件”禁按
                    button10.Enabled = false;//“复位终端”禁按
                    button7.Enabled = false;//“出厂设置”禁按
                    button6.Enabled = false;//“终端设置”禁按
                    button8.Enabled = false;//“时间同步”禁按
                    button9.Enabled = true;//“查询终端”可按
                    serialPort1.DataReceived += new System.IO.Ports.SerialDataReceivedEventHandler(this.serialPort1_DataReceived);
                }
                catch
                {
                    MessageBox.Show("串口连接失败", "错误");
                }
            }
        }

        public static int UpdateState = 0;//升级状态

        private void serialPort1_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            string s = "";
            string CurrentTime = "";
            string TerminalTypeText = "", TransmissionWayText = "";
            int count = serialPort1.BytesToRead;//接收数据的字符数
            byte[] data = new byte[count];//创建接收8位数据数组data
            bool ShowFlag = false;
            serialPort1.Read(data, 0, count);//将接收的数据存放到data
            CurrentTime = "【" + DateTime.Now.ToString("f") + ":" + DateTime.Now.Second.ToString("D2") + "】";

            if (Form2.ReadyForUpdateFlag && count == 1)
            {
                ShowFlag = true;
                switch (data[0])
                {
                    case 0x75: UpdateState = 1; break;//复位成功
                    case 0x76: UpdateState = 11; break;//固件版本校验未通过
                    case 0x77: UpdateState = 2; break;//固件版本校验通过
                    case 0x78: UpdateState = 3; break;//开始擦除芯片
                    case 0x79: UpdateState = 4; break;//擦除芯片完成
                    case 0x7A: UpdateState = 5; break;//开始发送固件
                    case 0x7B: UpdateState = 6; break;//固件升级完成，重启
                    default: UpdateState = 0; break;
                }
            }
            else
                ShowFlag = false;
            if(count == 15)//接收到“终端信息”
            {
                if (data[0] == 0xFF && data[14] == 0xFF)
                {
                    ShowFlag = true;
                    FirmwareVersionOld = data[1];
                    DeviceNumber[0] = data[2];
                    DeviceNumber[1] = data[3];
                    PDNumber[0] = data[4];
                    PDNumber[1] = data[5];
                    PDDate.Year = data[6];
                    PDDate.Month = data[7];
                    PDDate.Day = data[8];
                    GloabValue.TerminalTypeValue = TerminalType = data[9];
                    GloabValue.TransmissionTypeValue = TransmissionWay = data[10];
                    DEVEUI[0] = data[11];
                    DEVEUI[1] = data[12];
                    SendPeriod = data[13];

                    //this.comboBox1.SelectedIndex = TerminalType;

                    switch (TerminalType)
                    {
                        case 0x01: TerminalTypeText = "[01]空气"; break;
                        case 0x02: TerminalTypeText = "[02]VOC"; break;
                        case 0x03: TerminalTypeText = "[03]扬尘噪音"; break;
                        case 0x04: TerminalTypeText = "[04]水雨情"; break;
                        case 0x05: TerminalTypeText = "[05]气象"; break;
                        case 0x06: TerminalTypeText = "[06]水质水况"; break;
                        case 0x07: TerminalTypeText = "[07]土壤"; break;
                        case 0x08: TerminalTypeText = "[08]农业"; break;
                        case 0x09: TerminalTypeText = "[09]流量计"; break;

                        case 0x10: TerminalTypeText = "[10]总磷总氮"; break;
                        case 0x11: TerminalTypeText = "[11]易涝点"; break;
                        case 0x12: TerminalTypeText = "[12]窖井管道流量"; break;
                        case 0x13: TerminalTypeText = "[13]雨量"; break;
                        case 0x14: TerminalTypeText = "[14]一体化窖井液位"; break;

                        case 0x0A: TerminalTypeText = "[0A]二次供水"; break;
                        case 0x0B: TerminalTypeText = "[0B]TankMonitor"; break;
                        case 0x55: TerminalTypeText = "[55]定制需求"; break;
                        default: TerminalTypeText = "未知"; break;
                    }

                    //this.comboBox2.SelectedIndex = TerminalType;
                    switch (TransmissionWay)
                    {
                        case 0x01: TransmissionWayText = "[01]GPRS"; break;
                        case 0x07: TransmissionWayText = "[02]GPRS"; break;
                        case 0x02: TransmissionWayText = "[02]NBIoT"; break;
                        case 0x03: TransmissionWayText = "[02]NBIoT"; break;
                        case 0x04: TransmissionWayText = "[03]LoRa"; break;
                        case 0x05: TransmissionWayText = "[03]LoRa"; break;
                        case 0x06: TransmissionWayText = "[03]LoRa"; break;
                        default: TransmissionWayText = "未知"; break;
                    }

                    this.textBox1.Text = ((DEVEUI[0] << 8) + DEVEUI[1]).ToString("X2").PadLeft(4, '0');

                    if (this.InvokeRequired)//1.代理
                    {
                        this.Invoke(new MethodInvoker(delegate
                        {
                            textBox5.Text = FirmwareVersionOld.ToString("000");
                            textBox6.Text = ((DeviceNumber[0] << 8) + DeviceNumber[1]).ToString("00000");
                            textBox7.Text = "2" + PDDate.Year.ToString("000") + "年" + PDDate.Month.ToString() + "月" + PDDate.Day.ToString() + "日";
                            textBox8.Text = ((PDNumber[0] << 8) + PDNumber[1]).ToString("00000");
                            textBox9.Text = TerminalTypeText;
                            textBox10.Text = TransmissionWayText;
                            textBox11.Text = "004A77054800" + ((DEVEUI[0] << 8) + DEVEUI[1]).ToString("X2").PadLeft(4, '0');
                            textBox3.Text = SendPeriod.ToString() + "min";
                        }));
                    }
                    else//2.正常
                    {
                        textBox5.Text = FirmwareVersionOld.ToString("000");
                        textBox6.Text = ((DeviceNumber[0] << 8) + DeviceNumber[1]).ToString("00000");
                        textBox7.Text = "2" + PDDate.Year.ToString("000") + "年" + PDDate.Month.ToString() + "月" + PDDate.Day.ToString() + "日";
                        textBox8.Text = ((PDNumber[0] << 8) + PDNumber[1]).ToString("00000");
                        textBox9.Text = TerminalTypeText;
                        textBox10.Text = TransmissionWayText;
                        textBox11.Text = "004A77054800" + ((DEVEUI[0] << 8) + DEVEUI[1]).ToString("X2").PadLeft(4, '0');
                        textBox3.Text = SendPeriod.ToString() + "min";
                    }
                }
                else
                    ShowFlag = false;
            }
            if (radioButton3.Checked == true && !ShowFlag)//1.字符型接收
            {
                //foreach (byte item in data)
                //{
                //    s += Convert.ToChar(item);
                //}
                s = System.Text.Encoding.Default.GetString(data);
            }
            else if(radioButton4.Checked == true || ShowFlag)//2.十六进制接收
            {
                s = byteToHexStr(data);
            }

            //↓↓↓接收数据显示区代码↓↓↓
            if (this.InvokeRequired)//1.代理
            {
                this.Invoke(new MethodInvoker(delegate
                {
                    if (checkBox1.Checked == true)
                        this.textBox1.AppendText(CurrentTime + "\r\n" + s + "\n");
                    else
                        this.textBox1.AppendText(s + "\n");
                }));
            }
            else//2.正常
            {
                if (checkBox1.Checked == true)
                    this.textBox1.AppendText(CurrentTime + "\r\n" + s + "\n");
                else
                    this.textBox1.AppendText(s + "\n");
            }
            //↑↑↑接收数据显示区代码↑↑↑

        }
        public static string byteToHexStr(byte[] bytes)//将十六进制数组组合成字符串显示
        {
            string returnStr = "";
            if (bytes != null)
            {
                for (int i = 0; i < bytes.Length; i++)
                {
                    returnStr += bytes[i].ToString("X2");
                    returnStr += " ";
                }
            }
            return returnStr;
        }

        private void button3_Click(object sender, EventArgs e)//“发送数据”
        {
            byte[] SendBytes = null;
            string SendData = textBox2.Text;//需要发送的数据
            List<string> SendDataList = new List<string>();//字符两两存一个

            try
            {
                if (radioButton1.Checked == true)//1.字符型发送
                    serialPort1.WriteLine(SendData);
                else//2.十六进制发送
                {
                    //剔除所有空格
                    SendData = SendData.Replace("\n", "").Replace(" ", "").Replace("\r", "");
                    //每两个字符放进认为一个字节

                    for (int i = 0; i < SendData.Length; i = i + 2)
                    {
                        if (SendData.Length - i == 1)
                            SendDataList.Add(SendData.Substring(i, 1));
                        else
                            SendDataList.Add(SendData.Substring(i, 2));
                    }
                    SendBytes = new byte[SendDataList.Count];//创建发送数组
                    try
                    {
                        for (int j = 0; j < SendBytes.Length; j++)
                        {
                            SendBytes[j] = (byte)(Convert.ToInt32(SendDataList[j], 16));//把单个字符转十六进制
                        }
                        serialPort1.Write(SendBytes, 0, SendBytes.Length);
                    }
                    catch
                    {
                        MessageBox.Show("请输入正确的十六进制数", "提示");
                    }
                }
            }
            catch
            {
                MessageBox.Show("串口通讯错误", "错误");
            }
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)//TextBox输入事件
        {
            if(radioButton2.Checked == true)//十六进制发送，限定输入值
                e.Handled = "0123456789ABCDEF \b".IndexOf(char.ToUpper(e.KeyChar)) < 0;
        }

        private void button5_Click(object sender, EventArgs e)//清除接收区
        {
            textBox1.Clear();
        }

        
        private void timer1_Tick(object sender, EventArgs e)//定时器（1s）
        {
            bool AutosendFlag = false;
            int AutosendPeriod = 1;
            
            TimerMScounter++;
            if (TimerMScounter > 1000000000)
                TimerMScounter = 0;
            if (checkBox2.Checked == true)
            {
                AutosendFlag = true;
                AutosendPeriod = Convert.ToInt32(textBox4.Text);
            }
            if (AutosendFlag)
            {
                if (TimerMScounter % AutosendPeriod == 0)
                {
                    TimerMScounter = 0;
                    button3_Click(sender, e);
                }
            }
            if(textBox5.Text == null || textBox5.Text == "")
            {
                button4.Enabled = false;//“升级固件”禁按
                button10.Enabled = false;//“复位终端”禁按
                button7.Enabled = false;//“出厂设置”禁按
                button6.Enabled = false;//“终端设置”禁按
                button8.Enabled = false;//“时间同步”禁按
            }
            else if(textBox5.Text == "255")
            {
                button4.Enabled = false;//“升级固件”可按
                button10.Enabled = true;//“复位终端”可按
                button7.Enabled = false;//“出厂设置”禁按
                button6.Enabled = false;//“终端设置”禁按
                button8.Enabled = false;//“时间同步”禁按
            }
            else
            {
                button4.Enabled = false;//“升级固件”可按
                button10.Enabled = true;//“复位终端”可按
                button7.Enabled = false;//“出厂设置”禁按
                button6.Enabled = true;//“终端设置”禁按
                button8.Enabled = true;//“时间同步”禁按
            }
            if (textBox9.Text == "[01]空气")
                pictureBox2.Image = Firmware_Update_V1._0.Properties.Resources.空气;
            else if (textBox9.Text == "[02]VOC")
                pictureBox2.Image = Firmware_Update_V1._0.Properties.Resources.VOC;
            else if (textBox9.Text == "[03]扬尘噪音")
                pictureBox2.Image = Firmware_Update_V1._0.Properties.Resources.扬尘;
            else if (textBox9.Text == "[04]水雨情")
                pictureBox2.Image = Firmware_Update_V1._0.Properties.Resources.降雨;
            else if (textBox9.Text == "[05]气象")
                pictureBox2.Image = Firmware_Update_V1._0.Properties.Resources.气象;
            else if (textBox9.Text == "[06]水质水况")
                pictureBox2.Image = Firmware_Update_V1._0.Properties.Resources.水质;
            else if (textBox9.Text == "[07]土壤")
                pictureBox2.Image = Firmware_Update_V1._0.Properties.Resources.土壤;
            else if (textBox9.Text == "[08]农业")
                pictureBox2.Image = Firmware_Update_V1._0.Properties.Resources.农业;
            else if (textBox9.Text == "[09]流量计")
                pictureBox2.Image = Firmware_Update_V1._0.Properties.Resources.流量计;
            else if (textBox9.Text == "[10]总磷总氮")
                pictureBox2.Image = Firmware_Update_V1._0.Properties.Resources.总磷总氮;
            else if (textBox9.Text == "[11]易涝点")
                pictureBox2.Image = Firmware_Update_V1._0.Properties.Resources.易涝点;
            else if (textBox9.Text == "[12]窖井管道流量")
                pictureBox2.Image = Firmware_Update_V1._0.Properties.Resources.管道;
            else if (textBox9.Text == "[13]雨量")
                pictureBox2.Image = Firmware_Update_V1._0.Properties.Resources.雨量;
            else if (textBox9.Text == "[14]一体化窖井液位")
                pictureBox2.Image = Firmware_Update_V1._0.Properties.Resources.窖井;
            else if (textBox9.Text == "[0A]二次供水")
                pictureBox2.Image = Firmware_Update_V1._0.Properties.Resources.二次供水;
            else if (textBox9.Text == "[0B]TankMonitor")
                pictureBox2.Image = Firmware_Update_V1._0.Properties.Resources.TankMonitor;
            else if (textBox9.Text == "[55]定制需求")
                pictureBox2.Image = Firmware_Update_V1._0.Properties.Resources.定制需求1;
            else
                pictureBox2.Image = Firmware_Update_V1._0.Properties.Resources.透明logo;
        }

        private void button4_Click(object sender, EventArgs e)//固件升级窗口
        {
            Form2 frm = new Form2(this);//首先实例化
            frm.ShowDialog();
        }

        private void button6_Click(object sender, EventArgs e)//终端设置窗口
        {
            Form3 frm = new Form3(this);//首先实例化
            frm.ShowDialog();
        }

        private void button7_Click(object sender, EventArgs e)//出厂设置窗口
        {
            Form4 frm = new Form4(this);
            frm.ShowDialog();
        }

        private void button8_Click(object sender, EventArgs e)//时钟同步
        {
            byte[] SendBytes = new byte[10];

            try
            {
                SendBytes[0] = 0x0D;
                SendBytes[1] = 0xF5;//功能码
                SendBytes[9] = 0x0D;
                SendBytes[2] = ByteToBCD((byte)(DateTime.Now.Year % 100));//年
                SendBytes[3] = ByteToBCD((byte)DateTime.Now.Month);//月
                SendBytes[4] = ByteToBCD((byte)DateTime.Now.Day);//日
                SendBytes[8] = (byte)DateTime.Now.DayOfWeek;//星期
                SendBytes[5] = ByteToBCD((byte)DateTime.Now.Hour);//时
                SendBytes[6] = ByteToBCD((byte)DateTime.Now.Minute);//分
                SendBytes[7] = ByteToBCD((byte)DateTime.Now.Second);//秒
                Form1.serialPort1.Write(SendBytes, 0, SendBytes.Length);
                MessageBox.Show("同步成功！", "提示");
            }
            catch
            {
                MessageBox.Show("串口通讯出错", "警告");
            }
        }
        public static byte ByteToBCD(byte b)//byte转BCD
        {           
            byte b1 = (byte)(b / 10);//高四位         
            byte b2 = (byte)(b % 10);//低四位  

            return (byte)((b1 << 4) | b2);
        }

        private void button9_Click(object sender, EventArgs e)//查询终端
        {
            byte[] SendBytes = new byte[10];

            try
            {
                SendBytes[0] = 0x0D;
                SendBytes[1] = 0xF0;//查询
                SendBytes[2] = 0x00;
                SendBytes[3] = 0X00;
                SendBytes[4] = 0X00;
                SendBytes[5] = 0X00;
                SendBytes[6] = 0x00;
                SendBytes[7] = 0x00;
                SendBytes[8] = 0x00;
                SendBytes[9] = 0x0D;
                Form1.serialPort1.Write(SendBytes, 0, SendBytes.Length);
            }
            catch
            {
                MessageBox.Show("串口通讯错误","错误");
            }
        }

        private void button10_Click(object sender, EventArgs e)//复位终端
        {
            byte[] SendBytes = new byte[10];

            try
            {
                SendBytes[0] = 0x0D;
                SendBytes[1] = 0xFD;//复位
                SendBytes[2] = 0x00;
                SendBytes[3] = 0X00;
                SendBytes[4] = 0X00;
                SendBytes[5] = 0X00;
                SendBytes[6] = 0x00;
                SendBytes[7] = 0x00;
                SendBytes[8] = 0x00;
                SendBytes[9] = 0x0D;
                Form1.serialPort1.Write(SendBytes, 0, SendBytes.Length);
                MessageBox.Show("复位成功！", "提示");
            }
            catch
            {
                MessageBox.Show("串口通讯错误", "错误");
            }
        }

        public void ResetMessages(byte e)
        {
            if (this.InvokeRequired)//1.代理
            {
                this.Invoke(new MethodInvoker(delegate
                {
                    textBox5.Text = "";
                    textBox6.Text = "";
                    textBox7.Text = "";
                    textBox8.Text = "";
                    textBox9.Text = "";
                    textBox10.Text = "";
                    textBox11.Text = "";
                    textBox3.Text = "";
                }));
            }
            else//2.正常
            {
                textBox5.Text = "";
                textBox6.Text = "";
                textBox7.Text = "";
                textBox8.Text = "";
                textBox9.Text = "";
                textBox10.Text = "";
                textBox11.Text = "";
                textBox3.Text = "";
            }
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {   
            if (textBox2.Text != "")
            {
                if (radioButton2.Checked == true)//切换到十六进制
                {
                    byte[] array = System.Text.Encoding.ASCII.GetBytes(textBox2.Text);
                    string ASCIIstr2 = null;
                    for(int i = 0; i < array.Length; i++)
                    {
                        int asciicode = (int)(array[i]);
                        ASCIIstr2 += (Convert.ToString(asciicode, 16).ToUpper().PadLeft(2,'0') + " ");
                    }
                    textBox2.Text = ASCIIstr2;
                }
                else
                {
                    byte[] CharBytes = null;
                    List<string> CharDataList = new List<string>();//字符两两存一个
                    //剔除所有空格
                    string charstring = textBox2.Text.Replace(" ", "").Replace("\r", "");
                    //每两个字符放进认为一个字节

                    for (int i = 0; i < charstring.Length; i = i + 2)
                    {
                        if (charstring.Length - i == 1)
                            CharDataList.Add(charstring.Substring(i, 1));
                        else
                            CharDataList.Add(charstring.Substring(i, 2));
                    }
                    CharBytes = new byte[CharDataList.Count];//创建发送数组

                    for (int j = 0; j < CharBytes.Length; j++)
                    {
                        CharBytes[j] = (byte)(Convert.ToInt32(CharDataList[j], 16));//把单个字符转十六进制
                    }
                    textBox2.Text = System.Text.Encoding.ASCII.GetString(CharBytes);
                }
            }
        }

        private void textBox10_TextChanged(object sender, EventArgs e)
        {

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void textBox9_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
