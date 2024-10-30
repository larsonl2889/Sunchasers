using System.Collections;
using System.Collections.Generic;
using Cells;
using Parts;
using Blocks;
using UnityEngine;
using System.Data;
using Pipes;
public class BuildAreaTest : MonoBehaviour
{
    internal LookupTable<GameObject> table;
    public GameObject emptyCell;
    public int scale = 1;
    public int xPos;
    public int yPos;

    /// <summary>
    /// Return's the block's class data.
    /// </summary>
    /// <param name="where">Where to look</param>
    /// <returns>Block data</returns>
    internal Block GetBlockData(Vector2 where)
    {
        return table.Get(where).GetComponent<Cell>().GetBlock().GetComponent<Block>();
    }

    /// <summary>
    /// Returns the SteamState of the pipe here. Returns null if there is no pipe here.
    /// </summary>
    /// <param name="where">Where to look</param>
    /// <returns>Either a SteamState, or null.</returns>
    public SteamState GetSteamState(Vector2 where)
    {
        return GetBlockData(where).GetSteamState();
    }

    /// <summary>
    /// Sets the SteamState of the Pipe here, if there is one.
    /// </summary>
    /// <param name="where">Where to look</param>
    /// <param name="state">The new steam state</param>
    public void SetSteamState(Vector2 where, SteamState state)
    {
        GetBlockData(where).SetSteamState(state);
    }

    /// <summary>
    /// Returns whether a pipe here can activate objectives.
    /// </summary>
    /// <param name="where">where we're looking in the table.</param>
    /// <returns>Whether a pipe here can activate objectives.</returns>
    public bool IsOn(Vector2 where)
    {
        return GetSteamState(where).IsOn();
    }

    /// <summary>
    /// Returns whether a pipe here should be animated as though it had steam flowing through it.
    /// </summary>
    /// <param name="where">Where we're looking in the table.</param>
    /// <returns></returns>
    public bool IsSteaming(Vector2 where)
    {
        return GetSteamState(where).IsSteaming();
    }

    internal List<Vector2> GetConnectionLocations(Vector2 where)
    {
        int x = (int)where.x;
        int y = (int)where.y;
        List<Vector2> augmentedLinks = new();
        if (table.IsIndexInBounds(x, y))
        {
            // get the block data here
            List<Vector2> links = GetBlockData(where).GetAllLinksList();
            for (int i = 0; i < links.Count; i++)
            {
                // Assemble the location the connection leads to
                augmentedLinks.Add(new Vector2(x, y) + links[i]);
            }
        }
        return augmentedLinks;
    }
    
    /// <summary>
    /// Returns a list of unit vectors indicating the directions this part should be leaking steam. 
    /// <br></br>This list may be empty.
    /// </summary>
    /// <param name="where">Where we're looking in the table.</param>
    /// <returns>A list of vectors indicating the directions this part is leaking steam.</returns>
    public List<Vector2> GetLeakDirections(Vector2 where)
    {
        List<Vector2> leakVectors = new();
        if (GetSteamState(where) == SteamState.LEAKING || 
            GetSteamState(where) == SteamState.SOURCE)
        {
            List<Vector2> leakCandidates = GetBlockData(where).GetAllLinksList();
            foreach ( Vector2 link in leakCandidates)
            {
                if (!table.IsIndexInBounds((int)(where + link).x, (int)(where + link).y)) {
                    leakVectors.Add(link);
                }
                else
                {
                    // check if the direction is returned
                    if (!GetConnectionLocations(link).Contains(where))
                    {
                        leakVectors.Add(link);
                    }
                }
            }
        }
        return leakVectors;
    }

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
                float xCoord = xPos - ((float)scale / 2);
                float yCoord = yPos - ((float)scale / 2);
                instantiated.transform.localPosition = new Vector3(i + xPos + 0.5f, j + yPos + 0.5f, 0);
                instantiated.GetComponent<Cell>().xPos = i;
                instantiated.GetComponent<Cell>().yPos = j;
                instantiated.GetComponent<Cell>().CellStart();
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
