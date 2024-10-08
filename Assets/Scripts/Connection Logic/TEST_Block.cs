using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Blocks;
using Cells;
using Testing; // custom test battery

// This test class written by Leif Larson
// Last modified 10/8/2024.
// Previous result: Block SUCCEEDED its tests on 10/8/2024.

public class TEST_Block : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        // initialize the testing battery
        TestBattery pcf = new(
            "Part cell functionality",
            "PCF", 
            Verbosity.ALL
            );
        // setup the lookup table for the part
        LookupTable<Cell> part = new(2, 2);
        // populate the lookup table
        for (int i_x = 0; i_x < part.x_size; i_x++)
        {
            for (int i_y = 0; i_y < part.y_size; i_y++)
            {
                part.Put(i_x, i_y, new Cell(new Vector2(i_x, i_y)));
            }
        }
        // setup the lookup table for the world
        LookupTable<Cell> world = new(3, 3);
        // populate the lookup table
        for (int i_x = 0; i_x < world.x_size; i_x++)
        {
            for (int i_y = 0; i_y < world.y_size; i_y++)
            {
                world.Put(i_x, i_y, new Cell(new Vector2(i_x, i_y)));
            }
        }
        // run the tests, creating blocks as needed

        // GetPartCell()
        Vector2 b1_pos = new(0, 0);
        Block b1 = new(part.Get(b1_pos)); // make this part think it's from 0,0 in the part.
                                          // The part has no idea but we don't mind that yet.
                                          // Mobility of the block itself is supposed to be handled by cells later.
                                          // So we'll handle it in TEST_Cell.cs.
        pcf.Test("GetPartCell() [should be IDENTICAL!]", b1.GetPartCell(), part.Get(b1_pos));
        // SetPartCell()
        Vector2 b1_second_pos = new(1, 1);
        b1.SetPartCell(part.Get(b1_second_pos));
        pcf.Test("SetPartCell() [should be IDENTICAL!]", b1.GetPartCell(), part.Get(b1_second_pos));

        // read out the tests
        Debug.Log(pcf.GetRecord());
        Debug.Log("should have 2 identicals: \n" + pcf.GetSummary());

        TestBattery wcf = new(
            "World cell functionality",
            "WCF",
            Verbosity.ALL
            );

        // checking initial state
        wcf.Test("initially in-play?", false, b1.IsInPlay());

        // trying to pull out the default world cell.
        wcf.Test("GetCell(), getting initial world cell [Should be identical!]", null, b1.GetCell());

        // setting the world cell
        Vector2 b1_world_pos = new(1, 2);
        b1.SetCell(world.Get(b1_world_pos));
        wcf.Test("SetCell(), GetCell() [Should be identical!]", world.Get(b1_world_pos), b1.GetCell());

        // Checking in-play status with a world cell
        wcf.Test("finally in-play?", true, b1.IsInPlay());

        // Checking in-play status after pulling it back out of play
        b1.SetCell(null);
        wcf.Test("OOP, SetCell(), GetCell() with no cell! [Should be identical!]", null, b1.GetCell());
        wcf.Test("ending in play?", false, b1.IsInPlay());

        Debug.Log(wcf.GetRecord());
        Debug.Log("Should be 3 identicals, 3 passes: \n" + wcf.GetSummary());
    }
}
