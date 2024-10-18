using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectTest_William : MonoBehaviour
{
    // Start is called before the first frame update
    private SpriteRenderer sprite;
    [SerializeField] bool isOn;
    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
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
        
    }
    
}
