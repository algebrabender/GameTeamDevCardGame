using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SoundTurnOnOffScript : MonoBehaviour
{

    [SerializeField] Image SoundsONImage;
    [SerializeField] Image SoundsOFFImage;

    private bool muted = false;



     void Start()
    {
        if (!PlayerPrefs.HasKey("muted"))
        {
            PlayerPrefs.SetInt("muted", 0);
            Load();
        }
        else
        {
            Load();
        }
        UpdateButtonIcon();
        AudioListener.pause = muted;
    }

    public void onButtonPress()
    {
        if(muted == false)
        {
            muted = true;
            AudioListener.pause = true;
        }
        else
        {
            muted = false;
            AudioListener.pause = false;

        }

        Save();
        UpdateButtonIcon();
    }


    public void UpdateButtonIcon()
    {
        if (muted == false)
        {
            SoundsONImage.enabled = true;
            SoundsOFFImage.enabled = false;
        }
        else
        {
            SoundsONImage.enabled = false;
            SoundsOFFImage.enabled = true;

        }
    }



    public void Load()
    {
        muted = PlayerPrefs.GetInt("muted") == 1;
    }

    public void Save()
    {
        PlayerPrefs.SetInt("muted", muted ? 1 : 0);
    }

}
