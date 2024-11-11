using System;
using System.Collections.Generic;
using System.Reflection;
using DirectionOps;

public static class PipeIndex
{
    // internal indecies
    /*  0=none   1=u     2=l     3=ul
     *  4=d      5=ud    6=ld    7=uld
     *  8=r      9=ur   10=lr   11=ulr
     * 12=dr    13=udr  14=ldr  15=uldr
     */
    // Actual indecies depend on the order of the sprites in the sprite sheet.



    /// <summary>
    /// Given the directions the pipe faces, returns the index in the sprite sheet.
    /// </summary>
    /// <param name="dirs"></param>
    /// <returns></returns>
    public static int DirectionsToIndex(List<Direction> dirs)
    {
        // TODO implement this stub!
        return -1;
    }

    /// <summary>
    /// Convert from one pipe's internal index to the next pipe's internal index, given a rotation dir. 
    /// Because I'm fancy like that.
    /// </summary>
    /// <param name="internalIndex">starting index</param>
    /// <param name="dir">direction to rotate to</param>
    /// <returns>The new pipe's internal index</returns>
    internal static int ApplyRotation(this int internalIndex, Direction dir)
    {
        if (dir == Direction.LEFT)
        {
            return internalIndex < 8 ? internalIndex * 2 : (internalIndex * 2 + 1) % 16;
        }
        if (dir == Direction.RIGHT)
        {
            //return internalIndex % 2 == 1 ? internalIndex * 2 - 1 % 16;
        }
        return -1; // TODO implement! See notes.
    }

    internal static int DirectionsToInternalIndex(List<Direction> dirs)
    {
        int index = 0;
        if (dirs.Contains(Direction.UP))
        {
            index += 1;
        }
        if (dirs.Contains(Direction.LEFT))
        {
            index += 2;
        }
        if (dirs.Contains(Direction.DOWN))
        {
            index += 4;
        }
        if (dirs.Contains(Direction.RIGHT))
        {
            index += 8;
        }
        return index;
    }

    // TODO OMG PLEASE TEST THIS WHAT DOES << EVEN DO AND WHY AM I SO SURE OF MYSELF?!
    internal static List<Direction> InternalIndexToDirections(int index)
    {
        List<Direction> li = new();
        if (index % 2 == 1)
        {
            li.Add(Direction.UP);
        }
        if ((index << 1) % 2 == 1)
        {
            li.Add(Direction.LEFT);
        }
        if ((index << 2) % 2 == 1)
        {
            li.Add(Direction.DOWN);
        }
        if ((index << 3) % 2 == 1)
        {
            li.Add(Direction.RIGHT);
        }
        return li;

    }

    
}



