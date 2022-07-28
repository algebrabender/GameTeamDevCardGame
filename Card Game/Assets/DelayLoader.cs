using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class DelayLoader : MonoBehaviour
{
    void Start()
    {
        StartCoroutine(TimeDelay());
    }


    IEnumerator TimeDelay()
    {
        yield return new WaitForSeconds(25);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
