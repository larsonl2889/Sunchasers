using System.Collections;
using System.Collections.Generic;
using Cells;
using Parts;
using Blocks;
using UnityEngine;
using System.Data;
using UnityEditor.U2D.Aseprite;

public class BuildAreaTest
{
    internal LookupTable<Cell> table;
    public float scale = 1.0f;
    private bool isLegal = true;

    //Gives you manual control to place cells. (Good for setting up tests)
    public void placeCellManual(Cell cell, Vector2 pos)
    {
        table.Put(pos, cell);
    }
    //Constructor override, size dictates the size of the lookup table
    public BuildAreaTest(int sizeX, int sizeY)
    {
        table = new LookupTable<Cell>(sizeX, sizeY, new Cell(new Vector2(0, 0)));
        for (int i = 0; i < sizeX; i++)
        {
            for (int j = 0; j < sizeY; j++)
            {
                table.Put(i, j, new Cell(new Vector2(i, j)));
            }
        }
    }
    //retrieves the cell at a specific location in the build area
    public Cell GetCell(Vector2 pos)
    {

        Cell testCell = table.Get(pos);
        if (testCell == null)
        {
            Debug.Log("Coordinates not in table");
            return null;
        }else
        {
            return testCell;
        }
    }
    //Checks if the part is able to be placed in that location
    public bool CanMerge(Part part, Vector2 startPosition)
    {
        int boundsY = part.table.y_size;
        int boundsX = part.table.x_size;
        int startX = (int)startPosition.x;
        int startY = (int)startPosition.y;

        for (int i = 0; i < boundsX; i++)
        {
            for (int j = 0; j < boundsY; j++)
            {
                Cell cellOne = part.table.Get(i, j);
                if (!cellOne.IsEmpty())
                {
                    Cell cellTwo = table.Get(i + startX, j + startY);
                    if (cellTwo != null && !cellTwo.IsEmpty())
                    {
                        return false;
                    }
                }
            }
        }
        return true;
    }

    //Merges the table passed as a parameter into the one calling the function. (You would pass the part as parameter)
    public void MergeTables(Part part, Vector2 startPosition)
    {
        if(CanMerge(part, startPosition))
        {
            for(int i = 0; i < part.table.x_size; i++)
            {
                for(int j = 0; j < part.table.y_size; j++)
                {
                    if(!part.table.Get(i, j).IsEmpty())
                    {
                        Debug.Log("Method Ran");
                        part.table.Get(i, j).GetBlock().SetCell(table.Get((int)startPosition.x + i, (int)startPosition.y + j));
                        Vector2 testVector = part.table.Get(i, j).GetBlock().GetCell().pos;
                        int testX = (int)testVector.x;
                        int testY = (int)testVector.y;
                        Debug.Log("Block at X = " + i + " Y = " + j + " Assigned Cell X = " + testX + " Y = " + testY);
                        table.Get((int)startPosition.x + i, (int)startPosition.y + j).SetBlock(part.table.Get(i, j).GetBlock());
                    }
                }
            }
            part.SetPosInWorld(startPosition);
        }
    }
}
