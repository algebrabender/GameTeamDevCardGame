using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static System.TimeZoneInfo;

public class SceneController : MonoBehaviour
{
    // taking reference for black image 
    public Animator transition;

    public Image backgroundImage = null;
    public Sprite lostLevelOne;
    public Sprite lostLevelTwoAndThree;

    public Sprite imageOne;
    public Sprite imageTwo;
    public Button newGameButton;
    public Button quitButton;
    public Text gameWinText;
    public Image blackImage;
    public Text credits;

    public Button MuteButton = null;

    void Start()
    {
        if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            if (AudioManager.instance.isON)
            {
                MuteButton.image.sprite = AudioManager.instance.soundOnImage;
                //AudioManager.instance.PauseBackgroundAudio();
            }
            else
            {
                MuteButton.image.sprite = AudioManager.instance.soundOFFImage;
                //AudioManager.instance.UnpauseBackgroundAudio();
            }
        }

        if (SceneManager.GetActiveScene().buildIndex == 4)
        {
            AudioManager.instance.PlayGameOverAudio();
            
            if (GameController.instance.lastPlayedLevel == 0)
                backgroundImage.sprite = lostLevelOne;
            else
                backgroundImage.sprite = lostLevelTwoAndThree;
        }
        
        if (SceneManager.GetActiveScene().buildIndex == 5)
        {
            backgroundImage.sprite = imageOne;

            StartCoroutine(Delay());
        }
    }

    // Function for start game when we hit play button
    public void PlayGame()
    {
        StartCoroutine(LoadLevel(3));
        StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex + 2));
    }

    public void CreateANewGame()
    {
        PlayerPrefs.SetInt("lastPlayedLevel", 0);
        PlayerPrefs.Save();
        StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex + 1));
    }

    //Function for quit the game when hit quit button
    public void Quit()
    {
        #if UNITY_EDITOR
            EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }
    
    //Function for returning to main menu when we hit main menu button
    public void MainMenu()
    {
        if (SceneManager.GetActiveScene().buildIndex == 4)
            AudioManager.instance.StopGameOverAudio();
        SceneManager.LoadScene(1);

        //StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex - 4));
      
    }

    public void HowToPlayButton()
    {
        StartCoroutine(LoadLevel(6));
    }


    IEnumerator LoadLevel(int levelIndex)
    {
        transition.SetTrigger("Start");

        yield return new WaitForSeconds(1);

        SceneManager.LoadScene(levelIndex);

    }

    IEnumerator Delay()
    {
        yield return new WaitForSeconds(5.0f);

        //AudioManager.instance.PlayWinScreenAudio();

        backgroundImage.sprite = imageTwo;

        gameWinText.gameObject.SetActive(true);

        //yield return new WaitForSeconds(0.5f);

        

       // yield return new WaitForSeconds(0.5f);

       

        yield return new WaitForSeconds(4);
        credits.gameObject.SetActive(true);
        blackImage.gameObject.SetActive(true);

        yield return new WaitForSeconds(34);
        credits.gameObject.SetActive(false);
        blackImage.gameObject.SetActive(false);
        
        newGameButton.gameObject.SetActive(true);
        quitButton.gameObject.SetActive(true);

    }

    public void PressButton()
    {
        if (AudioManager.instance.isON)
        {
            MuteButton.image.sprite = AudioManager.instance.soundOFFImage;
            AudioManager.instance.isON = false;
            AudioManager.instance.PauseBackgroundAudio();


        }
        else
        {
            MuteButton.image.sprite = AudioManager.instance.soundOnImage;
            AudioManager.instance.isON = true;
            AudioManager.instance.UnpauseBackgroundAudio();
        }
    }


}

