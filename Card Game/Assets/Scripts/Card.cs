using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Card : MonoBehaviour
{
    public CardData cardData = null;

    public void Initialize()
    {
        if (cardData == null)
        {
            Debug.LogError("Card has no CardData");
            return;
        }

        //setting up card
    }
}
