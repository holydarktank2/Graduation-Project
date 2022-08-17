using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class turnsignalAudio : MonoBehaviour
{
    public float audioPitch = 1;
    public float audioVolume = 1;
    AudioSource audiosource;
    // private player

    // Start is called before the first frame update
    void Start()
    {
        audiosource=GetComponent<AudioSource>();
        audiosource.pitch=audioPitch;
        audiosource.volume=audioVolume;
    }

    void Awake(){

    }
    // Update is called once per frame
    public void turnOnAudio(){
        audiosource.mute=false;
    }
}
