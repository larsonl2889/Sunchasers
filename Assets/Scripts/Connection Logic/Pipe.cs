using System;
using Blocks;
using Cells;
using Connectors;

// Contributors: Leif Larson
// Last updated 10/9/2024

namespace Pipes
{

    public class Pipe : Block
    {
        private SteamState steamState;
        private Connector pipeConnector;

        public Pipe(Connector pipeConnector, Cell cell, bool isSource = false) : base(cell)
        {
            this.pipeConnector = pipeConnector;
            this.steamState = isSource switch
            {
                true => SteamState.SOURCE,
                false => SteamState.EMPTY,
            };
        }

        /// <summary>
        /// Doesn't allow modifying SOURCEs.
        /// </summary>
        /// <param name="steamState"></param>
        public void SetSteamState(SteamState steamState)
        {
            if (this.steamState != SteamState.SOURCE)
            {
                this.steamState = steamState;
            }
        }

        public SteamState GetSteamState() { return this.steamState; }

        /// <summary>
        /// Returns the pipe connector
        /// </summary>
        /// <returns>the pipe connector</returns>
        public Connector GetPipeConnector() { return pipeConnector; }

        /// <summary>
        /// Returns a string representation of the pipe. e.g.,
        /// <br></br>"Pipe{uld}, @(1,3)"
        /// <br></br>Would represent a pipe with connectors for UP, LEFT, DOWN, but not RIGHT, and is located in a cell at position (1,3) in its lookup table.
        /// </summary>
        /// <returns>Returns a string representation of the pipe.</returns>
        public override string ToString()
        {
            string s = "Pipe" + pipeConnector.ToString();

            if (cell == null)
            {
                s += ", orphaned";
            }
            else
            {
                // ... in cell @... (coordinates)
                s += ", @(" + cell.pos.x + ", " + cell.pos.y + ")";
            }
            return s;
        }
    }
}

