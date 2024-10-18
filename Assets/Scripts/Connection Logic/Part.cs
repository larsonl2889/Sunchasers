using System;
using Blocks;
using Cells;
using DirectionOps;
using Unity.VisualScripting;
using UnityEngine;

namespace Parts
{
    public class Part : MonoBehaviour
    {
        public LookupTable<Cell> table;
        public int tableSize;
        private Vector2 pivot; // location within its own table that'll be our "center". We place and rotate with respect to the pivot.
        private bool posInWorld; // the position in world, if I'm in play.
        private LookupTable<Cell> buildArea;
        private GameObject[] childCells;

        public Part(Vector2 pivot)
        {
            Cell badCell = new Cell(new Vector2(0, 0));
            LookupTable<Cell> goodTable = new LookupTable<Cell>(tableSize, tableSize, badCell);
            table = goodTable;
            this.pivot = pivot;
        }
        /*
        public void Start()
        {
            Debug.Log("Method Ran Part");
            Cell badCell = new Cell(new Vector2(0, 0));
            LookupTable<Cell> goodTable = new LookupTable<Cell>(tableSize, tableSize, badCell);
            table = goodTable;
            Cell[] cells = gameObject.GetComponentsInChildren<Cell>();
            for(int i = 0; i < cells.Length; i++)
            {
                cells[i].GetBlock().BlockStart();
                cells[i].CellStart();
            }
            for(int i = 0; i < cells.Length; i++)
            {
                Debug.Log("Cell Location X = " + cells[i].xPos + " Y = " + cells[i].yPos);
            }
            for(int i = 0; i < cells.Length; i++)
            {
                placeCellManual(cells[i], new Vector2(cells[i].xPos, cells[i].yPos));
            }
            for(int i = 0; i < tableSize; i++)
            {
                for(int j = 0; j < tableSize; j++)
                {
                   if(!table.Get(i, j).IsEmpty())
                   {
                        Debug.Log("There is a Cell at X = " + table.Get(i, j).xPos + " Y = " + table.Get(i, j).yPos);
                   }
                }
            }
            for(int i = 0; i < tableSize; i++)
            {
                for(int j = 0; j < tableSize; j++)
                {
                    if(!table.Get(i, j).IsEmpty())
                    {
                        Debug.Log("Block has Cell X = " + table.Get(i, j).GetBlock().GetCell().xPos + " Y = " + table.Get(i, j).GetBlock().GetCell().yPos);
                    }
                }
            }

            
        }
        */
       
        public void Start()
        {
            //FormTable();
        }
        

        public void FormTable()
        {
            Debug.Log("Method Ran FormTable");
            Cell badCell = new Cell(new Vector2(0, 0));
            LookupTable<Cell> goodTable = new LookupTable<Cell>(tableSize, tableSize, badCell);
            table = goodTable;
            Cell[] cells = gameObject.GetComponentsInChildren<Cell>();
            for (int i = 0; i < cells.Length; i++)
            {
                cells[i].CellStart();
                cells[i].GetBlock().GetComponent<Block>().BlockStart();
            }
            for (int i = 0; i < cells.Length; i++)
            {
                Debug.Log("Cell Location X = " + cells[i].xPos + " Y = " + cells[i].yPos);
            }
            for (int i = 0; i < cells.Length; i++)
            {
                placeCellManual(cells[i], new Vector2(cells[i].xPos, cells[i].yPos));
            }
            for (int i = 0; i < tableSize; i++)
            {
                for (int j = 0; j < tableSize; j++)
                {
                    if (!table.Get(i, j).IsEmpty())
                    {
                        Debug.Log("There is a Cell at X = " + table.Get(i, j).xPos + " Y = " + table.Get(i, j).yPos);
                    }
                }
            }
            for (int i = 0; i < tableSize; i++)
            {
                for (int j = 0; j < tableSize; j++)
                {
                    if(!table.Get(i, j).isEmpty)
                    {
                        if (table.Get(i, j).GetBlock().GetComponent<Block>().GetCell() != null)
                        {
                            Debug.Log("Cell Initialized Properly");
                        }
                    }
                    
                }
            }
            /*
            for (int i = 0; i < tableSize; i++)
            {
                for (int j = 0; j < tableSize; j++)
                {
                    if (!table.Get(i, j).IsEmpty())
                    {
                        Debug.Log("Block has Cell X = " + table.Get(i, j).GetBlock().GetComponent<Block>().GetCell().xPos + " Y = " + table.Get(i, j).GetBlock().GetComponent<Block>().GetCell().yPos);
                    }
                }
            }
            */


        }

        public void Update()
        {
            
        }

        public void placeCellManual(Cell cell, Vector2 position)
        {
            table.Put(position, cell);
        }

        public LookupTable<Cell> GetTable()
        {
            return table;
        }

        public Vector2 GetPivot()
        {
            return pivot;
        }

        /// <summary>
        /// Returns the position in the world. May return null if the part is not in play.
        /// </summary>
        /// <returns>Returns the position in world, if it exists.</returns>
        public bool GetPos()
        {
            return posInWorld;
        }

        public bool CanMerge(Vector2 pos)
        {
            return false; // TODO not implemented!
        }

        public void SetPosInWorld()
        {
            this.posInWorld = true;
        }

        // So long as the cells in the part keep the references to the block,
        // we don't really have to consult the build area to extract the part.
        // Pretty fancy. You know, if it works.
        // TODO TEST!
        /// <summary>
        /// Pulls the part out of play.
        /// </summary>
        public void Extract()
        { 
            //FormTable();
            Debug.Log("Extract Ran");
            Debug.Log("In Play");
            for (int i_x = 0; i_x < table.x_size; i_x++)
            {
                for (int i_y = 0; i_y < table.y_size; i_y++)
                {
                    if (!table.Get(i_x, i_y).IsEmpty()) {
                        Debug.Log("Extract Ran");
                        Vector2 testVector = table.Get(i_x, i_y).GetBlock().GetComponent<Block>().GetCell().pos;
                        int testX = (int)testVector.x;
                        int testY = (int)testVector.y;
                        Debug.Log("Cell Position X = " + testX + " Y = " + testY);
                        table.Get(i_x, i_y).GetBlock().GetComponent<Block>().SetCell(table.Get(i_x, i_y));
                        table.Get(i_x, i_y).GetBlock().GetComponent<Block>().GetCell().EvictBlock();

                    }
                }
            }
            // pull the part out of play
            posInWorld = false;
            Debug.Log("Extract Ran");
            Destroy(this.gameObject);
        }

        

    }
}

