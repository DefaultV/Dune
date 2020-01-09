using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class dialogue_fadein : MonoBehaviour
{
    // Start is called before the first frame update
    Color newcol = Color.clear;
    void Start()
    {
        enableDialogueUI(true);
        reset();
        enableDialogueUI(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (fading)
            fadeindialogue();
    }
    public bool fading = false;
    void fadeindialogue() {
        //GridLayout gl = transform.GetChild(0).GetComponent<GridLayout>();
        //Vector2 newgap = gl.cellGap;
        //newgap = Vector2.Lerp(new Vector2(0, -100), newgap, Time.deltaTime * 2f);
        //gl.cellGap = newgap;

        newcol = Vector4.Lerp(newcol, Color.white, Time.deltaTime);

        foreach(Transform trans in transform.GetChild(0)) {
            trans.GetChild(0).GetComponent<Text>().color = newcol;
        }
        transform.GetChild(0).GetComponent<Image>().color = newcol;

        if (newcol == Color.white)
            fading = false;
    }

    public void enableDialogueUI(bool state) {
        transform.GetChild(0).gameObject.SetActive(state);
        transform.GetChild(1).gameObject.SetActive(state);
        if (!state) {
            fading = false;
            reset();
        }
    }

    void reset() {
        newcol = Color.clear;
        foreach (Transform trans in transform.GetChild(0)) {
            trans.GetChild(0).GetComponent<Text>().color = newcol;
        }
        transform.GetChild(0).GetComponent<Image>().color = newcol;
    }
}
