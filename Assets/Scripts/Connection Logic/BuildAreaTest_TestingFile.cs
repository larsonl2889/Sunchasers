using System.Collections;
using System.Collections.Generic;
using Cells;
using Parts;
using Blocks;
using UnityEngine;

public class BuildAreaTest_TestingFile : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        LookupTable<Cell> testTableOne = new LookupTable<Cell>(3, 3, new Cell(new Vector2(0, 0)));
        LookupTable<Cell> testTableTwo = new LookupTable<Cell>(3, 3, new Cell(new Vector2(0, 0)));
        for(int i = 0; i < 3; i++)
        {
            for(int j = 0; j < 3; j++)
            {
                testTableOne.Put(i, j, new Cell(new Vector2(i, j)));
                testTableTwo.Put(i, j, new Cell(new Vector2(i, j)));
            }
        }
        Block block = new Block(new Cell(new Vector2(0, 0)));
        BuildAreaTest grid = new BuildAreaTest(6, 6);
        Part part = new Part(testTableOne, new Vector2(1, 1));
        Part partTwo = new Part(testTableTwo, new Vector2(1, 1));



        for (int i = 0; i < 3; i++)
        {
            partTwo.placeCellManual(new Cell(block, new Vector2(i, 0)), new Vector2(i, 0));
        }

        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                if (!partTwo.table.Get(i, j).IsEmpty())
                {
                    Debug.Log("Part Contains X = " + i + " Y = " + j);
                }
            }
        }
        for (int i = 0; i < 3; i++)
        {
            part.placeCellManual(new Cell(block, new Vector2(1, i)), new Vector2(1, i));
        }
        part.placeCellManual(new Cell(block, new Vector2(0, 0)), new Vector2(0, 0));

        //grid.MergeTables(part, new Vector2(0, 0));
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

        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                if (!partTwo.table.Get(i, j).IsEmpty())
                {
                    Debug.Log("Part Contains X = " + i + " Y = " + j);
                }
            }
        }

        partTwo.Extract();

        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                if (!partTwo.table.Get(i, j).IsEmpty())
                {
                    Debug.Log("Part Contains X = " + i + " Y = " + j);
                }
            }
        }

        for (int i = 0; i < 6; i++)
        {
            for (int j = 0; j < 6; j++)
            {
                if (!grid.table.Get(i, j).IsEmpty())
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
