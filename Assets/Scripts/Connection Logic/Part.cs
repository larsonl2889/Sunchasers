﻿using System;
using Blocks;
using Cells;
using DirectionOps;
using Testing;
using UnityEngine;

namespace Parts
{
    public class Part
    {
        internal LookupTable<Cell> table;
        private Vector2 pivot; // location within its own table that'll be our "center". We place and rotate with respect to the pivot.
        private Vector2? posInWorld; // the position in world, if I'm in play.
        private LookupTable<Cell> buildArea;

        public Part(LookupTable<Cell> table, Vector2 pivot)
        {
            this.table = table;
            this.pivot = pivot;
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
        public Vector2? GetPos()
        {
            return posInWorld;
        }

        public bool IsInPlay()
        {
            return posInWorld != null;
        }

        public bool CanMerge(Vector2 pos)
        {
            return false; // TODO not implemented!
        }

        public void Merge(Vector2 pos)
        {
            posInWorld = pos; // puts the part in play
            // TODO not implemented!
            // TODO set each of my blocks to reference the world's cells.
            // TODO set each of the subject world cells to reference my blocks.
        }

        public void SetPosInWorld(Vector2 pos)
        {
            this.posInWorld = pos;
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
            if (!IsInPlay())
            {
                // Do nothing if the part is already out-of-play.
                return;
            }


            for (int i_x = 0; i_x < table.x_size; i_x++)
            {
                for (int i_y = 0; i_y < table.y_size; i_y++)
                {
                    if (!table.Get(i_x, i_y).IsEmpty()) {
                        // get each block
                        Block tmp = table.Get(i_x, i_y).GetBlock();
                        // remove it from the build area
                        tmp.GetCell().EvictBlock();
                        // and hook it back up to the part
                        tmp.SetCell(table.Get(i_x, i_y));
                    }
                }
            }
            // pull the part out of play
            posInWorld = null;
        }

        

    }
}

