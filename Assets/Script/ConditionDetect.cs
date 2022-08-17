using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConditionDetect : MonoBehaviour
{
    public static List<int> Leftovercheckpoint = new List<int>();
    private int count;
    private int num;
    private GameObject Num;
    private GameObject Level;

    void Awake()
    {
        Leftovercheckpoint=player.leftovercheckpoint;
        //player.leftovercheckpoint.Clear();

        if (Leftovercheckpoint.Count!=0)
        {
            conditiondetectfunction();
            
        }
        else if(Leftovercheckpoint.Count==0)
        {
            if (PlayerPrefs.GetInt("SavedFinishScene") == 2|| PlayerPrefs.GetInt("SavedFinishScene") == 4|| PlayerPrefs.GetInt("SavedFinishScene") == 6|| PlayerPrefs.GetInt("SavedFinishScene") == 7|| PlayerPrefs.GetInt("SavedFinishScene") == 9)
            {
                GameObject last = GameObject.Find("LastLevel");
                last.SetActive(false);
                
            }
            else if (PlayerPrefs.GetInt("SavedFinishScene") == 9|| PlayerPrefs.GetInt("SavedFinishScene") == 3|| PlayerPrefs.GetInt("SavedFinishScene") == 5|| PlayerPrefs.GetInt("SavedFinishScene") == 6|| PlayerPrefs.GetInt("SavedFinishScene") == 8)
            {
                GameObject next = GameObject.Find("NextLevel");
                next.SetActive(false);
                
            }
            GameObject Failed=GameObject.Find("ConditionFailed");
            Failed.SetActive(false);
            

        }
        
    }
    
    private void conditiondetectfunction()
    {
        GameObject Complete=GameObject.Find("Complete");
        Complete.SetActive(false);

        switch (PlayerPrefs.GetInt("SavedFinishScene"))
        {
            case 2:
                Level = GameObject.Find("Turning");
                Level.SetActive(true);
                Level = GameObject.Find("Circle");
                Level.SetActive(false);
                Level = GameObject.Find("Merge");
                Level.SetActive(false);
                Level = GameObject.Find("Unsigned");
                Level.SetActive(false);
                Level = GameObject.Find("턜Ŧ");
                Level.SetActive(false);
                break;

        
            case 3:
                Level = GameObject.Find("Turning");
                Level.SetActive(true);
                Level = GameObject.Find("Circle");
                Level.SetActive(false);
                Level = GameObject.Find("Merge");
                Level.SetActive(false);
                Level = GameObject.Find("Unsigned");
                Level.SetActive(false);
                Level = GameObject.Find("턜Ŧ");
                Level.SetActive(false);
                break;

            case 4:
                Level = GameObject.Find("Circle");
                Level.SetActive(true);
                Level = GameObject.Find("Turning");
                Level.SetActive(false);
                Level = GameObject.Find("Merge");
                Level.SetActive(false);
                Level = GameObject.Find("Unsigned");
                Level.SetActive(false);
                Level = GameObject.Find("턜Ŧ");
                Level.SetActive(false);
                break;

            case 5:
                Level = GameObject.Find("Cirecle");
                Level.SetActive(true);
                Level = GameObject.Find("Turning");
                Level.SetActive(false);
                Level = GameObject.Find("Merge");
                Level.SetActive(false);
                Level = GameObject.Find("Unsigned");
                Level.SetActive(false);
                Level = GameObject.Find("턜Ŧ");
                Level.SetActive(false);
                break;

            case 6:
                Level = GameObject.Find("Merge");
                Level.SetActive(true);
                Level = GameObject.Find("Circle");
                Level.SetActive(false);
                Level = GameObject.Find("Turning");
                Level.SetActive(false);
                Level = GameObject.Find("Unsigned");
                Level.SetActive(false);
                Level = GameObject.Find("턜Ŧ");
                Level.SetActive(false);
                break;

            case 7:
                Level = GameObject.Find("Merge");
                Level.SetActive(false);
                Level = GameObject.Find("Circle");
                Level.SetActive(false);
                Level = GameObject.Find("Turning");
                Level.SetActive(false);
                Level = GameObject.Find("Unsigned");
                Level.SetActive(true);
                Level = GameObject.Find("턜Ŧ");
                Level.SetActive(false);
                break;

            case 8:
                Level = GameObject.Find("Merge");
                Level.SetActive(false);
                Level = GameObject.Find("Circle");
                Level.SetActive(false);
                Level = GameObject.Find("Turning");
                Level.SetActive(false);
                Level = GameObject.Find("Unsigned");
                Level.SetActive(true);
                Level = GameObject.Find("턜Ŧ");
                Level.SetActive(false);
                break;

            case 9:
                Level = GameObject.Find("Merge");
                Level.SetActive(false);
                Level = GameObject.Find("Circle");
                Level.SetActive(false);
                Level = GameObject.Find("Turning");
                Level.SetActive(false);
                Level = GameObject.Find("Unsigned");
                Level.SetActive(false);
                Level = GameObject.Find("턜Ŧ");
                Level.SetActive(true);
                break;
        }

        for(int i=1;i<=5;i++)
        {
            count=0;
            for(int j=0;j<Leftovercheckpoint.Count;j++)
            {
                if(i!=Leftovercheckpoint[j]){
                    count++;
                }
                else if(i==Leftovercheckpoint[j])
                {
                    count=0;
                    break;
                }
            }
            if(count!=0)
            {
                num=i;
                switch (PlayerPrefs.GetInt("SavedFinishScene"))
                {
                    case 2:
                        Num = GameObject.Find(num.ToString() + "a");
                        Num.SetActive(false);
                        break;

                    case 3:
                        Num = GameObject.Find(num.ToString() + "a");
                        Num.SetActive(false);
                        break;

                    case 4:
                        Num = GameObject.Find(num.ToString() + "b");
                        Num.SetActive(false);
                        break;

                    case 5:
                        Num = GameObject.Find(num.ToString() + "b");
                        Num.SetActive(false);
                        break;

                    case 6:
                       Num = GameObject.Find(num.ToString() + "c");
                        Num.SetActive(false);
                        break;

                    case 7:
                        Num = GameObject.Find(num.ToString() + "d");
                        Num.SetActive(false);
                        break;

                    case 8:
                        Num = GameObject.Find(num.ToString() + "d");
                        Num.SetActive(false);
                        break;

                    case 9:
                        Num = GameObject.Find(num.ToString() + "e");
                        Num.SetActive(false);
                        break;
                }


                //Debug.Log(Num);

            }
            
        }

        if (PlayerPrefs.GetInt("SavedFinishScene") == 2 || PlayerPrefs.GetInt("SavedFinishScene") == 4 || PlayerPrefs.GetInt("SavedFinishScene") == 6 || PlayerPrefs.GetInt("SavedFinishScene") == 7 || PlayerPrefs.GetInt("SavedFinishScene") == 9)
        {
            GameObject last1 = GameObject.Find("LastLevel1");
            last1.SetActive(false);
           
        }
        else if(PlayerPrefs.GetInt("SavedFinishScene") == 9 || PlayerPrefs.GetInt("SavedFinishScene") == 3 || PlayerPrefs.GetInt("SavedFinishScene") == 5 || PlayerPrefs.GetInt("SavedFinishScene") == 6 || PlayerPrefs.GetInt("SavedFinishScene") == 8)
        {
            GameObject next1 = GameObject.Find("NextLevel1");
            next1.SetActive(false);
            
        }
        
       




    }
}
