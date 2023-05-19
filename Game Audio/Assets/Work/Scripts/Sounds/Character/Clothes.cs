using FMOD.Studio;
using FMODUnity;
using UnityEngine;

public class Clothes : MonoBehaviour
{
    [SerializeField]
    private EventInstance ClothesTrack;
    public Rigidbody PlayerRigidbody;

    void Start()
    {
        ClothesTrack = RuntimeManager.CreateInstance("event:/Clothes");
        RuntimeManager.AttachInstanceToGameObject(ClothesTrack, GetComponent<Transform>(), GetComponent<Rigidbody>());
        SoundManager.PlaySound(ClothesTrack);
    }
    private void Update()
    {
        float volume = PlayerRigidbody.velocity.magnitude > 0.1f ? 1.0f : 0.0f;
        ClothesTrack.setParameterByName("Volume", volume);
    }
}