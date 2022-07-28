using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class BSloader : MonoBehaviour
{
    public Animator transition;

    public float transitionTime = 2f;

   /* void Update()
    {
        if (Input.GetMouseButton(0))
        {
            LoadNextLevel();
        }
    }*/

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
}
