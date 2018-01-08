
using UnityEngine;

public class OpenCardMenu : MonoBehaviour {

    Animator anim;
    Card card;
    Turn turn;
    hudChange hud;
    Trade trade;
    
	// Use this for initialization
	void Start () {

        trade = GameObject.Find("HUD").GetComponentInChildren<Trade>(true);
        
        turn = GameObject.Find("players").GetComponent<Turn>();
        card = GetComponentInChildren<Card>();
        if (card.canBuy) anim = GetComponentInChildren<Animator>();

        hud = turn.hud;


    }

    private void OnMouseEnter()
    {
        if (card.owner == turn.player && card.isMonopoly ) 
        {
            anim.SetBool("isMouseOver", true);

        }
        if (card.canBuy) 
        card.ChangeLabelState(true);


    }





    private void OnMouseExit()
    {
        if (card.canBuy)
        {
            anim.SetBool("isMouseOver", false);
            card.ChangeLabelState(false);


        }

    }

    private void OnMouseDown()
    {
        // trade
        if (hud.tradeMenu.IsActive())
        {
            
            if (card.owner == trade.secondTrader || card.owner == turn.player)
                if (!turn.player.tablePlayer.GetComponent<TradeCenter>().added.Contains(card))
                    turn.player.tablePlayer.GetComponent<TradeCenter>().Add(card);
           
        }



        else if (card.owner == turn.player)
        {

            
            if (card.isMonopoly && !turn.player.upgradeMade && turn.player.GetComponent<Property>().CheckForUpgrade(card)
                && card.upgradeLevel < 5 && turn.player.GetComponent<Money>().Transaction(card.upgradeCost))
            {
                card.upgradeLevel++;
                card.upgradeCost += 50;
                turn.player.upgradeMade = true;
                card.cost *= 2;

            }
            else if (card.cost > 0 && !card.isMonopoly)
            {
                card.SellCard();
            }
            else if (card.cost == 0 && turn.player.GetComponent<Money>().Transaction(card.upgradeCost))
            {
                card.cost = card.startValue;
                turn.player.upgradeMade = true;
            }
        }
    }

   
}
