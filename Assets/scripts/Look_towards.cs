using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Look_towards : MonoBehaviour
{
    Vector3 orig = new Vector3(0, 0, 1);
    
    void Update()
    {
        look();
    }

    void look() {
        transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(orig), Time.time * 0.5f);
    }
}
