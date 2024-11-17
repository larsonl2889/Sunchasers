using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Testing;
using DirectionOps;

public class PipeIndexTest : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //List<TestBattery> allBatteries = new();
        TestBattery tb = new("MATH INDEX ROTATOR", "MIROT");
        string indecies = "";
        for (int alpha = 0; alpha < 4; alpha++)
        {
            string specific_indecies = "alpha = " + alpha + "\n";
            for (int i_4 = 0; i_4 < 4; i_4++)
            {
                for (int i_1 = 0; i_1 < 4; i_1++)
                {
                    specific_indecies += PipeIndexer.ApplyRotation(i_1 + i_4 * 4, DirectionOperator.ToDirection(alpha)) + " ";
                }
                specific_indecies += "\n";
            }
            indecies += specific_indecies;
            //allBatteries.Add(tb);
        }
        Debug.Log("INDECIES\n"+indecies);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
