using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    float orig_speed = 2f;
    float speed;
    public float sensitivity = 2f;
    float gravity = 9.82f;
    CharacterController player;
    public GameObject helm_ui;
    public GameObject eyes;
    public GameObject eye_holder;

    float moveFB;
    float moveLR;

    float camfollowspeed = 10f;

    float rotX;
    float rotY;
    float vSpeed;

    bool canMove = true;
    public bool npc_interacting = false;
    // Use this for initialization
    void Start() {
        player = GetComponent<CharacterController>();
        vSpeed = 0f;
    }

    public void setMove(bool state) {
        canMove = state;
    }

    public bool getMove() {
        return canMove;
    }

    // Update is called once per frame
    void Update() {
        if (npc_interacting)
            return;

        keyboardControls();
        if (canMove) {
            
            sensitivity = 2f;
            moveFB = Input.GetAxis("Vertical") * speed;
            moveLR = Input.GetAxis("Horizontal") * speed;
        }

        if (Input.GetButton("Shift"))
            speed = orig_speed + 2f;
        else
            speed = orig_speed;

        rotX = Input.GetAxis("Mouse X") * sensitivity;
        rotY = Input.GetAxis("Mouse Y") * sensitivity;

        if (player.isGrounded)
            vSpeed = 0f;
        else
            vSpeed -= gravity * Time.deltaTime;
        eye_holder.transform.position = Vector3.Lerp(eyes.transform.position, new Vector3(transform.position.x, transform.position.y + 0.781f, transform.position.z), Time.deltaTime * camfollowspeed);
        Vector3 movement = new Vector3(moveLR, vSpeed, moveFB);
        eye_holder.transform.Rotate(0, rotX, 0);
        transform.rotation = eye_holder.transform.rotation;
        if (eyes.transform.localEulerAngles.x <= 60 + rotY || eyes.transform.localEulerAngles.x >= 360 - 80 + rotY)
            eyes.transform.Rotate(-rotY, 0, 0);
        

        if (canMove) {
            movement = transform.rotation * movement;
            player.Move(movement * Time.deltaTime);
        }
        if (in_vehicle) {
            vehicle_state();
        }
    }
    bool in_vehicle;
    public VehicleCont vehicle;
    void keyboardControls() {
        if (Input.GetKeyDown(KeyCode.E) && Vector3.Distance(transform.position, vehicle.transform.position) <= 2f) {
            if (canMove) {
                vehicle.activate(true);
                canMove = false;
                in_vehicle = true;
                transform.GetComponent<CharacterController>().enabled = false;
                camfollowspeed = 30f;
            }
            else {
                transform.position = new Vector3(transform.position.x + 5, transform.position.y, transform.position.z);
                canMove = true;
                in_vehicle = false;
                vehicle.activate(false);
                transform.GetComponent<CharacterController>().enabled = true;
                camfollowspeed = 10f;
            }
        }

        if (Input.GetKey(KeyCode.W)) {
            if (!in_vehicle) {
                if (!GetComponent<AudioSource>().isPlaying)
                    GetComponent<AudioSource>().Play();
            }
            if (in_vehicle) {
                //if (!GetComponent<AudioSource>().isPlaying)
                //    GetComponent<AudioSource>().Play();
            }
        }
        else {
            GetComponent<AudioSource>().Stop();
        }
    }

    public void npc_interact(bool state) {
        npc_interacting = state;
        canMove = !state;
        GetComponent<CharacterController>().enabled = !state;
        helm_ui.SetActive(!state);
    }

    void vehicle_state() {
        transform.position = vehicle.transform.position;
    }
}
