using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeIndexController : MonoBehaviour
{
    public GameObject[] pipes;
    public Sprite[] pipeSprites;

    // Start is called before the first frame update
    void Awake()
    {
        PipeIndexer.GenerateData(pipes, pipeSprites);
    }
}
