using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

// Created by Leif Larson
// Last updated 2 Oct 2024

// THIS IS A TEST FILE

/// <summary>
/// This class performs a battery of tests on the LookupTable class.
/// </summary>
public class TEST_LookupTable : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        TestSpecifiedDefaultConstructor(); // labeled as SDC
        TestIntIntConstructor(); // labeled as IIC
    }

    void SubjectToBattery(string name, LookupTable<int> table)
    {
        // TESTING THE ERASER
        for (int i_x = 0; i_x < table.x_size; i_x++)
        {
            for (int i_y = 0; i_y < table.y_size; i_y++)
            {
                if (i_x == 3 || i_y == 5)
                {
                    table.Erase(i_x, i_y);
                }
            }
        }
        Debug.Log(name + ": Erase() test\n" + table.ReadTable());

        // TESTING READ-WRITE FUNCTIONALITY
        for (int i_x = 0; i_x < table.x_size; i_x++)
        {
            for (int i_y = 0; i_y < table.y_size; i_y++)
            {
                if (table.Get(i_x, i_y) == 0)
                {
                    table.Put(i_x, i_y, 42);
                }
                else if (table.Get(i_x, i_y) == 12)
                {
                    table.Put(i_x, i_y, 21);
                }
            }
        }
        Debug.Log(name + ": read-write test\n" + table.ReadTable());
    }

    void TestIntIntConstructor()
    {
        int X_SIZE = 12;
        int Y_SIZE = 12;

        // TESTING for the (int, int) CONSTRUCTOR
        LookupTable<int> table = new(X_SIZE, Y_SIZE);

        // TESTING DEFAULT
        Debug.Log("IIC" + ": Default value == " + table.default_value + " should be " + 0);

        // make a multiplication table
        for (int i_x = 0; i_x < table.x_size; i_x++)
        {
            for (int i_y = 0; i_y < table.y_size; i_y++)
            {
                int value = i_x * i_y;
                table.Put(i_x, i_y, (value == 0 ? -1 : value)); // set all zeros to -1's so I can tell when getters default.
            }
        }

        // read out the table to the log
        Debug.Log("IIC: Put() test\n" + table.ReadTable());

        // TESTING BOUNDS on the GETTER
        string accum = "IIC: Bounds testing...\n";
        accum += ("case 0: " + table.Get(X_SIZE, 0) + " = " + table.default_value) + "\n";
        accum += ("case 1: " + table.Get(X_SIZE, Y_SIZE) + " = " + table.default_value) + "\n";
        accum += ("case 2: " + table.Get(-1, 0) + " = " + table.default_value) + "\n";
        accum += ("case 3: " + table.Get(-2, -3) + " = " + table.default_value) + "\n";
        accum += ("case 4: " + table.Get(4, Y_SIZE + 2) + " = " + table.default_value) + "\n";
        accum += ("case 5: " + table.Get(X_SIZE + 3, 0) + " = " + table.default_value) + "\n";
        accum += ("case 6: " + table.Get(X_SIZE + 8989, -9001) + " = " + table.default_value) + "\n";
        accum += ("case 7: " + table.Get(5, -3) + " = " + table.default_value) + "\n";
        accum += ("case 8: " + table.Get(0, 0) + " = -1") + "\n"; // note: was deliberately changed to -1 to distinguish from unspecified default.
        accum += ("case 9: " + table.Get(4, 3) + " = 12") + "\n";
        accum += ("case 10: " + table.Get(9, 4) + " = 36") + "\n";
        Debug.Log(accum);

        SubjectToBattery("ICC", table);
    }

    void TestSpecifiedDefaultConstructor()
    {
        int X_SIZE = 12;
        int Y_SIZE = 12;

        // TESTING for the (int, int) CONSTRUCTOR
        LookupTable<int> table = new(X_SIZE, Y_SIZE, -1);

        // TESTING DEFAULT
        Debug.Log("SDC" + ": Default value == " + table.default_value + " should be " + -1);

        // make a multiplication table
        for (int i_x = 0; i_x < table.x_size; i_x++)
        {
            for (int i_y = 0; i_y < table.y_size; i_y++)
            {
                table.Put(i_x, i_y, i_x * i_y); // the default is already -1, so I don't have to mess with the data to get a good test out.
            }
        }
        // read out the table to the log
        Debug.Log("SDC: Put() test\n" + table.ReadTable());

        // TESTING BOUNDS on the GETTER
        string accum = "SDC: Bounds testing...\n";
        accum += ("case 0: " + table.Get(X_SIZE, 0) + " = " + table.default_value) + "\n";
        accum += ("case 1: " + table.Get(X_SIZE, Y_SIZE) + " = " + table.default_value) + "\n";
        accum += ("case 2: " + table.Get(-1, 0) + " = " + table.default_value) + "\n";
        accum += ("case 3: " + table.Get(-2, -3) + " = " + table.default_value) + "\n";
        accum += ("case 4: " + table.Get(4, Y_SIZE + 2) + " = " + table.default_value) + "\n";
        accum += ("case 5: " + table.Get(X_SIZE + 3, 0) + " = " + table.default_value) + "\n";
        accum += ("case 6: " + table.Get(X_SIZE + 8989, -9001) + " = " + table.default_value) + "\n";
        accum += ("case 7: " + table.Get(5, -3) + " = " + table.default_value) + "\n";
        accum += ("case 8: " + table.Get(0, 0) + " = 0") +"\n"; // note: is actually a 0. default here is set to -1.
        accum += ("case 9: " + table.Get(4, 3) + " = 12") + "\n";
        accum += ("case 10: " + table.Get(9, 4) + " = 36") + "\n";
        Debug.Log(accum);

        SubjectToBattery("SDC", table);
    }

}
