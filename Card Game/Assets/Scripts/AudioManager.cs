using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    public AudioClip backgroundAudioClip;
    public static AudioSource backgroundAudioSource;

    public AudioClip winScreenAudioClip;
    public static AudioSource winScreenAudioSource;

    public AudioClip gameoverScreenAudioClip;
    public static AudioSource gameoverScreenAudioSource;

    public AudioClip dealAudioClip;
    //public static AudioSource dealAudioSource;

    public Button button;

    void Awake()
    {
        instance = this;

        DontDestroyOnLoad(this.gameObject);
    }

    void Start()
    {
        if (backgroundAudioSource == null || !backgroundAudioSource.isPlaying)
        {
            backgroundAudioSource = AddAudio(backgroundAudioClip, false, true, 0.25f);
            backgroundAudioSource.Play();
        }

        winScreenAudioSource = AddAudio(winScreenAudioClip, false, false, 0.5f);
        gameoverScreenAudioSource = AddAudio(gameoverScreenAudioClip, false, false, 0.5f);
       // dealAudioSource = AddAudio(dealAudioClip, false, false, 1.0f);

    }

    public void PlayWinScreenAudio()
    {   
        backgroundAudioSource.Stop();
        winScreenAudioSource.Play();
        backgroundAudioSource.PlayDelayed(3.0f);
    }

    public void PlayGameOverAudio()
    {
        
   
            backgroundAudioSource.Stop();
            gameoverScreenAudioSource.Play();
            backgroundAudioSource.PlayDelayed(gameoverScreenAudioClip.length);
      
    }

    public void PlayBackgroundAudio()
    {
            backgroundAudioSource.Play();
    }
    /*
    public void PlayDealAudio()
    {
        dealAudioSource.Play();
    }

    */

    private AudioSource AddAudio(AudioClip clip, bool playOnAwake, bool loop, float volume)
    {
        AudioSource audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.clip = clip;
        audioSource.playOnAwake = playOnAwake;
        audioSource.loop = loop;
        audioSource.volume = volume;
        return audioSource;
    }
    //-----------------------------------------------------------------------------------

    public Sprite soundOnImage;
    public Sprite soundOFFImage;

    public Button MuteButton;
    public bool isON = true;


    public void PressButton()
    {
        if (isON)
        {
            MuteButton.image.sprite = soundOFFImage;
            isON = false;
            backgroundAudioSource.Stop();


            backgroundAudioSource.Stop();
            gameoverScreenAudioSource.Stop();
            winScreenAudioSource.Stop();

        }
        else
        {
            MuteButton.image.sprite = soundOnImage;
            isON = true;
            backgroundAudioSource.Play();
           
}
    }

}
      