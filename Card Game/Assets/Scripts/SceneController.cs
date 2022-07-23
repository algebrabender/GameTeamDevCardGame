using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
   
    // Function for start game when we hit play button
   public void PlayGame()
    {
       SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
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
        SceneManager.LoadScene(0);
    }

   
}
