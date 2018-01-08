using UnityEngine.UI;
using UnityEngine;

public class Game : MonoBehaviour
{

    Turn turn;
    public Image rollArea;
    float spinSeconds;
    float currTime;
    // Use this for initialization
    void Start()
    {
        spinSeconds = 0;
        currTime = 0;
        turn = GameObject.Find("players").GetComponent<Turn>();
        GetComponentsInChildren<Button>(true)[0].onClick.AddListener(() =>
        {
            turn.player.EndTurn();
        });
        GetComponentsInChildren<Button>(true)[1].onClick.AddListener(() =>
        {
            spinSeconds = Random.Range(2f, 4f);
        });
    }

    // Update is called once per frame
    void Update()
    {
        if (spinSeconds > 0)
        {
            currTime += Time.deltaTime;
            if (currTime > spinSeconds)
            {
                spinSeconds = 0;
                currTime = 0;



                float target = rollArea.transform.localEulerAngles.z;
                print(target);
                if (target > 0 && target <= 90)
                {
                    turn.player.GetComponent<Money>().Transaction(-Assets.New_scripts.GameSettings.sector1Money);
                    turn.player.EndTurn();


                }
                else if (target > 90 && target <= 180)
                {// dead
                    turn.player.Bancrupt();



                }
                else if (target >= 180 && target <= 270)
                {
                    turn.player.GetComponent<Money>().Transaction(-Assets.New_scripts.GameSettings.sector2Money);
                    turn.player.EndTurn();



                }
                else
                {
                    turn.player.GetComponent<Money>().Transaction(200 * turn.player.GetComponent<Property>().CountProperty());
                    turn.player.EndTurn();
                }


            }
            else
            {
                rollArea.transform.Rotate(Vector3.forward, 10 * (1 - currTime / spinSeconds));
            }
        }
    }
}
