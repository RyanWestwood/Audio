using FMOD.Studio;
using FMODUnity;
using UnityEngine;

public class Music : MonoBehaviour
{
    [SerializeField]
    private EventInstance MuiscTrack;

    void Start()
    {
        MuiscTrack = RuntimeManager.CreateInstance("event:/Music/MusicTransitstions");
        SetIntensity(0.3f);
        RuntimeManager.AttachInstanceToGameObject(MuiscTrack, GetComponent<Transform>(), GetComponent<Rigidbody>());
        SoundManager.PlaySound(MuiscTrack);
    }

    public void SetIntensity(float value)
    {
        MuiscTrack.setParameterByName("Intensity", value);
    }
}