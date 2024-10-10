using System;
using Blocks;
using Cells;
using UnityEngine;

namespace Parts
{
    public class Part
    {
        private LookupTable<Cell> table;
        private Vector2 pivot; // location within its own table that'll be our "center". We place and rotate with respect to the pivot.
        private Vector2? posInWorld; // the position in world, if I'm in play.
        private LookupTable<Cell> buildArea;

        public Part(LookupTable<Cell> table, Vector2 pivot)
        {
            this.table = table;
            this.pivot = pivot;
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

        public void Extract()
        {
            posInWorld = null; // pulls the part out of play
            // TODO not implemented!
            // TODO set each of the subject world cells to empty themselves.
            // TODO set each of my Blocks to reference my own cells.
        }

    }
}

