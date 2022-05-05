namespace WinForms
{
    partial class Normal
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.run = new System.Windows.Forms.Button();
            this.testCasesCombo = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.lstatus = new System.Windows.Forms.Label();
            this.ltime = new System.Windows.Forms.Label();
            this.lsummary = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // run
            // 
            this.run.Enabled = false;
            this.run.Location = new System.Drawing.Point(297, 26);
            this.run.Name = "run";
            this.run.Size = new System.Drawing.Size(75, 25);
            this.run.TabIndex = 0;
            this.run.Text = "Run";
            this.run.UseVisualStyleBackColor = true;
            this.run.Click += new System.EventHandler(this.run_Click);
            // 
            // testCasesCombo
            // 
            this.testCasesCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.testCasesCombo.FormattingEnabled = true;
            this.testCasesCombo.Items.AddRange(new object[] {
            "0",
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "7",
            "8",
            "9",
            "10"});
            this.testCasesCombo.Location = new System.Drawing.Point(12, 26);
            this.testCasesCombo.Name = "testCasesCombo";
            this.testCasesCombo.Size = new System.Drawing.Size(121, 23);
            this.testCasesCombo.TabIndex = 1;
            this.testCasesCombo.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(89, 15);
            this.label1.TabIndex = 2;
            this.label1.Text = "Select Test Case";
            // 
            // lstatus
            // 
            this.lstatus.AutoSize = true;
            this.lstatus.Location = new System.Drawing.Point(12, 70);
            this.lstatus.Name = "lstatus";
            this.lstatus.Size = new System.Drawing.Size(42, 15);
            this.lstatus.TabIndex = 3;
            this.lstatus.Text = "Status:";
            // 
            // ltime
            // 
            this.ltime.AutoSize = true;
            this.ltime.Location = new System.Drawing.Point(12, 52);
            this.ltime.Name = "ltime";
            this.ltime.Size = new System.Drawing.Size(36, 15);
            this.ltime.TabIndex = 4;
            this.ltime.Text = "Time:";
            // 
            // lsummary
            // 
            this.lsummary.AutoSize = true;
            this.lsummary.Location = new System.Drawing.Point(12, 93);
            this.lsummary.Name = "lsummary";
            this.lsummary.Size = new System.Drawing.Size(61, 15);
            this.lsummary.TabIndex = 5;
            this.lsummary.Text = "Summary:";
            // 
            // Normal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(384, 161);
            this.Controls.Add(this.lsummary);
            this.Controls.Add(this.ltime);
            this.Controls.Add(this.lstatus);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.testCasesCombo);
            this.Controls.Add(this.run);
            this.Name = "Normal";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Normal";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Button run;
        private ComboBox testCasesCombo;
        private Label label1;
        private Label lstatus;
        private Label ltime;
        private Label lsummary;
    }
}