using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Player : MonoBehaviour, IDropHandler
{
    public Image playerImage = null;
    public Image healthNumberImage = null;
    public Image glowImage = null;

    internal int maxHealth = 10;
    internal int health = 5; //current health
    internal int strength = 4; //6 max

    public GameObject[] strengthPoints = new GameObject[6];

    public AudioSource dealAudio = null;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    internal void UpdateHealth()
    {

    }

    internal void UpdateManaBalls()
    {

    }

    public void OnDrop(PointerEventData eventData)
    {
        throw new System.NotImplementedException();
    }

    internal void PlayDealSound()
    {
        dealAudio.Play();
    }

}
