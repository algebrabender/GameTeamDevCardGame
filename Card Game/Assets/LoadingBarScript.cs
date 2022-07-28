using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class LoadingBarScript : MonoBehaviour
{
    public static LoadingBarScript instance;



    [SerializeField]
    private GameObject Loading_Bar_Holder;

     void Awake()
    {
        MakeSingleton();
    }


    void MakeSingleton()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    } 






}
