﻿using System;
using Cells;

// implements https://app.diagrams.net/#G1jlsRcd-Jo9MzWTEkTyTaZTvIuyx-oHzH#%7B%22pageId%22%3A%22C5RBs43oDa-KdzZeNtuy%22%7D

namespace Blocks
{
    /// <summary>
    /// Handles data that is logically shared by all blocks in the game.
    /// </summary>
    public class Block
    {
        /// <summary>
        /// Which cell I occupy in my parent part (if I'm in play) or in the world.
        /// </summary>
        private Cell cell;

        //private Part part;
        
        /// <summary>
        /// Block constructor. Requires the cell it will occupy in its parent part.
        /// </summary>
        /// <param name="partCell">My cell in my parent part.</param>
        public Block(Cell cell)
        {
            this.cell = cell;
        }

        /// <summary>
        /// Gets the cell I occupy right now.
        /// </summary>
        /// <returns>My cell I occupy.</returns>
        public Cell GetCell() { return cell; }

        /// <summary>
        /// Sets the cell I occupy.
        /// <br></br>(This doesn't update the Cell.)
        /// </summary>
        /// <param name="newCell">The cell I will now occupy.</param>
        public void SetCell(Cell newCell) 
        { 
            cell = newCell; 
        }

        ///// <summary>
        ///// Whether the part believes itself to be in the world somewhere.
        ///// </summary>
        ///// <returns>Whether the block thinks its in a cell somewhere in the world.</returns>
        //public bool IsInPlay()
        //{
        //    return part.IsInPlay();
        //}

    }
}

