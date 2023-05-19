using FMOD.Studio;
using FMODUnity;
using UnityEngine;

public class Breathing : MonoBehaviour
{
    [SerializeField]
    private EventInstance BreathingTrack;

    void Start()
    {
        BreathingTrack = RuntimeManager.CreateInstance("event:/Breathing");
        RuntimeManager.AttachInstanceToGameObject(BreathingTrack, GetComponent<Transform>(), GetComponent<Rigidbody>());
        SoundManager.PlaySound(BreathingTrack);
    }
}
