using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class FindNonURPMaterials : MonoBehaviour
{
    [MenuItem("Tools/Find Non-URP Materials")]
    static void FindMaterials()
    {
        var materials = Resources.FindObjectsOfTypeAll<Material>();
        foreach (var mat in materials)
        {
            if (!mat.shader.name.Contains("Universal Render Pipeline"))
            {
                Debug.Log("Non-URP Material Found: " + mat.name, mat);
            }



        }

    }
}
