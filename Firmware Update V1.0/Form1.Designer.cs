namespace Firmware_Update_V1._0
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.button1 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.comListCbx = new System.Windows.Forms.ComboBox();
            this.baudRateCbx = new System.Windows.Forms.ComboBox();
            this.parityCbx = new System.Windows.Forms.ComboBox();
            this.dataBitsCbx = new System.Windows.Forms.ComboBox();
            this.stopBitsCbx = new System.Windows.Forms.ComboBox();
            this.openCloseSpbtn = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.radioButton1 = new System.Windows.Forms.RadioButton();
            this.radioButton2 = new System.Windows.Forms.RadioButton();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.radioButton4 = new System.Windows.Forms.RadioButton();
            this.radioButton3 = new System.Windows.Forms.RadioButton();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.button5 = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.textBox4 = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.checkBox2 = new System.Windows.Forms.CheckBox();
            this.query_mode_button = new DevExpress.XtraEditors.SimpleButton();
            this.update_button = new DevExpress.XtraEditors.SimpleButton();
            this.reset_button = new DevExpress.XtraEditors.SimpleButton();
            this.sidePanel1 = new DevExpress.XtraEditors.SidePanel();
            this.sidePanel4 = new DevExpress.XtraEditors.SidePanel();
            this.sidePanel2 = new DevExpress.XtraEditors.SidePanel();
            this.firmware_version = new System.Windows.Forms.TextBox();
            this.sendtbx = new System.Windows.Forms.TextBox();
            this.receivetbx = new System.Windows.Forms.TextBox();
            this.handshakingcbx = new System.Windows.Forms.ComboBox();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.sidePanel1.SuspendLayout();
            this.sidePanel4.SuspendLayout();
            this.sidePanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(23, 64);
            this.button1.Margin = new System.Windows.Forms.Padding(2);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(105, 20);
            this.button1.TabIndex = 0;
            this.button1.Text = "扫描端口";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(4, 18);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "端口号：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(197, 75);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "波特率：";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(233, 143);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 12);
            this.label3.TabIndex = 3;
            this.label3.Text = "校验位：";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(297, 192);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 12);
            this.label4.TabIndex = 4;
            this.label4.Text = "数据位：";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(405, 230);
            this.label5.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(53, 12);
            this.label5.TabIndex = 5;
            this.label5.Text = "停止位：";
            // 
            // comListCbx
            // 
            this.comListCbx.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comListCbx.FormattingEnabled = true;
            this.comListCbx.Location = new System.Drawing.Point(23, 36);
            this.comListCbx.Margin = new System.Windows.Forms.Padding(2);
            this.comListCbx.Name = "comListCbx";
            this.comListCbx.Size = new System.Drawing.Size(105, 20);
            this.comListCbx.TabIndex = 6;
            this.comListCbx.SelectedIndexChanged += new System.EventHandler(this.comListCbx_SelectedIndexChanged);
            // 
            // baudRateCbx
            // 
            this.baudRateCbx.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.baudRateCbx.FormattingEnabled = true;
            this.baudRateCbx.Items.AddRange(new object[] {
            "4800",
            "9600",
            "19200",
            "115200"});
            this.baudRateCbx.Location = new System.Drawing.Point(251, 72);
            this.baudRateCbx.Margin = new System.Windows.Forms.Padding(2);
            this.baudRateCbx.Name = "baudRateCbx";
            this.baudRateCbx.Size = new System.Drawing.Size(88, 20);
            this.baudRateCbx.TabIndex = 7;
            // 
            // parityCbx
            // 
            this.parityCbx.AutoCompleteCustomSource.AddRange(new string[] {
            "None",
            "Even",
            "Mark"});
            this.parityCbx.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.parityCbx.FormattingEnabled = true;
            this.parityCbx.Items.AddRange(new object[] {
            "None",
            "Even",
            "Mark",
            "Odd",
            "Space"});
            this.parityCbx.Location = new System.Drawing.Point(287, 141);
            this.parityCbx.Margin = new System.Windows.Forms.Padding(2);
            this.parityCbx.Name = "parityCbx";
            this.parityCbx.Size = new System.Drawing.Size(88, 20);
            this.parityCbx.TabIndex = 8;
            // 
            // dataBitsCbx
            // 
            this.dataBitsCbx.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.dataBitsCbx.FormattingEnabled = true;
            this.dataBitsCbx.Items.AddRange(new object[] {
            "5",
            "6",
            "7",
            "8"});
            this.dataBitsCbx.Location = new System.Drawing.Point(351, 190);
            this.dataBitsCbx.Margin = new System.Windows.Forms.Padding(2);
            this.dataBitsCbx.Name = "dataBitsCbx";
            this.dataBitsCbx.Size = new System.Drawing.Size(88, 20);
            this.dataBitsCbx.TabIndex = 9;
            this.dataBitsCbx.SelectedIndexChanged += new System.EventHandler(this.dataBitsCbx_SelectedIndexChanged);
            // 
            // stopBitsCbx
            // 
            this.stopBitsCbx.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.stopBitsCbx.FormattingEnabled = true;
            this.stopBitsCbx.Items.AddRange(new object[] {
            "One",
            "OnePointFive",
            "Two"});
            this.stopBitsCbx.Location = new System.Drawing.Point(459, 228);
            this.stopBitsCbx.Margin = new System.Windows.Forms.Padding(2);
            this.stopBitsCbx.Name = "stopBitsCbx";
            this.stopBitsCbx.Size = new System.Drawing.Size(88, 20);
            this.stopBitsCbx.TabIndex = 10;
            this.stopBitsCbx.SelectedIndexChanged += new System.EventHandler(this.stopBitsCbx_SelectedIndexChanged);
            // 
            // openCloseSpbtn
            // 
            this.openCloseSpbtn.Location = new System.Drawing.Point(23, 92);
            this.openCloseSpbtn.Margin = new System.Windows.Forms.Padding(2);
            this.openCloseSpbtn.Name = "openCloseSpbtn";
            this.openCloseSpbtn.Size = new System.Drawing.Size(105, 22);
            this.openCloseSpbtn.TabIndex = 11;
            this.openCloseSpbtn.Text = "开始连接";
            this.openCloseSpbtn.UseVisualStyleBackColor = true;
            this.openCloseSpbtn.Click += new System.EventHandler(this.openCloseSpbtn_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(102, 12);
            this.button3.Margin = new System.Windows.Forms.Padding(2);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(44, 60);
            this.button3.TabIndex = 14;
            this.button3.Text = "数据自测";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.sendbtn_Click);
            // 
            // timer1
            // 
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // radioButton1
            // 
            this.radioButton1.AutoSize = true;
            this.radioButton1.Checked = true;
            this.radioButton1.Location = new System.Drawing.Point(2, 2);
            this.radioButton1.Margin = new System.Windows.Forms.Padding(2);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.Size = new System.Drawing.Size(83, 16);
            this.radioButton1.TabIndex = 15;
            this.radioButton1.TabStop = true;
            this.radioButton1.Text = "字符型发送";
            this.radioButton1.UseVisualStyleBackColor = true;
            this.radioButton1.CheckedChanged += new System.EventHandler(this.radioButton1_CheckedChanged);
            // 
            // radioButton2
            // 
            this.radioButton2.AutoSize = true;
            this.radioButton2.Location = new System.Drawing.Point(2, 21);
            this.radioButton2.Margin = new System.Windows.Forms.Padding(2);
            this.radioButton2.Name = "radioButton2";
            this.radioButton2.Size = new System.Drawing.Size(95, 16);
            this.radioButton2.TabIndex = 16;
            this.radioButton2.Text = "十六进制发送";
            this.radioButton2.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.radioButton1);
            this.panel1.Controls.Add(this.radioButton2);
            this.panel1.Location = new System.Drawing.Point(7, 19);
            this.panel1.Margin = new System.Windows.Forms.Padding(2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(96, 39);
            this.panel1.TabIndex = 17;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.radioButton4);
            this.panel2.Controls.Add(this.radioButton3);
            this.panel2.Location = new System.Drawing.Point(4, 19);
            this.panel2.Margin = new System.Windows.Forms.Padding(2);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(98, 38);
            this.panel2.TabIndex = 18;
            // 
            // radioButton4
            // 
            this.radioButton4.AutoSize = true;
            this.radioButton4.Location = new System.Drawing.Point(3, 21);
            this.radioButton4.Margin = new System.Windows.Forms.Padding(2);
            this.radioButton4.Name = "radioButton4";
            this.radioButton4.Size = new System.Drawing.Size(95, 16);
            this.radioButton4.TabIndex = 1;
            this.radioButton4.Text = "十六进制接收";
            this.radioButton4.UseVisualStyleBackColor = true;
            this.radioButton4.CheckedChanged += new System.EventHandler(this.radioButton4_CheckedChanged);
            // 
            // radioButton3
            // 
            this.radioButton3.AutoSize = true;
            this.radioButton3.Checked = true;
            this.radioButton3.Location = new System.Drawing.Point(3, 2);
            this.radioButton3.Margin = new System.Windows.Forms.Padding(2);
            this.radioButton3.Name = "radioButton3";
            this.radioButton3.Size = new System.Drawing.Size(83, 16);
            this.radioButton3.TabIndex = 0;
            this.radioButton3.TabStop = true;
            this.radioButton3.Text = "字符型接收";
            this.radioButton3.UseVisualStyleBackColor = true;
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(8, 60);
            this.checkBox1.Margin = new System.Windows.Forms.Padding(2);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(84, 16);
            this.checkBox1.TabIndex = 19;
            this.checkBox1.Text = "显示时间戳";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.comListCbx);
            this.groupBox1.Controls.Add(this.button1);
            this.groupBox1.Controls.Add(this.openCloseSpbtn);
            this.groupBox1.Location = new System.Drawing.Point(11, 11);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox1.Size = new System.Drawing.Size(150, 126);
            this.groupBox1.TabIndex = 22;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "串口配置";
            this.groupBox1.Enter += new System.EventHandler(this.groupBox1_Enter);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.button5);
            this.groupBox2.Controls.Add(this.panel2);
            this.groupBox2.Controls.Add(this.checkBox1);
            this.groupBox2.Location = new System.Drawing.Point(11, 173);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox2.Size = new System.Drawing.Size(150, 80);
            this.groupBox2.TabIndex = 23;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "接收区";
            this.groupBox2.Enter += new System.EventHandler(this.groupBox2_Enter);
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(102, 19);
            this.button5.Margin = new System.Windows.Forms.Padding(2);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(44, 56);
            this.button5.TabIndex = 28;
            this.button5.Text = "清空接收";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.textBox4);
            this.groupBox3.Controls.Add(this.panel1);
            this.groupBox3.Controls.Add(this.label7);
            this.groupBox3.Controls.Add(this.button3);
            this.groupBox3.Controls.Add(this.checkBox2);
            this.groupBox3.Location = new System.Drawing.Point(11, 260);
            this.groupBox3.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox3.Size = new System.Drawing.Size(150, 102);
            this.groupBox3.TabIndex = 24;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "自测区";
            this.groupBox3.Enter += new System.EventHandler(this.groupBox3_Enter);
            // 
            // textBox4
            // 
            this.textBox4.Location = new System.Drawing.Point(80, 77);
            this.textBox4.Margin = new System.Windows.Forms.Padding(2);
            this.textBox4.Name = "textBox4";
            this.textBox4.Size = new System.Drawing.Size(66, 21);
            this.textBox4.TabIndex = 30;
            this.textBox4.Text = "1";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(7, 82);
            this.label7.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(71, 12);
            this.label7.TabIndex = 29;
            this.label7.Text = "发送周期(s)";
            // 
            // checkBox2
            // 
            this.checkBox2.AutoSize = true;
            this.checkBox2.Location = new System.Drawing.Point(9, 62);
            this.checkBox2.Margin = new System.Windows.Forms.Padding(2);
            this.checkBox2.Name = "checkBox2";
            this.checkBox2.Size = new System.Drawing.Size(72, 16);
            this.checkBox2.TabIndex = 28;
            this.checkBox2.Text = "自动发送";
            this.checkBox2.UseVisualStyleBackColor = true;
            // 
            // query_mode_button
            // 
            this.query_mode_button.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("query_mode_button.ImageOptions.Image")));
            this.query_mode_button.Location = new System.Drawing.Point(299, 14);
            this.query_mode_button.Name = "query_mode_button";
            this.query_mode_button.Size = new System.Drawing.Size(141, 43);
            this.query_mode_button.TabIndex = 2;
            this.query_mode_button.Text = "模式查询";
            this.query_mode_button.Click += new System.EventHandler(this.query_mode_button_Click);
            // 
            // update_button
            // 
            this.update_button.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("update_button.ImageOptions.Image")));
            this.update_button.Location = new System.Drawing.Point(153, 14);
            this.update_button.Name = "update_button";
            this.update_button.Size = new System.Drawing.Size(128, 43);
            this.update_button.TabIndex = 1;
            this.update_button.Text = "固件升级";
            this.update_button.Click += new System.EventHandler(this.update_button_Click);
            // 
            // reset_button
            // 
            this.reset_button.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("reset_button.ImageOptions.Image")));
            this.reset_button.Location = new System.Drawing.Point(12, 14);
            this.reset_button.Name = "reset_button";
            this.reset_button.Size = new System.Drawing.Size(127, 43);
            this.reset_button.TabIndex = 0;
            this.reset_button.Text = "开始测试";
            this.reset_button.Click += new System.EventHandler(this.reset_button_Click);
            // 
            // sidePanel1
            // 
            this.sidePanel1.Controls.Add(this.sidePanel4);
            this.sidePanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.sidePanel1.Location = new System.Drawing.Point(0, 0);
            this.sidePanel1.Name = "sidePanel1";
            this.sidePanel1.Size = new System.Drawing.Size(610, 444);
            this.sidePanel1.TabIndex = 39;
            this.sidePanel1.Text = "sidePanel1";
            // 
            // sidePanel4
            // 
            this.sidePanel4.Controls.Add(this.sidePanel2);
            this.sidePanel4.Controls.Add(this.sendtbx);
            this.sidePanel4.Controls.Add(this.groupBox1);
            this.sidePanel4.Controls.Add(this.receivetbx);
            this.sidePanel4.Controls.Add(this.label5);
            this.sidePanel4.Controls.Add(this.dataBitsCbx);
            this.sidePanel4.Controls.Add(this.stopBitsCbx);
            this.sidePanel4.Controls.Add(this.parityCbx);
            this.sidePanel4.Controls.Add(this.baudRateCbx);
            this.sidePanel4.Controls.Add(this.label4);
            this.sidePanel4.Controls.Add(this.groupBox3);
            this.sidePanel4.Controls.Add(this.groupBox2);
            this.sidePanel4.Controls.Add(this.label2);
            this.sidePanel4.Controls.Add(this.label3);
            this.sidePanel4.Controls.Add(this.handshakingcbx);
            this.sidePanel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.sidePanel4.Location = new System.Drawing.Point(0, 0);
            this.sidePanel4.Name = "sidePanel4";
            this.sidePanel4.Size = new System.Drawing.Size(610, 444);
            this.sidePanel4.TabIndex = 26;
            this.sidePanel4.Text = "sidePanel4";
            // 
            // sidePanel2
            // 
            this.sidePanel2.Controls.Add(this.firmware_version);
            this.sidePanel2.Controls.Add(this.reset_button);
            this.sidePanel2.Controls.Add(this.update_button);
            this.sidePanel2.Controls.Add(this.query_mode_button);
            this.sidePanel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.sidePanel2.Location = new System.Drawing.Point(0, 375);
            this.sidePanel2.Name = "sidePanel2";
            this.sidePanel2.Size = new System.Drawing.Size(610, 69);
            this.sidePanel2.TabIndex = 25;
            this.sidePanel2.Text = "sidePanel2";
            // 
            // firmware_version
            // 
            this.firmware_version.Location = new System.Drawing.Point(449, 14);
            this.firmware_version.Margin = new System.Windows.Forms.Padding(2);
            this.firmware_version.Multiline = true;
            this.firmware_version.Name = "firmware_version";
            this.firmware_version.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.firmware_version.Size = new System.Drawing.Size(150, 43);
            this.firmware_version.TabIndex = 26;
            this.firmware_version.Text = "固件版本：\r\n";
            this.firmware_version.TextChanged += new System.EventHandler(this.firmware_version_TextChanged);
            // 
            // sendtbx
            // 
            this.sendtbx.Location = new System.Drawing.Point(178, 266);
            this.sendtbx.Margin = new System.Windows.Forms.Padding(2);
            this.sendtbx.Multiline = true;
            this.sendtbx.Name = "sendtbx";
            this.sendtbx.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.sendtbx.Size = new System.Drawing.Size(427, 104);
            this.sendtbx.TabIndex = 21;
            this.sendtbx.TextChanged += new System.EventHandler(this.sendtbx_TextChanged);
            this.sendtbx.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.sendtbx_KeyPress);
            // 
            // receivetbx
            // 
            this.receivetbx.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.receivetbx.Location = new System.Drawing.Point(178, 2);
            this.receivetbx.Margin = new System.Windows.Forms.Padding(2);
            this.receivetbx.Multiline = true;
            this.receivetbx.Name = "receivetbx";
            this.receivetbx.ReadOnly = true;
            this.receivetbx.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.receivetbx.Size = new System.Drawing.Size(427, 256);
            this.receivetbx.TabIndex = 20;
            this.receivetbx.TextChanged += new System.EventHandler(this.receivetbx_TextChanged);
            // 
            // handshakingcbx
            // 
            this.handshakingcbx.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.handshakingcbx.FormattingEnabled = true;
            this.handshakingcbx.Items.AddRange(new object[] {
            "None",
            "XOnXOff",
            "RequestToSend",
            "RequestToSendXOnXOff"});
            this.handshakingcbx.Location = new System.Drawing.Point(209, 222);
            this.handshakingcbx.Margin = new System.Windows.Forms.Padding(2);
            this.handshakingcbx.Name = "handshakingcbx";
            this.handshakingcbx.Size = new System.Drawing.Size(88, 20);
            this.handshakingcbx.TabIndex = 29;
            this.handshakingcbx.SelectedIndexChanged += new System.EventHandler(this.handshakingcbx_SelectedIndexChanged);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(610, 444);
            this.Controls.Add(this.sidePanel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(2);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "TN19002单逆测试台客户端";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.sidePanel1.ResumeLayout(false);
            this.sidePanel4.ResumeLayout(false);
            this.sidePanel4.PerformLayout();
            this.sidePanel2.ResumeLayout(false);
            this.sidePanel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button button1;
        //private System.IO.Ports.SerialPort serialPort1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox comListCbx;
        private System.Windows.Forms.ComboBox baudRateCbx;
        private System.Windows.Forms.ComboBox parityCbx;
        private System.Windows.Forms.ComboBox dataBitsCbx;
        private System.Windows.Forms.ComboBox stopBitsCbx;
        private System.Windows.Forms.Button openCloseSpbtn;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.RadioButton radioButton1;
        private System.Windows.Forms.RadioButton radioButton2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.RadioButton radioButton4;
        private System.Windows.Forms.RadioButton radioButton3;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.TextBox textBox4;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.CheckBox checkBox2;
        private DevExpress.XtraEditors.SidePanel sidePanel1;
        private DevExpress.XtraEditors.SidePanel sidePanel4;
        private DevExpress.XtraEditors.SimpleButton query_mode_button;
        private DevExpress.XtraEditors.SimpleButton update_button;
        private DevExpress.XtraEditors.SimpleButton reset_button;
        private System.Windows.Forms.TextBox sendtbx;
        private System.Windows.Forms.TextBox receivetbx;
        private DevExpress.XtraEditors.SidePanel sidePanel2;
        private System.Windows.Forms.TextBox firmware_version;
        private System.Windows.Forms.ComboBox handshakingcbx;
    }
}

