using CourseWork___TRPG.direct2d;
using CourseWork___TRPG.game.models.entities;
using SharpDX.Direct2D1;
using SharpDX.DirectWrite;
using SharpDX.Mathematics.Interop;

namespace CourseWork___TRPG.game.models
{
	/*
	 * Class that represents a camera in the game world.
	 */
	public class Camera : IMovable
	{
		// Field to store the zoom level of the camera
		private float zoom = 1f;
		// Field to store the movement offset of the camera
		private PointF movementOffset = new PointF();
		// Field to store the resolution offset of the camera
		private PointF resolutionOffset = new PointF();

		public PointF Position { get; set; }
		public float Zoom {
			get => this.zoom;
			set {
				float newZoom = value;

				if (newZoom == 0)
					newZoom = newZoom > this.zoom ? 1 : -1;

				this.AdjustForZoom(newZoom);
			}
		}
		public float ZoomFactor {
			get => zoom > 0 ? zoom : 1f / (1 - zoom);
        }
		public PointF MovementOffset
		{
			get => this.movementOffset;
			set => this.movementOffset = value;
		}
		public PointF ResolutionOffset
		{
			get => this.resolutionOffset;
			set => this.resolutionOffset = value;
		}
		public PointF Offset
		{
			get => new PointF(
				this.movementOffset.X + this.resolutionOffset.X, 
				this.movementOffset.Y + this.resolutionOffset.Y
			);
		}
		public Size Resolution { get; set; }
		public PointF Velocity { get; set; }

		public Camera(PointF position, Size resolution)
		{
			this.Position = position;
			this.Resolution = resolution;

			this.AdjustForResolution(resolution);
		}

		/*
		 * AdjustForResolution method adjusts the camera offset based on the new resolution.
		 */
		public void AdjustForResolution(Size resolution)
		{
			// Calculate the center of the old and new resolutions
			SizeF oldResolutionCenter = new SizeF(
				this.Resolution.Width / 2,
				this.Resolution.Height / 2
			);

			SizeF newResolutionCenter = new SizeF(
				resolution.Width / 2,
				resolution.Height / 2
			);

			// Adjust the offset
			if (this.Offset.X == 0 && this.Offset.Y == 0)
				this.resolutionOffset = newResolutionCenter.ToPointF();

			else
			{
				this.resolutionOffset -= oldResolutionCenter;
				this.resolutionOffset += newResolutionCenter;
			}

			this.Resolution = resolution;
		}

		/*
		 * AdjustForZoom method adjusts the camera offset based on the new zoom level.
		 */
		public void AdjustForZoom(float newZoom)
		{
			this.zoom = newZoom;
		}

		/*
		 * Render method renders the camera view of the map.
		 */
		public void Render(EnhancedRenderTarget renderTarget, Map map)
		{
			int tileSize = Config.Renderer.TEXTURE_SIZE;

			// Compute zoom factor
			float zoomFactor = this.ZoomFactor;

			// Adjust tile size based on the zoom level
			float adjustedTileSize = tileSize * zoomFactor;

			// Draw map
			for (int layerNumber = 0; layerNumber < map.Layers.Count; layerNumber++)
			{
				List<MapTileData> layer = map.Layers[layerNumber];

				for (int i = 0; i < layer.Count; i++)
				{
					MapTileData tile = layer[i];

					// Calculate the tile position based on the camera offset and zoom level
					PointF tilePosition = new PointF(
						(tile.Position.X * tileSize + this.Offset.X) * zoomFactor,
						(tile.Position.Y * tileSize + this.Offset.Y) * zoomFactor
					);

					// Draw the tile with the adjusted size
					renderTarget.DrawBitmap(
						TileManager.Instance.GetTile(tile.Name).Texture,
						new RawRectangleF(
							tilePosition.X, tilePosition.Y,
							tilePosition.X + adjustedTileSize, tilePosition.Y + adjustedTileSize
						),
						1f,
						BitmapInterpolationMode.Linear
					);
				}
			}

			// Draw grid
			if (Config.Renderer.DRAW_GRID)
			{
				using (var brush = new SolidColorBrush(renderTarget, Config.Renderer.GRID_BRUSH_COLOR))
				{
					// Adjust grid line spacing based on the zoom level
					float adjustedTileSizeF = adjustedTileSize;

					// Draw horizontal grid lines
					for (float y = (this.Offset.Y * zoomFactor) % adjustedTileSizeF; y < this.Resolution.Height; y += adjustedTileSizeF)
					{
						renderTarget.DrawLine(
							new RawVector2(0, y),
							new RawVector2(this.Resolution.Width, y),
							brush
						);
					}

					// Draw vertical grid lines
					for (float x = (this.Offset.X * zoomFactor) % adjustedTileSizeF; x < this.Resolution.Width; x += adjustedTileSizeF)
					{
						renderTarget.DrawLine(
							new RawVector2(x, 0),
							new RawVector2(x, this.Resolution.Height),
							brush
						);
					}
				}
			}

			// Draw map center
			if (Config.Renderer.DRAW_MAP_CENTER)
			{
				using (var brush = new SolidColorBrush(renderTarget, Config.Renderer.GRID_BRUSH_COLOR))
				{
					renderTarget.FillEllipse(
						new Ellipse(
							new RawVector2(
								this.Offset.X * zoomFactor, this.Offset.Y * zoomFactor
							),
							5 * zoomFactor, 5 * zoomFactor
						),
						brush
					);
				}
			}
		}

		/*
		 * Move method moves the camera by the specified amount.
		 */
		public void Move(float dx, float dy)
		{
			this.Position += new SizeF(dx, dy);
			this.MovementOffset -= new SizeF(dx, dy);
		}

		/*
		 * Move method moves the camera by its velocity.
		 */
		public void Move()
		{
			this.Position = new PointF(
				this.Position.X + this.Velocity.X, 
				this.Position.Y + this.Velocity.Y
			);

			this.MovementOffset = new PointF(
				this.MovementOffset.X - this.Velocity.X, 
				this.MovementOffset.Y - this.Velocity.Y
			);
		}

		/*
		 * MoveTo method moves the camera to the specified position.
		 */
		public void MoveTo(PointF position)
		{
			this.Position = position;
			this.MovementOffset = new PointF(
				-position.X, 
				-position.Y
			);
		}

		/*
		 * Update method updates the camera.
		 */
		public void Update()
		{
			this.Move();
		}
	}

	/*
	 * GameCamera class represents a camera in the game world.
	 */
	internal class GameCamera(
		PointF position, Size resolution
	) : Camera(position, resolution)
	{
		/*
		 * Render method renders the camera view of the map and entities.
		 */
		public void Render(EnhancedRenderTarget renderTarget, Map map, List<LivingEntity> entities)
		{
			base.Render(renderTarget, map);

			int tileSize = Config.Renderer.TEXTURE_SIZE;

			// Draw entities
			for (int i = 0; i < entities.Count; i++)
			{
				LivingEntity entity = entities[i];

				PointF entityPosition = new PointF(
					(entity.Position.X * tileSize + this.Offset.X) * this.ZoomFactor, 
					(entity.Position.Y * tileSize + this.Offset.Y) * this.ZoomFactor
				);

				renderTarget.DrawBitmap(
					entity.Texture, 
					new RawRectangleF(
						entityPosition.X, entityPosition.Y, 
						entityPosition.X + tileSize * this.ZoomFactor, 
						entityPosition.Y + tileSize * this.ZoomFactor
					), 
					1f, 
					BitmapInterpolationMode.Linear
				);

				// Draw health bar
				float healthBarWidth = tileSize * this.ZoomFactor;
				float healthBarHeight = 7.5f * this.ZoomFactor;

				renderTarget.FillRectangle(
					new RawRectangleF(
						entityPosition.X, entityPosition.Y - healthBarHeight, 
						entityPosition.X + healthBarWidth, entityPosition.Y
					), 
					new SolidColorBrush(renderTarget, new RawColor4(1, 0, 0, 1))
				);

				renderTarget.FillRectangle(
					new RawRectangleF(
						entityPosition.X, entityPosition.Y - healthBarHeight, 
						entityPosition.X + healthBarWidth * entity.Health / entity.MaxHealth, 
						entityPosition.Y
					), 
					new SolidColorBrush(renderTarget, new RawColor4(0, 1, 0, 1))
				);

				// Draw entity information text
				TextFormat entityInformationTextFormat = new TextFormat(new SharpDX.DirectWrite.Factory(), "Arial", 14);
				entityInformationTextFormat.TextAlignment = TextAlignment.Center;

				string entityInformationText = (
					$"[Lvl. {entity.Level}] {entity.Name}" + 
					$"\n{entity.Health}/{entity.MaxHealth} HP"
				);

				TextMetrics entityInformationTextMetrics = renderTarget.MeasureText(
					entityInformationText, entityInformationTextFormat
				);

				renderTarget.DrawText(
					entityInformationText, 
					entityInformationTextFormat, 
					new RawColor4(1, 1, 1, 1), 
					new PointF(
						entityPosition.X + (healthBarWidth - entityInformationTextMetrics.Width) / 2, 
						entityPosition.Y - healthBarHeight - entityInformationTextMetrics.Height
					)
				);
			}
		}

		/*
		 * Update method updates the camera.
		 */
		public void Update(MainCharacter mainCharacter)
		{
			PointF newPosition = new PointF(
				mainCharacter.Position.X * Config.Renderer.TEXTURE_SIZE, 
				mainCharacter.Position.Y * Config.Renderer.TEXTURE_SIZE
			);

			this.Position = newPosition;
			this.MovementOffset = new PointF(
				-newPosition.X - Config.Renderer.TEXTURE_SIZE / 2, 
				-newPosition.Y - Config.Renderer.TEXTURE_SIZE / 2
			);
		}
	}
}