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
    public int scale = 1;
    public float xPos;
    public float yPos;
    private bool isLegal = true;

    //Gives you manual control to place cells. (Good for setting up tests)
    public void placeCellManual(GameObject cell, Vector2 pos)
    {
        table.Put(pos, cell);
    }
    //Constructor override, size dictates the size of the lookup table
    public void Start()
    {
        table = new LookupTable<GameObject>(scale, scale, gameObject.GetComponentInChildren<Cell>().gameObject);
        Cell[] cells = gameObject.GetComponentsInChildren<Cell>();
        GameObject[] cellsTwo = new GameObject[cells.Length];
        Debug.Log(cells.Length);
        for(int i = 0; i < cells.Length; i++)
        {
            cellsTwo[i] = cells[i].gameObject;
        }
        float cellOffset = (float)scale / 2;
        float newPosX = xPos - cellOffset;
        float newPosY = yPos - cellOffset;
        int full = 0;
        for (int i = 0; i < scale; i++)
        {
            for(int j = 0; j < scale; j++)
            {
                table.Put(cellsTwo[full].GetComponent<Cell>().xPos, cellsTwo[full].GetComponent<Cell>().yPos, cellsTwo[full]);
                table.Get(i, j).transform.localPosition = new Vector3(newPosX + table.Get(i, j).GetComponent<Cell>().xPos, newPosY + table.Get(i, j).GetComponent<Cell>().yPos, 0);
                full++;
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
                GameObject testing = part.GetComponent<Part>().table.Get(i, j);
                Cell cell = testing.GetComponent<Cell>();
                Cell cellOne = part.GetComponent<Part>().table.Get(i, j).GetComponent<Cell>();
                if (!cellOne.IsEmpty())
                {
                    if (i + startX > scale - 1 || j + startY > scale - 1)
                    {
                        return false;
                    }
                    Cell cellTwo = table.Get(i + startX, j + startY).GetComponent<Cell>();
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
        if(CanMerge(part, startPosition))
        {
            for(int i = 0; i < part.GetComponent<Part>().table.x_size; i++)
            {
                for(int j = 0; j < part.GetComponent<Part>().table.y_size; j++)
                {
                    if (!part.GetComponent<Part>().table.Get(i, j).GetComponent<Cell>().IsEmpty())
                    {
                        Debug.Log("Method Ran");
                        Block niceBlock = part.GetComponent<Part>().table.Get(i, j).GetComponent<Cell>().GetBlock().GetComponent<Block>();
                        niceBlock.SetCell(table.Get(i + (int)startPosition.x, j + (int)startPosition.y).GetComponent<Cell>());
                        part.GetComponent<Part>().table.Get(i, j).GetComponent<Cell>().SetBlock(niceBlock.gameObject);
                        Vector2 testVector = part.GetComponent<Part>().table.Get(i, j).GetComponent<Cell>().GetBlock().GetComponent<Block>().GetCell().pos;
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
