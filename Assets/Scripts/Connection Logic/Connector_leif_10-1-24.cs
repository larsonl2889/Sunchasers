using System;
using UnityEngine;
using DirectionOps;

namespace Connectors
{
    public class Connector
    {
        private Vector2[] allLinks;
        private Direction dir;
        private Vector2 absPos;
        public Vector2 parentPartPosition; // TODO this is debug and NOT actually stored here!

        public Connector(Vector2[] allLinks, Direction dir)
        {
            this.allLinks = allLinks;
            this.dir = dir;
        }

        /// <summary>
        /// Returns the direction.
        /// </summary>
        /// <returns>current direction</returns>
        public Direction GetDirection() { return dir; }

        public Vector2[] GetAllLinks() { return allLinks; }

        /// <summary>
        /// Applies a rotation to the single connector. Is used internally.
        /// </summary>
        /// <param name="rotation">the direction to use to rotate.</param>
        private void ApplyRotation(Direction rotation)
        {
            for (int i = 0; i < allLinks.Length; i++)
            {
                allLinks[i] = rotation.ApplyRotation(allLinks[i]);
            }
            dir = dir.Add(rotation);
        }

        // THIS IS A STAND IN FOR GetBlock().GetParentPart().GetPos() used for testing.
        public Vector2 GetParentPartPos()
        {
            return parentPartPosition;
        }

        public void Pivot(Direction rotation)
        {
            Vector2 deltaPos = absPos - GetParentPartPos();
            deltaPos = rotation.ApplyRotation(deltaPos);
            absPos = deltaPos + GetParentPartPos();
            ApplyRotation(rotation);
        }


    }
}

