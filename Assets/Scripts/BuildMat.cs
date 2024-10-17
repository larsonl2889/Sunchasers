using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildMat : MonoBehaviour
{
    internal BuildAreaTest buildArea;
    // Start is called before the first frame update
    void Start()
    {
        buildArea = gameObject.GetComponentInParent<BuildAreaTest>();
        gameObject.transform.localScale = new Vector3(buildArea.scale, buildArea.scale, 1f);
        float pos = (float)buildArea.scale / 2;
        gameObject.transform.localPosition = new Vector3(pos + buildArea.xPos, pos + buildArea.yPos, 0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
