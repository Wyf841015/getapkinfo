namespace exitiptv
{
    partial class AdvForm
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
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.APKpathtxb = new System.Windows.Forms.TextBox();
            this.getAPKBtn = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.fontDialog1 = new System.Windows.Forms.FontDialog();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(532, 281);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.button1);
            this.tabPage1.Controls.Add(this.getAPKBtn);
            this.tabPage1.Controls.Add(this.APKpathtxb);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(524, 255);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "安装应用";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(524, 255);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "获取脚本日志";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // APKpathtxb
            // 
            this.APKpathtxb.Location = new System.Drawing.Point(62, 37);
            this.APKpathtxb.Name = "APKpathtxb";
            this.APKpathtxb.Size = new System.Drawing.Size(227, 21);
            this.APKpathtxb.TabIndex = 0;
            // 
            // getAPKBtn
            // 
            this.getAPKBtn.Location = new System.Drawing.Point(319, 37);
            this.getAPKBtn.Name = "getAPKBtn";
            this.getAPKBtn.Size = new System.Drawing.Size(119, 23);
            this.getAPKBtn.TabIndex = 1;
            this.getAPKBtn.Text = "浏览apk文件";
            this.getAPKBtn.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(62, 103);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(376, 23);
            this.button1.TabIndex = 2;
            this.button1.Text = "安装APK";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // AdvForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(532, 281);
            this.Controls.Add(this.tabControl1);
            this.Name = "AdvForm";
            this.Text = "AdvForm";
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button getAPKBtn;
        private System.Windows.Forms.TextBox APKpathtxb;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.FontDialog fontDialog1;
    }
}