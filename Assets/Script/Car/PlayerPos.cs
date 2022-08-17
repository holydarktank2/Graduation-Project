using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerPos : MonoBehaviour
{
    private GameMaster gm;

    void Start()
    {
        gm= GameObject.FindGameObjectWithTag("GM").GetComponent<GameMaster>();
        transform.position=gm.lastCheckPointPos;
        transform.forward = GameObject.Find("Start").transform.forward;
    }

    // void Update()
    // {
    //     if(Input.GetKeyDown(KeyCode.DownArrow)){
    //         SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    //     }
    // }
}
