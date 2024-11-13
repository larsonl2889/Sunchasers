using System.Collections;
using System.Collections.Generic;
using Cells;
using Parts;
using Blocks;
using UnityEngine;
using System.Data;
using Pipes;
using System;
using DirectionOps;

public class BuildAreaTest : MonoBehaviour
{
    internal LookupTable<GameObject> table;
    internal List<Vector2> allPipeLocations;
    public GameObject emptyCell;
    public GameObject badCell;
    public GameObject startingPart;
    public int scale = 1;
    public int xPos;
    public int yPos;

    public Vector2Int SourceLocation;
    public Vector2Int ObjectiveLocation;

    public void ToggleSource()
    {
        if (GetSteamState((Vector2)SourceLocation) == SteamState.SOURCE)
        {
            DeactivateSource();
        }
        else
        {
            ActivateSource();
        }
    }

    public void ActivateSource()
    {
        SetSteamState((Vector2)SourceLocation, SteamState.SOURCE);
        UpdateSteam();
    }

    public void DeactivateSource()
    {
        SetSteamState((Vector2)SourceLocation, SteamState.EMPTY);
        UpdateSteam();
    }

    public bool IsObjectiveOn()
    {
        return IsOn((Vector2)ObjectiveLocation);
    }

    /// <summary>
    /// Updates the steam gushers here.
    /// </summary>
    /// <param name="where">Where to update</param>
    public void UpdateSteamGushers(Vector2 where)
    {
        GetBlockData(where).gusherParent.GetComponent<SteamGusher>().UpdateGushers(GetLeakDirections(where));
    }

    /// <summary>
    /// Updates the steam states of all of the pipes in this build area.
    /// </summary>
    public void UpdateSteam()
    {
        Debug.LogWarning("Updating steam...");
        List<List<Vector2>> allPipeSystems = GetAllPipeSystems();
        Debug.LogWarning("System # = " + allPipeSystems.Count);
        for (int system_index = 0; system_index < allPipeSystems.Count; system_index++)
        {
            Debug.LogWarning("propagating at " + allPipeSystems[system_index]);
            PropagateSteam(allPipeSystems[system_index]);
        }
        // update steam particles here
        foreach (Vector2 where in allPipeLocations)
        {
            UpdateSteamGushers(where);
        }

        GetComponent<BuildAreaDelegator>().UpdateObjective();
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

    /// <summary>
    /// Returns a list of unit vectors indicating the directions this part should be leaking steam. 
    /// <br></br>This list may be empty.
    /// </summary>
    /// <param name="where">Where we're looking in the table.</param>
    /// <returns>A list of vectors indicating the directions this part is leaking steam.</returns>
    public List<Vector2> GetLeakVectors(Vector2 where)
    {
        List<Vector2> leakVectors = new();
        if (GetSteamState(where) == SteamState.LEAKING ||
            GetSteamState(where) == SteamState.SOURCE)
        {
            List<Vector2> leakCandidates = GetBlockData(where).GetAllLinksList();
            foreach (Vector2 link in leakCandidates)
            {
                if (!table.IsIndexInBounds((int)(where + link).x, (int)(where + link).y))
                {
                    leakVectors.Add(link);
                }
                else
                {
                    // check if the direction is returned
                    if (!GetConnectionLocations(where + link).Contains(where))
                    {
                        leakVectors.Add(link);
                    }
                }
            }
        }
        return leakVectors;
    }

    public List<Direction> GetLeakDirections(Vector2 where)
    {
        List<Vector2> lvecs = GetLeakVectors(where); // TODO stub
        List<Direction> ldirs = new List<Direction>();
        foreach (Vector2 vec in lvecs)
        {
            if (vec == Vector2.up) { ldirs.Add(Direction.UP); }
            else if (vec == Vector2.down) { ldirs.Add(Direction.DOWN); }
            else if (vec == Vector2.left) { ldirs.Add(Direction.LEFT); }
            else if (vec == Vector2.right) { ldirs.Add(Direction.RIGHT); }
            else { Debug.LogWarning("GetLeakDirections() defaulted on the Vector (" + vec.x + ", " + vec.y + ")"); }
        }
        return ldirs;
    }

    internal bool IsEmpty(Vector2 where)
    {
        return table.Get(where).GetComponent<Cell>().isEmpty;
    }



    /// <summary>
    /// Return's the block's class data. If it doesn't exist, return's the bad block's data.
    /// </summary>
    /// <param name="where">Where to look</param>
    /// <returns>Block data</returns>
    internal Block GetBlockData(Vector2 where)
    {
        if (table.Get(where).GetComponent<Cell>().isEmpty) {
            //throw new NullReferenceException("This cell @ (" + (int)where.x + ", " + (int)where.y + ") has no block! Cannot get its data!");
            return badCell.GetComponent<Cell>().GetBlock().GetComponent<Block>();
        }
        else
        {
            return table.Get(where).GetComponent<Cell>().GetBlock().GetComponent<Block>();
        }
    }

    /// <summary>
    /// Gets the absolute locations the pipe here connects to within the build area.
    /// </summary>
    /// <param name="where">the location of the pipe we're looking at.</param>
    /// <returns>the list of locations this pipe tries to connect to.</returns>
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
    /// Gets the list of all locations this pipe is connected to.
    /// <br></br><b>Algorithm Description</b>
    /// <br></br>Basically, we're getting each pipe recursively.
    /// <br></br>(0) Create an empty list of locations called fullSystem.
    /// <br></br>(1) Add my own location to fullSystem.
    /// <br></br>(2) Then, get the list of locations I'm connected to.
    /// <br></br> | For each location:
    /// <br></br> | (2a) If I am already in fullSystem, do NOT proceed to (2b) for me.
    /// <br></br> | (2b) Check to see if I connect back to the location added in step (1).
    /// <br></br> |  |  (2b_i) YES: Add me to the fullSystem. Plug me into Step (1).
    /// <br></br> |  |  (2b_ii) NO: Don't add me to fullSystem.
    /// <br></br>(3) Repeat until the algorithm does not give further instructions. Return the fullSystem.
    /// 
    /// </summary>
    /// <param name="where">the location of a pipe</param>
    /// <returns>list of connected locations</returns>
    internal List<Vector2> GetFullPipeSystem(Vector2 where)
    {
        // (0): Create fullSystem as an empty list of locations (Vector2).
        List<Vector2> fullSystem = new();
        // Steps (1) to (2b_ii) are handled recursively.
        GetPartOfFullSystem(where, fullSystem);
        // (3): return the fullSystem.
        return fullSystem;
    }


    /// <summary>
    /// Implements Step (1) thru (2b_ii) from GetFullPipeSystem(). 
    /// <br></br>(see <see cref="GetFullPipeSystem(Vector2)"/>.)
    /// </summary>
    /// <param name="where">Location given to Step (1)</param>
    /// <returns></returns>
    internal void GetPartOfFullSystem(Vector2 where, List<Vector2> fullSystem)
    {
        // (1): Add self to fullSystem
        fullSystem.Add(where);
        // (2): Get the list of locations I'm connected to
        List<Vector2> allConnectedLocations = GetConnectionLocations(where);
        for (int i = 0; i < allConnectedLocations.Count; i++)
        {
            // (2a): Check if I'm already accounted for in fullSystem. If so, do NOT proceed to (2b) with me.
            bool isAlreadyInSystem = false;
            for (int i_sysCheck = 0; i_sysCheck < fullSystem.Count; i_sysCheck++)
            {
                if (fullSystem[i_sysCheck] == allConnectedLocations[i])
                {
                    // do NOT proceed to step (2b)
                    isAlreadyInSystem = true;
                    break;
                }
            }
            // if allowed to proceed to step (2b)...
            if (!isAlreadyInSystem)
            {
                // (2b): Check to see if I connect back to the location in "where"
                List<Vector2> subConnectedLocations = GetConnectionLocations(allConnectedLocations[i]);
                bool connectsBack = false;
                for (int j = 0; j < subConnectedLocations.Count; j++)
                {
                    if (subConnectedLocations[j] == where)
                    {
                        connectsBack = true;
                        break;
                    }
                }
                // (2b_i): If YES, do recursion on me.
                if (connectsBack)
                {
                    GetPartOfFullSystem(allConnectedLocations[i], fullSystem);
                }
                // (2b_ii): If NO, ignore me.
                else { /*do nothing */ }
            }
        }
    }

    /// <summary>
    /// Returns all "systems". A system is made up of mutually connected pipes. Every pipe in a system is connected to every other pipe, directly or indirectly.
    /// </summary>
    /// <returns>An array of all "systems". Each "system" is an array of connected pipes.</returns>
    internal List<List<Vector2>> GetAllPipeSystems()
    {
        UpdateAllPipeLocations();
        List<Vector2> uncategorizedPipeLocations = new();
        for (int i = 0; i < allPipeLocations.Count; i++)
        {
            uncategorizedPipeLocations.Add(allPipeLocations[i]);
        }
        // we now have a list of all uncategorized pipes.
        // Let's categorize them into their distinct "systems".
        List<List<Vector2>> allPipeSystems = new();
        // keep adding systems until we run out of pipes.
        while (uncategorizedPipeLocations.Count > 0)
        {
            List<Vector2> thisSystem = GetFullPipeSystem(uncategorizedPipeLocations[0]);
            // remove all pipes in the system from "uncategorizedPipeLocations"
            for (int i = 0; i < thisSystem.Count; i++)
            {
                uncategorizedPipeLocations.Remove(thisSystem[i]);
            }
            allPipeSystems.Add(thisSystem);
        }
        return allPipeSystems;
    }

    /// <summary>
    /// Does part of the UpdateSteam algorithm.
    /// </summary>
    /// <param name="system">A list of connected pipes.</param>
    internal void PropagateSteam(List<Vector2> system)
    {
        bool hasSteamSource = false;
        for (int i = 0; i < system.Count; i++)
        {
            //Block b = table.Get(system[i]).GetComponent<Block>();
            Block b = GetBlockData(system[i]);
            if (b.GetSteamState() == SteamState.SOURCE)
            {
                hasSteamSource = true;
                break;
            }
        }
        SteamState propagateMe = SteamState.EMPTY;
        if (hasSteamSource)
        {
            // Checking for leaks...
            // check if there are any stray connections that lead outside of the system
            propagateMe = SteamState.FULL;
            bool hasALeak = false;
            for (int i = 0; i < system.Count; i++)
            {
                // Get all connections
                List<Vector2> checkLocations = GetConnectionLocations(system[i]);
                for (int i2 = 0; i2 < checkLocations.Count; i2++)
                {
                    bool isInTheSystem = false;
                    // Check if they're all in the system
                    for (int i3 = 0; i3 < system.Count; i3++)
                    {
                        if (system[i3] == checkLocations[i2])
                        {
                            isInTheSystem = true;
                            break;
                        }
                    }
                    if (!isInTheSystem)
                    {
                        hasALeak = true;
                        break;
                    }
                }
                if (hasALeak) { break; }
            }
            if (hasALeak) { propagateMe = SteamState.LEAKING; }
        }

        // Apply the state to the whole system
        for (int i = 0; i < system.Count; i++)
        {
            //Block b = table.Get(system[i]).GetComponent<Block>();
            Block b = GetBlockData(system[i]);
            // Don't overwrite steam sources!
            if (b.GetSteamState() != SteamState.SOURCE)
            {
                b.SetSteamState(propagateMe);
            }
        }
    }

    


    //Gives you manual control to place cells. (Good for setting up tests)
    public void placeCellManual(GameObject cell, Vector2 pos)
    {
        table.Put(pos, cell);
    }
    public void Start()
    {
        table = new LookupTable<GameObject>(scale, scale);
        float xCoord = xPos - ((float)scale / 2);
        float yCoord = yPos - ((float)scale / 2);
        for (int i = 0; i < scale; i++)
        {
            for(int j = 0; j < scale; j++)
            {
                GameObject instantiated = Instantiate(emptyCell, gameObject.transform);
                instantiated.transform.localPosition = new Vector3(i + xPos + 0.5f, j + yPos + 0.5f, 0);
                instantiated.GetComponent<Cell>().xPos = i;
                instantiated.GetComponent<Cell>().yPos = j;
                instantiated.GetComponent<Cell>().CellStart();
                table.Put(i, j, instantiated);
            }
        }
        GameObject instantiatedPart = Instantiate(startingPart);
        instantiatedPart.GetComponent<Part>().FormTable();
        instantiatedPart.transform.localPosition = new Vector3(xPos + 0.5f, yPos + 0.5f, 0);
        Vector2 test = new Vector2(0, 0);
        MergeTables(instantiatedPart, test);
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
        Debug.Log("Can merge Ran");
        if(part.GetComponent<Part>().table == null)
        {
            Debug.Log("Bad Table");
        }
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
        // update steam here
        UpdateSteam();
    }

    public void UpdateAllPipeLocations()
    {
        allPipeLocations = new();
        for (int i_x = 0; i_x < table.x_size; i_x++)
        {
            for (int i_y = 0; i_y < table.y_size; i_y++)
            {
                Vector2 vec = new(i_x, i_y);
                if (!table.Get(i_x, i_y).GetComponent<Cell>().isEmpty)
                {
                    Block blockHere = GetBlockData(vec);
                    if (blockHere.GetAllLinksList().Count > 0)
                    {
                        allPipeLocations.Add(vec);
                    }
                }
            }
        }
    }



}
