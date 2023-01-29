using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
using FMOD.Studio;

public class AudioOcclusion : MonoBehaviour
{
    [Header("FMOD Event")]
    [SerializeField]
    public EventReference SelectAudio;
    private EventInstance Audio;
    private EventDescription AudioDes;
    private StudioListener Listener;
    private PLAYBACK_STATE pb;

    [Header("Occlusion Options")]
    [SerializeField, Range(0f, 10f)]
    private float SoundOcclusionWidening = 1f;
    [SerializeField, Range(0f, 10f)]
    private float PlayerOcclusionWidening = 1f;
    [SerializeField, Range(0f, 1f), Tooltip("Lower is less muffled")]
    private float MuffleMultiplier = 0.5f;
    [SerializeField]
    private LayerMask OcclusionLayer;

    [SerializeField]
    public GameObject Player;

    private bool AudioIsVirtual;
    private float MinDistance;
    private float MaxDistance;
    private float ListenerDistance;
    private float LineCastHitCount = 0f;

    private void Start()
    {
        Audio = RuntimeManager.CreateInstance(SelectAudio);
        RuntimeManager.AttachInstanceToGameObject(Audio, GetComponent<Transform>(), GetComponent<Rigidbody>());
        Audio.start();
        Audio.release();

        AudioDes = RuntimeManager.GetEventDescription(SelectAudio);
        AudioDes.getMinMaxDistance(out MinDistance, out MaxDistance);

        Listener = Player.GetComponent<StudioListener>();
    }

    private void FixedUpdate()
    {
        Audio.isVirtual(out AudioIsVirtual);
        Audio.getPlaybackState(out pb);
        ListenerDistance = Vector3.Distance(transform.position, Listener.transform.position);

        if (!AudioIsVirtual && pb == PLAYBACK_STATE.PLAYING && ListenerDistance <= MaxDistance)
            OccludeBetween(transform.position, Listener.transform.position);

        LineCastHitCount = 0f;
    }

    private void OccludeBetween(Vector3 sound, Vector3 listener)
    {
        Vector3 SoundLeft = CalculatePoint(sound, listener, SoundOcclusionWidening, 1);
        Vector3 SoundRight = CalculatePoint(sound, listener, SoundOcclusionWidening, -1);

        Vector3 SoundAbove = new Vector3(sound.x, sound.y + SoundOcclusionWidening, sound.z);
        Vector3 SoundBelow = new Vector3(sound.x, sound.y - SoundOcclusionWidening, sound.z);

        Vector3 ListenerLeft = CalculatePoint(listener, sound, PlayerOcclusionWidening, 1);
        Vector3 ListenerRight = CalculatePoint(listener, sound, PlayerOcclusionWidening, -1);

        Vector3 ListenerAbove = new Vector3(listener.x, listener.y + PlayerOcclusionWidening * 0.5f, listener.z);
        Vector3 ListenerBelow = new Vector3(listener.x, listener.y - PlayerOcclusionWidening * 0.5f, listener.z);

        CheckRay(SoundLeft, ListenerLeft);
        CheckRay(SoundLeft, listener);
        CheckRay(SoundLeft, ListenerRight);

        CheckRay(sound, ListenerLeft);
        CheckRay(sound, listener);
        CheckRay(sound, ListenerRight);

        CheckRay(SoundRight, ListenerLeft);
        CheckRay(SoundRight, listener);
        CheckRay(SoundRight, ListenerRight);

        CheckRay(SoundAbove, ListenerAbove);
        CheckRay(SoundBelow, ListenerBelow);
        SetOcclusion();
    }

    private Vector3 CalculatePoint(Vector3 a, Vector3 b, float m, int positive_or_negative)
    {
        float x, z;
        float n = Vector3.Distance(new Vector3(a.x, 0f, a.z), new Vector3(b.x, 0f, b.z));
        float mn = (m / n);

        x = a.x + (positive_or_negative * (mn * (a.z - b.z)));
        z = a.z - (positive_or_negative * (mn * (a.x - b.x)));

        return new Vector3(x, a.y, z);
    }

    private void CheckRay(Vector3 Start, Vector3 End)
    {
        RaycastHit hit;
        Physics.Linecast(Start, End, out hit, OcclusionLayer);

        if (hit.collider)
            LineCastHitCount++;
    }

    private void SetOcclusion()
    {
        var OcclusionModifier = (LineCastHitCount / 11) * MuffleMultiplier;
        Audio.setParameterByName("Occlusion", OcclusionModifier);
    }
}