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

    void Start()
    {
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
        SceneManager.LoadScene(3);
        //StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex - 4));
      
    }
    
    IEnumerator LoadLevel(int levelIndex)
    {
        transition.SetTrigger("Start");

        yield return new WaitForSeconds(1);

        SceneManager.LoadScene(levelIndex);

    }

    IEnumerator Delay()
    {
        yield return new WaitForSeconds(15.0f);

        //AudioManager.instance.PlayWinScreenAudio();

        backgroundImage.sprite = imageTwo;

        gameWinText.gameObject.SetActive(true);

        yield return new WaitForSeconds(0.5f);

        newGameButton.gameObject.SetActive(true);

        yield return new WaitForSeconds(0.5f);

        quitButton.gameObject.SetActive(true); 
    }


}

