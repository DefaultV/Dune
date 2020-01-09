using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotate_const : MonoBehaviour
{
    float speed = 15.0f;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.GetChild(0).localEulerAngles += new Vector3(0, Time.deltaTime * speed, 0);
    }
}
