using System;
using Cells;
using UnityEditor;
using UnityEngine;
using Connectors;
using Pipes;
using System.Collections.Generic;
using DirectionOps;

// implements https://app.diagrams.net/#G1jlsRcd-Jo9MzWTEkTyTaZTvIuyx-oHzH#%7B%22pageId%22%3A%22C5RBs43oDa-KdzZeNtuy%22%7D

namespace Blocks
{
    /// <summary>
    /// Handles data that is logically shared by all blocks in the game.
    /// </summary>
    public class Block : MonoBehaviour
    {
        /// <summary>
        /// Which cell I occupy in my parent part (if I'm in play) or in the world.
        /// </summary>
        public GameObject cell;
        public Direction[] directions;
        private List<Vector2> links;
        public bool isSource;
        public int variant;
        public GameObject gusherParent;
        [SerializeField] private SteamState steamState;

        public void Awake()
        {
            // Find your cell.
            cell = transform.parent.gameObject;
            // Set the steam state
            if (isSource) { steamState = SteamState.SOURCE; }
            else { steamState = SteamState.EMPTY; }
            // create the links
            links = new List<Vector2>();
            for (int i = 0; i < directions.Length; i++)
            {
                links.Add(directions[i].ToVector());
            }
        }

        public int GetVariant()
        {
            return variant; // TODO implement!
        }

        /// <summary>
        /// Returns a list of all links as Vectors.
        /// </summary>
        /// <returns></returns>
        public List<Vector2> GetAllLinksList()
        {
            List<Vector2> list = new();
            foreach (Vector2 link in links)
            {
                list.Add(link);
            }
            return list;
        }
        public void ApplyRotation(Direction rotation)
        {
            for (int i = 0; i < links.Count; i++)
            {
                links[i] = rotation.ApplyRotation(links[i]);
            }
        }

        public SteamState GetSteamState() { return steamState; }

        public void SetSteamState(SteamState steamState) { this.steamState = steamState; }


        public bool IsOn()
        {
            return steamState == SteamState.FULL || steamState == SteamState.SOURCE;
        }

        public bool IsSteaming()
        {
            return steamState != SteamState.EMPTY;
        }

        /// <summary>
        /// Gets the cell I occupy right now.
        /// </summary>
        /// <returns>My cell I occupy.</returns>
        public GameObject GetCell() { return cell; }

        /// <summary>
        /// Sets the cell I occupy.
        /// <br></br>(This doesn't update the Cell.)
        /// </summary>
        /// <param name="newCell">The cell I will now occupy.</param>
        public void SetCell(GameObject newCell)
        {
            cell = newCell;
        }

        

    }
}

