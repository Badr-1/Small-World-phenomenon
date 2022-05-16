namespace SmallWorld
{
    partial class GUI
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
            this.components = new System.ComponentModel.Container();
            this.Run = new System.Windows.Forms.Button();
            this.TestCase = new System.Windows.Forms.ComboBox();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.time = new System.Windows.Forms.Label();
            this.query = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // Run
            // 
            this.Run.Location = new System.Drawing.Point(264, 12);
            this.Run.Name = "Run";
            this.Run.Size = new System.Drawing.Size(75, 23);
            this.Run.TabIndex = 0;
            this.Run.Text = "Run";
            this.Run.UseVisualStyleBackColor = true;
            this.Run.Click += new System.EventHandler(this.Run_Click);
            // 
            // TestCase
            // 
            this.TestCase.DisplayMember = "1";
            this.TestCase.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.TestCase.FormattingEnabled = true;
            this.TestCase.Items.AddRange(new object[] {
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
            this.TestCase.Location = new System.Drawing.Point(12, 12);
            this.TestCase.Name = "TestCase";
            this.TestCase.Size = new System.Drawing.Size(121, 21);
            this.TestCase.TabIndex = 1;
            // 
            // timer1
            // 
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // time
            // 
            this.time.AutoSize = true;
            this.time.Location = new System.Drawing.Point(12, 40);
            this.time.Name = "time";
            this.time.Size = new System.Drawing.Size(13, 13);
            this.time.TabIndex = 2;
            this.time.Text = "0";
            // 
            // query
            // 
            this.query.AutoSize = true;
            this.query.Location = new System.Drawing.Point(109, 40);
            this.query.Name = "query";
            this.query.Size = new System.Drawing.Size(24, 13);
            this.query.TabIndex = 3;
            this.query.Text = "0/0";
            // 
            // GUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(351, 62);
            this.Controls.Add(this.query);
            this.Controls.Add(this.time);
            this.Controls.Add(this.TestCase);
            this.Controls.Add(this.Run);
            this.Name = "GUI";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "GUI";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button Run;
        private System.Windows.Forms.ComboBox TestCase;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Label time;
        private System.Windows.Forms.Label query;
    }
}