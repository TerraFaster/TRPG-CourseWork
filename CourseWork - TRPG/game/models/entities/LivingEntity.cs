using Bitmap = SharpDX.Direct2D1.Bitmap;

namespace CourseWork___TRPG.game.models.entities
{
    /*
     * Class that represents a living entity in the game.
     */
	public class LivingEntity : Entity
    {
        // Field to store the health of the entity.
        protected float health;
        // Field to store the maximum health of the entity.
        protected float maxHealth;
        // Field to store the level of the entity.
        protected int level;
        // Field to store the attack of the entity.
        protected float attack;
        // Field to store the defense of the entity.
        protected float defense;
        // Field to store the speed of the entity.
        protected float speed;

		public float Health
        { get => health; }

        public float MaxHealth
        { get => maxHealth; }

        public float Attack
        { get => attack; }

        public float Defense
        { get => defense; }

        public float Speed
        { get => speed; }

        public int Level
        { get => level; }

		public LivingEntity(
            string name, PointF position, Bitmap texture, 
            float health, int level, float attack, float defense, float speed
        ) : base(name, position, texture)
		{
            this.health = health;
			this.maxHealth = health;
			this.level = level;
			this.attack = attack;
			this.defense = defense;
			this.speed = speed;
		}

        /*
         * TakeDamage method that reduces the health of the entity by the given amount.
         */
		public void TakeDamage(float damage)
        {
            health -= damage - defense;

            if (health < 0)
                health = 0;
        }

        /*
         * Heal method that increases the health of the entity by the given amount.
         */
        public void Heal(float amount)
        {
            health += amount;

            if (health > maxHealth)
                health = maxHealth;
        }
    }
}
