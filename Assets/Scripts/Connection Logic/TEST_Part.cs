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
        TestBattery cmTs = new("CanMerge()", "Can");
        TestBattery mTs = new("Merge()", "Mer");
        TestBattery iipTs = new("IsInPlay()", "IIP");
        TestBattery exTs = new("Extract()", "Extr");

        // set up the part TODO we need a better way to do this oh my goodness
        LookupTable<Cell> partTable = new(3, 3);
        partTable.Get(1, 0).SetBlock(new Block(partTable.Get(1, 0)));
        partTable.Get(0, 1).SetBlock(new Block(partTable.Get(0, 1)));
        partTable.Get(2, 1).SetBlock(new Block(partTable.Get(2, 1)));
        partTable.Get(2, 2).SetBlock(new Block(partTable.Get(2, 2)));
        // the part looks like this:
        //     []
        // []  []
        //   []
        // It is chiral and has only 4 blocks.
        Part p = new(partTable, new Vector2(1, 1));
        // the pivot is in the middle of the part.

        // TODO if we need this test battery, complete it.

    }

    
}
