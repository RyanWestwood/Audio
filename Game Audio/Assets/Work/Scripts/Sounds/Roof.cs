using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
using FMOD.Studio;

public class Roof : MonoBehaviour
{
    private EventInstance SeagulSound;
    private float RandomNumber;

    void Start()
    {
        SeagulSound = RuntimeManager.CreateInstance("event:/Birds/SeagulsRoof");
        RuntimeManager.AttachInstanceToGameObject(SeagulSound, GetComponent<Transform>(), GetComponent<Rigidbody>());
        RandomNumber = 1;
        StartCoroutine(StartSeaguls());
    }

    private void PlaySound(EventInstance Sound)
    {
        PLAYBACK_STATE PbState;
        Sound.getPlaybackState(out PbState);
        if (PbState != PLAYBACK_STATE.PLAYING)
        {
            Sound.start();
        }
    }

    private IEnumerator StartSeaguls()
    {
        if (RandomNumber > 0.3)
        {
            yield return new WaitForSeconds(Random.Range(0f, 20f));
            RandomNumber = 0;
        }
        PlaySound(SeagulSound);
        yield return new WaitForSeconds(Random.Range(20f, 50f));
        StartCoroutine(StartSeaguls());
    }
}