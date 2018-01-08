using UnityEngine;
using Assets.New_scripts;

public class DisablePropertyMenu : MonoBehaviour {

    float timeout;
    bool isDisabling;

    private void Start()
    {
        timeout = GameSettings.CardTimeOut;
    }

    private void Update()
    {
        if (isDisabling)
        {
            timeout -= Time.deltaTime;
            if (timeout < 0)
            {
                gameObject.SetActive(false);
                isDisabling = false;
            }
        }
    }

    private void OnMouseExit()
    {

        isDisabling = true;

    }

    private void OnMouseEnter()
    {
        isDisabling = false;
        timeout = GameSettings.CardTimeOut;
    }
}
 