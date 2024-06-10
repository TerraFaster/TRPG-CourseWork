using System.Text.Json;
using Bitmap = SharpDX.Direct2D1.Bitmap;

namespace CourseWork___TRPG.game.models
{
	/*
	 * Class that represents the data of a tile.
	 */
	public class TileData
	{
		public string Name { get; set; }
		public string TextureName { get; set; }
		public int Layer { get; set; }
		public bool IsCollidable { get; set; }
	}

	/*
	 * Class that represents a tile in the game world.
	 */
	public class Tile
	{
		// Field to store the name of the tile.
		private string name;
		// Field to store the name of the texture of the tile.
		private string textureName;
		// Field to store the layer of the tile.
		private int layer;
		// Field to store whether the tile is collidable.
		private bool isCollidable;
		// Field to store the texture of the tile.
		private Bitmap texture;

		public string Name
		{ get => name; set => name = value; }

		public string TextureName
		{ get => textureName; set => textureName = value; }

		public int Layer
		{ get => layer; set => layer = value; }

		public bool IsCollidable
		{ get => isCollidable; set => isCollidable = value; }

		public Bitmap Texture
		{ get => texture; set => texture = value; }

		public Tile(string name, string textureName, int layer, bool isCollidable)
		{
			this.name = name;
			this.textureName = textureName;
			this.layer = layer;
			this.isCollidable = isCollidable;

			if (textureName != null)
				this.texture = ResourcesLoader.LoadTileDXTexture(textureName);
		}

		// Serialization
		/*
		 * Serialize method returns a JSON string representation of the tile.
		 */
		public string Serialize()
		{
			var tileData = new
			{
				Name = this.name, 
				TextureName = this.textureName, 
				Layer = this.layer, 
				IsCollidable = this.isCollidable
			};

			var options = new JsonSerializerOptions
			{
				WriteIndented = true
			};

			return JsonSerializer.Serialize(tileData, options);
		}

		/*
		 * Deserialize method returns a Tile object from a JSON string.
		 */
		public static Tile Deserialize(string data)
		{
			var tileData = JsonSerializer.Deserialize<TileData>(data);

			if (tileData != null)
			{
				return new Tile(
					name: tileData.Name, 
					textureName: tileData.TextureName, 
					layer: tileData.Layer, 
					isCollidable: tileData.IsCollidable
				);
			}
			else
			{
				throw new Exception("Failed to deserialize tile data.");
			}
		}

		/*
		 * SaveToFile method saves the tile to a file.
		 */
		public void SaveToFile(string path)
		{
			File.WriteAllText(path, this.Serialize());
		}

		/*
		 * LoadFromFile method loads a tile from a file.
		 */
		public static Tile LoadFromFile(string path)
		{
			return Deserialize(File.ReadAllText(path));
		}
	}
}
