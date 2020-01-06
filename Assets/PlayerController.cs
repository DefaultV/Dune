using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    float orig_speed = 2f;
    float speed;
    public float sensitivity = 2f;
    float gravity = 9.82f;
    CharacterController player;

    public GameObject eyes;

    float moveFB;
    float moveLR;

    float rotX;
    float rotY;
    float vSpeed;

    bool canMove = true;
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
        if (canMove) {
            keyboardControls();
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


        Vector3 movement = new Vector3(moveLR, vSpeed, moveFB);
        transform.Rotate(0, rotX, 0);
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
        if (Input.GetKey(KeyCode.E)) {
            vehicle.activate();
            canMove = false;
            in_vehicle = true;
            transform.GetComponent<CharacterController>().enabled = false;
        }
    }

    void vehicle_state() {
        transform.position = vehicle.transform.position;
    }
}
