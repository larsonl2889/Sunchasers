using System;
using UnityEngine;
using DirectionOps;
using System.Collections.Generic;

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

        public List<Vector2> GetAllLinksList()
        {
            Vector2[] links = GetAllLinks();
            List<Vector2> accum = new();
            foreach (Vector2 link in links)
            {
                accum.Add(link);
            }
            return accum;
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

        public override string ToString()
        {
            string units = String.Empty;
            string unusuals = String.Empty;
            for (int i = 0; i < allLinks.Length; i++)
            {
                // if the vector is an orthogonal unit vector, abbreviate it
                if (allLinks[i] == Vector2.up) { units += "u"; }
                else if (allLinks[i] == Vector2.left) { units += "l"; }
                else if (allLinks[i] == Vector2.down) { units += "d"; }
                else if (allLinks[i] == Vector2.right) { units += "r"; }
                // otherwise, just write it out.
                else { unusuals += "(" + allLinks[i].x + ", " + allLinks[i].y + ")"; }
            }
            string s = "{" + units + unusuals + "}";
            return s;
        }

    }
}

