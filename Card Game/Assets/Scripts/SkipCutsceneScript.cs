using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SkipCutsceneScript : MonoBehaviour
{
    public void SkipSceneFunction() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            }
}
