using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMaster : MonoBehaviour
{
    private GameMaster instance;
    public Vector3 lastCheckPointPos;
    public Vector3 lastCheckPointRota;

    void Awake()
    {
        lastCheckPointPos = GameObject.Find("Start").transform.position;
        GameObject.Find("player").transform.position = lastCheckPointPos;
    }
}
