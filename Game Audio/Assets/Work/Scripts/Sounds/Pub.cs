using FMOD.Studio;
using FMODUnity;
using UnityEngine;

public class Pub : MonoBehaviour
{
    [SerializeField]
    private EventInstance PubAmbience;

    void Start()
    {
        PubAmbience = RuntimeManager.CreateInstance("event:/Pub");
        RuntimeManager.AttachInstanceToGameObject(PubAmbience, GetComponent<Transform>(), GetComponent<Rigidbody>());
        SoundManager.PlaySound(PubAmbience);
        PubAmbience.setParameterByName("Occlusion", .5f);
    }
}
