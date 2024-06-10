namespace CourseWork___TRPG
{
	partial class MapEditorForm
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
			layerComboBox = new ComboBox();
			panel1 = new Panel();
			focusControl = new Label();
			openSettingsButton = new Button();
			tilePreview = new PictureBox();
			tileComboBox = new ComboBox();
			label2 = new Label();
			label3 = new Label();
			label1 = new Label();
			menuStrip = new MenuStrip();
			mapToolStripMenuItem = new ToolStripMenuItem();
			openToolStripMenuItem = new ToolStripMenuItem();
			createToolStripMenuItem = new ToolStripMenuItem();
			saveToolStripMenuItem = new ToolStripMenuItem();
			settingsToolStripMenuItem = new ToolStripMenuItem();
			panel1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)tilePreview).BeginInit();
			menuStrip.SuspendLayout();
			SuspendLayout();
			// 
			// layerComboBox
			// 
			layerComboBox.BackColor = SystemColors.Control;
			layerComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
			layerComboBox.FormattingEnabled = true;
			layerComboBox.Location = new Point(8, 29);
			layerComboBox.Name = "layerComboBox";
			layerComboBox.Size = new Size(220, 29);
			layerComboBox.TabIndex = 0;
			layerComboBox.SelectedIndexChanged += layerComboBox_SelectedIndexChanged;
			// 
			// panel1
			// 
			panel1.BackColor = SystemColors.ControlDark;
			panel1.Controls.Add(focusControl);
			panel1.Controls.Add(openSettingsButton);
			panel1.Controls.Add(tilePreview);
			panel1.Controls.Add(tileComboBox);
			panel1.Controls.Add(label2);
			panel1.Controls.Add(label3);
			panel1.Controls.Add(label1);
			panel1.Controls.Add(layerComboBox);
			panel1.Dock = DockStyle.Left;
			panel1.Location = new Point(0, 24);
			panel1.Name = "panel1";
			panel1.Padding = new Padding(5);
			panel1.Size = new Size(236, 483);
			panel1.TabIndex = 1;
			// 
			// focusControl
			// 
			focusControl.AutoSize = true;
			focusControl.BackColor = Color.Transparent;
			focusControl.Dock = DockStyle.Top;
			focusControl.Location = new Point(5, 5);
			focusControl.Name = "focusControl";
			focusControl.Size = new Size(26, 21);
			focusControl.TabIndex = 6;
			focusControl.Text = "    ";
			// 
			// openSettingsButton
			// 
			openSettingsButton.Dock = DockStyle.Bottom;
			openSettingsButton.Location = new Point(5, 439);
			openSettingsButton.Name = "openSettingsButton";
			openSettingsButton.Size = new Size(226, 39);
			openSettingsButton.TabIndex = 5;
			openSettingsButton.Text = "Settings";
			openSettingsButton.UseVisualStyleBackColor = true;
			openSettingsButton.Click += OpenSettingsForm_OnClick;
			// 
			// tilePreview
			// 
			tilePreview.BackColor = SystemColors.Control;
			tilePreview.BackgroundImageLayout = ImageLayout.Zoom;
			tilePreview.Location = new Point(52, 129);
			tilePreview.Name = "tilePreview";
			tilePreview.Size = new Size(128, 128);
			tilePreview.TabIndex = 4;
			tilePreview.TabStop = false;
			// 
			// tileComboBox
			// 
			tileComboBox.BackColor = SystemColors.Control;
			tileComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
			tileComboBox.FormattingEnabled = true;
			tileComboBox.Location = new Point(8, 94);
			tileComboBox.Name = "tileComboBox";
			tileComboBox.Size = new Size(220, 29);
			tileComboBox.TabIndex = 3;
			tileComboBox.SelectedIndexChanged += tileComboBox_SelectedIndexChanged;
			// 
			// label2
			// 
			label2.AutoSize = true;
			label2.Location = new Point(97, 70);
			label2.Name = "label2";
			label2.Size = new Size(34, 21);
			label2.TabIndex = 2;
			label2.Text = "Tile";
			// 
			// label3
			// 
			label3.AutoSize = true;
			label3.Location = new Point(88, 5);
			label3.Name = "label3";
			label3.Size = new Size(48, 21);
			label3.TabIndex = 1;
			label3.Text = "Layer";
			// 
			// label1
			// 
			label1.AutoSize = true;
			label1.Location = new Point(88, 5);
			label1.Name = "label1";
			label1.Size = new Size(48, 21);
			label1.TabIndex = 1;
			label1.Text = "Layer";
			// 
			// menuStrip
			// 
			menuStrip.ImageScalingSize = new Size(20, 20);
			menuStrip.Items.AddRange(new ToolStripItem[] { mapToolStripMenuItem, settingsToolStripMenuItem });
			menuStrip.Location = new Point(0, 0);
			menuStrip.Name = "menuStrip";
			menuStrip.Size = new Size(876, 24);
			menuStrip.TabIndex = 2;
			menuStrip.Text = "menuStrip1";
			// 
			// mapToolStripMenuItem
			// 
			mapToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { openToolStripMenuItem, createToolStripMenuItem, saveToolStripMenuItem });
			mapToolStripMenuItem.Name = "mapToolStripMenuItem";
			mapToolStripMenuItem.Size = new Size(43, 20);
			mapToolStripMenuItem.Text = "Map";
			// 
			// openToolStripMenuItem
			// 
			openToolStripMenuItem.Name = "openToolStripMenuItem";
			openToolStripMenuItem.Size = new Size(108, 22);
			openToolStripMenuItem.Text = "Open";
			openToolStripMenuItem.Click += openToolStripMenuItem_Click;
			// 
			// createToolStripMenuItem
			// 
			createToolStripMenuItem.Name = "createToolStripMenuItem";
			createToolStripMenuItem.Size = new Size(108, 22);
			createToolStripMenuItem.Text = "Create";
			createToolStripMenuItem.Click += createToolStripMenuItem_Click;
			// 
			// saveToolStripMenuItem
			// 
			saveToolStripMenuItem.Name = "saveToolStripMenuItem";
			saveToolStripMenuItem.Size = new Size(108, 22);
			saveToolStripMenuItem.Text = "Save";
			saveToolStripMenuItem.Click += saveToolStripMenuItem_Click;
			// 
			// settingsToolStripMenuItem
			// 
			settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
			settingsToolStripMenuItem.Size = new Size(61, 20);
			settingsToolStripMenuItem.Text = "Settings";
			settingsToolStripMenuItem.Click += OpenSettingsForm_OnClick;
			// 
			// MapEditorForm
			// 
			AutoScaleDimensions = new SizeF(9F, 21F);
			AutoScaleMode = AutoScaleMode.Font;
			ClientSize = new Size(876, 507);
			Controls.Add(panel1);
			Controls.Add(menuStrip);
			DoubleBuffered = true;
			Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 204);
			KeyPreview = true;
			MainMenuStrip = menuStrip;
			Margin = new Padding(4);
			Name = "MapEditorForm";
			StartPosition = FormStartPosition.CenterScreen;
			Text = "Map Editor";
			WindowState = FormWindowState.Maximized;
			Load += MapEditor_Load;
			KeyDown += OnKeyDownEvent;
			KeyUp += OnKeyUpEvent;
			panel1.ResumeLayout(false);
			panel1.PerformLayout();
			((System.ComponentModel.ISupportInitialize)tilePreview).EndInit();
			menuStrip.ResumeLayout(false);
			menuStrip.PerformLayout();
			ResumeLayout(false);
			PerformLayout();
		}

		#endregion

		private ComboBox layerComboBox;
		private Panel panel1;
		private Label label1;
		private Label label2;
		private Label label3;
		private ComboBox tileComboBox;
		private PictureBox tilePreview;
		private MenuStrip menuStrip;
		private ToolStripMenuItem mapToolStripMenuItem;
		private ToolStripMenuItem openToolStripMenuItem;
		private ToolStripMenuItem createToolStripMenuItem;
		private ToolStripMenuItem saveToolStripMenuItem;
		private Button openSettingsButton;
		private ToolStripMenuItem settingsToolStripMenuItem;
		private Label focusControl;
	}
}