using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
using FMOD.Studio;

public class Torch : MonoBehaviour
{
    private EventInstance CracklingSound;

    [Header("Parameters")]

    [SerializeField, Range(0f, 1f), Tooltip("0.f = -20dB, 1.f = 0dB")]
    private float Volume = Random.Range(0.65f, 0.8f);

    [SerializeField, Range(0f, 1f), Tooltip("0.f = 0%, 1.f = 100%")]
    private float Feedback = Random.Range(0.0f, 0.06f);

    [SerializeField, Range(0f, 1f), Tooltip("0.f = -2dB, 1.f = 2dB")]
    private float WetLevel = Random.Range(0f, 1f);

    [SerializeField, Range(0f, 1f), Tooltip("0.f = -2dB, 1.f = 2dB")]
    private float DryLevel = Random.Range(0f, 0f);

    [SerializeField, Range(0f, 1f), Tooltip("0.f = -3dB, 1.f = 3dB")]
    private float Gain = Random.Range(0f, 1f);

    void Start()
    {
        CracklingSound = RuntimeManager.CreateInstance("event:/Fire/Crackling");
        RuntimeManager.AttachInstanceToGameObject(CracklingSound, GetComponent<Transform>(), GetComponent<Rigidbody>());
        CracklingSound.setParameterByName("Volume", Volume);
        CracklingSound.setParameterByName("Feedback", Feedback);
        CracklingSound.setParameterByName("Wet Level", WetLevel);
        CracklingSound.setParameterByName("Dry Level", DryLevel);
        CracklingSound.setParameterByName("Gain", Gain);

        StartCoroutine(StartSound());
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

    private IEnumerator StartSound()
    {
        yield return new WaitForSeconds(Random.Range(0f, 0.5f));
        PlaySound(CracklingSound);
    }
}
