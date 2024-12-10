using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TogglableCollider : MonoBehaviour
{
    public void SetCollider(bool doEnable)
    {
        gameObject.GetComponent<BoxCollider2D>().enabled = doEnable;
    }
    public void EnableCollider() { SetCollider(true); }
    public void DisableCollider() { SetCollider(false); }
}