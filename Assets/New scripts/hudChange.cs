using UnityEngine;
using UnityEngine.UI;


public class hudChange : MonoBehaviour
{
    public DialogCentre dia;
    public Button rollButton;
    public GameObject tradePrefab;

    public Image tradeMenu;
    public Image bancruptMenu;

    public GameObject cubes;
    public GameObject music;
    public VerticalLayoutGroup table;
    public Image Dialog;
    public Game miniGame;

    public Image auction;
    
    public Turn turn;
    public Button sellout;
    bool wasEnabled;
    
    private void Start()
    {
        miniGame = GetComponentInChildren<Game>(true);
        rollButton.onClick.AddListener(RollCubes);
        rollButton.onClick.AddListener(HideRoll);
        
    }
    
    public void RollCubes()
    {
        cubes.SetActive(true);
    }


    public void SetTransparency(Image p_image, float p_transparency)
    {
        if (p_image != null)
        {
            Color __alpha = p_image.color;
            __alpha.a = p_transparency;
            p_image.color = __alpha;

        }
    }

   

    public void ShowRoll()
    {
        rollButton.gameObject.SetActive(true);
        if (!wasEnabled)
        {
            // sellout.gameObject.SetActive(true);
            wasEnabled = true;

        }
    }

    public void HideRoll()
    {
        rollButton.gameObject.SetActive(false);
        // sellout.gameObject.SetActive(false);
    }

    public void HideAll()
    {
        foreach (Transform child in transform)
        {
            if (child.gameObject != music)
                child.gameObject.SetActive(false);
            
        }
        table.gameObject.SetActive(true);
        dia.gameObject.SetActive(true);
        
        


    }

    internal void EnableBancruptMenu(PlayerStuff player)
    {
        bancruptMenu.gameObject.SetActive(true);
        bancruptMenu.GetComponentsInChildren<Button>()[0].onClick.AddListener(() => bancruptMenu.gameObject.SetActive(false));

        bancruptMenu.GetComponentsInChildren<Button>()[1].onClick.AddListener(() =>
        {
            player.Bancrupt();
            bancruptMenu.gameObject.SetActive(false);
        });

    }

    public void EnableTradeMenu(PlayerStuff player)
    {
        tradeMenu.gameObject.SetActive(true);
        tradeMenu.GetComponentsInChildren<Text>()[0].text = turn.player.name;
        tradeMenu.GetComponentsInChildren<Text>()[1].text = player.name;
        tradeMenu.GetComponent<Trade>().secondTrader = player;
    }




}
