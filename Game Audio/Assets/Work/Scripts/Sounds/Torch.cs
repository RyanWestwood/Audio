using FMOD.Studio;
using FMODUnity;
using System.Collections;
using UnityEngine;

public class Torch : MonoBehaviour
{
    private EventInstance CracklingSound;

    [Header("Parameters")]

    [SerializeField, Range(0f, 1f), Tooltip("0.f = -20dB, 1.f = 0dB")]
    private float Volume;

    [SerializeField, Range(0f, 1f), Tooltip("0.f = 0%, 1.f = 100%")]
    private float Feedback;

    [SerializeField, Range(0f, 1f), Tooltip("0.f = -2dB, 1.f = 2dB")]
    private float WetLevel;

    [SerializeField, Range(0f, 1f), Tooltip("0.f = -2dB, 1.f = 2dB")]
    private float DryLevel;

    [SerializeField, Range(0f, 1f), Tooltip("0.f = -3dB, 1.f = 3dB")]
    private float Gain;

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

    private void RandomizeTorch()
    {
        Volume = Random.Range(0.55f, 0.65f);
        Feedback = Random.Range(0.0f, 0.06f);
        WetLevel = Random.Range(0f, 1f);
        DryLevel = Random.Range(0f, 0f);
        Gain = Random.Range(0f, 1f);
    }

    private IEnumerator StartSound()
    {
        yield return new WaitForSeconds(Random.Range(0f, 0.5f));
        SoundManager.PlaySound(CracklingSound);
    }
}