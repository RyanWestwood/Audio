using UnityEngine;

public class TriggerMusic : MonoBehaviour
{
    // Start is called before the first frame update
    public Music MusicTrack;
    public float Value;

    private void OnTriggerEnter(Collider other)
    {
        MusicTrack.SetIntensity(Value);
    }
}
