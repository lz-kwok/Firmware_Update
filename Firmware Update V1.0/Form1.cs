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
using System.IO.Ports;


namespace Firmware_Update_V1._0
{
    public interface IView
    {
        void SetController(IController controller);
        //Open serial port event
        void OpenComEvent(Object sender, SerialPortEventArgs e);
        //Close serial port event
        void CloseComEvent(Object sender, SerialPortEventArgs e);
        //Serial port receive data event
        void ComReceiveDataEvent(Object sender, SerialPortEventArgs e);
    }
    public partial class Form1 : Form, IView
    {
        private IController controller;
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

         public void SetController(IController controller)
         {
             this.controller = controller;
         }

         

        private void Form1_Load(object sender, EventArgs e)
        {
            this.baudRateCbx.SelectedIndex = 3;//波特率默认“115200”
            this.parityCbx.SelectedIndex = 0;//校验位默认“None”
            this.dataBitsCbx.SelectedIndex = 3;//数据位默认“8”
            this.stopBitsCbx.SelectedIndex = 0;//停止位默认“1”
            this.handshakingcbx.SelectedIndex = 0;//流控位默认“0”

            update_button.Enabled = false;                  //“升级固件”禁按
            reset_button.Enabled = false;                   //“复位终端”禁按
            query_mode_button.Enabled = false;               //“查询终端”可按
            simpleButton2.Enabled = false;
        }

        private void button1_Click(object sender, EventArgs e)//“搜索”
        {
            comListCbx.Text = "";
            SearchAndAddSerialToComboBox(serialPort1, comListCbx);//调用扫描代码
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

        private void openCloseSpbtn_Click(object sender, EventArgs e)//“打开串口”
        {
            try
            {
                if (openCloseSpbtn.Text == "开始连接")
                {
                    controller.OpenSerialPort(comListCbx.Text, baudRateCbx.Text,
                        dataBitsCbx.Text, stopBitsCbx.Text, parityCbx.Text,            //dataBitsCbx.Text, stopBitsCbx.Text, parityCbx.Text,
                        handshakingcbx.Text);
                    openCloseSpbtn.Text = "断开连接";
                    update_button.Enabled = false;                  //“升级固件”禁按
                    reset_button.Enabled = false;                   //“复位终端”禁按
                    query_mode_button.Enabled = true;               //“查询终端”可按
                    simpleButton2.Enabled = false;
                }
                else
                {
                    controller.CloseSerialPort();
                }
            }
            catch
            {
                MessageBox.Show("串口连接失败", "错误");
            }
            
        }

        public void OpenComEvent(Object sender, SerialPortEventArgs e)
        {
            if (this.InvokeRequired)
            {
                Invoke(new Action<Object, SerialPortEventArgs>(OpenComEvent), sender, e);
                return;
            }

            if (e.isOpend)  //Open successfully
            {
                try
                {
                    timer1.Enabled = false;//关闭定时器
                    openCloseSpbtn.Text = "开始连接";
                    comListCbx.Enabled = true;//“端口号”可选
                    baudRateCbx.Enabled = true;//“波特率”可选
                    parityCbx.Enabled = true;//“校验位”可选
                    dataBitsCbx.Enabled = true;//“数据位”可选
                    stopBitsCbx.Enabled = true;//“停止位”可选
                    button1.Enabled = true;//“搜索”可按
                    update_button.Enabled = false;//“升级固件”禁按
                    reset_button.Enabled = false;//“复位终端”禁按

                    query_mode_button.Enabled = false;//“查询模式”禁按
                    reset_button.Enabled = false;
                }
                catch
                {
                    MessageBox.Show("串口断开失败", "错误");
                }
            }
        }

        public void CloseComEvent(Object sender, SerialPortEventArgs e)
        {
            if (this.InvokeRequired)
            {
                Invoke(new Action<Object, SerialPortEventArgs>(CloseComEvent), sender, e);
                return;
            }

            if (!e.isOpend) //close successfully
            {
                openCloseSpbtn.Text = "开始连接";

                comListCbx.Enabled = true;
                baudRateCbx.Enabled = false;
                dataBitsCbx.Enabled = false;
                stopBitsCbx.Enabled = false;
                parityCbx.Enabled = false;
                handshakingcbx.Enabled = false;
            }
        }

        public void ComReceiveDataEvent(Object sender, SerialPortEventArgs e)
        {
            if (this.InvokeRequired)
            {
                try
                {
                    Invoke(new Action<Object, SerialPortEventArgs>(ComReceiveDataEvent), sender, e);
                }
                catch (System.Exception)
                {
                    //disable form destroy exception
                }
                return;
            }

            if (radioButton3.Checked) //display as string 
            {
                this.receivetbx.AppendText(Encoding.Default.GetString(e.receivedBytes));
            }
            else //display as hex
            {
                if (receivetbx.Text.Length > 0)
                {
                    receivetbx.AppendText("-");
                }
                receivetbx.AppendText(IController.Bytes2Hex(e.receivedBytes));
            }

            if ((e.receivedBytes[0] == 0x0D) && (e.receivedBytes[e.receivedBytes.Length - 1] == 0x0D))
            {
                switch (e.receivedBytes[1])
                { 
                    case 0xfe:
                        firmware_version.Text = "固件版本" + "1.0.0\r\n";
                        break;
                }
            }

        }

        private void sendbtn_Click(object sender, EventArgs e)
        {
            String sendText = sendtbx.Text;
            bool flag = false;
            if (sendText == null)
            {
                return;
            }
            //set select index to the end
            sendtbx.SelectionStart = sendtbx.TextLength;

            if (radioButton3.Checked)
            {
                flag = controller.SendDataToCom(sendText);
            }
            else 
            {
                Byte[] bytes = IController.Hex2Bytes(sendText);
                flag = controller.SendDataToCom(bytes);
            }

            if (!flag)
            {
                MessageBox.Show("数据发送", "失败");
            }

        }

        public static int UpdateState = 0;//升级状态

        private void serialPort1_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            string s = "";
            string CurrentTime = "";
            int count = serialPort1.BytesToRead;//接收数据的字符数
            byte[] data = new byte[count];//创建接收8位数据数组data
            bool ShowFlag = false;
            int deviceMode = 0; //默认手动模式
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
                    deviceMode = data[2];

                    GloabValue.TerminalTypeValue = TerminalType = data[9];
                    GloabValue.TransmissionTypeValue = TransmissionWay = data[10];

                    switch (deviceMode)
                    {
                     //   case 0x01: this.tabPage1.TabIndex = 0; break;
                    //    case 0x02: this.tabPage1.TabIndex = 1; ; break;
                        default: break;
                    }

                    if (this.InvokeRequired)//1.代理
                    {
                        this.Invoke(new MethodInvoker(delegate
                        {

                        }));
                    }
                    else//2.正常
                    {

                    }
                }
                else
                    ShowFlag = false;
            }

            if (ShowFlag == false) {
                if (radioButton3.Checked == true) {
                    s = System.Text.Encoding.Default.GetString(data);
                }
                if (radioButton4.Checked == true) {
                    s = byteToHexStr(data);
                }
            }

            //↓↓↓接收数据显示区代码↓↓↓
            if (this.InvokeRequired)//1.代理
            {
                this.Invoke(new MethodInvoker(delegate
                {
                    if (checkBox1.Checked == true)
                        this.receivetbx.AppendText(CurrentTime + "\r\n" + s + "\n");
                    else
                        this.receivetbx.AppendText(s + "\n");
                }));
            }
            else//2.正常
            {
                if (checkBox1.Checked == true)
                    this.receivetbx.AppendText(CurrentTime + "\r\n" + s + "\n");
                else
                    this.receivetbx.AppendText(s + "\n");
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
            string SendData = sendtbx.Text;//需要发送的数据
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

        private void sendtbx_KeyPress(object sender, KeyPressEventArgs e)//TextBox输入事件
        {
            if(radioButton2.Checked == true)//十六进制发送，限定输入值
                e.Handled = "0123456789ABCDEF \b".IndexOf(char.ToUpper(e.KeyChar)) < 0;
        }

        private void button5_Click(object sender, EventArgs e)//清除接收区
        {
            receivetbx.Clear();
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
            if (firmware_version.Text == null || firmware_version.Text == "")
            {
                update_button.Enabled = false;//“升级固件”禁按
                reset_button.Enabled = false;//“复位终端”禁按
            
     
            }
            else if (firmware_version.Text == "255")
            {
                update_button.Enabled = false;//“升级固件”可按
                reset_button.Enabled = false;//“复位终端”可按           
          
            }
            else
            {
                update_button.Enabled = true;//“升级固件”可按
                reset_button.Enabled = true;//“复位终端”可按             
            }
        }

        private void update_button_Click(object sender, EventArgs e)//固件升级窗口
        {
            Form2 frm = new Form2(this);//首先实例化
            frm.ShowDialog();
        }
        public static byte ByteToBCD(byte b)//byte转BCD
        {           
            byte b1 = (byte)(b / 10);//高四位         
            byte b2 = (byte)(b % 10);//低四位  

            return (byte)((b1 << 4) | b2);
        }

        private void query_mode_button_Click(object sender, EventArgs e)//查询终端
        {
            byte[] SendBytes = new byte[8];

            try
            {
                SendBytes[0] = 0x0D;
                SendBytes[1] = 0xFE;//查询
                SendBytes[2] = 0x00;
                SendBytes[3] = 0X00;
                SendBytes[4] = 0X00;
                SendBytes[5] = 0X00;
                SendBytes[6] = 0x00;
                SendBytes[7] = 0x0D;
                controller.SendDataToCom(SendBytes);
            }
            catch
            {
                MessageBox.Show("串口通讯错误","错误");
            }
        }

        private void reset_button_Click(object sender, EventArgs e)//复位终端
        {
            Form3 frm = new Form3(this);//首先实例化
            frm.ShowDialog();
        }

        public void ResetMessages(byte e)
        {
            if (this.InvokeRequired)//1.代理
            {
                this.Invoke(new MethodInvoker(delegate
                {
                    firmware_version.Text = "";
                }));
            }
            else//2.正常
            {
                firmware_version.Text = "";
            }
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (sendtbx.Text != "")
            {
                if (radioButton2.Checked == true)//切换到十六进制
                {
                    byte[] array = System.Text.Encoding.ASCII.GetBytes(sendtbx.Text);
                    string ASCIIstr2 = null;
                    for(int i = 0; i < array.Length; i++)
                    {
                        int asciicode = (int)(array[i]);
                        ASCIIstr2 += (Convert.ToString(asciicode, 16).ToUpper().PadLeft(2,'0') + " ");
                    }
                    sendtbx.Text = ASCIIstr2;
                }
                else
                {
                    byte[] CharBytes = null;
                    List<string> CharDataList = new List<string>();//字符两两存一个
                    //剔除所有空格
                    string charstring = sendtbx.Text.Replace(" ", "").Replace("\r", "");
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
                    sendtbx.Text = System.Text.Encoding.ASCII.GetString(CharBytes);
                }
            }
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }


        private void out_1_0_button_Click(object sender, EventArgs e)
        {
            byte[] SendBytes = new byte[8];

            try
            {
                SendBytes[0] = 0x0D;
                SendBytes[1] = 0x01;        //Q1.0
                SendBytes[2] = 0x00;
                SendBytes[3] = 0x00;
                SendBytes[4] = 0x00;
                SendBytes[5] = 0x00;
                SendBytes[6] = 0x00;
                SendBytes[7] = 0x0D;
                Form1.serialPort1.Write(SendBytes, 0, SendBytes.Length);
            }
            catch
            {
                MessageBox.Show("串口通讯错误", "错误");
            }
        }

        private void out_1_1_button_Click(object sender, EventArgs e)
        {
            byte[] SendBytes = new byte[8];

            try
            {
                SendBytes[0] = 0x0D;
                SendBytes[1] = 0x02;        //Q1.1
                SendBytes[2] = 0x00;
                SendBytes[3] = 0x00;
                SendBytes[4] = 0x00;
                SendBytes[5] = 0x00;
                SendBytes[6] = 0x00;
                SendBytes[7] = 0x0D;
                Form1.serialPort1.Write(SendBytes, 0, SendBytes.Length);
            }
            catch
            {
                MessageBox.Show("串口通讯错误", "错误");
            }
        }

        private void out_1_2_button_Click(object sender, EventArgs e)
        {
            byte[] SendBytes = new byte[8];

            try
            {
                SendBytes[0] = 0x0D;
                SendBytes[1] = 0x03;        //Q1.2
                SendBytes[2] = 0x00;
                SendBytes[3] = 0x00;
                SendBytes[4] = 0x00;
                SendBytes[5] = 0x00;
                SendBytes[6] = 0x00;
                SendBytes[7] = 0x0D;
                Form1.serialPort1.Write(SendBytes, 0, SendBytes.Length);
            }
            catch
            {
                MessageBox.Show("串口通讯错误", "错误");
            }
        }

        private void out_1_3_button_Click(object sender, EventArgs e)
        {
            byte[] SendBytes = new byte[8];

            try
            {
                SendBytes[0] = 0x0D;
                SendBytes[1] = 0x04;        //Q1.3
                SendBytes[2] = 0x00;
                SendBytes[3] = 0x00;
                SendBytes[4] = 0x00;
                SendBytes[5] = 0x00;
                SendBytes[6] = 0x00;
                SendBytes[7] = 0x0D;
                Form1.serialPort1.Write(SendBytes, 0, SendBytes.Length);
            }
            catch
            {
                MessageBox.Show("串口通讯错误", "错误");
            }
        }

        private void groupBox5_Enter(object sender, EventArgs e)
        {

        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void groupBox4_Enter(object sender, EventArgs e)
        {

        }

        private void firmware_version_TextChanged(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void groupControl1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void groupBox3_Enter(object sender, EventArgs e)
        {

        }

        private void sendtbx_TextChanged(object sender, EventArgs e)
        {

        }

        private void sidePanel3_Click(object sender, EventArgs e)
        {

        }

        private void firmware_version_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void tabPane1_Click(object sender, EventArgs e)
        {

        }

        private void textEdit4_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void tabNavigationPage1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void receivetbx_TextChanged(object sender, EventArgs e)
        {
            receivetbx.SelectionStart = receivetbx.Text.Length;
            receivetbx.ScrollToCaret();
        }

        private void tabNavigationPage2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {

        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            Form4 frm = new Form4(this);//首先实例化
            frm.ShowDialog();
        }
       

        private void sidePanel7_Click(object sender, EventArgs e)
        {

        }

        private void comListCbx_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void dataBitsCbx_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void stopBitsCbx_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void handshakingcbx_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

    }
}
