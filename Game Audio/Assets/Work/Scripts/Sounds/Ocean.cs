using FMOD.Studio;
using FMODUnity;
using System.Collections;
using UnityEngine;

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

    private IEnumerator StartAmbient()
    {
        yield return new WaitForSeconds(Random.Range(0f, 0.5f));
        SoundManager.PlaySound(AmbientWaves);
    }

    private IEnumerator StartCrash()
    {
        yield return new WaitForSeconds(Random.Range(5, 20));
        SoundManager.PlaySound(CrashWaves);
        StartCoroutine(StartCrash());
    }
}