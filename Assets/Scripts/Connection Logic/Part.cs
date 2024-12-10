using System;
using Blocks;
using Cells;
using System.Collections.Generic;
using DirectionOps;
using Unity.VisualScripting;
using UnityEngine;

namespace Parts
{
    public class Part : MonoBehaviour
    {
        public int index;
        public LookupTable<GameObject> table;
        public GameObject emptyCell;
        public int tableSize;
        private Vector2Int pivot; // location within its own table that'll be our "center". We place and rotate with respect to the pivot.
        private bool posInWorld; // the position in world, if I'm in play.
        private LookupTable<GameObject> buildArea;
        private GameObject[] childCells;
        

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

        private void Awake()
        {
            
        }

        public void SetIndex(int index)
        {
            this.index = index;
        }
        public void Start()
        {
            //FormTable();  // rip T_T
            
        }
        
        
        public void FormTable()
        {
            Cell[] cells2 = gameObject.GetComponentsInChildren<Cell>();
            for(int i = 0; i < cells2.Length; i++)
            {
                if (cells2[i].isEmpty)
                {
                    Destroy(cells2[i].gameObject);
                }
            }
            table = new LookupTable<GameObject>(tableSize, tableSize, emptyCell);
            for (int i = 0; i < tableSize; i++)
            {
                for(int j = 0; j < tableSize; j++)
                {
                    try
                    {
                        emptyCell.GetComponent<Cell>().xPos = i;
                        emptyCell.GetComponent<Cell>().yPos = j;
                        GameObject instantiated = Instantiate(emptyCell, this.gameObject.transform);
                        instantiated.transform.localPosition = new Vector3(100, 100, 0);
                        table.Put(i, j, instantiated);
                        Debug.Log("Added to part table (Empty)");
                    }
                    catch (Exception e)
                    {
                        Debug.LogError("\"" + gameObject.name + "\", (Part.cs) ERROR = " + e.Message);
                        throw e;
                    }
                }
            }
            Cell[] cellsOne = gameObject.GetComponentsInChildren<Cell>();
            GameObject[] cells = new GameObject[cellsOne.Length];
            for(int i = 0; i < cellsOne.Length; i++)
            {
                cells[i] = cellsOne[i].gameObject;
            }
            for(int i = 0; i < cells.Length; i++)
            {
                if (!cells[i].GetComponent<Cell>().isEmpty)
                {
                    placeCellManual(cells[i], new Vector2(cells[i].GetComponent<Cell>().xPos, cells[i].GetComponent<Cell>().yPos));
                    Debug.Log("Added to part table (With Block)");
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

        /// <summary>
        /// Sets the colliders for boxes.
        /// </summary>
        /// <param name="doEnable">whether to turn on or off the colliders</param>
        public void SetWalkableColliders(bool doEnable)
        {
            for (int i_x = 0; i_x < tableSize; i_x++)
            {
                for (int i_y = 0; i_y < tableSize; i_y++)
                {
                    // make sure it's not empty!
                    if (!table.Get(i_x, i_y).GetComponent<Cell>().isEmpty)
                    {
                        // If the block is walkable, change its collider
                        if (table.Get(i_x, i_y).GetComponent<Cell>().GetBlock().GetComponent<Block>().isWalkable)
                        {
                            table.Get(i_x, i_y).GetComponent<Cell>().GetBlock().GetComponent<BoxCollider2D>().enabled = doEnable;
                        }
                        
                    }
                }
            }
        }

        public void SetCellVisibility(bool doEnable)
        {
            for (int i_x = 0; i_x < tableSize; i_x++)
            {
                for (int i_y = 0; i_y < tableSize; i_y++)
                {
                    // make sure it's not empty!
                    if (!table.Get(i_x, i_y).GetComponent<Cell>().isEmpty)
                    {
                        table.Get(i_x, i_y).GetComponent<Cell>().GetComponent<SpriteRenderer>().enabled = doEnable;
                    }
                }
            }
        }

        public void Update()
        {
            
        }

        public void placeCellManual(GameObject cell, Vector2 position)
        {
            table.Put(position, cell);
        }

        public LookupTable<GameObject> GetTable()
        {
            return table;
        }

        public void SetPivot(Vector2 newPivot) { pivot = new Vector2Int((int)newPivot.x, (int)newPivot.y); }

        public Vector2 GetPivot() { 
            // moves any pivot at (0, 0)
            //if (pivot == null || (int)pivot.x==0 && (int)pivot.y==0)
            //{
            //    return new Vector2((int)(tableSize / 2 + 1), (int)(tableSize / 2 + 1));
            //}
            return (Vector2)pivot; 
        
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
                    if (!table.Get(i_x, i_y).GetComponent<Cell>().isEmpty) {
                        Debug.Log("Extract Ran");
                        Vector2 testVector = table.Get(i_x, i_y).GetComponent<Cell>().GetBlock().GetComponent<Block>().GetCell().GetComponent<Cell>().pos;
                        int testX = (int)testVector.x;
                        int testY = (int)testVector.y;
                        Debug.Log("Cell Position X = " + testX + " Y = " + testY);
                        table.Get(i_x, i_y).GetComponent<Cell>().GetBlock().GetComponent<Block>().GetCell().GetComponent<Cell>().EvictBlock();
                        table.Get(i_x, i_y).GetComponent<Cell>().GetBlock().GetComponent<Block>().SetCell(table.Get(i_x, i_y));
                        //table.Get(i_x, i_y).GetComponent<Cell>().GetBlock().GetComponent<Block>().GetCell().GetComponent<Cell>().EvictBlock();

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

