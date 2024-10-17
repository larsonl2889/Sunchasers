using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Testing;
using Parts;
using Cells;
using Blocks;

// Contributors: Leif Larson
// Last updated 10-14-2024
// Previous test result: 10-14-2024 passed all tests.

public class TEST_Part : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //TestBattery cmTs = new("CanMerge()", "Can");
        //TestBattery mTs = new("Merge()", "Mer");
        TestBattery ltTs = new("LookupTable() construction testing", "LTbl");
        TestBattery cfgTs = new("Initial config testing", "Icfg");
        TestBattery gpfTs = new("General part functionality test", "PART");
        TestBattery brefTs = new("Block reference tests", "B_Ref");
        TestBattery iipTs = new("IsInPlay()", "IIP");
        //TestBattery exTs = new("Extract()", "Extr");
        //TestBattery ba_cmTs = new("BuildArea.CanMerge()", "BA_Can");
        //TestBattery ba_mTs = new("BuildArea.Merge()", "BA_Mer");
        TestBattery[] allBatteries =
        {
            iipTs, brefTs, gpfTs, cfgTs, ltTs
        };

        // Putting together the build area
        LookupTable<Cell> worldTable = new(5, 5);
        // TODO finish building the build area!


        // Putting together the part
        LookupTable<Cell> partTable = new(3, 3);
        for (int i_x = 0; i_x < partTable.x_size; i_x++)
        {
            for (int i_y = 0; i_y < partTable.y_size; i_y++)
            {
                partTable.Put(i_x, i_y, new Cell(new Vector2(i_x, i_y)));
            }
        }

        // Check to see if the cells in the table are identical
        ltTs.Test("Cursory Reference Test 1", false, ReferenceEquals(partTable.Get(1, 0), partTable.Get(0, 2)));
        ltTs.Test("Cursory Reference Test 2", false, ReferenceEquals(partTable.Get(0, 0), partTable.Get(2, 1)));
        //// because I'm paranoid I guess.
        ltTs.Test("Is empty at (0,0)", true, partTable.Get(0, 0).IsEmpty());
        ltTs.Test("Is empty at (1,0)", true, partTable.Get(1, 0).IsEmpty());
        ltTs.Test("Is empty at (2,0)", true, partTable.Get(2, 0).IsEmpty());
        ltTs.Test("Is empty at (0,1)", true, partTable.Get(0, 1).IsEmpty());
        ltTs.Test("Is empty at (1,1)", true, partTable.Get(1, 1).IsEmpty());
        ltTs.Test("Is empty at (2,1)", true, partTable.Get(2, 1).IsEmpty());
        ltTs.Test("Is empty at (0,2)", true, partTable.Get(0, 2).IsEmpty());
        ltTs.Test("Is empty at (1,2)", true, partTable.Get(1, 2).IsEmpty());
        ltTs.Test("Is empty at (2,2)", true, partTable.Get(2, 2).IsEmpty());


        // set up the part TODO we need a better way to do this oh my goodness
        Block[] allBlocks =
        {
            new Block(partTable.Get(1, 0)),
            new Block(partTable.Get(0, 1)),
            new Block(partTable.Get(2, 1)),
            new Block(partTable.Get(2, 2))
        };
        partTable.Get(1, 0).SetBlock(allBlocks[0]);
        partTable.Get(0, 1).SetBlock(allBlocks[1]);
        partTable.Get(2, 1).SetBlock(allBlocks[2]);
        partTable.Get(2, 2).SetBlock(allBlocks[3]);
        // the part looks like this:
        //     []
        // []  []
        //   []
        // It is chiral and has only 4 blocks.
        Part part = new(new Vector2(1, 1));
        // the pivot is in the middle of the part.

        // Conduct structure tests
        // Basically, "Are there blocks where there's supposed to be blocks?"
        cfgTs.Test("Initial, structure at (0,0)",  true, partTable.Get(0, 0).IsEmpty());
        cfgTs.Test("Initial, structure at (1,0)", false, partTable.Get(1, 0).IsEmpty());
        cfgTs.Test("Initial, structure at (2,0)",  true, partTable.Get(2, 0).IsEmpty());
        cfgTs.Test("Initial, structure at (0,1)", false, partTable.Get(0, 1).IsEmpty());
        cfgTs.Test("Initial, structure at (1,1)",  true, partTable.Get(1, 1).IsEmpty());
        cfgTs.Test("Initial, structure at (2,1)", false, partTable.Get(2, 1).IsEmpty());
        cfgTs.Test("Initial, structure at (0,2)",  true, partTable.Get(0, 2).IsEmpty());
        cfgTs.Test("Initial, structure at (1,2)",  true, partTable.Get(1, 2).IsEmpty());
        cfgTs.Test("Initial, structure at (2,2)", false, partTable.Get(2, 2).IsEmpty());

        // Conduct reference tests on the blocks
        brefTs.Test("Test 1", false, ReferenceEquals(part.GetTable().Get(1, 0), part.GetTable().Get(0, 1)));
        brefTs.Test("Test 2", false, ReferenceEquals(part.GetTable().Get(2, 2), part.GetTable().Get(2, 1)));
        brefTs.Test("Test 3", false, ReferenceEquals(part.GetTable().Get(1, 0), part.GetTable().Get(2, 2)));
        brefTs.Test("Control Test (2,2)", true, ReferenceEquals(part.GetTable().Get(2, 2), part.GetTable().Get(2, 2)));

        // Testing initial 
        gpfTs.Test("Initial, GetPivot()", new Vector2(1, 1), part.GetPivot());
        gpfTs.Test("Initial, GetPos()", null, part.GetPos());
        iipTs.Test("Initial, IsInPlay()", false, part.IsInPlay());

        // Read the contents of the blocks
        cfgTs.Test("Contents (1, 0) ", allBlocks[0], part.GetTable().Get(1, 0).GetBlock());
        cfgTs.Test("Contents (0, 1) ", allBlocks[1], part.GetTable().Get(0, 1).GetBlock());
        cfgTs.Test("Contents (2, 1) ", allBlocks[2], part.GetTable().Get(2, 1).GetBlock());
        cfgTs.Test("Contents (2, 2) ", allBlocks[3], part.GetTable().Get(2, 2).GetBlock());


        //print the summaries and any failing records
        string allSummaries = "";
        for (int i = 0; i < allBatteries.Length; i++)
        {
            allSummaries += allBatteries[i].GetSummary() + "\n";
            if (allBatteries[i].fails > 0)
            {
                Debug.LogError(allBatteries[i].GetRecord());
            }
        }
        Debug.Log(allSummaries);

    }

    
}
