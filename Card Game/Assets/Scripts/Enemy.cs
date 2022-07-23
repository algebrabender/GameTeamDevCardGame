using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Enemy : MonoBehaviour, IDropHandler
{
    public Sprite level1Enemy = null;
    public Sprite level2Enemy = null;
    public Sprite level3Enemy = null;

    internal int maxHealth;
    internal int health; //current health
    internal int strength;

    public GameObject[] strengthPoints = new GameObject[6];

    void Start()
    {
        //setting up enemy based on level
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
}
