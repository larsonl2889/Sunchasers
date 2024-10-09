using System.Collections;
using System.Collections.Generic;
using Cells;
using Blocks;
using UnityEngine;

public class BuildAreaTest_TestingFile : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Block block = new Block(new Cell(new Vector2(0, 0)));
        BuildAreaTest grid = new BuildAreaTest(6, 6);
        BuildAreaTest part = new BuildAreaTest(3, 3);
        BuildAreaTest partTwo = new BuildAreaTest(3, 3);

        for(int i = 0; i < 3; i++)
        {
            partTwo.placeCellManual(new Cell(block, new Vector2(i, 0)), new Vector2(i, 0));
        }

        for(int i = 0; i < 3; i++)
        {
            part.placeCellManual(new Cell(block, new Vector2(1, i)), new Vector2(1, i));
        }
        part.placeCellManual(new Cell(block, new Vector2(0, 0)), new Vector2(0, 0));

        grid.MergeTables(part, new Vector2(0, 0));
        grid.MergeTables(partTwo, new Vector2(0, 0));

        for(int i = 0; i < 6; i++)
        {
            for(int j = 0; j < 6; j++)
            {
                if(!grid.table.Get(i, j).IsEmpty())
                {
                    Debug.Log("You Put a Thing There X = " + i + " Y = " + j);
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
