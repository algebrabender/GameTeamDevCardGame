using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingMenuScript : MonoBehaviour
{

    public void Loading()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }


    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Loading();
        }

    }
}