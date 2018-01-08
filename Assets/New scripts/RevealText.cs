using UnityEngine;
using UnityEngine.UI;

public class RevealText : MonoBehaviour {


    public float time;
    float currentChangeTime;
    bool changing;
    private bool _visible;
    Text text;
    Color color;

    float lastingTime = 2f;

    public bool makeVisible
    {
        get { return _visible; }
        set
        {
            _visible = value;
            changing = true;
            currentChangeTime = 0f;
        }
    }

    private void Start()
    {
        text = GetComponent<Text>();
        time = 1f;
        color = text.color;
        color.a = 0;
        text.color = color;




    }

    // Update is called once per frame
    void Update()
    {
        if (changing)
        {
            

            currentChangeTime += Time.deltaTime;

            color = text.color;
            if (makeVisible)
                color.a = Mathf.Lerp(0f, 1f, currentChangeTime / time);
            else
            {
                color.a = Mathf.Lerp(1f, 0f, currentChangeTime / time);

            }
            text.color = color;


            if (makeVisible)
            {
                if (currentChangeTime >= time + lastingTime)
                {
                    ChangeLabelState(false);
                    
                }
            }
            else if (currentChangeTime >= time)
            {
                changing = false;
                currentChangeTime = 0f;
            }
            
        }
    }

    internal void ChangeLabelState(bool reveal)
    {
        makeVisible = reveal;
        
    }
}

