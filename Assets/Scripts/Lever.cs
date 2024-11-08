using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cells;
using Blocks;
using Pipes;
using UnityEngine.Events;
public class Lever : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] Sprite onSprite;
    [SerializeField] Sprite offSprite;
    private bool isOn = false;
    private SpriteRenderer spriteRenderer;
    public UnityEvent onLeverPull;
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
            
            
        }
        else
        {
            spriteRenderer.sprite = offSprite;
            
        }
        onLeverPull.Invoke();

    }
}