using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

// MUST BE ON A BUILD AREA!

public class BuildAreaDelegator : MonoBehaviour
{
    public bool objectiveIsOn;

    public UnityEvent activator;
    public UnityEvent deactivator;

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
        activator.Invoke();
    } 

    private void DeactivateObjective()
    {
        deactivator.Invoke();
    }

    protected BuildAreaTest GetData()
    {
        return GetComponent<BuildAreaTest>();
    }

}
