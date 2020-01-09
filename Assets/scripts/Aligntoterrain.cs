using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aligntoterrain : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        align();
    }

    // Update is called once per frame
    void Update()
    {
        //align();
    }

    void align() {
        RaycastHit hit;

        if (Physics.Raycast(transform.position, -Vector3.up, out hit)) {
            // Find the line from the gun to the point that was clicked.
            Vector3 incomingVec = hit.point - transform.position;
            /*float dist = Vector3.Distance(hit.point, transform.position);
            Debug.Log(dist);
            if (dist > min_dist) {
                Vector3 newpos = transform.position;
                newpos.y = Mathf.Abs(min_dist + (1-dist));
                transform.position = newpos;
            }*/
            // Use the point's normal to calculate the reflection vector.
            Vector3 reflectVec = Vector3.Reflect(incomingVec, hit.normal);

            // Draw lines to show the incoming "beam" and the reflection.
            Vector3 rot = Quaternion.LookRotation(reflectVec).eulerAngles;
            rot.y = 0;
            transform.GetChild(0).eulerAngles = rot;
            Debug.DrawLine(transform.position, hit.point, Color.red);
            Debug.DrawRay(hit.point, reflectVec, Color.green);
        }
    }
}
