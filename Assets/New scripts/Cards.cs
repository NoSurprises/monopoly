using System.Collections.Generic;
using UnityEngine;

public class Cards : MonoBehaviour {

    public static Card[] cards;

    public Dictionary<int, int> fullMonopolyQuants;

    private void Start()
    {
        cards = GetComponentsInChildren<Card>();
        
        fullMonopolyQuants = new Dictionary<int, int>();
        fullMonopolyQuants[0] = 2;
        fullMonopolyQuants[1] = 3;
        fullMonopolyQuants[2] = 3;
        fullMonopolyQuants[3] = 2;
        fullMonopolyQuants[4] = 3;
        fullMonopolyQuants[5] = 3;
        fullMonopolyQuants[6] = 3;
        fullMonopolyQuants[7] = 3;
        fullMonopolyQuants[15] = 3;
    }

    
}
