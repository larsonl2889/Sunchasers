using System;
using System.Collections.Generic;
using System.Reflection;
using DirectionOps;
using UnityEngine;

public static class PipeIndexer
{
    /// <summary>
    /// The 16-element array of pipe prefab instances, in order of their math indecies.
    /// </summary>
    public static GameObject[] pipes;
    public static List<Sprite[]> pipeSpritesVariations;

    // math indecies
    /*  0=none   1=u     2=l     3=lu
     *  4=d      5=du    6=dl    7=dlu
     *  8=r      9=ru   10=rl   11=rlu
     * 12=rd    13=rdu  14=rdl  15=rdlu
     */
    // Actual indecies depend on the order of the sprites in the sprite sheet.

    internal static void GenerateData(GameObject[] new_pipes, List<Sprite[]> new_pipeSpritesVariations)
    {
        pipes = new_pipes;
        pipeSpritesVariations = new_pipeSpritesVariations;
    }

    internal static Sprite SelectSprite(int mathIndex, int variant=0)
    {
        return pipeSpritesVariations[variant][mathIndex];
    }

    /// <summary>
    /// Create an instance of a pipe, given its math index and sprite variant number.
    /// </summary>
    /// <param name="mathIndex">given math index</param>
    /// <param name="parent">this object's new parent</param>
    /// <param name="variant">sprite variant number</param>
    /// <returns>new instance of the pipe</returns>
    public static GameObject Instantiate(int mathIndex, Transform parent, int variant=0)
    {
        GameObject obj = GameObject.Instantiate(pipes[mathIndex], parent);
        obj.GetComponent<SpriteRenderer>().sprite = SelectSprite(mathIndex, variant);
        return obj;
    }

    /// <summary>
    /// Given the math index, returns the actual index on the sprite sheet.<br>
    /// Returns -1 as a sentinel.
    /// </summary>
    /// <param name="mathIndex">The math index of the </param>
    /// <returns>Sprite index. May return -1 if there is no corresponding pipe.</returns>
    internal static int MathIndexToSpriteIndex(int mathIndex)
    {
        return mathIndex switch
        {
            0 => -1,
            1 => 11,
            2 => 14,
            3 => 6,
            4 => 13,
            5 => 10,
            6 => 9,
            7 => 5,
            8 => 12,
            9 => 7,
            10=> 0,
            11=> 2,
            12=> 8,
            13=> 4,
            14=> 3,
            15=> 1,
            _ => -1 // default
        };
        
    }

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
    /// Convert from one pipe's math index to the next pipe's math index, given a rotation dir.<br>
    /// Because I'm fancy like that.
    /// </summary>
    /// <param name="mathIndex">starting index</param>
    /// <param name="dir">direction to rotate to</param>
    /// <returns>The new pipe's math index</returns>
    internal static int ApplyRotation(this int mathIndex, Direction dir)
    {
        int rotationId = dir.ToId();
        int greatestAddend;
        int leastAddend = mathIndex >> rotationId;
        if (rotationId == 0)
        {
            greatestAddend = 0;
        }
        else
        {
            greatestAddend = (1 << (4 - rotationId)) * (mathIndex % (1 << rotationId));
        }
        return greatestAddend + leastAddend;
    }

    /// <summary>
    /// Converts from a List of Directions to the math index of the pipe.
    /// </summary>
    /// <param name="dirs">List of directions</param>
    /// <returns>math index of the pipe</returns>
    internal static int DirectionsToMathIndex(List<Direction> dirs)
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

    internal static List<Direction> MathIndexToDirections(int index)
    {
        List<Direction> li = new();
        if (index % 2 == 1)
        {
            li.Add(Direction.UP);
        }
        if ((index >> 1) % 2 == 1)
        {
            li.Add(Direction.LEFT);
        }
        if ((index >> 2) % 2 == 1)
        {
            li.Add(Direction.DOWN);
        }
        if ((index >> 3) % 2 == 1)
        {
            li.Add(Direction.RIGHT);
        }
        return li;

    }

    
}



