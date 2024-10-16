using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectTest_William : MonoBehaviour
{
    // Start is called before the first frame update
    private SpriteRenderer sprite;
    private bool isOn = false;
    private void Awake()
    {
        sprite = GetComponent<SpriteRenderer>();
    }
    void Start()
    {
        sprite.color = Color.red;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void changeColor() {
        isOn = !isOn;
        if (isOn)
        {
            sprite.color = Color.green;
        }
        else
        {
            sprite.color = Color.red;
        }
        
        
        Debug.Log("event yes");
    }

    private void OnMouseEnter()
    {
        Debug.Log("test");
    }
    private void OnMouseExit()
    {
    }
}
