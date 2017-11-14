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
    public Button enemy1;   // dove
    public Button enemy2;   // woman


    // terrain
    public Terrain t1;  // sandy
    public Terrain t2;  // rocks
    public Terrain t3;  // grass
    public MeshFilter t4;   // water

    // Audio clips
    public AudioClip intro;
    public AudioClip hero;
    public AudioClip enemy;
    public AudioClip cat;
    public AudioClip man;
    public AudioClip snake;

    public AudioClip catWin;    // cat vs dove
    public AudioClip catLose;   // cat vs woman

    public AudioClip manLose;   // cat vs woman
    public AudioClip manWin;    // cat vs dove

    public AudioClip snakeWin;  // snake vs woman
    public AudioClip snakeLose; // snake vs dove

    private AudioClip[] heroClips;
    private Button[] buttons;

    private int state = 0;
    private int theHero = 0;
    private int theEnemy = 0;

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
            catLose, manLose, manWin,
            snakeLose, snakeWin
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

        // turn off terrain
        t1.gameObject.SetActive(false);
        t2.gameObject.SetActive(false);
        t3.gameObject.SetActive(false);
        t4.gameObject.SetActive(false);
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

                    if (enemyChosen)
                    {
                        // resume video
                        playerToControl.Play();
                        
                        // turn off hero buttons
                        hero1.gameObject.SetActive(false);
                        hero2.gameObject.SetActive(false);
                        hero3.gameObject.SetActive(false);

                    }


                    state = 3;

                    break;
                // enemy chosen
                case 3:
                    state = 4;

                    if(enemyChosen)
                    {
                        // play the fight spiel
                        narration01.clip = heroClips[theEnemy];
                        narration01.Play();
                    }
                    else
                    {
                        playerToControl.Pause();

                        // turn on enemy buttons
                        enemy1.gameObject.SetActive(true);
                        enemy2.gameObject.SetActive(true);
                    }
 
                    break;
                case 4:
                    playerToControl.Play();
                    narration01.Stop();
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
    
    public void wmnBtn()
    {
        enemyChosen = true;
        switch(theHero)
        {
            case 0:
                theEnemy = 4;
                break;
            case 1:
                theEnemy = 6;
                break;
            case 2:
                theEnemy = 8;
                break;

        }
        enemy1.gameObject.SetActive(false);
    }

    public void dveBtn()
    {
        enemyChosen = true;
        switch (theHero)
        {
            case 0:
                theEnemy = 3;
                break;
            case 1:
                theEnemy = 5;
                break;
            case 2:
                theEnemy = 7;
                break;

        }
        enemy2.gameObject.SetActive(false);
    }

    void PlaySound()
    {
        narration01.Play();
    }
}
