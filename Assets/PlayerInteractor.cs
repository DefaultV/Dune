using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInteractor : MonoBehaviour
{
    RaycastHit hit;
    public Text interactText;

    void Update()
    {
        Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, 5f);
        Debug.DrawRay(Camera.main.transform.position, Camera.main.transform.forward, Color.red, 0.1f);
        if (hit.collider != null) {
            if (hit.collider.transform.parent.GetComponent<ObjectInformation>() != null) {
                //Debug.Log(hit.collider.transform.parent.GetComponent<ObjectInformation>().UIActionName);
                interactText.text = hit.collider.transform.parent.GetComponent<ObjectInformation>().UIActionName;
            }
        }
    }
}
