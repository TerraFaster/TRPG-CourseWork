using CourseWork___TRPG.direct2d;
using CourseWork___TRPG.game;
using CourseWork___TRPG.game.models;
using CourseWork___TRPG.game.models.entities;
using CourseWork___TRPG.game.models.entities.enemies;
using SharpDX.Direct2D1;
using SharpDX.DirectWrite;
using SharpDX.Mathematics.Interop;
using Timer = System.Threading.Timer;

namespace CourseWork___TRPG
{
	public partial class MainForm : Form
	{
		private Direct2DControl direct2DControl;

		private Map map;
		private MainCharacter mainCharacter;
		private GameCamera camera;

		private Timer updateTimer;
		private Timer renderTimer;

		private List<LivingEntity> entities = new List<LivingEntity>();
		private int stoneItemsCollected = 0;

		public MainForm()
		{
			InitializeComponent();

			// Set up Direct2D control
			this.direct2DControl = new Direct2DControl();
			this.direct2DControl.Dock = DockStyle.Fill;

			this.Controls.Add(direct2DControl);

			// Initialize Direct2D
			Direct2D.Instance.Initialize(direct2DControl);
		}

		private void MainForm_Load(object sender, EventArgs e)
		{
			// Set up direct2DControl events
			this.direct2DControl.Render += Render;
			this.direct2DControl.MouseClick += OnMouseEvent;

			// Load the map
			this.map = ResourcesLoader.LoadMap("main");

			// Create main character
			this.mainCharacter = new MainCharacter(
				name: "Main Character", 
				position: this.map.SpawnPoint, 
				health: Config.MainCharacter.BASE_HEALTH, 
				level: Config.MainCharacter.BASE_LEVEL, 
				attack: Config.MainCharacter.BASE_ATTACK, 
				defense: Config.MainCharacter.BASE_DEFENSE, 
				speed: Config.MainCharacter.MOVEMENT_SPEED
			);

			// Create golem enemy
			var golem1 = new Golem(
				position: new PointF(4, 4), 
				health: 100, 
				level: 1, 
				attack: 10, 
				defense: 5, 
				speed: 5
			);

			var golem2 = new Golem(
				position: new PointF(-4, 4), 
				health: 100, 
				level: 1, 
				attack: 10, 
				defense: 5, 
				speed: 5
			);

			var golem3 = new Golem(
				position: new PointF(2, 4), 
				health: 100, 
				level: 1, 
				attack: 10, 
				defense: 5, 
				speed: 5
			);

			this.entities = new List<LivingEntity>() {
				this.mainCharacter, golem1, golem2, golem3
			};

			// Set up camera
			this.camera = new GameCamera(
				new PointF(0, 0), 
				this.ClientSize
			);

			this.direct2DControl.Resize += (s, e) =>
			{ this.camera.AdjustForResolution(direct2DControl.ClientSize); };

			// Set up timers
			this.updateTimer = new Timer(UpdateLoop, null, 0, 1000 / Config.Game.UPDATE_RATE);
			this.renderTimer = new Timer(RenderLoop, null, 0, 1000 / Config.Game.FRAME_RATE);
		}

		// Timers
		private void UpdateLoop(object? state)
		{
			for (int i = 0; i < this.entities.Count; i++)
				this.entities[i].Update();

			this.camera.Update(this.mainCharacter);
		}

		private void RenderLoop(object? state)
		{
			this.direct2DControl.Invalidate();
		}

		// Render
		private void Render(EnhancedRenderTarget renderTarget)
		{
			// Draw camera view
			this.camera.Render(renderTarget, this.map, this.entities);

			// Draw info text
			TextFormat infoTextFormat = new TextFormat(new SharpDX.DirectWrite.Factory(), "Arial", 15);

			string characterPositionText = $"Character Position: {this.mainCharacter.Position}";
			string cameraPositionText = $"Camera Position: {this.camera.Position}";
			string cameraOffsetText = $"Camera Offset: {this.camera.Offset}";
			string cameraResolutionText = $"Camera Resolution: {this.camera.Resolution}";
			string cameraZoomText = $"Camera Zoom: {this.camera.Zoom}";
			string mapNameText = $"Map Name: {this.map.Name}";
			string cursorPositionText = $"Cursor Position: {this.direct2DControl.PointToClient(Cursor.Position)}";
			string fpsText = $"FPS: {this.direct2DControl.CurrentFPS}";

			string infoText = (
				$"{characterPositionText}\n" + 
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
					0
				)
			);

			// Draw rock bitmap and stone items collected
			float resourcesImageSize = 48;
			TextFormat resourcesTextFormat = new TextFormat(new SharpDX.DirectWrite.Factory(), "Arial", 30);

			renderTarget.DrawBitmap(
				TileManager.Instance.GetTile("rock").Texture, 
				new RawRectangleF(
					0, 0,
					resourcesImageSize,
					resourcesImageSize
				), 
				1f,
				BitmapInterpolationMode.Linear
			);

			TextMetrics resourcesTextMetrics = renderTarget.MeasureText(
				$"x{this.stoneItemsCollected}", 
				resourcesTextFormat
			);

			renderTarget.DrawText(
				$"x{this.stoneItemsCollected}",
				resourcesTextFormat, 
				new RawColor4(1, 1, 1, 1), 
				new PointF(resourcesImageSize, resourcesImageSize - resourcesTextMetrics.Height)
			);
		}

		// Keyboard and mouse input
		private void OnKeyDownEvent(object? sender, KeyEventArgs e)
		{
			float speed = this.mainCharacter.Speed;
			PointF newMainCharacterVelocity = this.mainCharacter.Velocity;

			// Movement
			if (e.KeyCode == Keys.W)
				newMainCharacterVelocity.Y = -speed;

			if (e.KeyCode == Keys.S)
				newMainCharacterVelocity.Y = speed;

			if (e.KeyCode == Keys.A)
				newMainCharacterVelocity.X = -speed;

			if (e.KeyCode == Keys.D)
				newMainCharacterVelocity.X = speed;

			this.mainCharacter.Velocity = newMainCharacterVelocity;
		}

		private void OnKeyUpEvent(object? sender, KeyEventArgs e)
		{
			PointF newMainCharacterVelocity = this.mainCharacter.Velocity;

			if (e.KeyCode == Keys.W || e.KeyCode == Keys.S)
				newMainCharacterVelocity.Y = 0;

			if (e.KeyCode == Keys.A || e.KeyCode == Keys.D)
				newMainCharacterVelocity.X = 0;

			this.mainCharacter.Velocity = newMainCharacterVelocity;
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

			if (e.Button == MouseButtons.Left)
			{
				// Check if distance between click and character is less than 3
				if (this.mainCharacter.DistanceToPoint(tilePosition) > 3)
					return;

				// Get tile from map
				Tile? tile = this.map.GetTile(1, tilePosition);

				if (tile != null && tile.Name == "rock")
					this.stoneItemsCollected++;

				// Check if any entity is at the clicked position
				for (int i = 0; i < this.entities.Count; i++)
				{
					LivingEntity entity = this.entities[i];

					if (entity.Position == tilePosition)
					{
						// Check if entity is an enemy
						if (entity is Enemy enemy)
						{
							// Main character attacks enemy
							enemy.TakeDamage(this.mainCharacter.Attack);

							// Check if enemy died
							if (enemy.Health <= 0)
								this.entities.Remove(enemy);

							// Otherwise, enemy attacks main character
							else
								this.mainCharacter.TakeDamage(enemy.Attack);

							// Check if main character died
							if (this.mainCharacter.Health <= 0)
							{
								MessageBox.Show("You died!");
								Application.Exit();
							}
						}
					}
				}
			}
		}
	}
}
