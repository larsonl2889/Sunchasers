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
    public class Cell
    {
        internal Vector2 pos;
        internal Block block;

        public Cell(Vector2 pos)
        {
            this.pos = pos;
        }

        public Cell(Block block, Vector2 pos)
        {
            this.block = block;
            this.pos = pos;
        }

        public bool IsEmpty()
        {
            return block == null;
        }

        public Block GetBlock()
        {
            return block;
        }

        public void SetBlock(Block block)
        {
            this.block = block;
        }


        public Block EvictBlock() { 
            Block tmp = block;
            block = null;
            return tmp;
        }

        /// <summary>
        /// Returns a string representation of the cell.
        /// <br></br>"Cell @(0, 0) w/ Block @(1, 3)"
        /// <br></br>Represents a Cell at position (0, 0) in its own lookup table, storing a block that is connected to some other cell at position (1, 3) in its lookup table.
        /// </summary>
        /// <returns>a string representation of the cell</returns>
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

    }
}

