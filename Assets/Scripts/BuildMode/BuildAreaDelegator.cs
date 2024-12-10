using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

// MUST BE ON A BUILD AREA!

public class BuildAreaDelegator : MonoBehaviour
{
    public bool objectiveIsOn;
    public bool secondObjectiveIsOn;

    public UnityEvent activator;
    public UnityEvent deactivator;
    public UnityEvent secondActivator;
    public UnityEvent secondDeactivator;

    public void UpdateObjective()
    {
        // If we need to turn off the objective
        if (objectiveIsOn && !GetData().IsObjectiveOn()) {
            DeactivateObjective();
            objectiveIsOn = false;
        }
        // If we need to turn on the objective
        else if (!objectiveIsOn && GetData().IsObjectiveOn())
        {
            ActivateObjective();
            objectiveIsOn = true;
        }

        // do the same to number two
        // If we need to turn off the objective
        if (secondObjectiveIsOn && !GetData().IsSecondObjectiveOn())
        {
            DeactivateSecondObjective();
            secondObjectiveIsOn = false;
        }
        // If we need to turn on the objective
        else if (!secondObjectiveIsOn && GetData().IsSecondObjectiveOn())
        {
            ActivateSecondObjective();
            secondObjectiveIsOn = true;
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

    private void ActivateSecondObjective()
    {
        secondActivator.Invoke();
    }

    private void DeactivateSecondObjective()
    {
        secondDeactivator.Invoke();
    }
    protected BuildAreaTest GetData()
    {
        return GetComponent<BuildAreaTest>();
    }

}
