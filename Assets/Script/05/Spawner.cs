using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{

    public GameObject[] AIcars;
    public bool spawnEverypoint;
    public int spawncount;//生成數量
    
    // Start is called before the first frame update
    /*void Start()
    {
        StartCoroutine(Spawn());
    }

    // Update is called once per frame
    IEnumerator Spawn()
    {
        int count = 0;
        if (spawnEverypoint)
        {
            while (count < transform.childCount)
            {
                GameObject obj = Instantiate(AIcars[Random.Range(0, AIcars.Length)]);//生成
                Transform child = transform.GetChild(count);//生成在哪個waypoint
                obj.transform.position = child.position;//生成位置
                obj.transform.forward = child.forward;

                yield return new WaitForEndOfFrame();

                count++;
            }
        }
        else
        {
            /*int[] random = new int[spawncount];
            for(int i = 0; i < spawncount; i++)
            {
                int num = Random.Range(0, transform.childCount);
                for(int j = 0; j < i; j++)
                {
                    if(random[j] == num)
                    {
                        j = 0;
                        num = Random.Range(0, transform.childCount);
                    }
                }
                random[i] = num;
            }
            int[] random = new int[transform.childCount];
            for(int i = 0; i < random.Length; i++)
            {
                random[i] = 0;
            }
            for (int i = 0; i < spawncount; i++)
            {
                int num = Random.Range(0, transform.childCount);
                while(random[num] == 1)
                {
                    num = Random.Range(0, transform.childCount);
                }
                random[num] = 1;
            }

            for(int i = 0; i < random.Length; i++)
            {
                if(random[i] == 1)
                {
                    GameObject obj = Instantiate(AIcars[Random.Range(0, AIcars.Length)]);//生成
                    Transform child = transform.GetChild(i);//生成在哪個waypoint
                    obj.transform.position = child.position;//生成位置
                    obj.transform.forward = child.forward;
                }

                yield return new WaitForEndOfFrame();
            }
           
        }*/
    public void Spawn()
    {
        int count = 0;
        if (spawnEverypoint)
        {
            while (count < transform.childCount)
            {
                GameObject obj = Instantiate(AIcars[Random.Range(0, AIcars.Length)]);//生成
                Transform child = transform.GetChild(count);//生成在哪個waypoint
                obj.transform.position = child.position;//生成位置
                obj.transform.forward = child.forward;

                count++;
            }
        }
        else
        {
            /*int[] random = new int[spawncount];
            for(int i = 0; i < spawncount; i++)
            {
                int num = Random.Range(0, transform.childCount);
                for(int j = 0; j < i; j++)
                {
                    if(random[j] == num)
                    {
                        j = 0;
                        num = Random.Range(0, transform.childCount);
                    }
                }
                random[i] = num;
            }*/
            int[] random = new int[transform.childCount];
            for(int i = 0; i < random.Length; i++)
            {
                random[i] = 0;
            }
            for (int i = 0; i < spawncount; i++)
            {
                int num = Random.Range(0, transform.childCount);
                while(random[num] == 1)
                {
                    num = Random.Range(0, transform.childCount);
                }
                random[num] = 1;
            }

            for(int i = 0; i < random.Length; i++)
            {
                if (random[i] == 1)
                {
                    GameObject obj = Instantiate(AIcars[Random.Range(0, AIcars.Length)]);//生成
                    Transform child = transform.GetChild(i);//生成在哪個waypoint
                    obj.transform.position = child.position;//生成位置
                    obj.transform.forward = child.forward;
                }
            }      
        }

    }
}
