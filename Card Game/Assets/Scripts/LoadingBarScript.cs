using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class LoadingBarScript : MonoBehaviour
{


    private Slider slider;

    private float targetProgress = 0;
    private float FillSpeed = 0.5f;

    public Animator transition;
    public float transitionTime = 2f;
    private void Awake()
    {
        slider = gameObject.GetComponent<Slider>();
    }

    void Start()
    {
        IncrementProgress(1);
    }
    // Update is called once per frame


    void Update()
    {
        if (slider.value < targetProgress)
        {
            slider.value += FillSpeed * Time.deltaTime;
        }
        else
            LoadNextLevel();
    }

    public void IncrementProgress(float newProgress)
    {
       targetProgress = slider.value + newProgress;
    }
    public void LoadNextLevel()
    {
        //it doesn't work for now, I dntkn why
        StartCoroutine(LoadLvl(SceneManager.GetActiveScene().buildIndex + 1));
    }

    IEnumerator LoadLvl(int levelIndex)
    {
        transition.SetTrigger("Start");
        yield return new WaitForSeconds(transitionTime);
        SceneManager.LoadScene(levelIndex);
    }
}
