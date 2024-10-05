using System;
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
        /// Which cell I occupy in my parent part.
        /// </summary>
        private Cell partCell;
        /// <summary>
        /// Which cell I occupy in a build area, if any.
        /// <br></br>(Doesn't start with this value.)
        /// </summary>
        private Cell worldCell;
        
        /// <summary>
        /// Block constructor. Requires the cell it will occupy in its parent part.
        /// </summary>
        /// <param name="partCell">My cell in my parent part.</param>
        public Block(Cell partCell)
        {
            this.partCell = partCell;
        }

        /// <summary>
        /// Gets the cell I occupy in my parent part.
        /// </summary>
        /// <returns>My cell in my parent part.</returns>
        public Cell GetPartCell() { return partCell(); }

        /// <summary>
        /// Sets the cell I occupy in my parent part.
        /// <br></br>(This doesn't update the Cell.)
        /// </summary>
        /// <param name="newCell">The cell I will now occupy in my parent part.</param>
        public void SetPartCell(Cell newCell) 
        { 
            partCell = newCell; 
        }

        /// <summary>
        /// Gets the cell I occupy in my parent part.
        /// </summary>
        /// <returns>My cell in my parent part.</returns>
        public Cell GetCell() { return worldCell; }

        public void SetCell(Cell newCell)
        {
            worldCell = newCell;
        }

        public bool IsInPlay()
        {
            return worldCell != null;
        }

    }
}

