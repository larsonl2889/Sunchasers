using System.Collections;
using System.Collections.Generic;
using Testing;
using UnityEngine;

using Cells;
using Blocks;

// This test class written by Leif Larson
// Last modified 10/9/2024.
// Previous result: All tests succeeded on 10/9/2024.

// Tests are not exhaustive. TODO finish writing tests! Cover ALL functionality!

public class TEST_Cell : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
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
        // run the tests, creating blocks as needed to be shuffled around.

        // initialize the testing battery we'll use
        TestBattery cpf = new(
            "Cell: part functionality",
            "C:PF",
            Verbosity.ALL
            );

        Vector2 b1_ppos = new(0, 1); // b1 part pos
        Vector2 b1_ppos2 = new(2, 2); // b1 second pos in the part

        cpf.Test("Initial, cell_1 is null", null, part.Get(b1_ppos).GetBlock());
        cpf.Test("Initial, cell_1 is empty()", true, part.Get(b1_ppos).IsEmpty());

        // make the block, and put it in the part
        Block b1 = new(part.Get(b1_ppos));
        part.Get(b1_ppos).SetBlock(b1);

        cpf.Test("Making a block, block matches", b1, part.Get(b1_ppos).GetBlock());
        cpf.Test("Checking if cell is full: ", false, part.Get(b1_ppos).IsEmpty());

        // TODO test the blocks in parts exhaustively!

        Debug.Log(cpf.GetRecord());
        Debug.Log(cpf.GetSummary());

    }
}
