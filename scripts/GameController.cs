﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityStandardAssets.Characters.FirstPerson;
using UnityEngine.UI;
using TMPro;

//GameController : counter and score management / display messages and pannels /show hide some elements

public class GameController : MonoBehaviour
{
    [SerializeField] private GameObject loadingScreen;//loading screen reference
    [SerializeField] private Slider slider;//progressbar
    //public TextMeshProUGUI progressText;//text to display in % -not used
    public Animator animator;//animation fade in/out using a trigger
    public GameObject musicToFade;//main sound to fade
    public string nextScene;//name of next scene

    public GameObject hitBehaviour;//ref to Hitbahaviour to know the score
    public GameObject scoreAndTimeController;//ref to ScoreAndTimeController to disable it in walking mode
    public GameObject overlayIntroLoading;//ref to overlauy GUI enabled when loading
    public GameObject overlayTooltips;//ref to tootltips pannel from intro to disable when loading
    public GameObject settingsPanel;//settings pannel reference
    public GameObject endGamePanel;//end game pannel reference
    public GameObject pausePanel;//pause game pannel reference
    public GameObject quitPanel;//quit game pannel reference
    public GameObject overlayMain;//Intro: main GUI overlay / Game: Score+Overlay-Game-Minimap reference
    public GameObject fpsControler;//ref to fpsControler to disable it
    public GameObject crossHair;//ref to crossHair to hide it
    public GameObject rubbish;//ref to rubbish to hide it

    public TextMeshProUGUI endGameTitle;//text to display when game is finished
    public TextMeshProUGUI endGameText;//text to display when game is finished

    public GameObject audioWin;//soud when you win
    public GameObject audioLost;//soud when you loose 
    private AudioSource musicPlayer;//main music source   
    private AudioSource musicPlayer2;//second music source 
    private bool stopUpdate;//used to check if the sound is playing

    public ScriptableVars scriptableVars;//scriptable object ref

    //private Dictionary<string, KeyCode> keys = new Dictionary<string, KeyCode>();
    private Scene scene;//for the scene detection


    void Start()
    {
        scene = SceneManager.GetActiveScene(); //needed for try again and reload the same scene
        //Detect the mode selected in the menu Game mode or Walking mode
        if (scriptableVars.walkingMode == true && scene.buildIndex != 0)
        {
            walkingMode();
        }
        //set loading screen unactive
        loadingScreen.SetActive(false);
        //unfreeze the game
        Time.timeScale = 1;
        //fade in main sound 
        musicPlayer = musicToFade.GetComponent<AudioSource>();
        StartCoroutine(AudioController.FadeIn(musicPlayer, 1.5f));
        //update activated to check if the sound is playing, needed to launch the preloader afted fading out the sound
        stopUpdate = false;
        //Reset the time for Enviro
        if (scene.buildIndex != 0)
        {
            EnviroSkyLite.instance.SetInternalTimeOfDay(15.0f);
        }
    }

    public void Update()
    {
        //after fade out of the intro sound, start coroutine to load next scene
        if (!musicPlayer.isPlaying && !stopUpdate)
        {
            stopUpdate = true;
            //to avoid infinite loop
            StartCoroutine(LoadAsynchronously(nextScene));
            //start the coroutine with loading bar/loading next scene
        }
    }

    //Scene changer: first sound fade out then if soundvolume at zero it's detected in the update. Load scene preloader is launched
    public void LoadScene()
    {
        Time.timeScale = 1;
        //otherwise no update
        if (endGamePanel != null && endGamePanel.activeSelf == true)
        {
            endGamePanel.SetActive(false);
        }//if pannel active: hide (activeSelf: do not depend on if parent/child is a active >< activeInHierarchy)
        if (pausePanel != null && pausePanel.activeSelf == true)
        {
            pausePanel.SetActive(false);
        }//if pannel active: hide
        if (overlayMain != null && overlayMain.activeSelf == true)
        {
            overlayMain.SetActive(false);
        }//if overlay active: hide

        if (scene.buildIndex == 0)
        {
            StartCoroutine(AudioController.FadeOut(musicPlayer, 1.5f));
            //fade out main sound on intro       
        }
        else
        {
            StartCoroutine(LoadAsynchronously(nextScene));
        }
    }

    //If try again: change scene target
    public void ChangeSceneTarget()
    {
        Scene scene = SceneManager.GetActiveScene();
        nextScene = scene.name;
    }

    IEnumerator LoadAsynchronously(string name)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(name);
        Time.timeScale = 0;
        //freeze the game (to avoid player continue the game during preload
        loadingScreen.SetActive(true);
        //set loading screen active
        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / .9f);
            slider.value = progress;
            //progressText.text = progress * 100f + "%";

            if (progress == 1.0f)
            {
                animator.SetTrigger("FadeOut");
            }
            yield return null;
        }
        //preload bar then screen fade out
    }

    //Forest - Win: hide some GO and display lost message / try again - back to menu
    public void WinGame(int cans, int minutes, int seconds)
    {
        musicPlayer2 = audioWin.GetComponent<AudioSource>();
        musicPlayer2.Play(0);
        //Freeze the game - Disable scripts that still work while timescale is set to 0
        Time.timeScale = 0;
        //all these elements are disabled
        fpsControler.GetComponent<FirstPersonControllerEdited>().enabled = false;
        hitBehaviour.SetActive(false);
        crossHair.SetActive(false);
        //use of mouse to click buttons
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        //Display win pannel
        endGamePanel.SetActive(true);
        endGameTitle.text = "congratulations!";
        print("minutes: " + minutes);
        if (minutes == 0)
        {
            endGameText.text = "You picked up all the " + cans.ToString() + " cans in " + seconds + " seconds!";
        }
        else
        {
            endGameText.text = "You picked up all the " + cans.ToString() + " cans in " + minutes + " minute(s) and " + seconds + " second(s)!";
        }
    }

    //Forest - LOST: hide some GO and display lost message / try again - back to menu
    public void LostGame(int cans)
    {
        musicPlayer2 = audioLost.GetComponent<AudioSource>();
        musicPlayer2.Play(0);
        //Freeze the game - Disable scripts that still work while timescale is set to 0
        Time.timeScale = 0;
        //all these elements are disabled
        fpsControler.GetComponent<FirstPersonControllerEdited>().enabled = false;
        hitBehaviour.SetActive(false);
        crossHair.SetActive(false);
        //use of mouse to click buttons
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        //Display lost pannel
        endGamePanel.SetActive(true);
        endGameTitle.text = "It's late...";
        if (cans > 1)
        {
            endGameText.text = "Time to come back home...  \n You picked up: " + cans.ToString() + " cans!";
        }
        else if (cans == 1)
        {
            endGameText.text = "Time to come back home...  \n You picked up: " + cans.ToString() + " can!";
        }
        else
        {
            endGameText.text = "Time to come back home...  \n You picked up nothing!";
        }
    }

    //Detect if walking mode has been selected in menu
    public void GameMode(bool modeSelected)
    {
        scriptableVars.walkingMode = modeSelected; ;
    }

    //Disable some GO in gaming mode
    private void walkingMode()
    {
        overlayMain.SetActive(false);
        hitBehaviour.SetActive(false);
        rubbish.SetActive(false);
        scoreAndTimeController.SetActive(false);
        crossHair.SetActive(false);
    }


    //Intro: open and close settings tab - settiings not implemented
    public void openSettings()
    {
        settingsPanel.SetActive(true);
    }

    public void closeSettings()
    {
        settingsPanel.SetActive(false);
    }

    //to hide GUI when we leave intro
    public void hideGui()
    {
        overlayIntroLoading.SetActive(true);
        overlayMain.SetActive(false);
        overlayTooltips.SetActive(false);
    }

    //Intro: exit button
    public void ExitGame()
    {
        quitPanel.SetActive(true);
    }

    public void ExitGameChoice(bool choice)
    {
        if (choice)
        {
            quitPanel.SetActive(false);
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
                        Application.Quit ();
#endif
        }
        else
        {
            quitPanel.SetActive(false);
        }
}
}
