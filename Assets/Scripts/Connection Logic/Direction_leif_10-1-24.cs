﻿using System;
using UnityEngine;
using System.Collections.Generic;
using Parts;
using Cells;
using Blocks;
using System.Collections;
using UnityEngine.UIElements;

// Contributors: Leif Larson, Kilian Wilinski
// Last updated 21 Nov 2024

// Contains Direction and DirectionOps
namespace DirectionOps
{

    /// <summary>
    /// An enum for handling the 4 cardinal directions efficiently.
    /// <br></br>Is usefuil for interfacing between the floats used by Unity's GameObjects for angles, and the coordinate system used by the LookupTable. 
    /// <br></br>Use the DirectionOps utility class to access the operations.
    /// </summary>
    public enum Direction : int
    {
        UP = 0, LEFT = 1, DOWN = 2, RIGHT = 3
    }

    /// <summary>
    /// A utility class for performing operations on and with Direction objects.
    /// </summary>
    public static class DirectionOperator
    {
        public static GameObject emptyPart;

        public static void SetEmptyPart(GameObject newEmptyPart) { emptyPart = newEmptyPart; }
        /*
        public static GameObject RotatePart(GameObject partObject, Direction dir)
        {
            List<GameObject> newCellsList = new();
            Debug.Log("Called DirectionOperator.RotatePart()");
            Part part = partObject.GetComponent<Part>();
            if (part.table == null)
            {
                Debug.Log("Forming previously empty table for rotation :)");
                part.FormTable();
            }
            GameObject newPartObject = GameObject.Instantiate(emptyPart);
            LookupTable<GameObject> newTable = new (part.tableSize, part.tableSize);
            // iterate over all the cell objects in this table.
            for (int i_x = 0; i_x < newTable.x_size; i_x++)
            {
                for (int i_y = 0; i_y < newTable.y_size; i_y++)
                {
                    // calculate new position
                    Vector2 new_pos = dir.ApplyRotation((new Vector2(i_x, i_y) - part.GetPivot())) + part.GetPivot();
                    // Calculate the list of connection directions...
                    List<Vector2> vecsList = new();
                    // debug stuff TODO
                    GameObject CellObject = part.table.Get(i_x, i_y);
                    Debug.LogWarning("name @ (" + i_x + ", " + i_y + ") = '" + CellObject.name + "'");
                    Cell CellData = CellObject.GetComponent<Cell>();
                    GameObject BlockObject = CellData.GetBlock();
                    if (BlockObject != null)
                    {
                        Block BlockData;
                        try
                        {
                            BlockData = BlockObject.GetComponent<Block>();
                            vecsList = BlockData.GetAllLinksList();
                        }
                        catch (Exception e)
                        {
                            Debug.LogError("Failed getting BlockData, DirectionOperator.RotatePart(), Cell = " + CellObject.name + "\n" + e.Message);
                        }

                        // actual statement
                        //vecsList = part.table.Get(i_x, i_y).GetComponent<Cell>().GetBlock().GetComponent<Block>().GetAllLinksList();


                        List<Direction> dirList = new();
                        int variant = part.table.Get(i_x, i_y).GetComponent<Cell>().GetBlock().GetComponent<Block>().GetVariant();
                        foreach (Vector2 v in vecsList)
                        {
                            dirList.Add(v.ToDirection());
                        }
                        // ... And use it to calculate the new index
                        int mathIndex = PipeIndexer.DirectionsToMathIndex(dirList);
                        GameObject newCellObject;
                        // if the object is a pipe
                        if (mathIndex > 0)
                        {
                            // Make the game object
                            newCellObject = PipeIndexer.Instantiate(mathIndex, newPartObject.transform, variant);
                        }
                        // if the object is not a pipe
                        else
                        {
                            // Just don't rotate it if it's not a pipe.
                            newCellObject = GameObject.Instantiate(part.table.Get(i_x, i_y), newPartObject.transform);
                        }
                        
                        // Adjust each cell's position...
                        //      ... in engine
                        newCellObject.transform.localPosition = new_pos;
                        //      ... in data
                        newCellObject.GetComponent<Cell>().pos = new_pos;
                    }
                    else
                    {
                        // do nothing lol :)
                        Debug.Log("DirectionOperator.RotatePart() ... Did nothing on (" + i_x + ", " + i_y + ")");
                    }
                    
                    

                }
            }
            // Create the new rotated part
            // TODO go over what data needs to be loaded into a part!
            if (emptyPart == null) { Debug.LogError("No 'emptyPart' set for DirectionOps!"); }
            newPartObject.AddComponent<Part>();
            newPartObject.GetComponent<Part>().table = newTable;
            newPartObject.GetComponent<Part>().tableSize = newTable.x_size;
            newPartObject.GetComponent<Part>().SetPivot(part.GetPivot());
            // And we'll call the actual method for this too
            newPartObject.GetComponent<Part>().FormTable();
            return newPartObject;
        }
        */
        public static GameObject RotatePart(GameObject partObject, Direction dir)
        {
            List<GameObject> rotatedCells = new();

            partObject.GetComponent<Part>().FormTable();

            for (int i = 0; i < partObject.GetComponent<Part>().tableSize; i++)
            {
                for (int j = 0; j < partObject.GetComponent<Part>().tableSize; j++)
                {
                    GameObject cell = partObject.GetComponent<Part>().table.Get(i, j);
                        if (!cell.GetComponent<Cell>().isEmpty)
                        {
                            Vector2 cellLocation = new Vector2(i, j);
                            Vector2 newLocation = new Vector2(j, -i);
                            cell.GetComponent<Cell>().xPos = (int)newLocation.x;
                            cell.GetComponent<Cell>().yPos = (int)newLocation.y;
                            rotatedCells.Add(cell);
                        }
                }
            }
            bool checking = true;
            while(checking)
            {
                checking = false;
                bool foundY = false;
                bool foundX = false;
                foreach(GameObject cell in rotatedCells)
                {
                    if(cell.GetComponent<Cell>().yPos < 0)
                    {
                        foundY = true; 
                    }
                    if(cell.GetComponent<Cell>().xPos == 0)
                    {
                        foundX = true;
                    }
                }
                if (foundY)
                {
                    foreach (GameObject cell in rotatedCells)
                    {
                        cell.GetComponent<Cell>().yPos += 1;
                    }
                }
                if(!foundX)
                {
                    foreach(GameObject cell in rotatedCells)
                    {
                        cell.GetComponent<Cell>().xPos -= 1;
                    }
                }
                if(foundY || !foundX)
                {
                    checking = true;
                }
            }
            List<GameObject> placementCells = new();
            foreach(GameObject cell in rotatedCells)
            {
                Debug.Log("Rotated Cells Instantiated");
                GameObject instantiated = GameObject.Instantiate(cell, partObject.transform);
                //Doing Rotation on each cell
                //TODO: Use DirectionOperator.ApplyRotation() instead
                List<Direction> listOfDirections = new();
                foreach (Vector2 link in instantiated.GetComponent<Cell>().GetBlock().GetComponent<Block>().GetAllLinksList())
                {
                    listOfDirections.Add(link.ToDirection());
                }
                // calculate the new sprite, if the object is a pipe.
                if (listOfDirections.Count > 0)
                {
                    List<Direction> newListOfDirections = new();
                    foreach (Direction d in listOfDirections)
                    {
                        newListOfDirections.Add(dir.Add(d));
                    }
                    // Calculate math index and variant from the instantiated block
                    int mathIndex = PipeIndexer.DirectionsToMathIndex(newListOfDirections);
                    int variant = instantiated.GetComponent<Cell>().GetBlock().GetComponent<Block>().variant;
                    // destroy its block
                    GameObject.Destroy(instantiated.GetComponent<Cell>().GetBlock());
                    // and make the new one
                    GameObject instantiatedCell = PipeIndexer.Instantiate(mathIndex, instantiated.transform, variant);
                    GameObject instantiatedBlock = GameObject.Instantiate(instantiatedCell.GetComponent<Cell>().GetBlock(), instantiated.transform);
                    GameObject.Destroy(instantiatedCell);
                    instantiated.GetComponent<Cell>().SetBlock(
                        instantiatedBlock
                    );
                    // And give the block the reference to its cell
                    instantiated.GetComponent<Cell>().GetBlock().GetComponent<Block>().SetCell(instantiated);
                    
                }
                
                
                //Update Sprited For Instantiated Cells
                //TODO: ^^^^^
                instantiated.transform.localPosition = new Vector2(instantiated.GetComponent<Cell>().xPos, instantiated.GetComponent<Cell>().yPos);
                placementCells.Add(instantiated);
            }
            for(int i = 0; i < partObject.GetComponent<Part>().tableSize; i++)
            {
                for(int j = 0; j < partObject.GetComponent<Part>().tableSize; j++)
                {
                    GameObject.Destroy(partObject.GetComponent<Part>().table.Get(i, j));
                }
            }
            partObject.GetComponent<Part>().FormTable();
            Debug.Log("Part Rotated");
            //GameObject instantiatedPart = GameObject.Instantiate(partObject);
            //GameObject.Destroy(partObject);
            return partObject;
        }

        /// <summary>
        /// TODO document!<br>
        /// Default is Direction.UP
        /// </summary>
        /// <param name="vec">given vector</param>
        /// <returns>direction</returns>
        public static Direction ToDirection(this Vector2 vec)
        {
            if (vec == Vector2.up) { return (Direction.UP); }
            if (vec == Vector2.right) { return (Direction.RIGHT); }
            if (vec == Vector2.down) { return (Direction.DOWN); }
            if (vec == Vector2.left) { return (Direction.LEFT); }
            return Direction.UP; // default is Direction.UP
        }

        public static string ToString(this Direction dir)
        {
            return dir switch
            {
                Direction.UP => "UP",
                Direction.LEFT => "LEFT",
                Direction.DOWN => "DOWN",
                // RIGHT
                _ => "RIGHT",
            };
        }

        /// <summary>
        /// Given the id, returns the Direction
        /// </summary>
        /// <param name="id">the int Id</param>
        /// <returns>the Direction</returns>
        public static Direction ToDirection(this int id)
        {
            // because the modulo operator does not work as expected for negative
            // numbers, this loop adds 4's until the number is non-negative.
            while (id < 0) { id += 4; }
            id %= 4;
            return id switch
            {
                0 => Direction.UP,
                1 => Direction.LEFT,
                2 => Direction.DOWN,
                _ => Direction.RIGHT,
            };
        }

        /// <summary>
        /// Converts the direction to its int id
        /// </summary>
        /// <param name="dir">this direction</param>
        /// <returns>the relevant id</returns>
        public static int ToId(this Direction dir)
        {
            return dir switch
            {
                Direction.UP => 0,
                Direction.LEFT => 1,
                Direction.DOWN => 2,
                // RIGHT
                _ => 3,
            };
        }

        /// <summary>
        /// Given the direction as an enum, gives the angle in degrees.
        /// </summary>
        /// <param name="dir">the Direction enum</param>
        /// <returns>angle in degrees.</returns>
        public static float ToAngle(this Direction dir)
        {
            return dir switch
            {
                Direction.UP => 0.0f,
                Direction.LEFT => 90.0f,
                Direction.DOWN => 180.0f,
                // RIGHT
                _ => 270.0f,
            };
        }

        /// <summary>
        /// Adds two directions together. Good for adding rotations together.
        /// </summary>
        /// <param name="dir1">The first direction. Order doesn't matter.</param>
        /// <param name="dir2">The second direction. Order doesn't matter.</param>
        /// <returns>the resultant Direction</returns>
        public static Direction Add(this Direction dir1, Direction dir2)
        {
            return ToDirection(( dir1.ToId() + dir2.ToId() ) % 4);
        }

        /// <summary>
        /// Converts the direction to a vector
        /// </summary>
        /// <param name="dir">the direction</param>
        /// <returns></returns>
        public static Vector2 ToVector(this Direction dir)
        {
            return dir switch
            {
                Direction.UP => Vector2.up,
                Direction.LEFT => Vector2.left,
                Direction.DOWN => Vector2.down,
                // RIGHT
                _ => Vector2.right,
            };
        }

        /// <summary>
        /// Applies the direction as a rotation upon a given vector.
        /// </summary>
        /// <param name="dir">this direction</param>
        /// <param name="vec">the subject vector</param>
        /// <returns>the rotated vector</returns>
        public static Vector2 ApplyRotation(this Direction dir, Vector2 vec)
        {
            return dir switch
            {
                Direction.UP => vec,
                Direction.LEFT => new Vector2(-vec.y, vec.x),
                Direction.DOWN => -vec,
                // RIGHT
                _ => new Vector2(vec.y, -vec.x),
            };
        }


    }


}

