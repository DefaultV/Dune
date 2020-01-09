using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInteractor : MonoBehaviour
{
    RaycastHit hit;
    public Text interactText;
    public GameObject inspect_mdl;// = GameObject.FindGameObjectWithTag("inspectmdl");
    public dialogue_fadein dialogue_ui; 
    Vector3 original_campos;
    float ui_cooldown = 2f;

    void Start() {
        inspect_mdl.SetActive(false);
        original_campos = new Vector3(0, 0.781f, 0); // dont ask
    }

    void Update()
    {
        checkcooldown();
        if (ui_cooldown < 2)
            return;
        Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, 5f);
        Debug.DrawRay(Camera.main.transform.position, Camera.main.transform.forward, Color.red, 0.1f);
        if (hit.collider != null) {
            if (hit.collider.transform.parent != null) {
                ObjectInformation comp = hit.collider.transform.parent.GetComponent<ObjectInformation>();
                if (comp) {
                    if (interactText.enabled == false) {
                        //Debug.Log(hit.collider.transform.parent.GetComponent<ObjectInformation>().UIActionName);
                        interactText.enabled = true;
                    }
                    if (Input.GetKeyDown(KeyCode.E)) {
                        comp.inspect();
                        if (comp.ObjectName == "NPC") {
                            GetComponent<PlayerController>().npc_interact(true);
                            interactText.enabled = false;
                            dialogue_ui.enableDialogueUI(true);
                            dialogue_ui.fading = true;
                        }
                        ui_cooldown = 0f;
                    }
                    interactText.text = hit.collider.transform.parent.GetComponent<ObjectInformation>().UIActionName;
                }
                else if (interactText.enabled == true) {
                    interactText.enabled = false;
                }
            }
        }
        else if (inspect_mdl.activeSelf && Input.GetKeyDown(KeyCode.E)) {
            inspect_mdl.SetActive(false);
            Debug.Log("Enabling inspector");
        }
        else if (interactText.enabled == true) {
            interactText.enabled = false;
        }
        else if (GetComponent<PlayerController>().npc_interacting && Input.GetKeyDown(KeyCode.E)) {
            GetComponent<PlayerController>().npc_interact(false);
            //Camera.main.transform.localPosition = original_campos;
            dialogue_ui.enableDialogueUI(false);
            //ui_cooldown = 0f;
        }
    }

    void checkcooldown() {
        if (ui_cooldown < 2)
            ui_cooldown += Time.deltaTime;
    }
}
