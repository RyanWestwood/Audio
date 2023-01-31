using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMOD.Studio;

public class TestSound : MonoBehaviour
{
    EventInstance Sound;

    void Start()
    {
        Sound = FMODUnity.RuntimeManager.CreateInstance("event:/Test Audio/number");
        Sound.setParameterByName("Parameter 1", 1.0f);
    }

    public void PlaySound()
    {
        PLAYBACK_STATE PbState;
        Sound.getPlaybackState(out PbState);
        if (PbState != PLAYBACK_STATE.PLAYING)
        {
            Sound.start();
        }
    }
}