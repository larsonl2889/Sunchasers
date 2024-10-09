using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Blocks;
using Cells;
using Testing; // custom test battery

// This test class written by Leif Larson
// Last modified 10/9/2024.
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
        Block b1 = new(part.Get(b1_pos)); 
        pcf.Test("GetCell() [should be IDENTICAL!]", b1.GetCell(), part.Get(b1_pos));
        // SetPartCell()
        Vector2 b1_second_pos = new(1, 1);
        b1.SetCell(part.Get(b1_second_pos));
        pcf.Test("SetCell() [should be IDENTICAL!]", b1.GetCell(), part.Get(b1_second_pos));

        // read out the tests
        Debug.Log(pcf.GetRecord());
        Debug.Log("should have 2 identicals: \n" + pcf.GetSummary());

        
    }
}
