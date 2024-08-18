namespace EAFC_24_Career_mode_save_extention
{
    partial class Form1
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
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            label1 = new Label();
            number_of_saves_trackbar = new TrackBar();
            save_button = new Button();
            groupBox1 = new GroupBox();
            panel1 = new Panel();
            log = new Label();
            pictureBox1 = new PictureBox();
            groupBox2 = new GroupBox();
            label7 = new Label();
            label8 = new Label();
            check_frequency_trackbar = new TrackBar();
            label4 = new Label();
            label3 = new Label();
            revert_button = new Button();
            buttonBrowse = new Button();
            textBoxFolderPath = new TextBox();
            resume_button = new Button();
            exit_button = new Button();
            clear_log = new Button();
            notifyIcon1 = new NotifyIcon(components);
            ((System.ComponentModel.ISupportInitialize)number_of_saves_trackbar).BeginInit();
            groupBox1.SuspendLayout();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)check_frequency_trackbar).BeginInit();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(6, 50);
            label1.Name = "label1";
            label1.Size = new Size(142, 15);
            label1.TabIndex = 0;
            label1.Text = "EAFC 24 Save file location";
            // 
            // number_of_saves_trackbar
            // 
            number_of_saves_trackbar.Location = new Point(112, 102);
            number_of_saves_trackbar.Maximum = 25;
            number_of_saves_trackbar.Minimum = 3;
            number_of_saves_trackbar.Name = "number_of_saves_trackbar";
            number_of_saves_trackbar.Size = new Size(196, 45);
            number_of_saves_trackbar.TabIndex = 1;
            number_of_saves_trackbar.Value = 3;
            number_of_saves_trackbar.Scroll += number_of_saves_trackbar_Scroll;
            // 
            // save_button
            // 
            save_button.Location = new Point(400, 464);
            save_button.Name = "save_button";
            save_button.Size = new Size(75, 23);
            save_button.TabIndex = 4;
            save_button.Text = "Save";
            save_button.UseVisualStyleBackColor = true;
            save_button.Click += save_button_Click;
            // 
            // groupBox1
            // 
            groupBox1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right;
            groupBox1.Controls.Add(panel1);
            groupBox1.Location = new Point(499, 12);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(656, 493);
            groupBox1.TabIndex = 5;
            groupBox1.TabStop = false;
            groupBox1.Text = "Save log";
            // 
            // panel1
            // 
            panel1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right;
            panel1.AutoScroll = true;
            panel1.Controls.Add(log);
            panel1.Location = new Point(6, 17);
            panel1.Name = "panel1";
            panel1.Size = new Size(644, 470);
            panel1.TabIndex = 0;
            // 
            // log
            // 
            log.AutoSize = true;
            log.Font = new Font("Consolas", 8.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            log.ForeColor = Color.Blue;
            log.Location = new Point(3, 2);
            log.Name = "log";
            log.Size = new Size(0, 13);
            log.TabIndex = 1;
            // 
            // pictureBox1
            // 
            pictureBox1.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            pictureBox1.BackgroundImage = (Image)resources.GetObject("pictureBox1.BackgroundImage");
            pictureBox1.BackgroundImageLayout = ImageLayout.Center;
            pictureBox1.InitialImage = (Image)resources.GetObject("pictureBox1.InitialImage");
            pictureBox1.Location = new Point(499, 674);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(178, 151);
            pictureBox1.TabIndex = 6;
            pictureBox1.TabStop = false;
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(label7);
            groupBox2.Controls.Add(label8);
            groupBox2.Controls.Add(check_frequency_trackbar);
            groupBox2.Controls.Add(label4);
            groupBox2.Controls.Add(label3);
            groupBox2.Controls.Add(revert_button);
            groupBox2.Controls.Add(buttonBrowse);
            groupBox2.Controls.Add(textBoxFolderPath);
            groupBox2.Controls.Add(save_button);
            groupBox2.Controls.Add(number_of_saves_trackbar);
            groupBox2.Controls.Add(label1);
            groupBox2.Location = new Point(12, 12);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(481, 493);
            groupBox2.TabIndex = 6;
            groupBox2.TabStop = false;
            groupBox2.Text = "Configuration";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(381, 153);
            label7.Name = "label7";
            label7.Size = new Size(13, 15);
            label7.TabIndex = 12;
            label7.Text = "0";
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(10, 153);
            label8.Name = "label8";
            label8.Size = new Size(157, 15);
            label8.TabIndex = 11;
            label8.Text = "Check frequency per minute";
            // 
            // check_frequency_trackbar
            // 
            check_frequency_trackbar.Location = new Point(179, 153);
            check_frequency_trackbar.Maximum = 60;
            check_frequency_trackbar.Minimum = 2;
            check_frequency_trackbar.Name = "check_frequency_trackbar";
            check_frequency_trackbar.Size = new Size(196, 45);
            check_frequency_trackbar.SmallChange = 2;
            check_frequency_trackbar.TabIndex = 10;
            check_frequency_trackbar.Value = 2;
            check_frequency_trackbar.Scroll += check_frequency_trackbar_Scroll;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(314, 102);
            label4.Name = "label4";
            label4.Size = new Size(13, 15);
            label4.TabIndex = 9;
            label4.Text = "0";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(10, 102);
            label3.Name = "label3";
            label3.Size = new Size(96, 15);
            label3.TabIndex = 8;
            label3.Text = "Number of saves";
            // 
            // revert_button
            // 
            revert_button.Location = new Point(290, 464);
            revert_button.Name = "revert_button";
            revert_button.Size = new Size(104, 23);
            revert_button.TabIndex = 7;
            revert_button.Text = "Revert to default";
            revert_button.UseVisualStyleBackColor = true;
            revert_button.Click += revert_button_Click;
            // 
            // buttonBrowse
            // 
            buttonBrowse.Location = new Point(441, 47);
            buttonBrowse.Name = "buttonBrowse";
            buttonBrowse.Size = new Size(34, 26);
            buttonBrowse.TabIndex = 6;
            buttonBrowse.Text = "...";
            buttonBrowse.UseVisualStyleBackColor = true;
            buttonBrowse.Click += button2_Click;
            // 
            // textBoxFolderPath
            // 
            textBoxFolderPath.Enabled = false;
            textBoxFolderPath.Location = new Point(154, 47);
            textBoxFolderPath.Name = "textBoxFolderPath";
            textBoxFolderPath.Size = new Size(281, 23);
            textBoxFolderPath.TabIndex = 5;
            // 
            // resume_button
            // 
            resume_button.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            resume_button.Location = new Point(1080, 802);
            resume_button.Name = "resume_button";
            resume_button.Size = new Size(75, 23);
            resume_button.TabIndex = 10;
            resume_button.Text = "Resume";
            resume_button.UseVisualStyleBackColor = true;
            resume_button.Click += resume_button_Click;
            // 
            // exit_button
            // 
            exit_button.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            exit_button.Location = new Point(999, 802);
            exit_button.Name = "exit_button";
            exit_button.Size = new Size(75, 23);
            exit_button.TabIndex = 11;
            exit_button.Text = "Exit";
            exit_button.UseVisualStyleBackColor = true;
            exit_button.Click += exit_button_Click;
            // 
            // clear_log
            // 
            clear_log.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            clear_log.Location = new Point(1074, 511);
            clear_log.Name = "clear_log";
            clear_log.Size = new Size(75, 23);
            clear_log.TabIndex = 12;
            clear_log.Text = "Clear Log";
            clear_log.UseVisualStyleBackColor = true;
            clear_log.Click += clear_log_Click;
            // 
            // notifyIcon1
            // 
            notifyIcon1.BalloonTipText = "FC24 Save Tool";
            notifyIcon1.Icon = (Icon)resources.GetObject("notifyIcon1.Icon");
            notifyIcon1.Text = "FC24 Save Tool";
            notifyIcon1.Visible = true;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.Window;
            ClientSize = new Size(1174, 837);
            Controls.Add(clear_log);
            Controls.Add(exit_button);
            Controls.Add(resume_button);
            Controls.Add(groupBox2);
            Controls.Add(pictureBox1);
            Controls.Add(groupBox1);
            Icon = (Icon)resources.GetObject("$this.Icon");
            MinimumSize = new Size(1190, 876);
            Name = "Form1";
            Text = "FC24 Local Save extender and backup tool";
            FormClosing += Form1_FormClosing;
            FormClosed += Form1_FormClosed;
            Load += Form1_Load;
            ((System.ComponentModel.ISupportInitialize)number_of_saves_trackbar).EndInit();
            groupBox1.ResumeLayout(false);
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            groupBox2.ResumeLayout(false);
            groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)check_frequency_trackbar).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Label label1;
        private TrackBar number_of_saves_trackbar;
        private Button save_button;
        private GroupBox groupBox1;
        private PictureBox pictureBox1;
        private GroupBox groupBox2;
        private Label log;
        private Button buttonBrowse;
        private TextBox textBoxFolderPath;
        private Label label4;
        private Label label3;
        private Button revert_button;
        private Button resume_button;
        private Button exit_button;
        private Label label7;
        private Label label8;
        private TrackBar check_frequency_trackbar;
        private Panel panel1;
        private Button clear_log;
        private NotifyIcon notifyIcon1;
    }
}
