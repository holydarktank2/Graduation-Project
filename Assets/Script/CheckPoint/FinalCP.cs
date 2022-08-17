using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FinalCP : MonoBehaviour
{
    void OnTriggerEnter(Collider other){

        if(other.CompareTag("Player")){
            StartCoroutine(Wait());
        }
    }

    IEnumerator Wait(){
        yield return new WaitForSeconds(1);
        PlayerPrefs.SetInt("SavedFinishScene", SceneManager.GetActiveScene().buildIndex);
        SceneManager.LoadScene("GameFinished");
    }
}
