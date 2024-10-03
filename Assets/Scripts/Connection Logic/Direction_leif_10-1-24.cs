using System;
using UnityEngine;

// created by Leif Larson
// Last updated 3 Oct 2024


// Contains Direction and DirectionOps
namespace DirectionOps
{
    /// <summary>
    /// An enum for handling the 4 cardinal directions efficiently.
    /// <br></br>Is usefuil for interfacing between the floats used by Unity's GameObjects for angles, and the coordinate system used by the LookupTable. 
    /// <br></br>Use the DirectionOps utility class to access the operations.
    /// </summary>
    public enum Direction : int
    {
        UP = 0, LEFT = 1, DOWN = 2, RIGHT = 3
    }

    // TODO check each angle to make sure it's appropriate!
    // TODO test!
    /// <summary>
    /// A utility class for performing operations on and with Direction objects.
    /// </summary>
    public static class DirectionOps
    {

        public static string ToString(this Direction dir)
        {
            return dir switch
            {
                Direction.UP => "UP",
                Direction.LEFT => "LEFT",
                Direction.DOWN => "DOWN",
                // RIGHT
                _ => "RIGHT",
            };
        }

        /// <summary>
        /// Given the id, returns the Direction
        /// </summary>
        /// <param name="id">the int Id</param>
        /// <returns>the Direction</returns>
        public static Direction ToDirection(this int id)
        {
            id %= 4;
            return id switch
            {
                0 => Direction.UP,
                1 => Direction.LEFT,
                2 => Direction.DOWN,
                _ => Direction.RIGHT,
            };
        }

        /// <summary>
        /// Converts the direction to its int id
        /// </summary>
        /// <param name="dir">this direction</param>
        /// <returns>the relevant id</returns>
        public static int ToId(this Direction dir)
        {
            return dir switch
            {
                Direction.UP => 0,
                Direction.LEFT => 1,
                Direction.DOWN => 2,
                // RIGHT
                _ => 3,
            };
        }

        /// <summary>
        /// Given the direction as an enum, gives the angle in degrees.
        /// </summary>
        /// <param name="dir">the Direction enum</param>
        /// <returns>angle in degrees.</returns>
        public static float ToAngle(this Direction dir)
        {
            return dir switch
            {
                Direction.UP => 0.0f,
                Direction.LEFT => 90.0f,
                Direction.DOWN => 180.0f,
                // RIGHT
                _ => 270.0f,
            };
        }

        /// <summary>
        /// Adds two directions together. Good for adding rotations together.
        /// </summary>
        /// <param name="dir1">The first direction. Order doesn't matter.</param>
        /// <param name="dir2">The second direction. Order doesn't matter.</param>
        /// <returns>the resultant Direction</returns>
        public static Direction Add(this Direction dir1, Direction dir2)
        {
            return ToDirection(( dir1.ToId() + dir2.ToId() ) % 4);
        }

        /// <summary>
        /// Converts the direction to a vector
        /// </summary>
        /// <param name="dir">the direction</param>
        /// <returns></returns>
        public static Vector2 ToVector(this Direction dir)
        {
            return dir switch
            {
                Direction.UP => Vector2.up,
                Direction.LEFT => Vector2.left,
                Direction.DOWN => Vector2.down,
                // RIGHT
                _ => Vector2.right,
            };
        }

        /// <summary>
        /// Applies the direction as a rotation upon a given vector.
        /// </summary>
        /// <param name="dir">this direction</param>
        /// <param name="vec">the subject vector</param>
        /// <returns>the rotated vector</returns>
        public static Vector2 ApplyRotation(this Direction dir, Vector2 vec)
        {
            return dir switch
            {
                Direction.UP => vec,
                Direction.LEFT => new Vector2(-vec.y, vec.x),
                Direction.DOWN => -vec,
                // RIGHT
                _ => new Vector2(vec.y, -vec.x),
            };
        }
    }


}

