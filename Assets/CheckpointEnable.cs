using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointEnable : MonoBehaviour
{
    [HideInInspector]
    public Renderer rend;

    void Start()
    {
        rend = GetComponent<Renderer>();
        
        if (PlayerPrefs.GetInt("checkpointenable") == 0)
        {
            rend.enabled = true;
        }
        else
        {
            rend.enabled = false;
        }
        

    }
}
