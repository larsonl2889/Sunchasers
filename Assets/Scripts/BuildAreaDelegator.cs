using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// MUST BE ON A BUILD AREA!

public class BuildAreaDelegator : MonoBehaviour
{

    public bool objectiveIsOn;

    public void UpdateObjective()
    {
        // If we need to turn off the objective
        if (objectiveIsOn && !GetData().IsObjectiveOn()) {
            DeactivateObjective();
        }
        // If we need to turn on the objective
        else if (!objectiveIsOn && GetData().IsObjectiveOn())
        {
            ActivateObjective();
        }
    }

    private void ActivateObjective()
    {
        // TODO
    } 

    private void DeactivateObjective()
    {
        // TODO
    }

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
