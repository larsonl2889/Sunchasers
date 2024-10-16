using System.Collections;
using System.Collections.Generic;
using Cells;
using Parts;
using Blocks;
using Pipes;
using UnityEngine;
using System.Data;
using UnityEditor.U2D.Aseprite;
using System.Linq;
using Unity.VisualScripting;

public class BuildAreaTest
{
    internal LookupTable<Cell> table;
    public float scale = 1.0f;
    private bool isLegal = true;
    private Vector2[] allPipeLocations;

    /// <summary>
    /// Sets up the list of all pipe locations in the build area.
    /// </summary>
    public void SetAllPipeLocations()
    {
        allPipeLocations = new Vector2[0];
        for (int i_x = 0; i_x < table.x_size; i_x++)
        {
            for (int i_y = 0; i_y < table.y_size; i_y++)
            {
                Block blockHere = table.Get(i_x, i_y).GetBlock();
                if (blockHere is Pipe)
                {
                    allPipeLocations.Append(new Vector2(i_x, i_y));
                }
            }
        }
    }

    /// <summary>
    /// If there is a pipe here, return a list of locations of other pipes.
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <returns></returns>
    private Vector2[] GetAllConnections(Vector2 where)
    {
        int x = (int)where.x;
        int y = (int)where.y;
        // the locations to look up that are connected to me.
        Vector2[] augmentedLinks = new Vector2[0];
        if (table.IsIndexInBounds(x, y))
        {
            // Get the block here
            Block blockHere = table.Get(x, y).GetBlock();
            if (blockHere is Pipe)
            {
                // Get the pipe here
                Pipe pipeHere = (Pipe)blockHere;
                Vector2[] links = pipeHere.GetPipeConnector().GetAllLinks();
                
                for (int i = 0; i < links.Length; i++)
                {
                    augmentedLinks.Append( new Vector2(x, y) + links[i] );
                }
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
    public Vector2[] GetFullPipeSystem(Vector2 where)
    {
        // (0): Create fullSystem as an empty list of locations (Vector2).
        Vector2[] fullSystem = new Vector2[0];
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
    private void GetPartOfFullSystem(Vector2 where, Vector2[] fullSystem)
    {
        // (1): Add self to fullSystem
        fullSystem.Append(where);
        // (2): Get the list of locations I'm connected to
        Vector2[] allConnectedLocations = GetAllConnections(where);
        for (int i = 0; i < allConnectedLocations.Length; i++)
        {
            // (2a): Check if I'm already accounted for in fullSystem. If so, do NOT proceed to (2b) with me.
            bool isAlreadyInSystem = false;
            for (int i_sysCheck = 0; i_sysCheck < fullSystem.Length; i_sysCheck++)
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
                Vector2[] subConnectedLocations = GetAllConnections(allConnectedLocations[i]);
                bool connectsBack = false;
                for (int j = 0; j < subConnectedLocations.Length; j++)
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
    public Vector2[][] GetAllPipeSystems()
    {
        SetAllPipeLocations();
        List<Vector2> uncategorizedPipeLocations = new();
        for (int i = 0; i < allPipeLocations.Length; i++)
        {
            uncategorizedPipeLocations.Append(allPipeLocations[i]);
        }
        // we now have a list of all uncategorized pipes.
        // Let's categorize them into their distinct "systems".
        Vector2[][] allPipeSystems = new Vector2[0][];
        // keep adding systems until we run out of pipes.
        while (uncategorizedPipeLocations.Count > 0)
        {
            Vector2[] thisSystem = GetFullPipeSystem(uncategorizedPipeLocations[0]);
            // remove all pipes in the system from "uncategorizedPipeLocations"
            for (int i = 0; i < thisSystem.Length; i++)
            {
                uncategorizedPipeLocations.Remove(thisSystem[i]);
            }
            allPipeSystems.Append(thisSystem);
        }

        return allPipeSystems;
    }
    
    // TODO document!
    public void PropagateSteam(Vector2[] system)
    {
        bool hasSteamSource = false;
        for (int i = 0; i < system.Length; i++)
        {
            Block b = table.Get(system[i]).GetBlock();
            if (b is Pipe)
            {
                Pipe p = (Pipe)b;
                if (p.GetSteamState() == SteamState.SOURCE)
                {
                    hasSteamSource = true;
                    break;
                }
            }
        }
        SteamState propagateMe = SteamState.EMPTY;
        if (hasSteamSource)
        {
            // Checking for leaks...
            // check if there are any stray connections that lead outside of the system
            propagateMe = SteamState.FULL;
            bool hasALeak = false;
            for (int i = 0; i < system.Length; i++)
            {
                // Get all connections
                Vector2[] checkLocations = GetAllConnections(system[i]);
                for (int i2=0; i2<checkLocations.Length; i2++)
                {
                    bool isInTheSystem = false;
                    // Check if they're all in the system
                    for (int i3=0; i3<system.Length; i3++)
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
        for (int i = 0; i < system.Length; i++)
        {
            Block b = table.Get(system[i]).GetBlock();
            if (b is Pipe)
            {
                Pipe p = (Pipe)b;
                // Don't overwrite steam sources!
                if (p.GetSteamState() != SteamState.SOURCE)
                {
                    p.SetSteamState(propagateMe);
                }
            }
        }
    }

    public void PropagateAllSteam()
    {
        Vector2[][] allPipeSystems = GetAllPipeSystems();
        for (int system_index = 0; system_index < allPipeSystems.Length; system_index++)
        {
            PropagateSteam(allPipeSystems[system_index]);
        }
    }

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
                        table.Get((int)startPosition.x + i, (int)startPosition.y + j).SetBlock(part.table.Get(i, j).GetBlock());
                    }
                }
            }
            part.SetPosInWorld(startPosition);
        }
    }
}
