using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TEST_LookupTable : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        int X_SIZE = 12;
        int Y_SIZE = 12;
        LookupTable<int> table = new(X_SIZE, Y_SIZE);

        // make a multiplication table
        for (int i_x = 0; i_x < table.x_size; i_x++)
        {
            for (int i_y = 0; i_y < table.y_size; i_y++)
            {
                table.Put(i_x, i_y, i_x * i_y);
            }
        }

        // read out the table to the log
        Debug.Log(table.ReadTable());
        Console.WriteLine(table.ReadTable());
    }

}
