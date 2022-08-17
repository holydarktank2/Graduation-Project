using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ErrorDetect : MonoBehaviour
{
    public static int R;
    public static int S;
    public static int M;
    public static int W;
    public static int AI;

    void Awake()
    {
        R = player.right;
        S = player.square;
        M = player.middle;
        W = player.wrongway;
        AI = player.aicar;
        //Debug.Log(AI);
        if(R!=0||M!=0||S!=0||W!=0||AI!=0)
        {
            detectfunction();
        }
        
    }
    
    private void detectfunction()
    {
        
        if(R==1)
        {
            GameObject Square=GameObject.Find("square");
            GameObject Middle=GameObject.Find("middle");
            GameObject WrongWay=GameObject.Find("wrongway");
            GameObject Aicar = GameObject.Find("aicar");
            Aicar.SetActive(false);
            WrongWay.SetActive(false);
            Square.SetActive(false);
            Middle.SetActive(false);
        }
        else if(S==1)
        {
            GameObject Right=GameObject.Find("right");
            GameObject Middle=GameObject.Find("middle");
            GameObject WrongWay=GameObject.Find("wrongway");
            GameObject Aicar = GameObject.Find("aicar");
            Aicar.SetActive(false);
            WrongWay.SetActive(false);
            Right.SetActive(false);
            Middle.SetActive(false);
        }
        else if(M==1)
        {
            GameObject Square=GameObject.Find("square");
            GameObject Right=GameObject.Find("right");
            GameObject WrongWay=GameObject.Find("wrongway");
            GameObject Aicar = GameObject.Find("aicar");
            Aicar.SetActive(false);
            WrongWay.SetActive(false);
            Square.SetActive(false);
            Right.SetActive(false);
        }
        else if(W==1)
        {
            GameObject Square=GameObject.Find("square");
            GameObject Right=GameObject.Find("right");
            GameObject Middle=GameObject.Find("middle");
            GameObject Aicar = GameObject.Find("aicar");
            Aicar.SetActive(false);
            Middle.SetActive(false);
            Square.SetActive(false);
            Right.SetActive(false);
        }
        else if (AI == 1)
        {
            GameObject Square = GameObject.Find("square");
            GameObject Right = GameObject.Find("right");
            GameObject Middle = GameObject.Find("middle");
            GameObject WrongWay = GameObject.Find("wrongway");
            WrongWay.SetActive(false);
            Middle.SetActive(false);
            Square.SetActive(false);
            Right.SetActive(false);
        }


    }
    
}
