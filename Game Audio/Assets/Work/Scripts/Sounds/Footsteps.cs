using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
using FMOD.Studio;

public class Footsteps : MonoBehaviour
{
    private enum CURRENT_TERRAIN { WOOD, GRASS, STONE, SAND, MUD }; // Math these to FMOD order

    [SerializeField]
    private CURRENT_TERRAIN CurrentTerrain;
    private EventInstance FootstepSound;

    void Start()
    {
        FootstepSound = RuntimeManager.CreateInstance("event:/Footsteps/Footsteps");
        RuntimeManager.AttachInstanceToGameObject(FootstepSound, GetComponent<Transform>(), GetComponent<Rigidbody>());
        PlaySound(FootstepSound);
    }

    private void Update()
    {
        DetermineTerrain();
    }

    private void DetermineTerrain()
    {
        RaycastHit[] hit;
        hit = Physics.RaycastAll(transform.position, Vector3.down, 10.0f);

        foreach (RaycastHit rayhit in hit)
        {
            if (CheckLayer(rayhit, "Wood", CURRENT_TERRAIN.WOOD)) break;
            if (CheckLayer(rayhit, "Grass", CURRENT_TERRAIN.GRASS)) break;
            if (CheckLayer(rayhit, "Stone", CURRENT_TERRAIN.STONE)) break;
            if (CheckLayer(rayhit, "Sand", CURRENT_TERRAIN.SAND)) break;
            if (CheckLayer(rayhit, "Mud", CURRENT_TERRAIN.MUD)) break;
        }
    }

    private bool CheckLayer(RaycastHit hit,string layer, CURRENT_TERRAIN terrain)
    {
        if (hit.transform.gameObject.layer == LayerMask.NameToLayer(layer))
        {
            if(CurrentTerrain != terrain) PlayFootstep(terrain);
            CurrentTerrain = terrain;
            return true;
        }
        return false;
    }

    private void PlayFootstep(CURRENT_TERRAIN terrain)
    {
        FootstepSound.setParameterByName("Terrain", (int)terrain);
    }

    private void PlaySound(EventInstance Sound)
    {
        PLAYBACK_STATE PbState;
        Sound.getPlaybackState(out PbState);
        if (PbState != PLAYBACK_STATE.PLAYING)
        {
            Sound.start();
        }
    }
}