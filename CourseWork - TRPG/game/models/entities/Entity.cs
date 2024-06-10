using Bitmap = SharpDX.Direct2D1.Bitmap;

namespace CourseWork___TRPG.game.models.entities
{
    /*
     * Class that represents an entity in the game world.
     */
	public class Entity
    {
        // Field that stores the name of the entity.
        protected string name;
        // Field that stores the position of the entity.
        protected PointF position;
        // Field that stores the texture of the entity.
        protected Bitmap texture;

        public string Name
        { get => name; }

        public PointF Position
        { get => position; }

        public Bitmap Texture
        { get => texture; }

		public Entity(string name, PointF position, Bitmap texture)
        {
            this.name = name;
            this.position = position;
            this.texture = texture;
        }

        /*
         * DistanceTo method calculates the distance between this entity and another entity.
         */
        public float DistanceTo(Entity entity)
        {
			return (float)Math.Sqrt(
                Math.Pow(this.position.X - entity.position.X, 2) + 
                Math.Pow(this.position.Y - entity.position.Y, 2)
            );
		}

        /*
         * DistanceToPoint method calculates the distance between this entity and a point.
         */
        public float DistanceToPoint(PointF position)
        {
            return (float)Math.Sqrt(
                Math.Pow(this.position.X - position.X, 2) + 
                Math.Pow(this.position.Y - position.Y, 2)
            );
        }

        /*
         * Move method moves the entity by the specified amount.
         */
        public virtual void Move(float dx, float dy)
        {
            this.position.X += dx;
			this.position.Y += dy;
		}

        /*
         * MoveTo method moves the entity to the specified position.
         */
        public virtual void MoveTo(PointF position)
        {
			this.position = position;
        }

        /*
         * Update method updates the entity.
         */
        public virtual void Update()
        {

		}
    }
}
