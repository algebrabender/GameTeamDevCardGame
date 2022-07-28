using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class MainMenuScript : MonoBehaviour


{
    // taking reference for black image 
    public Animator transition;

    public Image backgroundImage = null;
    public Sprite lostLevelOne;
    public Sprite lostLevelTwoAndThree;


    void Start()
    {
        if (SceneManager.GetActiveScene().buildIndex == 4)
        {
            if (GameController.instance.lastPlayedLevel == 0)
                backgroundImage.sprite = lostLevelOne;
            else
                backgroundImage.sprite = lostLevelTwoAndThree;
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
        StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex - 4));

    }





    IEnumerator LoadLevel(int levelIndex)
    {
        transition.SetTrigger("Start");

        yield return new WaitForSeconds(1);

        SceneManager.LoadScene(levelIndex);

    }




}
