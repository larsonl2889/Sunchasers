using System.Collections;
using System.Collections.Generic;
using Cells;
using Blocks;
using UnityEngine;
using System.Data;

public class BuildAreaTest : MonoBehaviour
{
    private LookupTable<Cell> table;
    public float scale = 1.0f;
    private bool isLegal = true;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void placeCellManual(Cell cell, Vector2 pos)
    {
        table.Put(pos, cell);
    }

    public BuildAreaTest(int sizeX, int sizeY)
    {
        table = new LookupTable<Cell>(sizeX, sizeY, new Cell(new Vector2(0, 0)));
    }

    public Cell getCell(Vector2 pos)
    {

        Cell testCell = table.Get(pos);
        if (testCell == null)
        {
            Debug.Log("Coordinatesd not in table");
            return null;
        }else
        {
            return testCell;
        }
    }

    public void mergeTables(BuildAreaTest block, Vector2 startPosition)
    {
        int cellCount = 0;
        int boundsY = block.table.y_size;
        int boundsX = block.table.x_size;
        int startX = (int)startPosition.x;
        int startY = (int)startPosition.y;
        Vector2[] positions = new Vector2[boundsX * boundsY];
        Cell[] cells = new Cell[boundsX * boundsY];

        for(int i = 0; i < boundsX; i++)
        {
            for(int j = 0; j < boundsY; j++)
            {
                Cell cellOne = block.table.Get(i, j);
                if(cellOne != null)
                {
                    Cell cellTwo = table.Get(i + startX, j + startY);
                    if(cellTwo != null && cellTwo.IsEmpty())
                    {
                        cells[cellCount] = cellOne;
                        cellCount++;

                        positions[cellCount] = new Vector2(i + startX, j + startY);
                    }else
                    {
                        isLegal = false;
                        Debug.Log("Can't Put that There. X = " + (i + startX) + " Y = " + (j + startY));
                        return;
                    }
                }
            }
        }
        for(int i = 0; i < cellCount; i++)
        {
            table.Put(positions[i], cells[i]);
        }
    }
}
