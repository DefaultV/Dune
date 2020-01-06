using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ListofCrafts : MonoBehaviour
{
    const int MAX_COMPS = 1;
    public Component[] GT_components;
    void Start() {
        GT_components = new Component[MAX_COMPS];

        GT_components[0].name               = "Crystal";
        GT_components[0].id                 = 1;
        GT_components[0].ref_can_craft[0]   = 2; // Crystal cluster ID

        GT_components[1].name               = "Crystal Cluster";
        GT_components[1].id                 = 2;
        GT_components[1].ref_can_craft[0]   = 0; // 0 is crafting for nothing
        GT_components[1].ref_need_craft[0]  = 1; // crystal ID
    }
}

public class Component {
    public string name;
    public int id;
    public int[] ref_can_craft = new int[2];
    public int[] ref_need_craft = new int[2];
}