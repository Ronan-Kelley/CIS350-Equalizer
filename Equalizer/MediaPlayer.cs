using NAudio.Wave;
using System;

public class MediaPlayer
{
    // player objects
    private WaveOutEvent? outputDevice;
    private AudioFileReader? audioFile;

    // path of the currently loaded file
    private string? curFilePath;

    public MediaPlayer()
	{
        outputDevice = null;
        audioFile = null;
	}

    public bool LoadFile(string filePath)
    {
        // ensure file isn't already loaded
        if (filePath == curFilePath)
        {
            return true;
        }

        // initialize the output device
        if (outputDevice == null)
        {
            // create the waveout event
            outputDevice = new WaveOutEvent();
            // set up a handler for the PlaybackStopped event (defined below)
            outputDevice.PlaybackStopped += OnPlaybackStopped;
        }
        // initialize the audio file if the path exists
        if (File.Exists(filePath))
        {
            audioFile = new AudioFileReader(filePath);
            curFilePath = filePath;
            outputDevice.Init(audioFile);
        }
        else
        {
            Stop();
            return false;
        }

        return true;
    }

    public void Play()
    {
        if (outputDevice != null)
        {
            outputDevice.Play();
        }
    }

    public void Pause()
    {
        if (outputDevice != null)
        {
            outputDevice.Pause();
        }
    }

    public void Stop()
    {
        if (outputDevice != null)
        {
            outputDevice.Dispose();
        }
        outputDevice = null;
        if (audioFile != null)
        {
            audioFile.Dispose();
        }
        audioFile = null;
        curFilePath = null;
    }

    public void setVolumePercentage(float volume)
    {
        // clamp passed value
        if (volume > 100f)
        {
            volume = 100f;
        } else if (volume < 0f)
        {
            volume = 0f;
        }

        // set volume
        if (outputDevice != null)
        {
            outputDevice.Volume = volume / 100f;
        }
    }

    public void setPositionPercent(float percentDone)
    {
        // clamp percentDone
        if (percentDone > 100f)
        {
            percentDone = 100f;
        } else if (percentDone < 0f)
        {
            percentDone = 0f;
        }

        // assign the value
        if (audioFile != null)
        {
            audioFile.Position = (long) (audioFile.Length / 100 * (percentDone));
        }
    }

    public bool isReady()
    {
        return (outputDevice != null && audioFile != null);
    }

    private void OnPlaybackStopped(object? sender, StoppedEventArgs e)
    {
    }
}
