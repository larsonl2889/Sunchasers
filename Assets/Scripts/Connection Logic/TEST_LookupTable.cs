using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class TEST_LookupTable : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        TestSpecifiedDefaultConstructor(); // labeled as SDC
        TestIntIntConstructor(); // labeled as IIC
    }

    void TestIntIntConstructor()
    {
        int X_SIZE = 12;
        int Y_SIZE = 12;

        // TESTING for the (int, int) CONSTRUCTOR
        LookupTable<int> table = new(X_SIZE, Y_SIZE);

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

        // TESTING THE ERASER
        for (int i_x = 0; i_x < table.x_size; i_x++)
        {
            for (int i_y = 0; i_y < table.y_size; i_y++)
            {
                if (i_x + i_y % 3 == 0)
                {
                    table.Erase(i_x, i_y);
                }
            }
        }
        Debug.Log("IIC: Erase() test\n" + table.ReadTable());

        // TESTING FOR FUN :)
        for (int i_x = 0; i_x < table.x_size; i_x++)
        {
            for (int i_y = 0; i_y < table.y_size; i_y++)
            {
                if (table.Get(i_x, i_y) == 0)
                {
                    table.Put(i_x, i_y, 42);
                }
                if (table.Get(i_x, i_y) == 12)
                {
                    table.Put(i_x, i_y, 21);
                }
            }
        }
        Debug.Log("IIC: Get() test\n" + table.ReadTable());
    }

    void TestSpecifiedDefaultConstructor()
    {
        int X_SIZE = 12;
        int Y_SIZE = 12;

        // TESTING for the (int, int) CONSTRUCTOR
        LookupTable<int> table = new(X_SIZE, Y_SIZE, -1);

        // make a multiplication table
        for (int i_x = 0; i_x < table.x_size; i_x++)
        {
            for (int i_y = 0; i_y < table.y_size; i_y++)
            {
                int value = i_x * i_y;
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

        // TESTING THE ERASER
        for (int i_x = 0; i_x < table.x_size; i_x++)
        {
            for (int i_y = 0; i_y < table.y_size; i_y++)
            {
                if (i_x + i_y % 3 == 0)
                {
                    table.Erase(i_x, i_y);
                }
            }
        }
        Debug.Log("SDC: Erase() test\n" + table.ReadTable());

        // TESTING FOR FUN :)
        for (int i_x = 0; i_x < table.x_size; i_x++)
        {
            for (int i_y = 0; i_y < table.y_size; i_y++)
            {
                if (table.Get(i_x, i_y) == 0)
                {
                    table.Put(i_x, i_y, 42);
                }
                if (table.Get(i_x, i_y) == 12)
                {
                    table.Put(i_x, i_y, 21);
                }
            }
        }
        Debug.Log("SDC: Get() test\n" + table.ReadTable());
    }

}
