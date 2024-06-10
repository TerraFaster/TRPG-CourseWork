using CourseWork___TRPG.game;
using SharpDX.Mathematics.Interop;
using System;

namespace CourseWork___TRPG
{
	public partial class MapEditorSettingsForm : Form
	{
		public MapEditorSettingsForm()
		{
			InitializeComponent();
		}

		private void MapEditorSettingsForm_Load(object sender, EventArgs e)
		{
			this.TopMost = Config.MapEditor.SETTINGS_FORM_TOPMOST;

			drawGridCheckBox.Checked = Config.Renderer.DRAW_GRID;
			drawMapCenterCheckBox.Checked = Config.Renderer.DRAW_MAP_CENTER;
			replaceTileModeCheckBox.Checked = Config.MapEditor.REPLACE_TILE_MODE;
			topMostCheckBox.Checked = Config.MapEditor.SETTINGS_FORM_TOPMOST;

			gridColorPickerPanel.BackColor = Color.FromArgb(
				(int)(Config.Renderer.GRID_BRUSH_COLOR.A * 100),
				(int)(Config.Renderer.GRID_BRUSH_COLOR.R * 255),
				(int)(Config.Renderer.GRID_BRUSH_COLOR.G * 255),
				(int)(Config.Renderer.GRID_BRUSH_COLOR.B * 255)
			);
		}

		private void drawGridCheckBox_CheckedChanged(object sender, EventArgs e)
		{
			Config.Renderer.DRAW_GRID = drawGridCheckBox.Checked;
		}

		private void drawMapCenterCheckBox_CheckedChanged(object sender, EventArgs e)
		{
			Config.Renderer.DRAW_MAP_CENTER = drawMapCenterCheckBox.Checked;
		}

		private void replaceTileModeCheckBox_CheckedChanged(object sender, EventArgs e)
		{
			Config.MapEditor.REPLACE_TILE_MODE = replaceTileModeCheckBox.Checked;
		}

		private void topMostCheckBox_CheckedChanged(object sender, EventArgs e)
		{
			Config.MapEditor.SETTINGS_FORM_TOPMOST = topMostCheckBox.Checked;
			this.TopMost = topMostCheckBox.Checked;
		}

		private void gridColorPickerPanel_Click(object sender, EventArgs e)
		{
			ColorDialog colorDialog = new ColorDialog();

			colorDialog.FullOpen = true;
			colorDialog.AllowFullOpen = true;
			colorDialog.AnyColor = true;
			colorDialog.Color = gridColorPickerPanel.BackColor;

			if (colorDialog.ShowDialog() == DialogResult.OK)
			{
				gridColorPickerPanel.BackColor = colorDialog.Color;
				Config.Renderer.GRID_BRUSH_COLOR = new RawColor4(
					colorDialog.Color.R / 255f,
					colorDialog.Color.G / 255f,
					colorDialog.Color.B / 255f,
					colorDialog.Color.A / 100f
				);
			}
		}
	}
}
