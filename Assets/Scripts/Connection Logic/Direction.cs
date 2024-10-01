using System;
using System.Diagnostics;

public enum Direction : byte
{
    UP = 0, LEFT = 1, DOWN = 2, RIGHT = 3
}


// TODO check each angle to make sure it's appropriate!
public static class DirectionOps
{
    /// <summary>
    /// Given the id, returns the Direction
    /// </summary>
    /// <param name="id">the int Id</param>
    /// <returns>the Direction</returns>
    public static Direction fromId(int id)
    {
        switch {
            case 0:
                return Direction.UP;
            case 1:
                return Direction.LEFT;
            case 2:
                return Direction.DOWN
            default:
                return Direction.RIGHT;
        }
    }

    /// <summary>
    /// Given an angle, returns the appropriate Direction.
    /// </summary>
    /// <param name="angle">angle in degrees</param>
    /// <returns>the appropriate Direction</returns>
    public static Direction findDirection(float angle)
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

    /// <summary>
    /// Given the direction as an enum, gives the angle in degrees.
    /// </summary>
    /// <param name="dir">the Direction enum</param>
    /// <returns>angle in degrees.</returns>
    public static float getAngle(this Direction dir)
    {
        switch {
            case Direction.UP:
                return 0.0f;
            case Direction.LEFT:
                return 90.0f;
            case Direction.DOWN:
                return 180.0f;
            // RIGHT
            default:
                return 270.0f;
        }
    }

    /// <summary>
    /// Adds two directions together. Good for adding rotations together.
    /// </summary>
    /// <param name="dir1">The first direction. Order doesn't matter.</param>
    /// <param name="dir2">The second direction. Order doesn't matter.</param>
    /// <returns>the resultant Direction</returns>
    public static Direction add(this Direction dir1, Direction dir2)
    {
        return fromId((dir1 + dir2) % 4);
    }

    public static Vector2 getVector(this Direction dir)
    {
        switch {
            case Direction.UP:
                return Vector2.up;
            case Direction.LEFT:
                return Vector2.left;
            case Direction.DOWN:
                return Vector2.down;
            // RIGHT
            default:
                return Vector2.right;
        }
    }

    public static Vector2 applyRotation(this Direction dir, Vector2 vec)
    {
        switch {
            case Direction.UP:
                return Vector2.up;
            case Direction.LEFT:
                return Vector2.left;
            case Direction.DOWN:
                return Vector2.down;
            // RIGHT
            default:
                return Vector2.right;
        }
    }
}


static int main()
{

}


