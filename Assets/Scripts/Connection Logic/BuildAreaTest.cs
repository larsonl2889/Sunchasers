using System.Collections;
using System.Collections.Generic;
using Cells;
using Parts;
using Blocks;
using UnityEngine;
using System.Data;
using UnityEditor.U2D.Aseprite;

public class BuildAreaTest : MonoBehaviour
{
    internal LookupTable<GameObject> table;
    public GameObject emptyCell;
    public int scale = 1;
    public int xPos;
    public int yPos;
    private bool isLegal = true;

    //Gives you manual control to place cells. (Good for setting up tests)
    public void placeCellManual(GameObject cell, Vector2 pos)
    {
        table.Put(pos, cell);
    }
    public void Start()
    {
        table = new LookupTable<GameObject>(scale, scale);
        for(int i = 0; i < scale; i++)
        {
            for(int j = 0; j < scale; j++)
            {
                GameObject instantiated = Instantiate(emptyCell);
                instantiated.GetComponent<Cell>().xPos = i;
                instantiated.GetComponent<Cell>().yPos = j;
                table.Put(i, j, instantiated);
            }
        }
    }
    //retrieves the cell at a specific location in the build area
    /*
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
    */
    //Checks if the part is able to be placed in that location
    public bool CanMerge(GameObject part, Vector2 startPosition)
    {
        int boundsY = part.GetComponent<Part>().table.y_size;
        int boundsX = part.GetComponent<Part>().table.x_size;
        int startX = (int)startPosition.x;
        int startY = (int)startPosition.y;

        for (int i = 0; i < boundsX; i++)
        {
            for (int j = 0; j < boundsY; j++)
            {
                GameObject cellOne = part.GetComponent<Part>().table.Get(i, j);
                if (!cellOne.GetComponent<Cell>().isEmpty)
                {
                    Debug.Log("Cell One Not Empty");
                    if (i + startX > scale - 1 || j + startY > scale - 1)
                    {
                        return false;
                    }
                    GameObject cellTwo = table.Get(i + startX, j + startY);
                    if (!cellTwo.GetComponent<Cell>().isEmpty)
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
        if(CanMerge(part, startPosition))
        {
            for(int i = 0; i < part.GetComponent<Part>().tableSize; i++)
            {
                for(int j = 0; j < part.GetComponent<Part>().tableSize; j++)
                {
                    Debug.Log("Merge Loop Ran");
                    if (!part.GetComponent<Part>().table.Get(i, j).GetComponent<Cell>().isEmpty)
                    {
                        GameObject niceBlock = part.GetComponent<Part>().table.Get(i, j).GetComponent<Cell>().GetBlock();
                        niceBlock.GetComponent<Block>().SetCell(table.Get(i + (int)startPosition.x, j + (int)startPosition.y));
                        part.GetComponent<Part>().table.Get(i, j).GetComponent<Cell>().SetBlock(niceBlock);
                        Vector2 testVector = part.GetComponent<Part>().table.Get(i, j).GetComponent<Cell>().GetBlock().GetComponent<Block>().GetCell().GetComponent<Cell>().pos;
                        int testX = (int)testVector.x;
                        int testY = (int)testVector.y;
                        Debug.Log("Block at X = " + i + " Y = " + j + " Assigned Cell X = " + testX + " Y = " + testY);
                        table.Get((int)startPosition.x + i, (int)startPosition.y + j).GetComponent<Cell>().SetBlock(part.GetComponent<Part>().table.Get(i, j).GetComponent<Cell>().GetBlock());
                        table.Get((int)startPosition.x + i, (int)startPosition.y + j).GetComponent<Cell>().isEmpty = false;
                        Debug.Log("Tables Should Be Merged");
                    }
                }
            }
            part.GetComponent<Part>().SetPosInWorld();
        }
    }
}
