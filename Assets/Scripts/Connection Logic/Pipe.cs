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

        public Pipe(Connector pipeConnector, bool isSource=false)
            : base() // TODO put the parameters for Block in here!
        {
            // TODO implement!
        }
    }
}

