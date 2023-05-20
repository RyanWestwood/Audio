using FMOD.Studio;
using FMODUnity;
using System.Collections;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class Footsteps : MonoBehaviour
{
    private enum CURRENT_TERRAIN { WOOD, GRASS, STONE, SAND, MUD }; // Math these to FMOD order
    public Rigidbody PlayerRigidbody;
    public RigidbodyFirstPersonController PlayerController;
    [SerializeField]
    private CURRENT_TERRAIN CurrentTerrain;

    [SerializeField]
    private EventInstance FootstepSound;
    private bool WasGrounded = true;
    private bool IsJumping = false;

    void Start()
    {
        FootstepSound = RuntimeManager.CreateInstance("event:/Footsteps/Footsteps");
        RuntimeManager.AttachInstanceToGameObject(FootstepSound, GetComponent<Transform>(), GetComponent<Rigidbody>());
        SoundManager.PlaySound(FootstepSound);
    }

    private void Update()
    {
        bool is_grounded = PlayerController.Grounded;
        bool is_moving = PlayerRigidbody.velocity.magnitude > 0.1f;
        bool is_sprinting = PlayerRigidbody.velocity.magnitude > 10f;

        float speed = is_sprinting ? 1.0f : 0.0f;
        FootstepSound.setParameterByName("Pitch", speed);
        float volume = is_moving && is_grounded ? 1.0f : 0.0f;
        FootstepSound.setParameterByName("Volume", volume);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine(JumpSequence());
        }

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

    private bool CheckLayer(RaycastHit hit, string layer, CURRENT_TERRAIN terrain)
    {
        if (hit.transform.gameObject.layer == LayerMask.NameToLayer(layer))
        {
            if (CurrentTerrain != terrain) PlayFootstep(terrain);
            CurrentTerrain = terrain;
            return true;
        }
        return false;
    }

    private void PlayFootstep(CURRENT_TERRAIN terrain)
    {
        FootstepSound.setParameterByName("Terrain", (int)terrain);
    }
    private IEnumerator JumpSequence()
    {
        //SoundManager.PlaySound(Jump);
        yield return new WaitForSeconds(1.1f);
        Debug.Log("Landed!");
    }
}