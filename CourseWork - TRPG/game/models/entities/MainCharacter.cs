
namespace CourseWork___TRPG.game.models.entities
{
	/*
	 * Class that represents the main character of the game.
	 */
	public class MainCharacter(
		string name, PointF position, 
		float health, int level, float attack, float defense, float speed
	) : LivingEntity(
		name, position, 
		ResourcesLoader.LoadDXTexture("main-character"), 
		health, level, attack, defense, speed
	), IMovable
    {
		// Field to store the velocity of the main character.
		public PointF Velocity { get; set; }

		/*
		 * GetNextPosition method calculates the next position of the main character.
		 */
		private PointF GetNextPosition()
		{
			return new PointF(
				this.Position.X + this.Velocity.X, 
				this.Position.Y + this.Velocity.Y
			);
		}

		/*
		 * Move method moves the main character to the next position.
		 */
		public void Move()
		{
			this.position = this.GetNextPosition();
		}

		/*
		 * Update method updates the main character.
		 */
		public override void Update()
		{
			this.Move();
		}
	}
}
