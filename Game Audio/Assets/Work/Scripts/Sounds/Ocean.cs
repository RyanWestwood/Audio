using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
using FMOD.Studio;

public class Ocean : MonoBehaviour
{
    private EventInstance AmbientWaves;
    private EventInstance CrashWaves;

    void Start()
    {
        AmbientWaves = RuntimeManager.CreateInstance("event:/Ocean/Waves");
        RuntimeManager.AttachInstanceToGameObject(AmbientWaves, GetComponent<Transform>(), GetComponent<Rigidbody>());

        CrashWaves = RuntimeManager.CreateInstance("event:/Ocean/Crash");
        RuntimeManager.AttachInstanceToGameObject(CrashWaves, GetComponent<Transform>(), GetComponent<Rigidbody>());

        StartCoroutine(StartAmbient());
        StartCoroutine(StartCrash());
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

    private IEnumerator StartAmbient()
    {
        yield return new WaitForSeconds(Random.Range(0f, 0.5f));
        PlaySound(AmbientWaves);
    }

    private IEnumerator StartCrash()
    {
        yield return new WaitForSeconds(Random.Range(5, 20));
        PlaySound(CrashWaves);
        Debug.Log("CRASHJ");
        StartCoroutine(StartCrash());
    }
}
