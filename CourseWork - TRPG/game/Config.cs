using SharpDX.Mathematics.Interop;

namespace CourseWork___TRPG.game
{
	/*
	 * Class that contains all the configuration values for the game.
	 */
	public class Config
	{
		public static class Resources
		{
			public const string RESOURCES_DIRECTORY = "resources/";

			public const string TEXTURES_DIRECTORY = RESOURCES_DIRECTORY + "textures/";
			public const string TILES_DIRECTORY = TEXTURES_DIRECTORY + "tiles/";
			public const string ENTITIES_DIRECTORY = TEXTURES_DIRECTORY + "entities/";
			public const string MAPS_DIRECTORY = RESOURCES_DIRECTORY + "maps/";

			public const string TEXTURE_EXTENSION = ".png";
			public const string TILE_EXTENSION = ".json";
			public const string ENTITY_EXTENSION = ".json";
			public const string MAP_EXTENSION = ".tmap";
		}

		public static class Game
		{
			public static int UPDATE_RATE = 60;
			public static int FRAME_RATE = 75;
		}

		public static class Renderer
		{
			public const int TEXTURE_SIZE = 64;

			public static bool DRAW_MAP_CENTER = true;
			public static bool DRAW_GRID = false;
			public static RawColor4 GRID_BRUSH_COLOR = new RawColor4(1, 1, 1, 0.5f);
		}

		public static class MainCharacter
		{
			public const int BASE_HEALTH = 100;
			public const int BASE_LEVEL = 1;
			public const int BASE_ATTACK = 15;
			public const int BASE_DEFENSE = 5;
			public const int BASE_SPEED = 5;

			public const int BASE_EXPERIENCE_FOR_LEVEL = 100;
			public const double LEVEL_GROWTH_FACTOR = 1.5;

			public const float MOVEMENT_SPEED = 0.05f;
		}

		public static class MapEditor
		{
			public static int CAMERA_MOVEMENT_VELOCITY = 15;

			public static bool REPLACE_TILE_MODE = false;
			public static bool SETTINGS_FORM_TOPMOST = true;
		}
	}
}
