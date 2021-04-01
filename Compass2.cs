using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Compass2 : MonoBehaviour
{
    public GameObject toFollow;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 dir = toFollow.transform.position;
        dir.y = 0.0f;
        transform.LookAt(dir);
    }
}
