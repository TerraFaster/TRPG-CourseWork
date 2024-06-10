
namespace CourseWork___TRPG.game.models.entities.enemies
{
	/*
	 * Class that represents the Golem enemy.
	 */
	public class Golem : Enemy
	{
		public Golem(
			PointF position, float health, int level, float attack, float defense, float speed
		) : base(
			"Golem", position, 
			ResourcesLoader.LoadEntityDXTexture("golem"), 
			health, level, attack, defense, speed
		)
		{

		}
	}
}
