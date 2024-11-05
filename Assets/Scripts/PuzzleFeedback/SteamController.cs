using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DirectionOps;

public class SteamController : MonoBehaviour
{
    [SerializeField]
    private Direction direction;

    // Start is called before the first frame update
    private void Start()
    {
        transform.eulerAngles = new Vector3(0f, 0f, direction.ToAngle());
    }

    public void SetSteaming(bool s)
    {
        if (s)
        {
            GetComponent<ParticleSystem>().Play();
        }
        else
        {
            GetComponent<ParticleSystem>().Stop();
        }
    }
}
