using UnityEngine;

public class PlayerStuff : MonoBehaviour
{
 
    public Turn turn;
    public static hudChange hud;
    public GameObject tablePlayer;
    public Card[] cards;

    public Property property;
    public bool upgradeMade;
    public Color color;
    Card _targetCard;
    public bool inJail;
    playerMove move;
    public Card targetCard
    {
        get { return _targetCard; }
        set
        {
            _targetCard = value;
            move.target = targetCard;

            move.enabled = true;
        }
    }

    public bool isActive;
    public PlayerStuff nextPlayer;
    public int currentCardIndex;

    static System.Random ran = new System.Random();
    public bool dice = false;

    private int _jailTurns;

    public int jailTurns
    {
        get { return _jailTurns; }
        set { _jailTurns = value;
            if(jailTurns ==3)
            { 
                playerMove.cardMenu.ShowPay();
            }
        }
    }

    private int _turns;

    public int turns
    {
        get { return _turns; }
        set
        {
            _turns = value;
            if (turns == 4)
            {
                turns = 1;
                dice = false;
                GoToJail();
            }
        }
    }



    // Use this for initialization

    void Start()
    {
        // TODO optimisation

        property = GetComponent<Property>();
        cards = Cards.cards;
        _turns = 1;
        this.transform.position = cards[0].transform.position;
        currentCardIndex = 0;
        GetComponent<Renderer>().material.color = color;

        if (hud == null) hud = GameObject.Find("HUD").GetComponentInChildren<hudChange>();
        turn = transform.parent.GetComponent<Turn>();
        move = this.GetComponent<playerMove>();
    }

    public void Bancrupt()
    {

        int profit = GetComponent<Property>().SellAllCards();

        Money money = GetComponent<Money>();
        money.Transaction(-profit); // деньги за продажу всех территорий

        if ((targetCard != null) && !(targetCard.owner == null))
        {
            targetCard.owner.GetComponent<Money>().Transaction(-money.MoneyAmount);
        }
        money.Transaction(money.MoneyAmount); // деньги до нуля

        // перейти к следущему игроку
        hud.HideAll();
        turn.player = nextPlayer;
        hud.ShowRoll();
        //

        turn.RemovePlayer(this);

    }

    public  void GoToJail()
    {
        currentCardIndex = 10;
        inJail = true;
        targetCard = cards[10];
        EndTurn();
        
    }

    public void EndTurn()
    {
        hud.HideAll();
        PlayerStuff tmp = turn.player;
        if (!dice)
        {
            turns = 1;
            turn.player = nextPlayer;

        }
        else
        {
            turns++;
        }
        print("Конец хода у игрока " + tmp.name + ". Сейчас будет "+ turns + " ход у игрока " + turn.player.name);
        hud.ShowRoll();
        upgradeMade = false;
        dice = false;
    }

    internal void GoOutOfJail()
    {
        inJail = false;
        jailTurns = 0;
        EndTurn();
      
    }
}


