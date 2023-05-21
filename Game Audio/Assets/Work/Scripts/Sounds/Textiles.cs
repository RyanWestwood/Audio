using FMOD.Studio;
using FMODUnity;
using System.Collections;
using UnityEngine;
public class Textiles : MonoBehaviour
{
    [SerializeField]
    private EventInstance LoomTrack;
    private EventInstance ScissorTrack;

    void Start()
    {
        LoomTrack = RuntimeManager.CreateInstance("event:/Textiles/Loom");
        RuntimeManager.AttachInstanceToGameObject(LoomTrack, GetComponent<Transform>(), GetComponent<Rigidbody>());
        SoundManager.PlaySound(LoomTrack);

        ScissorTrack = RuntimeManager.CreateInstance("event:/Textiles/Scissors");
        RuntimeManager.AttachInstanceToGameObject(ScissorTrack, GetComponent<Transform>(), GetComponent<Rigidbody>());
        StartCoroutine(StartScissors());
    }

    private IEnumerator StartScissors()
    {
        yield return new WaitForSeconds(Random.Range(5f, 20f));
        SoundManager.PlaySound(ScissorTrack);
        StartCoroutine(StartScissors());
    }
}