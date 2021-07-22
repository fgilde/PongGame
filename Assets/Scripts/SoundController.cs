using UnityEngine;
using System.Collections;
using Assets.Scripts._base;
using UnityEngine.UI;

public class SoundController : UnityBaseBehaviour
{

    public AudioSource Music;

    public AudioSource WallSound;
    public AudioSource RacketSound;

    public Text SoundVolumeText;
    public Toggle SoundMuteToggle;
    public Slider SoundSlider;

    public Text MusicVolumeText;
    public Toggle MusicMuteToggle;
    public Slider MusicSlider;

    public override void Awake()
    {
        if (SoundMuteToggle != null && PlayerPrefs.HasKey("isSoundMuted"))
            SoundMuteToggle.isOn = PlayerPrefs.GetString("isSoundMuted") == "false";
        if (MusicMuteToggle != null && PlayerPrefs.HasKey("isMusicMuted"))
            MusicMuteToggle.isOn = PlayerPrefs.GetString("isMusicMuted") == "false";


        if (PlayerPrefs.HasKey("soundVolume"))
        {
            var volume = PlayerPrefs.GetFloat("soundVolume");
            if (WallSound != null && RacketSound != null)
            {
                WallSound.volume = volume/100f;
                RacketSound.volume = volume/100f;
            }
            if(SoundSlider != null)
                SoundSlider.value = volume;
            if(SoundVolumeText != null)
                SoundVolumeText.text = volume + " %";
        }

        if (PlayerPrefs.HasKey("musicVolume"))
        {
            var volume = PlayerPrefs.GetFloat("musicVolume");
            if(Music != null)
                Music.volume = volume / 100f;
            if(MusicSlider != null)
                MusicSlider.value = volume;
            if(MusicVolumeText != null)
                MusicVolumeText.text = volume + " %";
        }

    }

    public void ChangeMusicVolume(float f)
    {
        f = Mathf.Round(MusicSlider.value);
        PlayerPrefs.SetFloat("musicVolume", f);
        if(Music != null)
            Music.volume = f / 100f;
        if (MusicVolumeText != null)
            MusicVolumeText.text = f + " %";
    }

    public void ToggleMusicMute()
    {
        bool muted = !MusicMuteToggle.isOn;
        if(Music != null)
            Music.mute = muted;
        PlayerPrefs.SetString("isMusicMuted", muted.ToString().ToLower());
    }

    public void ChangeSoundVolume(float f)
    {        
        f = Mathf.Round(SoundSlider.value);
        PlayerPrefs.SetFloat("soundVolume", f);
        if (WallSound != null && RacketSound != null)
        {
            WallSound.volume = f/100f;
            RacketSound.volume = f/100f;
        }
        if (SoundVolumeText != null)
            SoundVolumeText.text = f + " %";
    }

    public void ToggleSoundMute()
    {
        bool muted = !SoundMuteToggle.isOn;
        if (WallSound != null && RacketSound != null)
            WallSound.mute = RacketSound.mute = muted;
        PlayerPrefs.SetString("isSoundMuted", muted.ToString().ToLower());
    }


}
