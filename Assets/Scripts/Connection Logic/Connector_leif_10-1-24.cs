using System;
using UnityEngine;
using DirectionOps;

// Created by Leif Larson
// last updated 10/9/2024

namespace Connectors
{
    public class Connector
    {
        private Vector2[] allLinks;
        
        public Connector(Vector2[] allLinks)
        {
            this.allLinks = allLinks;
        }

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
        }

    }
}

