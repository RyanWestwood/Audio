using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMOD.Studio;

public class Wind : MonoBehaviour
{
    private EventInstance GustSound;
    private EventInstance AmbientSound;

    [Header("Parameters")]
    [SerializeField, Range(0f, 360f), Tooltip("Cone angle range")]
    private float WindAngle = 100f;

    [SerializeField, Range(0f, 360f), Tooltip("0.f = BACK, 90.f = LEFT, 180.f = FRONT, 270.f = RIGHT")]
    private float WindDirection = 120f;

    [SerializeField, Range(0f, 1f), Tooltip("0.f = -20dB, 1.f = 0dB")]
    private float Volume = 1f;

    [SerializeField, Tooltip("Seconds")]
    public float MinTimeBetweenGusts = 10f;

    [SerializeField, Tooltip("Seconds")]
    public float MaxTimeBetweenGusts = 30f;

    void Start()
    {
        AmbientSound = FMODUnity.RuntimeManager.CreateInstance("event:/Wind/Ambient");
        PlaySound(AmbientSound);

        GustSound = FMODUnity.RuntimeManager.CreateInstance("event:/Wind/Gust");
        RandomizeParameters();
        StartCoroutine(Gust());
    }

    private void RandomizeParameters()
    {
        WindDirection = Random.Range(0f, 360f);
        WindAngle = Random.Range(100f, 140f);
        Volume = Random.Range(0f, 1f);

        GustSound.setParameterByName("WindAngle", WindAngle);
        GustSound.setParameterByName("WindDirection", WindDirection);
        GustSound.setParameterByName("Volume", Volume);
    }

    private void SetDirection()
    {
        // TODO: Change to in-game wind direction
        RandomizeParameters();
    }

    private void PlaySound(EventInstance Sound)
    {
        SetDirection();

        PLAYBACK_STATE PbState;
        Sound.getPlaybackState(out PbState);
        if (PbState != PLAYBACK_STATE.PLAYING)
        {
            Sound.start();
        }
    }

    private IEnumerator Gust()
    {
        PlaySound(GustSound);
        yield return new WaitForSeconds(Random.Range(10, 30));
        StartCoroutine(Gust());
    }
}
