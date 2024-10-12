using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Testing;
using Parts;
using Cells;
using Blocks;

// Contributors: Leif Larson
// Last updated 10-11-2024
// Previous test result: This test file is not complete.

public class TEST_Part : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //TestBattery cmTs = new("CanMerge()", "Can");
        //TestBattery mTs = new("Merge()", "Mer");
        TestBattery cfgTs = new("Initial config testing", "Icfg");
        TestBattery gpfTs = new("General part functionality test", "PART");
        TestBattery brefTs = new("Block reference tests", "B_Ref");
        TestBattery iipTs = new("IsInPlay()", "IIP");
        //TestBattery exTs = new("Extract()", "Extr");
        //TestBattery ba_cmTs = new("BuildArea.CanMerge()", "BA_Can");
        //TestBattery ba_mTs = new("BuildArea.Merge()", "BA_Mer");
        //TestBattery[] allBatteries =
        //{
        //    iipTs, exTs, ba_cmTs, ba_mTs
        //};

        LookupTable<Cell> partTable = new(3, 3);
        // Check to see if the cells in the table are identical
        cfgTs.Test("Cursory Reference Test 1", false, ReferenceEquals(partTable.Get(1, 0), partTable.Get(0, 2)));
        cfgTs.Test("Cursory Reference Test 2", false, ReferenceEquals(partTable.Get(0, 0), partTable.Get(2, 1)));
        //// because I'm paranoid I guess.
        //cfgTs.Test("Is empty at (0,0)", true, partTable.Get(0, 0).IsEmpty());
        //cfgTs.Test("Is empty at (1,0)", true, partTable.Get(1, 0).IsEmpty());
        //cfgTs.Test("Is empty at (2,0)", true, partTable.Get(2, 0).IsEmpty());
        //cfgTs.Test("Is empty at (0,1)", true, partTable.Get(0, 1).IsEmpty());
        //cfgTs.Test("Is empty at (1,1)", true, partTable.Get(1, 1).IsEmpty());
        //cfgTs.Test("Is empty at (2,1)", true, partTable.Get(2, 1).IsEmpty());
        //cfgTs.Test("Is empty at (0,2)", true, partTable.Get(0, 2).IsEmpty());
        //cfgTs.Test("Is empty at (1,2)", true, partTable.Get(1, 2).IsEmpty());
        //cfgTs.Test("Is empty at (2,2)", true, partTable.Get(2, 2).IsEmpty());


        // set up the part TODO we need a better way to do this oh my goodness
        partTable.Get(1, 0).SetBlock(new Block(partTable.Get(1, 0)));
        partTable.Get(0, 1).SetBlock(new Block(partTable.Get(0, 1)));
        partTable.Get(2, 1).SetBlock(new Block(partTable.Get(2, 1)));
        partTable.Get(2, 2).SetBlock(new Block(partTable.Get(2, 2)));
        // the part looks like this:
        //     []
        // []  []
        //   []
        // It is chiral and has only 4 blocks.
        Part part = new(partTable, new Vector2(1, 1));
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
        
        


    }

    
}
