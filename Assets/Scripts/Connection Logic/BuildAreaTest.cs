using System.Collections;
using System.Collections.Generic;
using Cells;
using Parts;
using Blocks;
using UnityEngine;
using System.Data;
public class BuildAreaTest : MonoBehaviour
{
    internal LookupTable<Cell> table;
    public int scale = 1;
    public int xPos;
    public int yPos;
    private bool isLegal = true;

    //Gives you manual control to place cells. (Good for setting up tests)
    public void placeCellManual(Cell cell, Vector2 pos)
    {
        table.Put(pos, cell);
    }
    //Constructor override, size dictates the size of the lookup table
    public void Start()
    {
        table = new LookupTable<Cell>(scale, scale, new Cell(new Vector2(0, 0)));
        for (int i = 0; i < scale; i++)
        {
            for (int j = 0; j < scale; j++)
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
                    if (i + startX > scale - 1 || j + startY > scale - 1)
                    {
                        return false;
                    }
                    Cell cellTwo = table.Get(i + startX, j + startY);
                    if (!cellTwo.IsEmpty())
                    {
                        return false;
                    }
                    
                }
            }
        }
        return true;
    }

    //Merges the table passed as a parameter into the one calling the function. (You would pass the part as parameter)
    public void MergeTables(GameObject part, Vector2 startPosition)
    {
        if(CanMerge(part.GetComponent<Part>(), startPosition))
        {
            for(int i = 0; i < part.GetComponent<Part>().table.x_size; i++)
            {
                for(int j = 0; j < part.GetComponent<Part>().table.y_size; j++)
                {
                    if(!part.GetComponent<Part>().table.Get(i, j).IsEmpty())
                    {
                        Debug.Log("Method Ran");
                        Block niceBlock = part.GetComponent<Part>().table.Get(i, j).GetBlock().GetComponent<Block>();
                        niceBlock.SetCell(table.Get(i + (int)startPosition.x, j + (int)startPosition.y));
                        part.GetComponent<Part>().table.Get(i, j).SetBlock(niceBlock.gameObject);
                        Vector2 testVector = part.GetComponent<Part>().table.Get(i, j).GetBlock().GetComponent<Block>().GetCell().pos;
                        int testX = (int)testVector.x;
                        int testY = (int)testVector.y;
                        Debug.Log("Block at X = " + i + " Y = " + j + " Assigned Cell X = " + testX + " Y = " + testY);
                        table.Get((int)startPosition.x + i, (int)startPosition.y + j).SetBlock(part.GetComponent<Part>().table.Get(i, j).GetBlock());
                        table.Get((int)startPosition.x + i, (int)startPosition.y + j).isEmpty = false;
                        Debug.Log("Tables Should Be Merged");
                    }
                }
            }
            part.GetComponent<Part>().SetPosInWorld();
        }
    }
}
