using FMOD.Studio;
using FMODUnity;
using System.Collections;
using UnityEngine;

public class Murmur : MonoBehaviour
{
    [SerializeField]
    private EventInstance TalkingTrack;

    void Start()
    {
        TalkingTrack = RuntimeManager.CreateInstance("event:/IndistinctTalking");
        RuntimeManager.AttachInstanceToGameObject(TalkingTrack, GetComponent<Transform>(), GetComponent<Rigidbody>());
        StartCoroutine(StartMurmur());
    }

    private IEnumerator StartMurmur()
    {
        TalkingTrack.setParameterByName("Volume", Random.Range(0.85f, 1.0f));
        SoundManager.PlaySound(TalkingTrack);
        float randomTime = Random.Range(30.0f, 60.0f);
        yield return new WaitForSeconds(randomTime);
        StartCoroutine(StartMurmur());
    }
}
