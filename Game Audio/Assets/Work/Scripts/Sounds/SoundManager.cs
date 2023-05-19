using FMOD.Studio;

public static class SoundManager
{
    public static void PlaySound(EventInstance sound)
    {
        PLAYBACK_STATE playbackState;
        sound.getPlaybackState(out playbackState);
        if (playbackState != PLAYBACK_STATE.PLAYING)
        {
            sound.start();
        }
    }
}