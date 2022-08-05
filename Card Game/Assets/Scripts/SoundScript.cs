using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SoundScript : MonoBehaviour
{
    private Sprite soundsOnImage;
    public  Sprite soundsOFFImage;
    public Button button;
    public bool isON = true;


    public AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        soundsOnImage = button.image.sprite;
    }

    public void ButtonClicked()
    {
        if (isON)
        {
            button.image.sprite = soundsOFFImage;
            isON = false;
            audioSource.mute = true;
        }
        else
        {
            button.image.sprite = soundsOnImage;
            isON = true;
            audioSource.mute = false;
        }

    }
}
