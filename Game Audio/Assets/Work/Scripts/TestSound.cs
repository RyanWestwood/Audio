using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
using FMOD.Studio;

public class TestSound : MonoBehaviour
{
    private EventInstance Audio;

    [SerializeField]
    public string EventName;

    void Start()
    {
        Audio = RuntimeManager.CreateInstance(EventName);
        Audio.setParameterByName("Parameter 1", 1.0f);
    }

    public void PlaySound()
    {
        PLAYBACK_STATE PbState;
        Audio.getPlaybackState(out PbState);
        if (PbState != PLAYBACK_STATE.PLAYING)
        {
            Audio.start();
        }
    }
}