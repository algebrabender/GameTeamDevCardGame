using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Hand
{
    public Card[] cards = new Card[3];
    public Transform[] positions = new Transform[3];
    public string[] animNames = new string[3];
    public bool isPlayers;

    public void RemoveCard(Card card)
    {
        
    }

    internal void ClearHand()
    {
        
    }
}
