using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

// MUST BE ON A BUILD AREA!

public class PipeHBThumbnailer : MonoBehaviour
{
    public Image[] orderedSprites;
    public int currentIndex;

    

    private void Ncrement(int n)
    {
        currentIndex += n + 4; // add 4 because negatives and % are dumb and dumber in C#.
        currentIndex %= 4;
        GetComponent<Image>() = orderedSprites[currentIndex];
    }

    public void Increment() {
        Ncrement(1);
    }

    public void Decrement() {
        Ncrement(-1);
    }

}
