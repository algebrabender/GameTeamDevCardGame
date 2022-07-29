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
        for(int i = 0; i < 3; i++)
        {
            if(cards[i] == card)
            {
                GameObject.Destroy(cards[i].gameObject);
                cards[i] = null;

                if (isPlayers)
                    GameController.instance.playerDeck.DealCard(this);
                else
                    GameController.instance.enemyDeck.DealCard(this);
                break;
            }
        }
        
    }

    internal void ClearHand(bool afterEnemy = false)
    {
        if (!afterEnemy)
        {
            for (int i = 0; i < 3; i++)
            {
                GameObject.Destroy(cards[i].gameObject);
                cards[i] = null;
            }
        }
        else
        {
            for (int i = 0; i < 3; i++)
            {
                GameController.instance.playerDeck.TakeBackCard(cards[i]);
                GameObject.Destroy(cards[i].gameObject);
                cards[i] = null;
            }
        }
    }
}
