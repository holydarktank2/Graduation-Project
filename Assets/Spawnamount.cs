using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawnamount : MonoBehaviour
{
    private Spawner spawner;
    // Start is called before the first frame update
    void Start()
    {
        spawner = this.GetComponent<Spawner>();
        if(PlayerPrefs.GetInt("spawnamount") == 0)
        {
            spawner.spawnEverypoint = false;
            spawner.spawncount = (this.transform.childCount) * 3 / 5;
        }
        else if (PlayerPrefs.GetInt("spawnamount") == 1)
        {
            spawner.spawnEverypoint = false;
            spawner.spawncount = (this.transform.childCount) * 4 / 5;
        }
        else
        {
            spawner.spawnEverypoint = true;
        }
        spawner.Spawn();
    }

    // Update is called once per frame
}
