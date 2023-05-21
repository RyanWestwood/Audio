using FMOD.Studio;

public static class VoiceLineManager
{
    private static bool IsBusPlaying()
    {
        var bus = FMODUnity.RuntimeManager.GetBus("bus:/Sound Effect Bus/Voices");
        bool isPlaying = false;
        bus.getPaused(out isPlaying);

        return isPlaying;
    }
    public static void AttemptVoiceLine(EventInstance sound)
    {
        if (!IsBusPlaying())
        {
            SoundManager.PlaySound(sound);
        }
    }
}
