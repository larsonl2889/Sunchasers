using System.Collections;
using System.Collections.Generic;
using Parts;
using UnityEngine;

public class BuildMat : MonoBehaviour
{
    internal BuildAreaTest buildAreaTest;
    internal float xPos;
    internal float yPos;

    // Start is called before the first frame update
    void Start()
    {
        buildAreaTest = gameObject.GetComponentInParent<BuildAreaTest>();
        gameObject.transform.localScale = new Vector3(buildAreaTest.scale, buildAreaTest.scale, 1);
        if(buildAreaTest.scale % 2 != 0)
        {
            float pos = ((float)buildAreaTest.scale / 2);
            xPos = pos + buildAreaTest.xPos;
            yPos = pos + buildAreaTest.yPos;
        }else
        {
            float pos = ((float)(buildAreaTest.scale / 2));
            xPos = pos + buildAreaTest.xPos - 0.5f;
            yPos = pos + buildAreaTest.yPos - 0.5f;
        }
        
        gameObject.transform.localPosition = new Vector3(xPos, yPos, 0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
