namespace CourseWork___TRPG
{
    partial class MapEditorSettingsForm
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
			drawGridCheckBox = new CheckBox();
			drawMapCenterCheckBox = new CheckBox();
			gridColorPickerPanel = new Panel();
			label1 = new Label();
			replaceTileModeCheckBox = new CheckBox();
			topMostCheckBox = new CheckBox();
			SuspendLayout();
			// 
			// drawGridCheckBox
			// 
			drawGridCheckBox.AutoSize = true;
			drawGridCheckBox.Location = new Point(12, 12);
			drawGridCheckBox.Name = "drawGridCheckBox";
			drawGridCheckBox.Size = new Size(100, 25);
			drawGridCheckBox.TabIndex = 1;
			drawGridCheckBox.Text = "Draw Grid";
			drawGridCheckBox.UseVisualStyleBackColor = true;
			drawGridCheckBox.CheckedChanged += drawGridCheckBox_CheckedChanged;
			// 
			// drawMapCenterCheckBox
			// 
			drawMapCenterCheckBox.AutoSize = true;
			drawMapCenterCheckBox.Location = new Point(12, 43);
			drawMapCenterCheckBox.Name = "drawMapCenterCheckBox";
			drawMapCenterCheckBox.Size = new Size(151, 25);
			drawMapCenterCheckBox.TabIndex = 2;
			drawMapCenterCheckBox.Text = "Draw Map Center";
			drawMapCenterCheckBox.UseVisualStyleBackColor = true;
			drawMapCenterCheckBox.CheckedChanged += drawMapCenterCheckBox_CheckedChanged;
			// 
			// gridColorPickerPanel
			// 
			gridColorPickerPanel.BackColor = SystemColors.ControlDark;
			gridColorPickerPanel.Location = new Point(306, 33);
			gridColorPickerPanel.Name = "gridColorPickerPanel";
			gridColorPickerPanel.Size = new Size(75, 75);
			gridColorPickerPanel.TabIndex = 3;
			gridColorPickerPanel.Click += gridColorPickerPanel_Click;
			// 
			// label1
			// 
			label1.AutoSize = true;
			label1.Location = new Point(306, 9);
			label1.Name = "label1";
			label1.Size = new Size(79, 21);
			label1.TabIndex = 4;
			label1.Text = "Grid color";
			// 
			// replaceTileModeCheckBox
			// 
			replaceTileModeCheckBox.AutoSize = true;
			replaceTileModeCheckBox.Location = new Point(12, 74);
			replaceTileModeCheckBox.Name = "replaceTileModeCheckBox";
			replaceTileModeCheckBox.Size = new Size(155, 25);
			replaceTileModeCheckBox.TabIndex = 5;
			replaceTileModeCheckBox.Text = "Replace Tile Mode";
			replaceTileModeCheckBox.UseVisualStyleBackColor = true;
			replaceTileModeCheckBox.CheckedChanged += replaceTileModeCheckBox_CheckedChanged;
			// 
			// topMostCheckBox
			// 
			topMostCheckBox.AutoSize = true;
			topMostCheckBox.Location = new Point(12, 236);
			topMostCheckBox.Name = "topMostCheckBox";
			topMostCheckBox.Size = new Size(92, 25);
			topMostCheckBox.TabIndex = 6;
			topMostCheckBox.Text = "Top Most";
			topMostCheckBox.UseVisualStyleBackColor = true;
			topMostCheckBox.CheckedChanged += topMostCheckBox_CheckedChanged;
			// 
			// MapEditorSettingsForm
			// 
			AutoScaleDimensions = new SizeF(9F, 21F);
			AutoScaleMode = AutoScaleMode.Font;
			ClientSize = new Size(397, 273);
			Controls.Add(topMostCheckBox);
			Controls.Add(replaceTileModeCheckBox);
			Controls.Add(label1);
			Controls.Add(gridColorPickerPanel);
			Controls.Add(drawMapCenterCheckBox);
			Controls.Add(drawGridCheckBox);
			Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 204);
			Margin = new Padding(4);
			Name = "MapEditorSettingsForm";
			Text = "MapEditorSettingsForm";
			Load += MapEditorSettingsForm_Load;
			ResumeLayout(false);
			PerformLayout();
		}

		#endregion

		private CheckBox drawGridCheckBox;
		private CheckBox drawMapCenterCheckBox;
		private Panel gridColorPickerPanel;
		private Label label1;
		private CheckBox replaceTileModeCheckBox;
		private CheckBox topMostCheckBox;
	}
}