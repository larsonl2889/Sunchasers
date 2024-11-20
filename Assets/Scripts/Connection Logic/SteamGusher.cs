using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DirectionOps;
using UnityEditor.Callbacks;

public class SteamGusher : MonoBehaviour
{
    public ParticleSystem up;
    public ParticleSystem left;
    public ParticleSystem down;
    public ParticleSystem right;
    private AudioSource steamSound;
    
    
    
    private void Awake()
    {
        steamSound = GetComponent<AudioSource>();
        
    }
    void Start()
    {
        QuellAllGushers();
    }

    /// <summary>
    /// Given a list of directions, update the gushers to match.
    /// </summary>
    /// <param name="dirs">List of directions</param>
    public void UpdateGushers(List<Direction> dirs)
    {

        // UP
        if (dirs.Contains(Direction.UP))
        {
            up.Play();
           
        }
        else 
        { 
            up.Stop();

        }
        // LEFT
        if (dirs.Contains(Direction.LEFT))
        {
            left.Play();
            
        }
        else
        {
            left.Stop();
            
        }
        // DOWN
        if (dirs.Contains(Direction.DOWN))
        {
            down.Play();
            
        }
        else
        {
            down.Stop();
           

        }
        // RIGHT
        if (dirs.Contains(Direction.RIGHT))
        {
            right.Play();
            
        }
        else
        {
            right.Stop();
            
        }
        if (dirs.Count > 0) {
            steamSound.enabled = true;
        }
        else
        {
            steamSound.enabled = false;
        }

        
       

    }
    
    public void playSound()
    {
        steamSound.enabled = true;
    }
    
    /// <summary>
    /// Shuts off all gushers.
    /// </summary>
    public void QuellAllGushers()
    {
        up.Stop();
        left.Stop();
        down.Stop();
        right.Stop();
        steamSound.enabled = false;

    }

}
