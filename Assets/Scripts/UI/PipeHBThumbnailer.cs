using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using DirectionOps;

// Attach me to a part!

public class PipeHBThumbnailer : MonoBehaviour
{
    public Sprite upSprite;
    public Sprite rightSprite;
    public Sprite downSprite;
    public Sprite leftSprite;
    public Direction currentDirection;

    private Sprite GetSprite()
    {
        return currentDirection switch
        {
            Direction.UP => upSprite,
            Direction.LEFT => leftSprite,
            Direction.DOWN => downSprite,
            Direction.RIGHT => rightSprite,
            _ => null
        };
    }

    public void Start() { UpdateThumbnail(); }

    public void UpdateThumbnail() { GetComponent<Image>().sprite = GetSprite(); }

    private void Ncrement(int n)
    {
        currentDirection.Add(n.ToDirection());
        UpdateThumbnail();
    }

    public void Increment() { Ncrement(1); }

    public void Decrement() { Ncrement(3); }

}
