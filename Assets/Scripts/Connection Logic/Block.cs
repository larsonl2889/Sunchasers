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
        [SerializeField] private SteamState steamState;
        private static Sprite[] pipeSheetSprites;
        public bool isChameleonPipe;

        public void Start()
        {
            // Set the steam state
            if (isSource) { steamState = SteamState.SOURCE; }
            else { steamState = SteamState.EMPTY; }
            // create the links
            links = new List<Vector2>();
            for (int i = 0; i < directions.Length; i++)
            {
                links.Add(directions[i].ToVector());
            }
            // Do chameleon things 
            if (isChameleonPipe)
            {
                // Load the pipe sprites
                if (pipeSheetSprites == null)
                {
                    pipeSheetSprites = Resources.LoadAll<Sprite>("Object Art/Pipe_Sprite_Sheet_MicahSwerman_Oct_17");
                    Debug.Log("Pipe Sprite Count: " +  pipeSheetSprites.Length);
                }
                // Use the appropriate pipe sprite
                UpdateChameleonSprite();
            }
        }

        /// <summary>
        /// Gets the pipe sprite that matches the pipe connectors' directions, if it exists.
        /// </summary>
        public Sprite GetProperPipeSprite()
        {
            // Automatically select the appropriate pipe sprite
            // 2-way pipes + straights
            if (links.Count == 2)
            {
                // if the pipe is straight
                if ((links[0] - links[1]).magnitude < 0.1)
                {
                    if (links.Contains(Direction.UP.ToVector()))
                    {
                        // return the UP-DOWN pipe sprite!
                    }
                    else
                    {
                        return pipeSheetSprites[0];
                    }
                }
                // if the pipe is an elbow (L-shaped)
                else if (links.Contains(Direction.UP.ToVector()))
                {
                    if (links.Contains(Direction.LEFT.ToVector()))
                    {
                        return pipeSheetSprites[6];
                    }
                    else
                    {
                        return pipeSheetSprites[7];
                    }
                }
                else
                {
                    if (links.Contains(Direction.RIGHT.ToVector()))
                    {
                        return pipeSheetSprites[8];
                    }
                    else
                    {
                        return pipeSheetSprites[9];
                    }
                }
            }
            // 3-way pipes
            else if (links.Count == 3)
            {
                if (!links.Contains(Direction.DOWN.ToVector())) { return pipeSheetSprites[2]; }
                if (!links.Contains(Direction.UP.ToVector())) { return pipeSheetSprites[3]; }
                if (!links.Contains(Direction.LEFT.ToVector())) { return pipeSheetSprites[4]; }
                if (!links.Contains(Direction.RIGHT.ToVector())) { return pipeSheetSprites[5]; }
            }
            // 4-way pipes and others
            return pipeSheetSprites[1];
        }

        public void UpdateChameleonSprite()
        {
            GetComponent<SpriteRenderer>().sprite = GetProperPipeSprite();
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
            if (isChameleonPipe)
            {
                UpdateChameleonSprite();
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

