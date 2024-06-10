using CourseWork___TRPG.game.models;

namespace CourseWork___TRPG.game
{
	/*
	 * Class that manages all the tiles in the game.
	 */
	public class TileManager
	{
		// Field to store the Singleton instance of the class.
		public static TileManager Instance { get; } = new TileManager();

		// Field to store all the available tiles.
		private Dictionary<string, Tile> tiles;

		public TileManager()
		{
			tiles = new Dictionary<string, Tile>();

			LoadTiles();
		}

		/*
		 * LoadTiles method that loads all the tiles from the resources directory.
		 */
		public void LoadTiles()
		{
			if (Directory.Exists(Config.Resources.TILES_DIRECTORY))
			{
				var jsonFiles = Directory.GetFiles(Config.Resources.TILES_DIRECTORY, "*.json");

				foreach (var file in jsonFiles)
				{
					Tile tile = Tile.LoadFromFile(file);
					tiles[tile.Name] = tile;
				}
			}
		}

		// Getters
		/*
		 * GetTile method that returns a tile by its name.
		 */
		public Tile GetTile(string name)
		{
			if (tiles.TryGetValue(name, out var tile))
				return tile;

			else
			{
				throw new KeyNotFoundException(
					$"The tile with the name '{name}' does not exist."
				);
			}
		}

		/*
		 * GetAllTiles method that returns all the tiles.
		 */
		public Dictionary<string, Tile> GetAllTiles()
		{
			return new Dictionary<string, Tile>(tiles);
		}

		/*
		 * GetTilesByLayer method that returns all the tiles by their layer.
		 */
		public List<Tile> GetTilesByLayer(int layer)
		{
			return tiles.Values.Where(tile => tile.Layer == layer).ToList();
		}
	}
}
