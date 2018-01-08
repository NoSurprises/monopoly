using UnityEngine;
using Assets.New_scripts;

public class playerMove : MonoBehaviour
{

    hudChange hud;
    public Card target;
    public static CardMenu cardMenu;
    PlayerStuff player;
    DialogCentre dia;



    public float speed = 5f;

    private void Start()
    {
        player = GetComponent<PlayerStuff>();
        hud = GameObject.Find("HUD").GetComponentInChildren<hudChange>();
        dia = hud.GetComponentInChildren<DialogCentre>(true);
        if (cardMenu == null)
        {
            cardMenu = hud.GetComponentInChildren<CardMenu>();
        }
    }
    void Update()
    {

        float step = speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, target.transform.position, step);
        if (transform.position == target.transform.position)
        {
            this.enabled = false;
        }
    }

    private void OnDisable()
    {
        dia.ShowMessage(player.name + " попадает на клетку \"" + target.label + "\"");
        if (target.canBuy)
        {
            if (target.owner == null)
            {
                cardMenu.ShowBuy();
                cardMenu.ShowAuction();
            }
            else if (target.owner != player)
            {
                cardMenu.ShowPay();
            }
            else
            {
                player.EndTurn();
            }
        }

        else if (target.index == 9) // старт
        {
            dia.ShowMessage("Игрок " + player.name + " попадает в начало карты. Бонус " + GameSettings.StartLapMoney + "$");
            player.EndTurn();
        }
        else if (target.index == 10) // посещене тюрьмы
        {
            if (!player.inJail)
                dia.ShowMessage("Игрок " + player.name + " посещает тюрьму с экскурсией.");

            player.EndTurn();
        }
        else if (target.index == 11) // карта миниигры
        {
            dia.ShowMessage("Игроку " + player.name + " выпадает шанс неплохо заработать.");
            
            hud.miniGame.gameObject.SetActive(true);
            
        }
        else if (target.index == 12) // карта тюрьмы
        {
            dia.ShowMessage("Против игрока " + player.name + " было составлено дело и он отправлен в тюрьму.");

            player.GoToJail();
        }
        else if (target.index == 13) // заплатить немного
        {
            switch (Random.Range(0, 4))
            {

                case 0: dia.ShowMessage("Игрок " + player.name + " был оштрафован за неправильную парковку"); break;
                case 1: dia.ShowMessage("Игрок " + player.name + " потерял телефон. Нужно купить новый!"); break;
                case 2: dia.ShowMessage("Игрок " + player.name + " замечает распродажу и не может пройти мимо!"); break;
                case 3: dia.ShowMessage("Игрок " + player.name + " становится жертвой ограбления"); break;
                case 4: dia.ShowMessage("Игрок " + player.name + " хочет стать умнее и записывается на курсы."); break;
            }


            cardMenu.ShowPay();
        }
        else if (target.index == 14) // заплатить много
        {
            switch (Random.Range(0, 4))
            {

                case 0: dia.ShowMessage("Игрок " + player.name + " ломает подвеску на любимой машине"); break;
                case 1: dia.ShowMessage("Игрок " + player.name + " проигрывает на валютной бирже"); break;
                case 2: dia.ShowMessage("Игрок " + player.name + " делает бездумное вложение"); break;
                case 3: dia.ShowMessage("Игрок " + player.name + " становится жертвой ограбления"); break;
                case 4: dia.ShowMessage("Игрока " + player.name + " ждут непредвиденные растраты"); break;
            }
            cardMenu.ShowPay();
        }
        else
        {
            cardMenu.ChanceCard();
        }

    }
}
