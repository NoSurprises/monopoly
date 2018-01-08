using UnityEngine;
using UnityEngine.UI;

public class RevealMessage : MonoBehaviour
{

    float time;
    float currentChangeTime;
    bool changing;
    private bool _visible;
    Text text;
    Color colorT;
    Color colorI;
    Image image;

    public bool makeVisible
    {
        get { return _visible; }
        set
        {
            _visible = value;
            changing = true;
        }
    }

    private void Start()
    {
        text = GetComponentInChildren<Text>();
        image = GetComponent<Image>();
        time = 0.5f;


    }

    // Update is called once per frame
    void Update()
    {
        if (changing)
        {
            currentChangeTime += Time.deltaTime;

            colorT = text.color;
            colorI = image.color;
            if (makeVisible)
            {
                colorT.a = Mathf.Lerp(0f, 1f, currentChangeTime / time);
                colorI.a = Mathf.Lerp(0f, 1f, currentChangeTime / time);
            }
            else
            {
                colorT.a = Mathf.Lerp(1f, 0f, currentChangeTime / time);
                colorI.a = Mathf.Lerp(1f, 0f, currentChangeTime / time);

            }
            text.color = colorT;
            image.color = colorI;
            if (currentChangeTime >= time)
            {
                changing = false;
                currentChangeTime = 0f;
            }
        }
    
    }
    internal void ChangeLabelState(bool reveal)
    {
        makeVisible = reveal;
        changing = true;
    }
}
