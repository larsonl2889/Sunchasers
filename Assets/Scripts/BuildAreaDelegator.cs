using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// MUST BE ON A BUILD AREA!

public class BuildAreaDelegator : MonoBehaviour
{

    public Vector2Int objectiveLocation;

    protected BuildAreaTest GetData()
    {
        return GetComponent<BuildAreaTest>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
