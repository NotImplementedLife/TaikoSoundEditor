using System.Windows.Forms;

namespace TaikoSoundEditor.Commons.Controls
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
            this.components = new System.ComponentModel.Container();
            this.MusicOrdersPanel = new System.Windows.Forms.Panel();
            this.RightButton = new System.Windows.Forms.Button();
            this.ControlsPanel = new System.Windows.Forms.Panel();
            this.ListStartButton = new System.Windows.Forms.Button();
            this.ListEndButton = new System.Windows.Forms.Button();
            this.Left10Button = new System.Windows.Forms.Button();
            this.Right10Button = new System.Windows.Forms.Button();
            this.RemoveButton = new System.Windows.Forms.Button();
            this.PasteButton = new System.Windows.Forms.Button();
            this.CutButton = new System.Windows.Forms.Button();
            this.PageLabel = new System.Windows.Forms.Label();
            this.LeftButton = new System.Windows.Forms.Button();
            this.GenreCloneMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.cloneAsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.popToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.animeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.kidsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.vocaloidToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.gameMusicToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.namcoOriginalToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.varietyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.classicalToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ControlsPanel.SuspendLayout();
            this.GenreCloneMenuStrip.SuspendLayout();
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
            this.MusicOrdersPanel.DoubleClick += new System.EventHandler(this.MusicOrdersPanel_DoubleClick);
            this.MusicOrdersPanel.MouseDown += new System.Windows.Forms.MouseEventHandler(this.MusicOrdersPanel_MouseDown);
            this.MusicOrdersPanel.MouseLeave += new System.EventHandler(this.MusicOrdersPanel_MouseLeave);
            this.MusicOrdersPanel.MouseMove += new System.Windows.Forms.MouseEventHandler(this.MusicOrdersPanel_MouseMove);
            this.MusicOrdersPanel.Resize += new System.EventHandler(this.MusicOrdersPanel_Resize);
            // 
            // RightButton
            // 
            this.RightButton.BackColor = System.Drawing.Color.Black;
            this.RightButton.Dock = System.Windows.Forms.DockStyle.Right;
            this.RightButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.RightButton.ForeColor = System.Drawing.Color.White;
            this.RightButton.Location = new System.Drawing.Point(487, 32);
            this.RightButton.Name = "RightButton";
            this.RightButton.Size = new System.Drawing.Size(20, 265);
            this.RightButton.TabIndex = 1;
            this.RightButton.Text = ">";
            this.RightButton.UseVisualStyleBackColor = false;
            this.RightButton.Click += new System.EventHandler(this.RightButton_Click);
            // 
            // ControlsPanel
            // 
            this.ControlsPanel.Controls.Add(this.ListStartButton);
            this.ControlsPanel.Controls.Add(this.ListEndButton);
            this.ControlsPanel.Controls.Add(this.Left10Button);
            this.ControlsPanel.Controls.Add(this.Right10Button);
            this.ControlsPanel.Controls.Add(this.RemoveButton);
            this.ControlsPanel.Controls.Add(this.PasteButton);
            this.ControlsPanel.Controls.Add(this.CutButton);
            this.ControlsPanel.Controls.Add(this.PageLabel);
            this.ControlsPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.ControlsPanel.Location = new System.Drawing.Point(0, 0);
            this.ControlsPanel.Name = "ControlsPanel";
            this.ControlsPanel.Size = new System.Drawing.Size(507, 32);
            this.ControlsPanel.TabIndex = 2;
            // 
            // ListStartButton
            // 
            this.ListStartButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ListStartButton.BackgroundImage = global::TaikoSoundEditor.Properties.Resources.ic_list_start;
            this.ListStartButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.ListStartButton.FlatAppearance.BorderSize = 0;
            this.ListStartButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ListStartButton.Location = new System.Drawing.Point(390, 3);
            this.ListStartButton.Name = "ListStartButton";
            this.ListStartButton.Size = new System.Drawing.Size(24, 24);
            this.ListStartButton.TabIndex = 7;
            this.ListStartButton.UseVisualStyleBackColor = true;
            this.ListStartButton.Click += new System.EventHandler(this.ListStartButton_Click);
            // 
            // ListEndButton
            // 
            this.ListEndButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ListEndButton.BackgroundImage = global::TaikoSoundEditor.Properties.Resources.ic_list_end;
            this.ListEndButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.ListEndButton.FlatAppearance.BorderSize = 0;
            this.ListEndButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ListEndButton.Location = new System.Drawing.Point(480, 3);
            this.ListEndButton.Name = "ListEndButton";
            this.ListEndButton.Size = new System.Drawing.Size(24, 24);
            this.ListEndButton.TabIndex = 6;
            this.ListEndButton.UseVisualStyleBackColor = true;
            this.ListEndButton.Click += new System.EventHandler(this.ListEndButton_Click);
            // 
            // Left10Button
            // 
            this.Left10Button.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Left10Button.BackgroundImage = global::TaikoSoundEditor.Properties.Resources.ic_left_10;
            this.Left10Button.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.Left10Button.FlatAppearance.BorderSize = 0;
            this.Left10Button.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Left10Button.Location = new System.Drawing.Point(420, 3);
            this.Left10Button.Name = "Left10Button";
            this.Left10Button.Size = new System.Drawing.Size(24, 24);
            this.Left10Button.TabIndex = 5;
            this.Left10Button.UseVisualStyleBackColor = true;
            this.Left10Button.Click += new System.EventHandler(this.Left10Button_Click);
            // 
            // Right10Button
            // 
            this.Right10Button.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Right10Button.BackgroundImage = global::TaikoSoundEditor.Properties.Resources.ic_right_10;
            this.Right10Button.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.Right10Button.FlatAppearance.BorderSize = 0;
            this.Right10Button.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Right10Button.Location = new System.Drawing.Point(450, 3);
            this.Right10Button.Name = "Right10Button";
            this.Right10Button.Size = new System.Drawing.Size(24, 24);
            this.Right10Button.TabIndex = 4;
            this.Right10Button.UseVisualStyleBackColor = true;
            this.Right10Button.Click += new System.EventHandler(this.Right10Button_Click);
            // 
            // RemoveButton
            // 
            this.RemoveButton.BackgroundImage = global::TaikoSoundEditor.Properties.Resources.ic_remove_gs;
            this.RemoveButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.RemoveButton.FlatAppearance.BorderSize = 0;
            this.RemoveButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.RemoveButton.Location = new System.Drawing.Point(63, 3);
            this.RemoveButton.Name = "RemoveButton";
            this.RemoveButton.Size = new System.Drawing.Size(24, 24);
            this.RemoveButton.TabIndex = 3;
            this.RemoveButton.UseVisualStyleBackColor = true;
            this.RemoveButton.Click += new System.EventHandler(this.RemoveButton_Click);
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
            this.LeftButton.BackColor = System.Drawing.Color.Black;
            this.LeftButton.Dock = System.Windows.Forms.DockStyle.Left;
            this.LeftButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.LeftButton.ForeColor = System.Drawing.Color.White;
            this.LeftButton.Location = new System.Drawing.Point(0, 32);
            this.LeftButton.Name = "LeftButton";
            this.LeftButton.Size = new System.Drawing.Size(20, 265);
            this.LeftButton.TabIndex = 3;
            this.LeftButton.Text = "<";
            this.LeftButton.UseVisualStyleBackColor = false;
            this.LeftButton.Click += new System.EventHandler(this.LeftButton_Click);
            // 
            // GenreCloneMenuStrip
            // 
            this.GenreCloneMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cloneAsToolStripMenuItem});
            this.GenreCloneMenuStrip.Name = "GenreCloneMenuStrip";
            this.GenreCloneMenuStrip.Size = new System.Drawing.Size(181, 48);
            // 
            // cloneAsToolStripMenuItem
            // 
            this.cloneAsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.popToolStripMenuItem,
            this.animeToolStripMenuItem,
            this.kidsToolStripMenuItem,
            this.vocaloidToolStripMenuItem,
            this.gameMusicToolStripMenuItem,
            this.namcoOriginalToolStripMenuItem,
            this.varietyToolStripMenuItem,
            this.classicalToolStripMenuItem});
            this.cloneAsToolStripMenuItem.Name = "cloneAsToolStripMenuItem";
            this.cloneAsToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.cloneAsToolStripMenuItem.Text = "Clone as";
            // 
            // popToolStripMenuItem
            // 
            this.popToolStripMenuItem.Name = "popToolStripMenuItem";
            this.popToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.popToolStripMenuItem.Text = "Pop";
            this.popToolStripMenuItem.Click += new System.EventHandler(this.GenreCloneToolStripMenuItem_Click);
            // 
            // animeToolStripMenuItem
            // 
            this.animeToolStripMenuItem.Name = "animeToolStripMenuItem";
            this.animeToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.animeToolStripMenuItem.Text = "Anime";
            this.animeToolStripMenuItem.Click += new System.EventHandler(this.GenreCloneToolStripMenuItem_Click);
            // 
            // kidsToolStripMenuItem
            // 
            this.kidsToolStripMenuItem.Name = "kidsToolStripMenuItem";
            this.kidsToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.kidsToolStripMenuItem.Text = "Kids";
            this.kidsToolStripMenuItem.Click += new System.EventHandler(this.GenreCloneToolStripMenuItem_Click);
            // 
            // vocaloidToolStripMenuItem
            // 
            this.vocaloidToolStripMenuItem.Name = "vocaloidToolStripMenuItem";
            this.vocaloidToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.vocaloidToolStripMenuItem.Text = "Vocaloid";
            this.vocaloidToolStripMenuItem.Click += new System.EventHandler(this.GenreCloneToolStripMenuItem_Click);
            // 
            // gameMusicToolStripMenuItem
            // 
            this.gameMusicToolStripMenuItem.Name = "gameMusicToolStripMenuItem";
            this.gameMusicToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.gameMusicToolStripMenuItem.Text = "GameMusic";
            this.gameMusicToolStripMenuItem.Click += new System.EventHandler(this.GenreCloneToolStripMenuItem_Click);
            // 
            // namcoOriginalToolStripMenuItem
            // 
            this.namcoOriginalToolStripMenuItem.Name = "namcoOriginalToolStripMenuItem";
            this.namcoOriginalToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.namcoOriginalToolStripMenuItem.Text = "NamcoOriginal";
            this.namcoOriginalToolStripMenuItem.Click += new System.EventHandler(this.GenreCloneToolStripMenuItem_Click);
            // 
            // varietyToolStripMenuItem
            // 
            this.varietyToolStripMenuItem.Name = "varietyToolStripMenuItem";
            this.varietyToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.varietyToolStripMenuItem.Text = "Variety";
            this.varietyToolStripMenuItem.Click += new System.EventHandler(this.GenreCloneToolStripMenuItem_Click);
            // 
            // classicalToolStripMenuItem
            // 
            this.classicalToolStripMenuItem.Name = "classicalToolStripMenuItem";
            this.classicalToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.classicalToolStripMenuItem.Text = "Classical";
            this.classicalToolStripMenuItem.Click += new System.EventHandler(this.GenreCloneToolStripMenuItem_Click);
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
            this.GenreCloneMenuStrip.ResumeLayout(false);
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
        private Button RemoveButton;
        private Button Left10Button;
        private Button Right10Button;
        private Button ListStartButton;
        private Button ListEndButton;
        private ContextMenuStrip GenreCloneMenuStrip;
        private ToolStripMenuItem cloneAsToolStripMenuItem;
        private ToolStripMenuItem popToolStripMenuItem;
        private ToolStripMenuItem animeToolStripMenuItem;
        private ToolStripMenuItem kidsToolStripMenuItem;
        private ToolStripMenuItem vocaloidToolStripMenuItem;
        private ToolStripMenuItem gameMusicToolStripMenuItem;
        private ToolStripMenuItem namcoOriginalToolStripMenuItem;
        private ToolStripMenuItem varietyToolStripMenuItem;
        private ToolStripMenuItem classicalToolStripMenuItem;
    }
}
