using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cells;
using Blocks;
using Pipes;

public class Lever : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] Sprite onSprite;
    [SerializeField] Sprite offSprite;
    private bool isOn = false;
    private SpriteRenderer spriteRenderer;
    public GameObject source;
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void changeState() {
        isOn = !isOn;
        if (isOn)
        {
            spriteRenderer.sprite = onSprite;
            source.GetComponent<Cell>().GetBlock().GetComponent<Block>().SetSteamState(SteamState.SOURCE);
        }
        else
        {
            spriteRenderer.sprite = offSprite;
            source.GetComponent<Cell>().GetBlock().GetComponent<Block>().SetSteamState(SteamState.EMPTY);
        }

    }
}
