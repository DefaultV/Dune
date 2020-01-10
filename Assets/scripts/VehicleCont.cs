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
    public ParticleSystem ps_dirt;
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
        controlAudio();
    }

    void keyboard() {
        Transform trns = Camera.main.transform;
        if (Input.GetKey(KeyCode.W)) {
            rb.AddForce(trns.forward * vec_speed * 2 * Time.deltaTime * 2000);
        }
        if (Input.GetKey(KeyCode.A)) {
            rb.AddForce(-trns.right * vec_speed * Time.deltaTime * 2000);
        }
        if (Input.GetKey(KeyCode.S)) {
            rb.AddForce(-trns.forward * vec_speed * 2 * Time.deltaTime * 2000);
        }
        if (Input.GetKey(KeyCode.D)) {
            rb.AddForce(trns.right * vec_speed * Time.deltaTime * 2000);
        }
        if (Input.GetKey(KeyCode.LeftShift)) {
            rb.AddForce(trns.forward * vec_speed * 2 * Time.deltaTime * 1000);
        }
        look();
        //Debug.Log(rb.angularVelocity);
        //if (rb.velocity.z >= 1) {
        //    rb.AddForce(-Vector3.forward * vec_speed);
        //}
        //Debug.Log(rb.velocity);
        //Debug.Log(rb.velocity);
    }

    void controlAudio() {
        //float speed = Vector3.Distance(rb.velocity, Vector3.zero);
        float vel = Vector3.Distance(rb.velocity, Vector3.zero);
        for (int i = 0; i < 2; i++) {
            GetComponents<AudioSource>()[i].pitch = Mathf.Max(.75f, vel / 3f);
        }
        if (operated)
            if (vel >= 2)
                Camera.main.fieldOfView = 60 + vel * 2;
    }

    void hover() {
        RaycastHit ray;
        Physics.Raycast(transform.position, -Vector3.up, out ray);
        //Debug.DrawRay(transform.position, -Vector3.up);
        float dist = Vector3.Distance(transform.position, ray.point);
        if (dist <= hover_dist) {
            rb.AddForce((Vector3.up * (hover_dist-dist) * hover_mult) * Time.deltaTime * 2000);
        }
    }
    public Transform vehiclemdl;
    void look() {
        Vector3 lTargetDir = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 10f)) - transform.position;
        lTargetDir.y = 0.0f;
        vehiclemdl.rotation = Quaternion.RotateTowards(vehiclemdl.rotation, Quaternion.LookRotation(Camera.main.transform.forward), Time.deltaTime * 200f);
    }

    public void activate(bool state) {
        operated = state;
        hover_dist = state ? 2.5f : 1.85f;
    }
}
