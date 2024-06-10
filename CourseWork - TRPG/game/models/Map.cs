using System.Text.Json;

namespace CourseWork___TRPG.game.models
{
	/*
	 * Class that represents a tile data in a map.
	 */
	public class MapTileData
	{
		public string Name { get; set; }
		public Point Position { get; set; }
	}

	/*
	 * Class that represents a map data.
	 */
	public class MapData
	{
		public string Name { get; set; }
		public Dictionary<int, List<MapTileData>> Layers { get; set; }
		public Point SpawnPoint { get; set; }
	}

	/*
	 * Class that represents a map in the game.
	 */
	public class Map
	{
		// Field to store the name of the map.
		private string name;
		// Field to store the layers of the map.
		private Dictionary<int, List<MapTileData>> layers;
		// Field to store the spawn point of the map.
		private Point spawnPoint;

		public string Name
		{ get { return name; } }

		public Dictionary<int, List<MapTileData>> Layers
		{ get { return layers; } }

		public Point SpawnPoint
		{ get { return this.spawnPoint; } }

		public Map(string name, Dictionary<int, List<MapTileData>> layers, Point spawnPoint)
		{
			this.name = name;
			this.layers = layers;
			this.spawnPoint = spawnPoint;
		}

		public Map(string name)
		{
			this.name = name;
			this.layers = new Dictionary<int, List<MapTileData>>();
			this.spawnPoint = new Point(0, 0);
		}

		/*
		 * GetTile method returns the tile at the given position in the given layer.
		 */
		public Tile? GetTile(int layerNumber, Point position)
		{
			if (layers.ContainsKey(layerNumber))
			{
				var tileData = layers[layerNumber].Find(tile => tile.Position == position);

				if (tileData != null)
				{
					return TileManager.Instance.GetTile(tileData.Name);
				}
			}

			return null;
		}

		/*
		 * SetTile method sets the tile by its name at the given position in the given layer.
		 */
		public void SetTile(int layerNumber, string tileName, Point position)
		{
			// Create the layer if it doesn't exist
			if (!layers.ContainsKey(layerNumber))
				layers[layerNumber] = new List<MapTileData>();

			// Remove the tile if it already exists
			this.RemoveTile(layerNumber, position);

			layers[layerNumber].Add(new MapTileData { Name = tileName, Position = position });
		}

		/*
		 * SetTile method sets the tile at the given position in the given layer.
		 */
		public void SetTile(int layerNumber, Tile tile, Point position)
		{
			this.SetTile(layerNumber, tile.Name, position);
		}

		/*
		 * RemoveTile method removes the tile at the given position in the given layer.
		 */
		public void RemoveTile(int layerNumber, Point position)
		{
			// Check if the layer exists
			if (!layers.ContainsKey(layerNumber))
				return;

			layers[layerNumber].RemoveAll(tile => tile.Position == position);
		}

		/*
		 * SetSpawnPoint method sets the spawn point of the map.
		 */
		public void SetSpawnPoint(Point position)
		{
			this.spawnPoint = position;
		}

		// Serialization
		/*
		 * Seralize method serializes the map data into a JSON string.
		 */
		public string Serialize()
		{
			var mapData = new
			{
				Name = this.name, 
				Layers = this.layers, 
				SpawnPoint = new { X = this.spawnPoint.X, Y = this.spawnPoint.Y }
			};

			var options = new JsonSerializerOptions
			{
				WriteIndented = true
			};

			return JsonSerializer.Serialize(mapData, options);
		}

		/*
		 * Deserialize method deserializes the map data from a JSON string.
		 */
		public static Map Deserialize(string data)
		{
			var mapData = JsonSerializer.Deserialize<MapData>(data);

			if (mapData != null)
			{
				return new Map(
					name: mapData.Name, 
					layers: mapData.Layers, 
					spawnPoint: mapData.SpawnPoint
				);
			}
			else
			{
				throw new Exception("Failed to deserialize tile data.");
			}
		}

		/*
		 * SaveToFile method saves the map data to a file.
		 */
		public void SaveToFile(string path)
		{
			File.WriteAllText(path, this.Serialize());
		}

		/*
		 * LoadFromFile method loads the map data from a file.
		 */
		public static Map LoadFromFile(string path)
		{
			return Deserialize(File.ReadAllText(path));
		}
	}
}
