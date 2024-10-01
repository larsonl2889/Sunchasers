using System;
//using UnityEngine;

/// <summary>
/// An enum for handling the 4 cardinal directions efficiently.
/// <br></br>Is usefuil for interfacing between the floats used by Unity's GameObjects for angles, and the coordinate system used by the LookupTable. 
/// <br></br>Use the DirectionOps utility class to access the operations.
/// </summary>
public enum Direction : byte
{
    UP = 0, LEFT = 1, DOWN = 2, RIGHT = 3
}

namespace DirectionOps
{
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
            return id switch
            {
                0 => Direction.UP,
                1 => Direction.LEFT,
                2 => Direction.DOWN,
                _ => Direction.RIGHT,
            };
        }

        /// <summary>
        /// Given an angle, returns the appropriate Direction.
        /// </summary>
        /// <param name="angle">angle in degrees</param>
        /// <returns>the appropriate Direction</returns>
        public static Direction ToDirection(this float angle)
        {
            // normalize the angle to the interval [0.0f, 360.0f).
            angle %= 360.0f;
            // bifurcated into two paths for performance. See included diagram.
            if (angle > 225.0f)
            {
                if (angle <= 315.0f) { return Direction.LEFT; }
            }
            else
            {
                if (angle > 135.0f) { return Direction.DOWN; }
                else if (angle > 45.0f) { return Direction.RIGHT; }
            }
            // Else is UP
            return Direction.UP;

        }

        public static float GetId(this Direction dir)
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
        public static float GetAngle(this Direction dir)
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
            return ToDirection(( (int)dir1 + (int)dir2 ) % 4);
        }
/*
        // TODO make the design orbit around this as reference vectors.
        public static Vector2 getVector(this Direction dir)
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

        public static Vector2 applyRotation(this Direction dir, Vector2 vec)
        {
            return dir switch
            {
                Direction.UP => vec,
                Direction.LEFT => new Vector2(vec.y, -vec.x), // TODO test!
                Direction.DOWN => -vec,
                // RIGHT
                _ => new Vector2(-vec.y, vec.x), // TODO test!
            };
        }*/
    }


}

