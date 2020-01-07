namespace Firmware_Update_V1._0
{
    partial class Form4
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form4));
            this.checkEdit1 = new DevExpress.XtraEditors.CheckEdit();
            this.checkEdit2 = new DevExpress.XtraEditors.CheckEdit();
            this.checkEdit3 = new DevExpress.XtraEditors.CheckEdit();
            this.checkEdit4 = new DevExpress.XtraEditors.CheckEdit();
            this.checkEdit5 = new DevExpress.XtraEditors.CheckEdit();
            this.checkEdit6 = new DevExpress.XtraEditors.CheckEdit();
            ((System.ComponentModel.ISupportInitialize)(this.checkEdit1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkEdit2.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkEdit3.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkEdit4.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkEdit5.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkEdit6.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // checkEdit1
            // 
            this.checkEdit1.Location = new System.Drawing.Point(12, 39);
            this.checkEdit1.Name = "checkEdit1";
            this.checkEdit1.Properties.Caption = "负载1.5kW";
            this.checkEdit1.Size = new System.Drawing.Size(89, 19);
            this.checkEdit1.TabIndex = 0;
            this.checkEdit1.CheckedChanged += new System.EventHandler(this.checkEdit1_CheckedChanged);
            // 
            // checkEdit2
            // 
            this.checkEdit2.Location = new System.Drawing.Point(12, 65);
            this.checkEdit2.Name = "checkEdit2";
            this.checkEdit2.Properties.Caption = "负载3kW";
            this.checkEdit2.Size = new System.Drawing.Size(75, 19);
            this.checkEdit2.TabIndex = 1;
            // 
            // checkEdit3
            // 
            this.checkEdit3.Location = new System.Drawing.Point(126, 39);
            this.checkEdit3.Name = "checkEdit3";
            this.checkEdit3.Properties.Caption = "输出过载";
            this.checkEdit3.Size = new System.Drawing.Size(75, 19);
            this.checkEdit3.TabIndex = 2;
            this.checkEdit3.CheckedChanged += new System.EventHandler(this.checkEdit3_CheckedChanged);
            // 
            // checkEdit4
            // 
            this.checkEdit4.Location = new System.Drawing.Point(126, 65);
            this.checkEdit4.Name = "checkEdit4";
            this.checkEdit4.Properties.Caption = "输出过流";
            this.checkEdit4.Size = new System.Drawing.Size(75, 19);
            this.checkEdit4.TabIndex = 3;
            // 
            // checkEdit5
            // 
            this.checkEdit5.Location = new System.Drawing.Point(225, 40);
            this.checkEdit5.Name = "checkEdit5";
            this.checkEdit5.Properties.Caption = "输出短路";
            this.checkEdit5.Size = new System.Drawing.Size(75, 19);
            this.checkEdit5.TabIndex = 4;
            // 
            // checkEdit6
            // 
            this.checkEdit6.Location = new System.Drawing.Point(225, 65);
            this.checkEdit6.Name = "checkEdit6";
            this.checkEdit6.Properties.Caption = "反接测试";
            this.checkEdit6.Size = new System.Drawing.Size(75, 19);
            this.checkEdit6.TabIndex = 5;
            // 
            // Form4
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(705, 358);
            this.Controls.Add(this.checkEdit6);
            this.Controls.Add(this.checkEdit5);
            this.Controls.Add(this.checkEdit4);
            this.Controls.Add(this.checkEdit3);
            this.Controls.Add(this.checkEdit2);
            this.Controls.Add(this.checkEdit1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(2);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form4";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "测试项设置";
            this.Load += new System.EventHandler(this.Form4_Load);
            ((System.ComponentModel.ISupportInitialize)(this.checkEdit1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkEdit2.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkEdit3.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkEdit4.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkEdit5.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkEdit6.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.CheckEdit checkEdit1;
        private DevExpress.XtraEditors.CheckEdit checkEdit2;
        private DevExpress.XtraEditors.CheckEdit checkEdit3;
        private DevExpress.XtraEditors.CheckEdit checkEdit4;
        private DevExpress.XtraEditors.CheckEdit checkEdit5;
        private DevExpress.XtraEditors.CheckEdit checkEdit6;

    }
}