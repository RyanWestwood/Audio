using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
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

    [SerializeField, Tooltip("Wind Zone Transform")]
    public Transform WindZoneTransform;

    void Start()
    {
        AmbientSound = RuntimeManager.CreateInstance("event:/Wind/Ambient");
        PlaySound(AmbientSound);

        GustSound = RuntimeManager.CreateInstance("event:/Wind/Gust");
        RandomizeParameters();
        StartCoroutine(Gust());
    }

    private void Update()
    {
        // Still need to shift these values between? 0-180 right, 0- -180 left.
        WindDirection = 90 + (WindZoneTransform.rotation.eulerAngles.y - CalculateDegrees(transform.rotation.eulerAngles.y));
        GustSound.setParameterByName("WindDirection", WindDirection);
    }

    private float CalculateDegrees(float number)
    {
         if (number >= 90)
        {
            return number - 90;
        }
        else
        {
            return 360 - (90 - number);
        }
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

    private void PlaySound(EventInstance Sound)
    {
        PLAYBACK_STATE PbState;
        Sound.getPlaybackState(out PbState);
        if (PbState != PLAYBACK_STATE.PLAYING)
        {
            Sound.start();
        }
    }

    private IEnumerator Gust()
    {
        yield return new WaitForSeconds(Random.Range(2, 5));
        PlaySound(GustSound);
        StartCoroutine(Gust());
    }
}
