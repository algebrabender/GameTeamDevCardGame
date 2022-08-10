using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

[System.Serializable]
public class Deck
{
    public List<CardData> cardDatas = new List<CardData>();

    public void Create()
    {
        List<CardData> cardDataInOrder = new List<CardData>();
        
        foreach (CardData cardData in GameController.instance.cards)
        {
            for (int i = 0; i < cardData.numberInDeck; i++)
                cardDataInOrder.Add(cardData);
        }

        while(cardDataInOrder.Count > 0)
        {
            int randomIndex = Random.Range(0, cardDataInOrder.Count);
            cardDatas.Add(cardDataInOrder[randomIndex]);
            cardDataInOrder.RemoveAt(randomIndex);
        }
    }

    public void CreateEnemyDeck(int level)
    {
        List<CardData> cardDataInOrder = new List<CardData>();
        List<CardData> enemyCards = null;
        cardDatas.Clear();

        switch(level)
        {
            case 0:
                enemyCards = GameController.instance.level1EnemyCards;
                break;
            case 1:
                enemyCards = GameController.instance.level2EnemyCards;
                break;
            case 2:
                enemyCards = GameController.instance.level3EnemyCards;
                break;

        }

        foreach (CardData cardData in enemyCards)
        {
            for (int i = 0; i < cardData.numberInDeck; i++)
                cardDataInOrder.Add(cardData);
        }

        while (cardDataInOrder.Count > 0)
        {
            int randomIndex = Random.Range(0, cardDataInOrder.Count);
            cardDatas.Add(cardDataInOrder[randomIndex]);
            cardDataInOrder.RemoveAt(randomIndex);
        }
    }

    private CardData RandomCard(bool isPlayer)
    {
        CardData result = null;

        //empty deck
        if (cardDatas.Count == 0)
        {
            if (isPlayer)
            {
                for (int i = 0; i < 3; i++)
                    if (GameController.instance.playersHand.cards[i] != null)
                        return result;

                GameController.instance.GameOverDueCards();
            }
            else
            {
                GameController.instance.messageText.text = "You are taking your time! Seems like you are struggling...";
                GameController.instance.enemy.maxHealth += 1;
                GameController.instance.enemy.maxStrength += 1;
                CreateEnemyDeck(GameController.instance.lastPlayedLevel);
            }
        }

        result = cardDatas[0];
        cardDatas.RemoveAt(0);

        return result;
    }

    private Card CreateNewCard(Vector3 position, string animName, GameObject prefab, bool isPlayer)
    {
       GameObject newCard = GameObject.Instantiate(prefab, GameController.instance.canvas.gameObject.transform);

        newCard.transform.position = position;
        Card card = newCard.GetComponent<Card>();
        if (card)
        {
            card.cardData = RandomCard(isPlayer);
            card.Initialize();

            if (isPlayer)
                card.isPlayers = true;

            Animator animator = newCard.GetComponentInChildren<Animator>();
            if (animator)
            {
                animator.CrossFade(animName, 0);
            }
            else
            {
                Debug.LogError("No Animator found!");
            }

            return card;
        }
        else
        {
            Debug.LogError("No card component found!");
            return null;
        }
    }
    
    public void DealCard(Hand hand)
    {
        GameObject prefab = null;
        for(int h =0;  h < 3; h++)
        {
            if(hand.cards[h] == null)
            {
                if (hand.isPlayers)
                {
                    //GameController.instance.player.PlayDealSound();
                    prefab = GameController.instance.cardPrefab1;
                }
                else
                {
                    //GameController.instance.enemy.PlayDealSound();
                    switch (GameController.instance.lastPlayedLevel)
                    {
                        case 0:
                            prefab = GameController.instance.teenagerCardPrefab;
                            break;
                        case 1:
                            prefab = GameController.instance.policemanCardPrefab;
                            break;
                        case 2:
                            prefab = GameController.instance.mayorCardPrefab;
                            break;

                    }
                }

                hand.cards[h] = CreateNewCard(hand.positions[h].position, hand.animNames[h], prefab, hand.isPlayers);
                return;
            }
        }
    }

    internal void TakeBackCard(Card card)
    {
        int randomIndex = Random.Range(0, cardDatas.Count);
        cardDatas.Insert(randomIndex, card.cardData);
    }
}
