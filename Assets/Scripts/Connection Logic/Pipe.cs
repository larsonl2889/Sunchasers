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
    }
}

