using FMOD.Studio;
using FMODUnity;
using System.Collections;
using UnityEngine;
public class CreakyFloor : MonoBehaviour
{
    [SerializeField]
    private EventInstance FloorTrack;

    public enum CREAK_FREQUENCY { HIGH, MEDIUM, LOW };
    public CREAK_FREQUENCY Frequency = CREAK_FREQUENCY.LOW;

    void Start()
    {
        FloorTrack = RuntimeManager.CreateInstance("event:/CreakyFloor");
        RuntimeManager.AttachInstanceToGameObject(FloorTrack, GetComponent<Transform>(), GetComponent<Rigidbody>());
        StartCoroutine(StartCreaks());
    }

    private IEnumerator StartCreaks()
    {
        FloorTrack.setParameterByName("Volume", Random.Range(0.85f, 1.0f));
        SoundManager.PlaySound(FloorTrack);
        float randomTime = Random.Range(5.0f, 10.0f);
        yield return new WaitForSeconds(randomTime * ((float)Frequency + 1));
        StartCoroutine(StartCreaks());
    }
}