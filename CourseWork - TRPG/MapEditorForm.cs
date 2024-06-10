using CourseWork___TRPG.direct2d;
using CourseWork___TRPG.game;
using CourseWork___TRPG.game.models;
using SharpDX.DirectWrite;
using SharpDX.Mathematics.Interop;
using Timer = System.Threading.Timer;

namespace CourseWork___TRPG
{
	public partial class MapEditorForm : Form
	{
		private Direct2DControl direct2DControl;

		public Camera camera;
		private Map map;

		private Timer updateTimer;
		private Timer renderTimer;

		private int selectedLayer = 0;
		private Tile? selectedTile;

		private Point lastMousePosition;

		private bool isMapSaved = false;
		private string mapFilePath = "";

		private bool isSettingsFormOpened = false;

		public MapEditorForm()
		{
			InitializeComponent();

			// Set up Direct2D control
			this.direct2DControl = new Direct2DControl();
			this.direct2DControl.Dock = DockStyle.Fill;

			this.Controls.Add(direct2DControl);

			// Initialize Direct2D
			Direct2D.Instance.Initialize(direct2DControl);
		}

		private void MapEditor_Load(object sender, EventArgs e)
		{
			// Set up direct2DControl events
			this.direct2DControl.Render += Render;
			this.direct2DControl.MouseClick += OnMouseEvent;
			this.direct2DControl.MouseMove += OnMouseEvent;
			this.direct2DControl.MouseWheel += OnMouseEvent;

			// Load the map
			this.map = new Map("New map");

			// Set up camera
			this.camera = new Camera(
				new PointF(0, 0), 
				this.ClientSize
			);

			this.direct2DControl.Resize += (s, e) =>
			{ this.camera.AdjustForResolution(direct2DControl.ClientSize); };

			// Set up combo boxes
			int layersCount = 0;

			foreach (Tile tile in TileManager.Instance.GetAllTiles().Values)
				layersCount = Math.Max(layersCount, tile.Layer);

			layerComboBox.Items.AddRange(Enumerable.Range(0, layersCount + 1).Select(i => i.ToString()).ToArray());

			layerComboBox.SelectedIndex = 0;
			this.UpdateTilesComboBox();

			// Set up timers
			this.updateTimer = new Timer(UpdateLoop, null, 0, 1000 / Config.Game.UPDATE_RATE);
			this.renderTimer = new Timer(RenderLoop, null, 0, 1000 / Config.Game.FRAME_RATE);
		}

		// Timers
		private void UpdateLoop(object? state)
		{
			this.camera.Update();
		}

		private void RenderLoop(object? state)
		{
			this.direct2DControl.Invalidate();
		}

		// Render

		private void Render(EnhancedRenderTarget renderTarget)
		{
			// Draw camera view
			this.camera.Render(renderTarget, this.map);

			// Draw Map status text
			TextFormat mapStatusTextFormat = new TextFormat(new SharpDX.DirectWrite.Factory(), "Arial", 17);

			renderTarget.DrawText(
				$"Map name: {this.map.Name}", 
				mapStatusTextFormat, 
				new RawColor4(1, 1, 1, 1), 
				new PointF(panel1.Width, menuStrip.Height)
			);
			renderTarget.DrawText(
				$"Saved: {(this.isMapSaved ? "Yes" : "No")}", 
				mapStatusTextFormat, 
				this.isMapSaved ? new RawColor4(0, 1, 0, 1) : new RawColor4(1, 0, 0, 1), 
				new PointF(panel1.Width, menuStrip.Height + 20)
			);

			// Draw info text
			TextFormat infoTextFormat = new TextFormat(new SharpDX.DirectWrite.Factory(), "Arial", 15);

			string cameraPositionText = $"Camera Position: {this.camera.Position}";
			string cameraOffsetText = $"Camera Offset: {this.camera.Offset}";
			string cameraResolutionText = $"Camera Resolution: {this.camera.Resolution}";
			string cameraZoomText = $"Camera Zoom: {this.camera.Zoom}";
			string mapNameText = $"Map Name: {this.map.Name}";
			string cursorPositionText = $"Cursor Position: {this.direct2DControl.PointToClient(Cursor.Position)}";
			string fpsText = $"FPS: {this.direct2DControl.CurrentFPS}";

			string infoText = (
				$"{cameraPositionText}\n" +
				$"{cameraOffsetText}\n" +
				$"{cameraResolutionText}\n" +
				$"{cameraZoomText}\n" +
				$"{mapNameText}\n" +
				$"{cursorPositionText}\n" +
				$"{fpsText}\n"
			);

			TextMetrics infoTextMetrics = renderTarget.MeasureText(
				infoText, infoTextFormat
			);

			renderTarget.DrawText(
				infoText,
				infoTextFormat,
				new RawColor4(1, 1, 1, 1),
				new PointF(
					renderTarget.Size.Width - infoTextMetrics.Width,
					menuStrip.Height
				)
			);
		}

		// Keyboard and mouse input
		private void OnKeyDownEvent(object? sender, KeyEventArgs e)
		{
			if (e.Alt)
			{
				// Eye dropper cursor
				this.direct2DControl.Cursor = Cursors.Cross;
			}

			if (e.Control)
			{
				// Save map
				if (e.KeyCode == Keys.S)
					this.SaveMap();

				// Load map
				else if (e.KeyCode == Keys.O)
					this.LoadMapFromFile();

				// Create new map
				else if (e.KeyCode == Keys.N)
				{
					this.map = new Map("New map");
					this.isMapSaved = false;
				}
			}
			else
			{
				float cameraVelocity = Config.MapEditor.CAMERA_MOVEMENT_VELOCITY;
				PointF newCameraVelocity = this.camera.Velocity;

				// Camera movement
				if (e.KeyCode == Keys.W)
					newCameraVelocity.Y = -cameraVelocity;

				if (e.KeyCode == Keys.S)
					newCameraVelocity.Y = cameraVelocity;

				if (e.KeyCode == Keys.A)
					newCameraVelocity.X = -cameraVelocity;

				if (e.KeyCode == Keys.D)
					newCameraVelocity.X = cameraVelocity;

				this.camera.Velocity = newCameraVelocity;
			}
		}

		private void OnKeyUpEvent(object? sender, KeyEventArgs e)
		{
			if (!e.Alt)
			{
				// Remove Eye dropper cursor
				this.direct2DControl.Cursor = Cursors.Default;
			}

			PointF newCameraVelocity = this.camera.Velocity;

			if (e.KeyCode == Keys.W || e.KeyCode == Keys.S)
				newCameraVelocity.Y = 0;

			if (e.KeyCode == Keys.A || e.KeyCode == Keys.D)
				newCameraVelocity.X = 0;

			this.camera.Velocity = newCameraVelocity;

			if (e.Control)
			{
				if (e.KeyCode == Keys.Add)
					this.camera.Zoom += 1;

				if (e.KeyCode == Keys.Subtract)
					this.camera.Zoom -= 1;
			}
		}

		private void OnMouseEvent(object? sender, MouseEventArgs e)
		{
			float zoomFactor = this.camera.ZoomFactor;
			float textureSize = Config.Renderer.TEXTURE_SIZE * zoomFactor;

			// Get tile position based on click position
			Point tilePosition = new Point(
				(int)Math.Floor((e.X - this.camera.Offset.X * zoomFactor) / textureSize),
				(int)Math.Floor((e.Y - this.camera.Offset.Y * zoomFactor) / textureSize)
			);

			// Left click
			if (e.Button == MouseButtons.Left)
			{
				// Eye dropper mode
				if (ModifierKeys == Keys.Alt)
				{
					Tile? currentTile = this.map.GetTile(selectedLayer, tilePosition);

					if (currentTile != null && tileComboBox.Items.Contains(currentTile.Name))
					{
						this.selectedTile = currentTile;
						tileComboBox.SelectedItem = currentTile.Name;
						tilePreview.BackgroundImage = ResourcesLoader.LoadTileTexture(currentTile.TextureName);
					}
				}
				// Place tile on map
				if (this.selectedTile != null)
				{
					Tile? currentTile = this.map.GetTile(selectedLayer, tilePosition);

					if (currentTile != this.selectedTile)
					{
						if (!Config.MapEditor.REPLACE_TILE_MODE && currentTile != null)
							return;

						this.map.SetTile(
							selectedLayer,
							this.selectedTile,
							tilePosition
						);

						this.isMapSaved = false;
					}
				}
			}
			// Right click
			else if (e.Button == MouseButtons.Right)
			{
				Tile? currentTile = this.map.GetTile(selectedLayer, tilePosition);

				if (currentTile != null)
				{
					// Remove tile from map
					this.map.RemoveTile(
						selectedLayer,
						tilePosition
					);

					this.isMapSaved = false;
				}
			}
			// Middle click
			else if (e.Button == MouseButtons.Middle)
			{
				// Move camera relatively to last mouse position
				this.camera.Move(
					-(e.X - lastMousePosition.X),
					-(e.Y - lastMousePosition.Y)
				);
			}
			// Mouse wheel
			else if (e.Delta != 0)
			{
				// Zoom camera
				if (e.Delta > 0)
					this.camera.Zoom += 1;

				else
					this.camera.Zoom -= 1;
			}

			// Update last mouse position
			lastMousePosition = e.Location;
		}

		private void layerComboBox_SelectedIndexChanged(object sender, EventArgs e)
		{
			this.selectedLayer = layerComboBox.SelectedIndex;
			this.UpdateTilesComboBox();
		}

		private void tileComboBox_SelectedIndexChanged(object sender, EventArgs e)
		{
			string? selectedTileName = tileComboBox.SelectedItem.ToString();

			if (selectedTileName != null)
				this.selectedTile = TileManager.Instance.GetTile(selectedTileName);

			else
				this.selectedTile = null;

			// Update tile preview
			if (this.selectedTile != null)
				tilePreview.BackgroundImage = ResourcesLoader.LoadTileTexture(this.selectedTile.TextureName);

			else
				tilePreview.BackgroundImage = null;
		}

		// Menu Strip
		private void openToolStripMenuItem_Click(object sender, EventArgs e)
		{
			this.LoadMapFromFile();
		}

		private void createToolStripMenuItem_Click(object sender, EventArgs e)
		{
			this.map = new Map("New map");
		}

		private void saveToolStripMenuItem_Click(object sender, EventArgs e)
		{
			this.SaveMap();
		}

		// Other functions
		private void UpdateTilesComboBox()
		{
			int selectedLayer = layerComboBox.SelectedIndex;

			tileComboBox.Items.Clear();

			foreach (Tile tile in TileManager.Instance.GetTilesByLayer(selectedLayer))
				tileComboBox.Items.Add(tile.Name);

			tileComboBox.SelectedIndex = 0;
			this.selectedTile = TileManager.Instance.GetTile(tileComboBox.SelectedItem.ToString());

			tilePreview.BackgroundImage = ResourcesLoader.LoadTileTexture(this.selectedTile.TextureName);
		}

		private void LoadMapFromFile()
		{
			string mapExtension = Config.Resources.MAP_EXTENSION;

			OpenFileDialog openFileDialog = new OpenFileDialog();

			openFileDialog.Filter = $"TRPG Map Files (*{mapExtension})|*{mapExtension}";
			openFileDialog.Title = "Open Map File";
			openFileDialog.CheckFileExists = true;
			openFileDialog.CheckPathExists = true;
			openFileDialog.InitialDirectory = Config.Resources.MAPS_DIRECTORY;
			openFileDialog.RestoreDirectory = true;

			if (openFileDialog.ShowDialog() == DialogResult.OK)
			{
				try
				{
					this.map = Map.LoadFromFile(openFileDialog.FileName);
				}
				catch (Exception ex)
				{
					MessageBox.Show("Error: Could not read file from disk. Original error: " + ex.Message);
					return;
				}

				this.isMapSaved = true;
				this.mapFilePath = openFileDialog.FileName;
			}
		}

		private void SaveMap()
		{
			// Select file path
			if (this.mapFilePath == "")
			{
				SaveFileDialog saveFileDialog = new SaveFileDialog();

				saveFileDialog.Filter = $"TRPG Map Files (*{Config.Resources.MAP_EXTENSION})|*{Config.Resources.MAP_EXTENSION}";
				saveFileDialog.Title = "Save Map File";
				saveFileDialog.InitialDirectory = Config.Resources.MAPS_DIRECTORY;
				saveFileDialog.RestoreDirectory = true;

				if (saveFileDialog.ShowDialog() == DialogResult.OK)
					this.mapFilePath = saveFileDialog.FileName;

				else
					return;
			}

			// Save map
			if (this.mapFilePath != "")
			{
				try
				{
					this.map.SaveToFile(this.mapFilePath);
				}
				catch (Exception ex)
				{
					MessageBox.Show("Error: Could not save file to disk. Original error: " + ex.Message);
					return;
				}

				this.isMapSaved = true;
			}
		}

		private void OpenSettingsForm_OnClick(object sender, EventArgs e)
		{
			focusControl.Focus();

			if (this.isSettingsFormOpened)
				return;

			MapEditorSettingsForm settingsForm = new MapEditorSettingsForm();
			settingsForm.Show();

			this.isSettingsFormOpened = true;

			settingsForm.FormClosed += (s, e) =>
			{ this.isSettingsFormOpened = false; };
		}
	}
}
