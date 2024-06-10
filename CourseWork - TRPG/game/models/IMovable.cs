
namespace CourseWork___TRPG.game.models
{
    /*
     * Interface for objects that can move
     */
    public interface IMovable
    {
        // Field to store the velocity of the object
        public PointF Velocity { get; set; }

        /*
         * Move method to move the object
         */
        public void Move();
    }
}
