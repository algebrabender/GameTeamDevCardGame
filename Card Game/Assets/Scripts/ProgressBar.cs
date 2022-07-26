using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class ProgressBar : MonoBehaviour
{

    private Slider slider;
    public float fillSpead = 0.35f;
    private float targetProgress = 0;

    private void Awake()
    {
        slider = gameObject.GetComponent<Slider>();
    }
    // Start is called before the first frame update
    void Start()
    {
        IncrementProgress(0.9f);
    }

    // Update is called once per frame
    void Update()
    {
        if (slider.value < targetProgress)
        {
            slider.value += fillSpead * Time.deltaTime;
        }
    }


    public void IncrementProgress(float newProgress)
    {
        targetProgress = slider.value + newProgress;
    }

}
