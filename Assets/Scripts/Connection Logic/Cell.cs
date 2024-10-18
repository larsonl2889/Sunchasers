using System;
using UnityEngine;
using Blocks;

// implements https://app.diagrams.net/#G1jlsRcd-Jo9MzWTEkTyTaZTvIuyx-oHzH#%7B%22pageId%22%3A%22C5RBs43oDa-KdzZeNtuy%22%7D

// TODO test!

namespace Cells
{
    /// <summary>
    /// Handles moving and storing blocks.
    /// </summary>
    public class Cell : MonoBehaviour
    {
        internal Vector2 pos;
        internal Cell cell;
        public int xPos;
        public int yPos;
        public GameObject block;
        public bool isEmpty = true;

        public Cell(Vector2 pos)
        {
            this.pos = pos;
        }
        /*
        public Cell(Block block, Vector2 pos)
        {
            this.block = block;
            this.pos = pos;
            isEmpty = false;
        }
        */
        public bool IsEmpty()
        {
            return isEmpty;
        }

        public GameObject GetBlock()
        {
            return block;
        }

        public void SetBlock(GameObject block)
        {
            this.block = block;
            isEmpty = false;
        }


        public void EvictBlock() { 
            block = null;
            isEmpty = true;
        }

        public void Start()
        {
        }

        public void CellStart()
        {
            pos.x = xPos;
            pos.y = yPos;
        }

        public void Update()
        {
            
        }

        /// <summary>
        /// Returns a string representation of the cell.
        /// <br></br>"Cell @(0, 0) w/ Block @(1, 3)"
        /// <br></br>Represents a Cell at position (0, 0) in its own lookup table, storing a block that is connected to some other cell at position (1, 3) in its lookup table.
        /// </summary>
        /// <returns>a string representation of the cell</returns>
        /*
        public override string ToString()
        {
            string s = "Cell @(" + pos.x + ", " + pos.y + ")";
            if (block != null)
            {
                s += " w/ " + block.ToString();
            }
            else
            {
                s += ", empty";
            }
            return s;
        }
        */

    }
}

