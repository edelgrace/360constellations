using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.UI;

public class Narration : MonoBehaviour {
    public VideoPlayer playerToControl;
    private AudioSource narration01;

    // buttons
    public Button hero1;
    public Button hero2;
    public Button hero3;
    public Button enemy1;
    public Button enemy2;

    // Audio clips
    public AudioClip intro;
    public AudioClip hero;
    public AudioClip enemy;
    public AudioClip cat;
    public AudioClip man;
    public AudioClip snake;
    public AudioClip catWin;
    public AudioClip catLose;
    public AudioClip manLose;
    public AudioClip manWin;
    public AudioClip snakeWin;
    public AudioClip snakeLose;

    private AudioClip[] heroClips;
    private Button[] buttons;

    private int state = 0;
    private int theHero = 0;
    private bool started = false;
    private bool heroChosen = false;
    private bool enemyChosen = false;
    
	// Use this for initialization
	void Start () {
        // play after 15 seconds
        narration01 = GetComponent<AudioSource>();
        narration01.clip = intro;
        Invoke("PlaySound", 15.0f);

        // put clips into an array
        AudioClip[] clips = { cat, man, snake, catWin, 
            catLose, manWin, manLose,
            snakeWin, snakeLose
            };

        heroClips = clips;

        // put buttons into an array
        Button[] btns = { hero1, hero2, hero3 };
        buttons = btns;

        // turn off hero buttons
        hero1.gameObject.SetActive(false);
        hero2.gameObject.SetActive(false);
        hero3.gameObject.SetActive(false);

        // turn off enemy buttons
        enemy1.gameObject.SetActive(false);
        enemy2.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        // check if started
        if(narration01.isPlaying)
        {
            started = true;
        }
        // go through states
        else if(!narration01.isPlaying && started) {
            switch(state)
            {
                // choose a hero
                case 0:
                    // play clip
                    narration01.clip = hero;
                    narration01.Play();
                    state = 1;
                    break;
                // hero chosen
                case 1:
                    // check if hero chosen
                    if(heroChosen)
                    {
                        // play clip
                        narration01.clip = heroClips[theHero];
                        narration01.Play();

                        // show buttons
                        ColorBlock prev = buttons[theHero].colors;
                        prev.normalColor = new Color(0.0f, 0.0f, 0.0f, 1.0f);
                        buttons[theHero].colors = prev;

                        state = 2;
                    }
                    else // turn on buttons and pause video
                    {
                        playerToControl.Pause();
                        hero1.gameObject.SetActive(true);
                        hero2.gameObject.SetActive(true);
                        hero3.gameObject.SetActive(true);
                    }
                    break;
                // choose an enemy
                case 2:
                    // resume video
                    playerToControl.Play();

                    // play enemy spiel
                    narration01.clip = enemy;
                    narration01.Play();

                    // turn off hero buttons
                    hero1.gameObject.SetActive(false);
                    hero2.gameObject.SetActive(false);
                    hero3.gameObject.SetActive(false);

                    state = 3;

                    break;
                // enemy chosen
                case 3:
                    if(enemyChosen)
                    {

                    }
                    else
                    {
                        playerToControl.Pause();

                        // turn on enemy buttons
                        enemy1.gameObject.SetActive(true);
                        enemy2.gameObject.SetActive(true);
                    }

                    break;
            }
        }
    }

    public void catBtn()
    {
        heroChosen = true;
        theHero = 0;
        hero2.gameObject.SetActive(false);
        hero3.gameObject.SetActive(false);
    }

    public void manBtn()
    {
        heroChosen = true;
        theHero = 1;
        hero1.gameObject.SetActive(false);
        hero3.gameObject.SetActive(false);
    }

    public void snkBtn()
    {
        heroChosen = true;
        theHero = 2;
        hero1.gameObject.SetActive(false);
        hero2.gameObject.SetActive(false);
    }

    void PlaySound()
    {
        narration01.Play();
    }
}
