using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectInformation : MonoBehaviour
{
    public GameObject inspect_mdl;
    public string ObjectName;
    public string UIActionName;
    public AudioClip greeting;

    public void inspect() {
        if (gameObject.tag == "Artifact") {
            inspect_mdl.SetActive(true);
            Debug.Log("inspecting... " + inspect_mdl.activeSelf);
            MeshFilter mshrnd = inspect_mdl.GetComponent<MeshFilter>();
            mshrnd.sharedMesh = transform.GetChild(0).GetComponent<MeshFilter>().sharedMesh;
        }
        if (ObjectName == "NPC") {
            Debug.Log("Interacting... ");
            Transform newtrans = getChildWithName("Camera_placement");
            g_newtrans = newtrans;
            cam_lerping = true;
            Invoke("lerpend", 2f);
            transform.GetComponentInChildren<AudioSource>().PlayOneShot(greeting);
            Camera.main.transform.localEulerAngles = Vector3.zero;
            //Camera.main.transform.position = newtrans.position;
            //Camera.main.transform.rotation = newtrans.rotation;
        }
    }

    bool cam_lerping = false;
    Transform g_newtrans;
    public void Update() {
        if (cam_lerping) {
            CameraLerpToPos(Camera.main.transform.parent, g_newtrans);
        }
    }

    Transform getChildWithName(string name) {
        foreach (Transform trans in transform) {
            if (trans.name == name)
                return trans;
        }
        return null;
    }

    void lerpend() {
        cam_lerping = false;
    }

    void CameraLerpToPos(Transform cameratrans, Transform newtrans) {
        cameratrans.position = Vector3.Lerp(cameratrans.position, newtrans.position, Time.deltaTime * 3.0f);
        cameratrans.rotation = Quaternion.Lerp(cameratrans.rotation, newtrans.rotation, Time.deltaTime * 3.0f);
    }
}
