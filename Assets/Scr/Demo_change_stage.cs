using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Demo_change_stage : MonoBehaviour
{
    public string sceneName;
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            foreach(Demo_npc dn in FindObjectsOfType<Demo_npc>())
            {
                dn.SavePosition();
            }
            SceneManager.LoadScene(sceneName);
        }
    }
}
