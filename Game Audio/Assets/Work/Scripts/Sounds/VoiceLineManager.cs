using FMOD.Studio;

public static class VoiceLineManager
{
    private static bool IsBusPlaying()
    {
        var bus = FMODUnity.RuntimeManager.GetBus("bus:/Sound Effect Bus/Voices");
        float volume;
        bus.getVolume(out volume);
        float volumeThreshold = 1f;
        return volume > volumeThreshold;
    }
    public static void AttemptVoiceLine(EventInstance sound)
    {
        if (!IsBusPlaying())
        {
            SoundManager.PlaySound(sound);
        }
    }
}
