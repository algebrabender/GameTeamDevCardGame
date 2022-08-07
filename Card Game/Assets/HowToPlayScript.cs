using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class HowToPlayScript : MonoBehaviour
{
    public Animator transition;

    public float transitionTime = 2f;


    public void LoadNextLevel()
    {
        StartCoroutine(LoadLvl(1));
    }


    public void LoadHowToPlayScene()
    {
        StartCoroutine(LoadLvl(6));
    }



    IEnumerator LoadLvl(int levelIndex)
    {
        transition.SetTrigger("Start");

        yield return new WaitForSeconds(transitionTime);


        SceneManager.LoadScene(levelIndex);
    }
}