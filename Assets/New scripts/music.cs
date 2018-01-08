using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class music : MonoBehaviour {
    AudioSource source;
    Image bound;

    bool shouldPlay;
    public List<AudioClip> gameMusic;

    List<AudioClip> clips;

    public List<AudioClip> menuMusic;
    int currentMusicPlaying;
	// Use this for initialization
	void Start () {
        bound = GetComponentsInParent<Image>()[1];
        shouldPlay = true;
        source = GetComponentInParent<AudioSource>();
        GetComponent<UnityEngine.UI.Button>().onClick.AddListener(() =>
       {
           Color tmp = bound.color;
           if (source.isPlaying)
           {
               source.Pause();
               
               tmp.g = 0f;
               tmp.b = 0f;
           }
           else
           {
               source.UnPause();
               tmp.g = 1f;
               tmp.b = 1f;
           }
           bound.color = tmp;
           shouldPlay = !shouldPlay;

       });

        transform.parent.GetComponentsInChildren<Button>()[1].onClick.AddListener(PreviousSong);
        transform.parent.GetComponentsInChildren<Button>()[2].onClick.AddListener(NextSong);

        if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex == 0){
            // menu
            clips = menuMusic;
        }
        else if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex == 1)
        {
            clips = gameMusic;

        }
        
	}


    private void PreviousSong()
    {
        currentMusicPlaying = Mathf.Abs((currentMusicPlaying - 1)) % clips.Count;
        source.clip = clips[currentMusicPlaying];
        source.Play();

    }

    private void NextSong()
    {
        
        currentMusicPlaying = (currentMusicPlaying + 1) % clips.Count;
        source.clip = clips[currentMusicPlaying];
        source.Play();
        
    }
    
    private void Update()
    {
        if (!source.isPlaying && shouldPlay)
            NextSong();
    } 
}
