using Bitmap = SharpDX.Direct2D1.Bitmap;

namespace CourseWork___TRPG.game.models.entities
{
    /*
     * Class that represents an enemy entity in the game
     */
    public class Enemy : LivingEntity
    {
		public Enemy(
            string name, PointF position, Bitmap texture, 
            float health, int level, float attack, float defense, float speed
        ) : base(name, position, texture, health, level, attack, defense, speed)
		{
		}
    }
}
