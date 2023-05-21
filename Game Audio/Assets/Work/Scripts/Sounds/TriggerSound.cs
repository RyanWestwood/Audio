using FMOD.Studio;
using FMODUnity;
using UnityEngine;

public class TriggerSound : MonoBehaviour
{
    [SerializeField]
    private EventInstance TriggerTrack;
    public string AudioEventID;
    public int Cooldown;
    private float Timer = 1000;

    void Start()
    {
        TriggerTrack = RuntimeManager.CreateInstance(AudioEventID);
        RuntimeManager.AttachInstanceToGameObject(TriggerTrack, GetComponent<Transform>(), GetComponent<Rigidbody>());
    }

    private void Update()
    {
        Timer += Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (Timer >= Cooldown)
        {
            SoundManager.PlaySound(TriggerTrack);
        }
    }
}