namespace PitchRollYawViewer
{
    partial class AppMainForm
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
            components = new System.ComponentModel.Container();
            DrawTimer = new System.Windows.Forms.Timer(components);
            GL_Monitor = new OpenTK.GLControl();
            panel1 = new System.Windows.Forms.Panel();
            cmbPort = new System.Windows.Forms.ComboBox();
            label1 = new System.Windows.Forms.Label();
            lblPitch = new System.Windows.Forms.Label();
            lblRoll = new System.Windows.Forms.Label();
            lblYaw = new System.Windows.Forms.Label();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // DrawTimer
            // 
            DrawTimer.Enabled = true;
            DrawTimer.Interval = 25;
            DrawTimer.Tick += DrawTimer_Tick;
            // 
            // GL_Monitor
            // 
            GL_Monitor.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            GL_Monitor.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            GL_Monitor.Location = new System.Drawing.Point(0, 90);
            GL_Monitor.Margin = new System.Windows.Forms.Padding(8, 10, 8, 10);
            GL_Monitor.Name = "GL_Monitor";
            GL_Monitor.Size = new System.Drawing.Size(1698, 1254);
            GL_Monitor.TabIndex = 15;
            GL_Monitor.VSync = true;
            GL_Monitor.Load += GL_Monitor_Load;
            GL_Monitor.Paint += GL_Monitor_Paint;
            // 
            // panel1
            // 
            panel1.Controls.Add(cmbPort);
            panel1.Controls.Add(label1);
            panel1.Dock = System.Windows.Forms.DockStyle.Top;
            panel1.Location = new System.Drawing.Point(0, 0);
            panel1.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            panel1.Name = "panel1";
            panel1.Size = new System.Drawing.Size(1698, 74);
            panel1.TabIndex = 23;
            // 
            // cmbPort
            // 
            cmbPort.FormattingEnabled = true;
            cmbPort.Location = new System.Drawing.Point(203, 12);
            cmbPort.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            cmbPort.Name = "cmbPort";
            cmbPort.Size = new System.Drawing.Size(373, 40);
            cmbPort.TabIndex = 1;
            cmbPort.SelectedIndexChanged += cmbPort_SelectedIndexChanged;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new System.Drawing.Point(20, 18);
            label1.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(195, 32);
            label1.TabIndex = 0;
            label1.Text = "Select COM port:";
            // 
            // lblPitch
            // 
            lblPitch.AutoSize = true;
            lblPitch.Location = new System.Drawing.Point(14, 100);
            lblPitch.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            lblPitch.Name = "lblPitch";
            lblPitch.Size = new System.Drawing.Size(71, 32);
            lblPitch.TabIndex = 24;
            lblPitch.Text = "Pitch:";
            // 
            // lblRoll
            // 
            lblRoll.AutoSize = true;
            lblRoll.Location = new System.Drawing.Point(14, 150);
            lblRoll.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            lblRoll.Name = "lblRoll";
            lblRoll.Size = new System.Drawing.Size(58, 32);
            lblRoll.TabIndex = 25;
            lblRoll.Text = "Roll:";
            // 
            // lblYaw
            // 
            lblYaw.AutoSize = true;
            lblYaw.Location = new System.Drawing.Point(14, 201);
            lblYaw.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            lblYaw.Name = "lblYaw";
            lblYaw.Size = new System.Drawing.Size(59, 32);
            lblYaw.TabIndex = 26;
            lblYaw.Text = "Yaw:";
            // 
            // AppMainForm
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(13F, 32F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(1698, 1344);
            Controls.Add(lblYaw);
            Controls.Add(lblRoll);
            Controls.Add(lblPitch);
            Controls.Add(panel1);
            Controls.Add(GL_Monitor);
            Margin = new System.Windows.Forms.Padding(6, 8, 6, 8);
            Name = "AppMainForm";
            StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            Text = "Orientation viewer";
            Load += Form1_Load;
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private System.Windows.Forms.Timer DrawTimer;
        private OpenTK.GLControl GL_Monitor;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ComboBox cmbPort;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblPitch;
        private System.Windows.Forms.Label lblRoll;
        private System.Windows.Forms.Label lblYaw;
    }
}

