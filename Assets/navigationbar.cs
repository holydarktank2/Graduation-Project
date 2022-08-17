using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class navigationbar : MonoBehaviour
{
    // Start is called before the first frame update
    public void showNav(int i)
    {
        this.GetComponent<Text>().text = this.gameObject.transform.GetChild(i-1).GetComponent<Text>().text;
        this.gameObject.SetActive(true);
        StartCoroutine(Wait());
        

    }
    IEnumerator Wait()
    {
        yield return new WaitForSeconds(3);
        this.gameObject.SetActive(false);
    }

}
