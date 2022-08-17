// Traffic Simulation
// https://github.com/mchrbn/unity-traffic-simulation

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TrafficSimulation;

public class RedLightStatus : MonoBehaviour
{

    public int lightGroupId;  // Belong to traffic light 1 or 2?
    public Intersection intersection;
    
    Light redPointLight;
    Light greenPointLight;
    Light yellowPointLight;

    private float lastgroup;
    private float yellowcount;
    private float greencount;

    void Start(){
        redPointLight = this.transform.Find("Red Point Light").GetComponent<Light>();
        greenPointLight = this.transform.Find("Green Point Light").GetComponent<Light>();
        yellowPointLight = this.transform.Find("Yellow Point Light").GetComponent<Light>();
        lastgroup = intersection.currentGreenLightsGroup;
        yellowcount = intersection.yellowLightDuration;
        greencount = 0;
        InvokeRepeating("SetTrafficLightColor", 0, 1);
    }


    void SetTrafficLightColor(){
        if(yellowcount < intersection.yellowLightDuration && yellowcount > 0)
        {
            redPointLight.color = new Color(0, 0, 0);
            greenPointLight.color = new Color(0, 0, 0);
            yellowPointLight.color = new Color(1, .92f, 0);
            yellowcount--;
        }
        else
        {
            if(lastgroup != intersection.currentGreenLightsGroup && lastgroup == lightGroupId)
            {
                redPointLight.color = new Color(0, 0, 0);
                greenPointLight.color = new Color(0, 0, 0);
                yellowPointLight.color = new Color(1, .92f, 0);
                yellowcount--;
            }
            else if (lightGroupId == intersection.currentGreenLightsGroup)
            {
                if (greencount == 0)
                {
                    redPointLight.color = new Color(0, 0, 0);
                    greenPointLight.color = new Color(0, 1, 0);
                    yellowPointLight.color = new Color(0, 0, 0);
                }
                else
                    greencount--;
            }
            else
            {
                redPointLight.color = new Color(1, 0, 0);
                greenPointLight.color = new Color(0, 0, 0);
                yellowPointLight.color = new Color(0, 0, 0);
                yellowcount = intersection.yellowLightDuration;
                greencount = yellowcount;
            }
        }
        lastgroup = intersection.currentGreenLightsGroup;
    }
}
