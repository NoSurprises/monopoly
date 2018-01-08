using UnityEngine;
using Assets.New_scripts;

public class Operations : MonoBehaviour
{
    public GameObject cubes;

    System.Random ran;
    public hudChange hud;
    public DialogCentre dialog;
    Turn turn;
    int firstDice;
    private int _secondDice;

    public int secondDice
    {
        get { return _secondDice; }
        set { _secondDice = value; ChooseNewCard(); cubes.SetActive(false); }
    }


    private void Start()
    {
        ran = new System.Random();
        turn = GetComponent<Turn>();
        dialog = hud.gameObject.GetComponentInChildren<DialogCentre>(true);
        foreach (Cube item in cubes.GetComponentsInChildren<Cube>(true))
        {
            item.cubeRolled += (x) =>
            {
                if (firstDice == 0)
                    firstDice = x;
                else
                    secondDice = x;
            };
        }

    }
    public void Buy()
    {
        PlayerStuff player = GetComponent<Turn>().player;
        Card card = player.targetCard;
        Property prop = player.GetComponent<Property>();

        if (player.gameObject.GetComponent<Money>().Transaction(card.cost))
        {
            print("Опреция завершена");

            player.GetComponent<Property>().AddCard();

            player.EndTurn();

        }
        else
        {
            print("Недостаточно стредств");

        }

    }


    public void Buy(Card card, PlayerStuff player)
    {


        Property prop = player.GetComponent<Property>();

        if (player.gameObject.GetComponent<Money>().Transaction(card.cost))
        {
            print("Опреция завершена");

            prop.AddCard(card);
            
        }
        else
        {
            print("Недостаточно стредств");

        }

    }

    public void Pay()
    {
        PlayerStuff player = GetComponent<Turn>().player;
        Card card = player.targetCard;


        if (player.gameObject.GetComponent<Money>().Transaction(card.cost))
        {
            if (card.owner != null)
            {
                card.owner.GetComponent<Money>().Transaction(-card.cost);

                print("Владельцу карты начислены деньги");
            }

            print("Опреция завершена");
            if (player.inJail)
                player.GoOutOfJail();
            player.EndTurn();

        }
        else
        {
            print("Недостаточно стредств");
        }
    }

    public void Decline()
    {
        
        turn.player.EndTurn();
    }


    public void UpgradeCard()
    {
        PlayerStuff player = GetComponent<Turn>().player;
        Card card = player.targetCard;

        if (!player.upgradeMade) 
        {
            card.Upgrade();
            player.upgradeMade = true; 
        }
    }

    public void ChooseNewCard()
    {
        
        // int secondDice = firstDice;
        if (firstDice == secondDice)
            turn.player.dice = true;

        if (turn.player.inJail) {

            if (turn.player.dice)
            {
                turn.player.GoOutOfJail();
                // добавить движение до той клетки
            }
            else
            {
                turn.player.jailTurns++;
                
            }
            if (turn.player.jailTurns == 3)
            {
                hud.HideRoll();
                playerMove.cardMenu.ShowPay();
            }
            else
                turn.player.EndTurn();
        }
        else
        {

            if (!(turn.player.dice && turn.player.turns == 3))
            {
                turn.player.currentCardIndex = turn.player.currentCardIndex + firstDice + secondDice;
                 //turn.player.currentCardIndex = 20; // ход на мини игру
                if (turn.player.currentCardIndex >= turn.player.cards.Length)
                {
                    turn.player.GetComponent<Money>().Transaction(-GameSettings.LapMoney);
                    if (turn.player.currentCardIndex == turn.player.cards.Length)
                        turn.player.GetComponent<Money>().Transaction(-GameSettings.StartLapMoney);

                }
                turn.player.currentCardIndex = turn.player.currentCardIndex % turn.player.cards.Length;


                turn.player.targetCard = turn.player.cards[turn.player.currentCardIndex];  // move

                hud.HideRoll();


            }
            else
            {

                turn.player.EndTurn();
            }
        }
        dialog.gameObject.SetActive(true);

        dialog.ShowMessage("Игроку " + turn.player.name + " выпадает " + (firstDice + secondDice));

        firstDice = 0;

    }
    
}
