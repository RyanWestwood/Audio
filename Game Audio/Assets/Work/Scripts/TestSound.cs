using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestSound : MonoBehaviour
{
    FMOD.Studio.EventInstance test_sound;

    void Start()
    {
        test_sound = FMODUnity.RuntimeManager.CreateInstance("event:/Test Audio/number");
        test_sound.setParameterByName("Parameter 1", 1.0f);
    }

    public void PlaySound()
    {
        FMOD.Studio.PLAYBACK_STATE PbState;
        test_sound.getPlaybackState(out PbState);
        if (PbState != FMOD.Studio.PLAYBACK_STATE.PLAYING)
        {
            test_sound.start();
        }
    }
}