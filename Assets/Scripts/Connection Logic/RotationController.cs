using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DirectionOps;

public class RotationController : MonoBehaviour
{
    public GameObject emptyPart;

    void Awake()
    {
        DirectionOperator.SetEmptyPart(emptyPart);
    }
}
