using FMOD.Studio;
using FMODUnity;
using UnityEngine;
public class CreakyFloor : MonoBehaviour
{
    [SerializeField]
    private EventInstance FloorTrack;

    public enum CURRENT_TERRAIN { WOOD, GRASS, STONE, SAND, MUD }; // Math these to FMOD order
    public CURRENT_TERRAIN Terrain;

    void Start()
    {
        FloorTrack = RuntimeManager.CreateInstance("event:/CreakyFloor");
        RuntimeManager.AttachInstanceToGameObject(FloorTrack, GetComponent<Transform>(), GetComponent<Rigidbody>());
        SoundManager.PlaySound(FloorTrack);
    }
}