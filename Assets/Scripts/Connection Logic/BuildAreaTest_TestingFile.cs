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
        BuildAreaTest grid = new BuildAreaTest(6, 6);
        Part part = new Part(new Vector2(1, 1));
        Part partTwo = new Part(new Vector2(1, 1));



        for (int i = 0; i < 3; i++)
        {
            Cell cell = new Cell(new Vector2(i, 0));
            Block block = new Block(cell);
            cell.SetBlock(block);
            partTwo.placeCellManual(cell, new Vector2(i, 0));
        }

        for (int i = 0; i < 3; i++)
        {
            Cell cell = new Cell(new Vector2(i, 0));
            Block block = new Block(cell);
            cell.SetBlock(block);
            part.placeCellManual(cell, new Vector2(i, 0));
        }
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                if (!part.table.Get(i, j).IsEmpty())
                {
                    Debug.Log("Part Contains X = " + i + " Y = " + j);
                }
            }
        }
        for(int i = 0; i < 3; i++)
        {
            for(int j = 0; j < 3; j++)
            {
                if (!part.table.Get(i, j).IsEmpty())
                {
                    Vector2 testVector = part.table.Get(i, j).GetBlock().GetCell().pos;
                    int testX = (int)testVector.x;
                    int testY = (int)testVector.y;
                    Debug.Log("Pre-Merge: Block at X = " + i + " Y = " + j + " Has Cell X = " + testX + " Y + " + testY);
                }
            }
        }
        grid.MergeTables(part, new Vector2(0, 1));
        //grid.MergeTables(partTwo, new Vector2(0, 0));
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                if (!part.table.Get(i, j).IsEmpty())
                {
                    Vector2 testVector = part.table.Get(i, j).GetBlock().GetCell().pos;
                    int testX = (int)testVector.x;
                    int testY = (int)testVector.y;
                    Debug.Log("Post-Merge: Block at X = " + i + " Y = " + j + " Has Cell X = " + testX + " Y + " + testY);
                }
            }
        }
        for (int i = 0; i < 6; i++)
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
                if (!part.table.Get(i, j).IsEmpty())
                {
                    Debug.Log("Part Contains X = " + i + " Y = " + j);
                }
            }
        }

        part.Extract();

        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                if (!part.table.Get(i, j).IsEmpty())
                {
                    Debug.Log("Part Contains (After Extract) X = " + i + " Y = " + j);
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
