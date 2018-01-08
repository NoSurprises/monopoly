using UnityEngine;
using UnityEngine.UI;

public class Blink : MonoBehaviour {

    Text textComp;
    float time;
    float currentTime;
    bool goRed;
    Color standartColor;
    int numberOfBlicks;
    private void Start()
    {
        time = 0.5f;
        textComp = GetComponent<Text>();
        standartColor = textComp.color;
    }
    private void OnEnable()
    {
        numberOfBlicks = 0;
        goRed = true;
        
        currentTime = 0f;  
    }
    private void Update()
    {
        currentTime += Time.deltaTime;
        Color col = textComp.color;
        
        if (goRed)
        {
            textComp.color = Color.red;
        }
        else
        {
            textComp.color = standartColor;
        }
        if (currentTime > time)
        {
            goRed = !goRed;
            currentTime = 0f;
            numberOfBlicks++;

        }
        if (numberOfBlicks > 4)
        {
            textComp.color = standartColor;
            enabled = false;
        }
        
    }
}
