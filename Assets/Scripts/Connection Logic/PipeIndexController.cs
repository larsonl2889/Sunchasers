using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeIndexController : MonoBehaviour
{
    public GameObject[] pipes;
    public Sprite[] pipeSprites_fresh;
    public Sprite[] pipeSprites1;

    // Start is called before the first frame update
    void Awake()
    {
        List<Sprite[]> psv = new();
        psv.Add(pipeSprites_fresh);
        psv.Add(pipeSprites1);
        PipeIndexer.GenerateData(pipes, psv);
    }
}
