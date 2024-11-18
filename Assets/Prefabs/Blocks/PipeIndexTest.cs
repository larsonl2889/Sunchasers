using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Testing;
using DirectionOps;

// Contributors: Leif Larson
// Last updated 18 Nov 2024
// Last result: 18 Nov 2024, all tests PASSED

public class PipeIndexTest : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //List<TestBattery> allBatteries = new();
        TestBattery tb = new("MATH INDEX ROTATOR", "MIROT");
        string[] expected_values =
        {
            "alpha = 0\n0 1 2 3 \n4 5 6 7 \n8 9 10 11 \n12 13 14 15 \n",
            "alpha = 1\n0 8 1 9 \n2 10 3 11 \n4 12 5 13 \n6 14 7 15 \n",
            "alpha = 2\n0 4 8 12 \n1 5 9 13 \n2 6 10 14 \n3 7 11 15 \n",
            "alpha = 3\n0 2 4 6 \n8 10 12 14 \n1 3 5 7 \n9 11 13 15 \n"
        };
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
            
            tb.Test("alpha = " + alpha, expected_values[alpha], specific_indecies);

        }
        Debug.Log("INDECIES\n"+indecies);
        Debug.LogWarning(tb.GetRecord());
        Debug.LogWarning(tb.GetSummary());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
