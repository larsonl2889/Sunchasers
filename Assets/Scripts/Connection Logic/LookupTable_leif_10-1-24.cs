using System;
using UnityEngine; // for debug logging only

// Created by Leif Larson
// Last updated 2 Oct 2024

// TODO the default_value is the SAME REFERENCE FOR ALL CELLS THIS IS BAAAAD!!

/// <summary>
/// A generic 2d lookup table class
/// <br></br>- Note: The array starts out full of a default_value. If unspecified, it's whatever default(<typeparamref name="T"/>) returns. This is usually either null or 0, depending on what <typeparamref name="T"/> is.
/// </summary>
/// <typeparam name="T">The type stored in the table.</typeparam>
public class LookupTable<T>
{
    // TODO the default_value is the SAME REFERENCE FOR ALL CELLS THIS IS BAAAAD!!

    internal int y_size;
    internal int x_size;
    internal T[,] grid;
    internal T default_value;

    /// <summary>
    /// Constructs an empty LookupTable.
    /// </summary>
    /// <param name="x_size">The number of columns. This is the x-axis maximum.</param>
    /// <param name="y_size">The number of rows. This is the y-axis maximum.</param>
    public LookupTable(int x_size, int y_size)
    {
        // TODO this is scuffed: is there a better way to overload this constructor without duplicating code?
        LookupTable<T> tmp = new(x_size, y_size, default(T));
        this.x_size = tmp.x_size;
        this.y_size = tmp.y_size;
        this.grid = tmp.grid;
        this.default_value = tmp.default_value;
    }

    /// <summary>
    /// Constructs an empty LookupTable.
    /// </summary>
    /// <param name="x_size">The number of columns. This is the x-axis maximum.</param>
    /// <param name="y_size">The number of rows. This is the y-axis maximum.</param>
    /// <param name="default_value">The value of an "empty" cell in the table.</param>
    public LookupTable(int x_size, int y_size, T default_value)
    {
        this.default_value = default_value;
        // determine if the given column and row counts are valid
        if (0 <= x_size && 0 <= y_size)
        {
            this.y_size = y_size;
            this.x_size = x_size;
        }
        else
        {
            this.y_size = 0; 
            this.x_size = 0;
            Debug.LogError(
                "LookupTable() constructor defaulted for a(n) " +
                x_size.ToString() + "x" + y_size.ToString() + " grid.\r" +
                "Malformed array is 0x0."
            );
        }
        // create the grids
        this.grid = new T[x_size, y_size];
        // fill the grid with default value
        for (int i_x = 0; i_x < x_size; i_x++)
        {
            for (int i_y = 0; i_y < y_size; i_y++)
            {
                grid[i_x, i_y] = default_value;
            }
        }

    }

    /// <summary>
    /// Returns whether the given x and y indecies are within the LookupTable.
    /// </summary>
    /// <param name="x">the x index</param>
    /// <param name="y">the y index</param>
    /// <returns>true if and only if (x,y) is in the LookupTable.</returns>
    private bool IsIndexInBounds(int x, int y)
    {
        return (x >= 0 && y >= 0 && x < x_size && y < y_size);
    }

    /// <summary>
    /// Puts the given object in the slot (x,y) in the LookupTable.
    /// </summary>
    /// <param name="x">target x index</param>
    /// <param name="y">target y index</param>
    /// <param name="obj">the object being housed at (x,y) in the LookupTable</param>
    public void Put(int x, int y, T obj)
    {
        if (IsIndexInBounds(x, y))
        {
            grid[x, y] = obj;
        }
        else
        {
            Debug.LogError(
                "LookupTable.Put("+x.ToString()+", "+y.ToString()+ ") is out of bounds for a(n) " +
                x_size.ToString() + "x" + y_size.ToString() + " grid.\r" +
                "Operation failed."
            );
        }
    }

    /// <summary>
    /// Sets the given slot (x,y) in the LookupTable to default_value
    /// </summary>
    /// <param name="x">target x index</param>
    /// <param name="y">target y index</param>
    public void Erase(int x, int y)
    {
        if (IsIndexInBounds(x, y))
        {
            grid[x, y] = default_value;
        }
        else
        {
            Debug.LogError(
                "LookupTable.Erase(" + x.ToString() + ", " + y.ToString() + ") is out of bounds for a(n) " +
                x_size.ToString() + "x" + y_size.ToString() + " grid.\r" +
                "Operation failed."
            );
        }
    }

    /// <summary>
    /// Gets the object at (x,y) in the LookupTable.
    /// </summary>
    /// <param name="x">target x index</param>
    /// <param name="y">target y index</param>
    /// <returns>The selected object, if it exists. Otherwise, returns default(<typeparamref name="T"/>).</returns>
    public T Get(int x, int y)
    {
        if (IsIndexInBounds(x, y))
        {
            return grid[x, y];
        }
        else
        {
            Debug.LogError(
                "LookupTable.Get(" + x.ToString() + ", " + y.ToString() + ") is out of bounds for a(n) " +
                x_size.ToString() + "x" + y_size.ToString() + " grid.\r" +
                "Returned default = "+default_value.ToString()+"."
            );
            return default_value;
        }
    }

    public void Put(Vector2 vec, T obj) { Put((int)vec.x, (int)vec.y, obj); }
    public void Erase(Vector2 vec) { Erase((int)vec.x, (int)vec.y); }
    public T Get(Vector2 vec) { return Get((int)vec.x, (int)vec.y); }

}

/// <summary>
/// A utility class used for testing code using the LookupTable.
/// <br></br>You will use the <see cref="ReadTable{T}(LookupTable{T})"/> method.
/// <br></br>It is recommended that you view the text in a monospace font (like the default in Notepad)
/// </summary>
public static class TableReader
{
    /// <summary>
    /// returns the number of digits in n
    /// </summary>
    /// <param name="n">some integer</param>
    /// <returns>digits in n</returns>
    private static int DigitCount(int n)
    {
        if (n == 0) { return 0; }
        return (int)Math.Floor(Math.Log10(Math.Abs(n) + 1));
    }

    // TODO this method is not functional!
    /// <summary>
    /// Returns a string with spaces added to the end to reach the desired length.
    /// </summary>
    /// <param name="s">the given string</param>
    /// <param name="desired_length">the desired length</param>
    /// <returns>a string of at least the desired length</returns>
    private static string PadSpace(string s, int desired_length)
    {
        while (s.Length < desired_length) { s += " "; }
        return s;
    }

    /// <summary>
    /// Returns a string representation of the lookup table (provided that <typeparamref name="T"/> has a ToString() method!)
    /// <br></br>- Note: It is recommended that you view the text in a monospace font (like the default in Notepad)
    /// </summary>
    /// <typeparam name="T">The type param in the LookupTable lt</typeparam>
    /// <param name="lt">The targeted LookupTable</param>
    /// <returns>A string representation of the table</returns>
    public static string ReadTable<T>(this LookupTable<T> lt)
    {
        const string VERTICAL_DIVIDER = " | ";
        const string VERTICAL_SPACER = "  ";
        const string LINE_BREAK = "\n";
        /*const string HORIZONTAL_DIVIDER = "-";*/

        // MEASURING THE TABLE'S COLUMNS
        int row_label_width = DigitCount(lt.x_size - 1);
        int col_width = DigitCount(lt.y_size - 1);
        // find the max size of an item in the table
        for (int i_y = 0; i_y < lt.y_size; i_y++)
        {
            for (int i_x = 0; i_x < lt.x_size; i_x++)
            {
                Math.Max(col_width, lt.Get(i_x, i_y).ToString().Length);
            }
        }

        // PRINTING THE TABLE
        // print the column labels
        string readout = PadSpace(String.Empty, DigitCount(lt.x_size - 1)) + VERTICAL_DIVIDER;
        for (int i_y = 0; i_y < lt.y_size; i_y++)
        {
            readout += PadSpace(i_y.ToString(), col_width) + VERTICAL_SPACER;
        }
        readout += LINE_BREAK;
        // print the table proper
        for (int i_y = 0; i_y < lt.y_size; i_y++)
        {
            // print the row label
            readout += PadSpace(i_y.ToString(), row_label_width) + VERTICAL_DIVIDER;
            // print the data in each cell
            for (int i_x = 0; i_x < lt.x_size; i_x++)
            {
                readout += PadSpace(lt.Get(i_x, i_y).ToString(), col_width) + VERTICAL_SPACER;
            }
            readout += LINE_BREAK;
        }
        return readout;
    }
}
