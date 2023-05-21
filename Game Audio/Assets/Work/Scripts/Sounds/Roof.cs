using FMOD.Studio;
using FMODUnity;
using System.Collections;
using UnityEngine;

public class Roof : MonoBehaviour
{
    private EventInstance SeagulSound;
    private float RandomNumber;

    void Start()
    {
        SeagulSound = RuntimeManager.CreateInstance("event:/Birds/SeagulsRoof");
        RuntimeManager.AttachInstanceToGameObject(SeagulSound, GetComponent<Transform>(), GetComponent<Rigidbody>());
        RandomNumber = 1;
        StartCoroutine(StartSeaguls());
    }

    private IEnumerator StartSeaguls()
    {
        if (RandomNumber > 0.3)
        {
            yield return new WaitForSeconds(Random.Range(0f, 20f));
            RandomNumber = 0;
        }
        SoundManager.PlaySound(SeagulSound);
        yield return new WaitForSeconds(Random.Range(20f, 50f));
        StartCoroutine(StartSeaguls());
    }
}