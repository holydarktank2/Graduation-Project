using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashRed : MonoBehaviour
{
    Light redPointLight;

    // Start is called before the first frame update
    void Start()
    {
        redPointLight = this.transform.Find("Red Point Light").GetComponent<Light>();
    }

    // Update is called once per frame
    void Update()
    {
        float floor = 0f;
        float ceiling = 1f;
        float emission = floor + Mathf.PingPong(Time.time * 2f, ceiling - floor);
        //yellowPointLight.SetColor("_EmissionColor", new Color(2f, 2f, 2f) * emission);
        redPointLight.color = new Color(1, 0, 0) * emission;

        //yellowPointLight.color = new Color(1, .92f, 0);
    }
}
