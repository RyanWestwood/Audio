using FMOD.Studio;
using FMODUnity;
using UnityEngine;

public class Music : MonoBehaviour
{
    [SerializeField]
    private EventInstance MuiscTrack;

    void Start()
    {
        MuiscTrack = RuntimeManager.CreateInstance("event:/Music/Music");
        RuntimeManager.AttachInstanceToGameObject(MuiscTrack, GetComponent<Transform>(), GetComponent<Rigidbody>());
        SoundManager.PlaySound(MuiscTrack);
    }
}