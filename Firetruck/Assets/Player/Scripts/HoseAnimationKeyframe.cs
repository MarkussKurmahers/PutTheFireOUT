using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoseAnimationKeyframe : MonoBehaviour
{
    HoseFollow hose;
    public AudioSource hoseaudio;
    private void Start()
    {
        hose = GetComponentInParent<HoseFollow>();
    }
    public void turnOn()
    {
        hose.SprayOn();
        StartCoroutine(soundUP());
    }
    IEnumerator soundUP()
    {
        while(hoseaudio.volume < .5f)
        {
            hoseaudio.volume += .07f;
            yield return null;
        }
    }
    IEnumerator soundDown()
    {
        while (hoseaudio.volume > 0)
        {
            hoseaudio.volume -= .1f;
            yield return null;
        }
    }
    public void turnOff()
    {
        hose.SprayOff();
        StartCoroutine(soundDown());

    }

}
