namespace TaikoSoundEditor.Controls
{
    partial class MusicOrderViewer
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.MusicOrdersPanel = new System.Windows.Forms.Panel();
            this.RightButton = new System.Windows.Forms.Button();
            this.ControlsPanel = new System.Windows.Forms.Panel();
            this.PasteButton = new System.Windows.Forms.Button();
            this.CutButton = new System.Windows.Forms.Button();
            this.PageLabel = new System.Windows.Forms.Label();
            this.LeftButton = new System.Windows.Forms.Button();
            this.ControlsPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // MusicOrdersPanel
            // 
            this.MusicOrdersPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MusicOrdersPanel.Location = new System.Drawing.Point(20, 32);
            this.MusicOrdersPanel.Name = "MusicOrdersPanel";
            this.MusicOrdersPanel.Size = new System.Drawing.Size(467, 265);
            this.MusicOrdersPanel.TabIndex = 0;
            this.MusicOrdersPanel.Paint += new System.Windows.Forms.PaintEventHandler(this.MusicOrdersPanel_Paint);
            this.MusicOrdersPanel.MouseDown += new System.Windows.Forms.MouseEventHandler(this.MusicOrdersPanel_MouseDown);
            this.MusicOrdersPanel.MouseLeave += new System.EventHandler(this.MusicOrdersPanel_MouseLeave);
            this.MusicOrdersPanel.MouseMove += new System.Windows.Forms.MouseEventHandler(this.MusicOrdersPanel_MouseMove);
            this.MusicOrdersPanel.Resize += new System.EventHandler(this.MusicOrdersPanel_Resize);
            // 
            // RightButton
            // 
            this.RightButton.Dock = System.Windows.Forms.DockStyle.Right;
            this.RightButton.Location = new System.Drawing.Point(487, 32);
            this.RightButton.Name = "RightButton";
            this.RightButton.Size = new System.Drawing.Size(20, 265);
            this.RightButton.TabIndex = 1;
            this.RightButton.Text = ">";
            this.RightButton.UseVisualStyleBackColor = true;
            this.RightButton.Click += new System.EventHandler(this.RightButton_Click);
            // 
            // ControlsPanel
            // 
            this.ControlsPanel.Controls.Add(this.PasteButton);
            this.ControlsPanel.Controls.Add(this.CutButton);
            this.ControlsPanel.Controls.Add(this.PageLabel);
            this.ControlsPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.ControlsPanel.Location = new System.Drawing.Point(0, 0);
            this.ControlsPanel.Name = "ControlsPanel";
            this.ControlsPanel.Size = new System.Drawing.Size(507, 32);
            this.ControlsPanel.TabIndex = 2;
            // 
            // PasteButton
            // 
            this.PasteButton.BackgroundImage = global::TaikoSoundEditor.Properties.Resources.ic_paste_gs;
            this.PasteButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.PasteButton.FlatAppearance.BorderSize = 0;
            this.PasteButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.PasteButton.Location = new System.Drawing.Point(33, 3);
            this.PasteButton.Name = "PasteButton";
            this.PasteButton.Size = new System.Drawing.Size(24, 24);
            this.PasteButton.TabIndex = 2;
            this.PasteButton.UseVisualStyleBackColor = true;
            this.PasteButton.Click += new System.EventHandler(this.PasteButton_Click);
            // 
            // CutButton
            // 
            this.CutButton.BackgroundImage = global::TaikoSoundEditor.Properties.Resources.ic_cut_gs;
            this.CutButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.CutButton.FlatAppearance.BorderSize = 0;
            this.CutButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.CutButton.Location = new System.Drawing.Point(3, 3);
            this.CutButton.Name = "CutButton";
            this.CutButton.Size = new System.Drawing.Size(24, 24);
            this.CutButton.TabIndex = 1;
            this.CutButton.UseVisualStyleBackColor = true;
            this.CutButton.Click += new System.EventHandler(this.CutButton_Click);
            // 
            // PageLabel
            // 
            this.PageLabel.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.PageLabel.AutoSize = true;
            this.PageLabel.Location = new System.Drawing.Point(217, 8);
            this.PageLabel.Name = "PageLabel";
            this.PageLabel.Size = new System.Drawing.Size(67, 15);
            this.PageLabel.TabIndex = 0;
            this.PageLabel.Text = "Page # of #";
            // 
            // LeftButton
            // 
            this.LeftButton.Dock = System.Windows.Forms.DockStyle.Left;
            this.LeftButton.Location = new System.Drawing.Point(0, 32);
            this.LeftButton.Name = "LeftButton";
            this.LeftButton.Size = new System.Drawing.Size(20, 265);
            this.LeftButton.TabIndex = 3;
            this.LeftButton.Text = "<";
            this.LeftButton.UseVisualStyleBackColor = true;
            this.LeftButton.Click += new System.EventHandler(this.LeftButton_Click);
            // 
            // MusicOrderViewer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.MusicOrdersPanel);
            this.Controls.Add(this.LeftButton);
            this.Controls.Add(this.RightButton);
            this.Controls.Add(this.ControlsPanel);
            this.Name = "MusicOrderViewer";
            this.Size = new System.Drawing.Size(507, 297);
            this.ControlsPanel.ResumeLayout(false);
            this.ControlsPanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private Panel MusicOrdersPanel;
        private Button RightButton;
        private Panel ControlsPanel;
        private Button LeftButton;
        private Label PageLabel;
        private Button CutButton;
        private Button PasteButton;
    }
}
