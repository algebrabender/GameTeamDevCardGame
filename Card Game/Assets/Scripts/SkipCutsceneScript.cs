using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SkipCutsceneScript : MonoBehaviour
{
    public Animator transition;

    public float transitionTime = 2f;

    void Start()
    {
        StartCoroutine(Delay());
    }

    public void SkipSceneFunction()
    {
        LoadNextLevel();
    }
    public void LoadNextLevel()
    {
        StartCoroutine(LoadLvl(SceneManager.GetActiveScene().buildIndex + 1));
    }


    IEnumerator LoadLvl(int levelIndex)
    {
        transition.SetTrigger("Start");

        yield return new WaitForSeconds(transitionTime);


        SceneManager.LoadScene(levelIndex);
    }

    IEnumerator Delay()
    {
        yield return new WaitForSeconds(15.0f);

        transition.SetTrigger("Start");

        yield return new WaitForSeconds(transitionTime);

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
