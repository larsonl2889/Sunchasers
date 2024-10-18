using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Blocks;
using Cells;
using Pipes;

public class Objective : MonoBehaviour
{
    public GameObject myCell;
    internal bool on;

    // Start is called before the first frame update
    void Start()
    {
        on = false;
    }

    public bool GetOn() { return on; }

    private void FixedUpdate()
    {
        on = myCell.GetComponent<Cell>().GetBlock().GetComponent<Block>().GetSteamState().IsOn();
    }
}
