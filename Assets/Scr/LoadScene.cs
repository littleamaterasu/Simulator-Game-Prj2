using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        foreach(Demo_npc dn in FindObjectsOfType<Demo_npc>())
        {
            //Debug.Log(dn.transform.position);
            dn.LoadPosition();
        }
    }
}
