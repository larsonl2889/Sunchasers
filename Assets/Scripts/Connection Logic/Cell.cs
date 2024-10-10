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

    }
}

