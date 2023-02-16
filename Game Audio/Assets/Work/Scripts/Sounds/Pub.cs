using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
using FMOD.Studio;

public class Pub : MonoBehaviour
{
    [SerializeField]
    private EventInstance PubAmbience;

    void Start()
    {
        PubAmbience = RuntimeManager.CreateInstance("event:/Pub");
        RuntimeManager.AttachInstanceToGameObject(PubAmbience, GetComponent<Transform>(), GetComponent<Rigidbody>());
        PlaySound(PubAmbience);
        PubAmbience.setParameterByName("Occlusion", 0.6f);
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
}
