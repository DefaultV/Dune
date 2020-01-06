using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VehicleCont : MonoBehaviour
{
    // Start is called before the first frame update
    Rigidbody rb;
    public float hover_dist;
    public float hover_mult;
    public float vec_speed;
    [HideInInspector]
    public bool operated;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (operated)
            keyboard();
        hover();
    }

    void keyboard() {
        Transform trns = Camera.main.transform;
        if (Input.GetKey(KeyCode.W)) {
            rb.AddForce(trns.forward * vec_speed * 2);
        }
        if (Input.GetKey(KeyCode.A)) {
            rb.AddForce(-trns.right * vec_speed);
        }
        if (Input.GetKey(KeyCode.S)) {
            rb.AddForce(-trns.forward * vec_speed * 2);
        }
        if (Input.GetKey(KeyCode.D)) {
            rb.AddForce(trns.right * vec_speed);
        }
        look();

        //if (rb.velocity.z >= 1) {
        //    rb.AddForce(-Vector3.forward * vec_speed);
        //}
        //Debug.Log(rb.velocity);
    }

    void hover() {
        RaycastHit ray;
        Physics.Raycast(transform.position, -Vector3.up, out ray);
        Debug.DrawRay(transform.position, -Vector3.up);
        float dist = Vector3.Distance(transform.position, ray.point);
        if (dist <= hover_dist) {
            rb.AddForce(Vector3.up * (hover_dist-dist) * hover_mult);
        }
    }
    public Transform vehiclemdl;
    void look() {
        Vector3 lTargetDir = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 10f)) - transform.position;
        lTargetDir.y = 0.0f;
        vehiclemdl.rotation = Quaternion.RotateTowards(vehiclemdl.rotation, Quaternion.LookRotation(Camera.main.transform.forward), Time.time * 0.2f);
    }

    public void activate() {
        operated = true;
    }
}
