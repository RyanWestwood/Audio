using FMOD.Studio;
using FMODUnity;
using UnityEngine;

public class Kitchen : MonoBehaviour
{
    [SerializeField]
    private EventInstance CauldronTrack;

    void Start()
    {
        CauldronTrack = RuntimeManager.CreateInstance("event:/Cauldron");
        RuntimeManager.AttachInstanceToGameObject(CauldronTrack, GetComponent<Transform>(), GetComponent<Rigidbody>());
        SoundManager.PlaySound(CauldronTrack);
    }
}