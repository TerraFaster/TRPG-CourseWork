using CourseWork___TRPG.direct2d;
using CourseWork___TRPG.game.models;
using SharpDX.WIC;
using Bitmap = SharpDX.Direct2D1.Bitmap;
using Image = System.Drawing.Image;

namespace CourseWork___TRPG.game
{
	/*
	 * Class that provides methods for loading game resources.
	 */
    public class ResourcesLoader
	{
		/*
		 * Loads a texture from the specified path.
		 */
		public static Image LoadTextureFromPath(string path)
		{
			return Image.FromFile(path);
		}

		/*
		 * Loads a texture from the specified name.
		 */
		public static Image LoadTexture(string textureName)
		{
			return LoadTextureFromPath(
				Path.Combine(
					Config.Resources.TEXTURES_DIRECTORY, 
					Path.ChangeExtension(textureName, Config.Resources.TEXTURE_EXTENSION)
				)
			);
		}

		/*
		 * Loads a tile texture from the specified name.
		 */
		public static Image LoadTileTexture(string tileName)
		{
			return LoadTextureFromPath(
				Path.Combine(
					Config.Resources.TILES_DIRECTORY, 
					Path.ChangeExtension(tileName, Config.Resources.TEXTURE_EXTENSION)
				)
			);
		}

		// SharpDX textures
		/*
		 * Loads a SharpDX texture from the specified path.
		 */
		public static Bitmap LoadDXTextureFromPath(string path)
		{
			EnhancedRenderTarget renderTarget = Direct2D.Instance.RenderTarget;

			ImagingFactory imagingFactory = new ImagingFactory();
			BitmapDecoder bitmapDecoder = new BitmapDecoder(
				imagingFactory, path, 
				DecodeOptions.CacheOnDemand
			);
			BitmapFrameDecode frame = bitmapDecoder.GetFrame(0);
			FormatConverter converter = new FormatConverter(imagingFactory);
			converter.Initialize(frame, SharpDX.WIC.PixelFormat.Format32bppPRGBA);

			return Bitmap.FromWicBitmap(renderTarget, converter);
		}

		/*
		 * Loads a SharpDX texture from the specified name.
		 */
		public static Bitmap LoadDXTexture(string textureName)
		{
			return LoadDXTextureFromPath(
				Path.Combine(
					Config.Resources.TEXTURES_DIRECTORY, 
					Path.ChangeExtension(textureName, Config.Resources.TEXTURE_EXTENSION)
				)
			);
		}

		/*
		 * Loads a SharpDX texture from the specified tile name.
		 */
		public static Bitmap LoadTileDXTexture(string tileName)
		{
			return LoadDXTextureFromPath(
				Path.Combine(
					Config.Resources.TILES_DIRECTORY, 
					Path.ChangeExtension(tileName, Config.Resources.TEXTURE_EXTENSION)
				)
			);
		}

		/*
		 * Loads a SharpDX texture from the specified entity name.
		 */
		public static Bitmap LoadEntityDXTexture(string entityName)
		{
			return LoadDXTextureFromPath(
				Path.Combine(
					Config.Resources.ENTITIES_DIRECTORY, 
					Path.ChangeExtension(entityName, Config.Resources.TEXTURE_EXTENSION)
				)
			);
		}

		// Game objects
		/*
		 * Loads a tile from the specified path.
		 */
		public static Tile LoadTile(string tileName)
		{
			return Tile.LoadFromFile(
				Path.Combine(
					Config.Resources.TILES_DIRECTORY, 
					Path.ChangeExtension(tileName, Config.Resources.TILE_EXTENSION)
				)
			);
		}

		/*
		 * Loads an entity from the specified path.
		 */
		public static Map LoadMap(string mapName)
		{
			return Map.LoadFromFile(
				Path.Combine(
					Config.Resources.MAPS_DIRECTORY, 
					Path.ChangeExtension(mapName, Config.Resources.MAP_EXTENSION)
				)
			);
		}
	}
}
