using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityStandardAssets.Characters.FirstPerson;
using UnityEngine.UI;
using TMPro;
using System;

//ActionKeysManager: Manage the keyboard shortcuts ingame and what's displayed / hidden then


public class ActionKeysManager : MonoBehaviour
{
    [SerializeField] private GameObject pausePanel;//pause pannel reference
    //[SerializeField] private GameObject timePanel;//time pannel reference
    [SerializeField] private GameObject miniMap;//minimap reference
    [SerializeField] private GameObject fpsControler;//ref to fpsControler to disable it
    [SerializeField] private GameObject hitBehaviour;//ref to hitbahaviour to disable raycast
    [SerializeField] private GameObject crossHair;//ref to crossHair to hide it
    [SerializeField] private GameObject timePannel;//TimeOptions reference
    [SerializeField] private GameObject timeDisplay;//DisplayTime reference

    public TextMeshProUGUI timeOfDay;


    public AudioSource playerDay;//main music source 
    public AudioSource playerNight;//main music source 

    public ScriptableVars scriptableVars;//scriptable object ref to know if we are in walking mode or game mode

    void Start()
    {
        playerDay.volume = 1.0f;
        playerNight.volume = 0.0f;
        pausePanel.SetActive(false);  
        
    }

    void Update()
    {
        //update atmosphere sound according the time of the day 
        int currentHour = EnviroSkyLite.instance.GameTime.Hours;
        if (currentHour >= 20 || currentHour <= 5)
        {
            playerDay.volume = 0.0f;
            playerNight.volume = 0.05f;
        }
        else
        {
            playerDay.volume = 1.0f;
            playerNight.volume = 0.0f;
        }
        

        timeOfDay.text = EnviroSkyMgr.instance.GetTimeString();//get time of the day from enviro
        if (Input.GetKeyDown(KeyCode.P) || Input.GetKeyDown(KeyCode.Escape) && Time.timeScale == 1)
        {
            if (!pausePanel.activeInHierarchy)
            {
                PauseGame();
            }
            else 
            {
                ContinueGame();
            }
        }
        //enable / disable pause pannel using 'p' or 'escape' key

        if (Input.GetKeyDown(KeyCode.F2) && Time.timeScale == 1 && scriptableVars.walkingMode == false)
        {
            if (!miniMap.activeInHierarchy)
            {
                miniMap.SetActive(true);
            }
            else
            {
                miniMap.SetActive(false);
            }
        }
        //enable / disable minimap using 'F2' key in play mode

        if (Input.GetKeyDown(KeyCode.F2) && Time.timeScale == 1 && scriptableVars.walkingMode == true)
        {
            if (!timePannel.activeInHierarchy)
            {
                timePannel.SetActive(true);
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
                fpsControler.GetComponent<FirstPersonControllerEdited>().enabled = false;
            }
            else
            {
                timePannel.SetActive(false);
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;
                fpsControler.GetComponent<FirstPersonControllerEdited>().enabled = true;
            }
        }
        //enable / disable minimap using 'F2' key in walking mode

        if (Input.GetKeyDown(KeyCode.F3) && Time.timeScale == 1 && scriptableVars.walkingMode == true)
        {
            EnviroSkyMgr.instance.SetTimeProgress(EnviroTime.TimeProgressMode.Simulated);
            EnviroSkyLite.instance.SetInternalTimeOfDay(7.0f);
        }
        if (Input.GetKeyDown(KeyCode.F4) && Time.timeScale == 1 && scriptableVars.walkingMode == true)
        {
            EnviroSkyMgr.instance.SetTimeProgress(EnviroTime.TimeProgressMode.Simulated);
            EnviroSkyLite.instance.SetInternalTimeOfDay(14.0f);
        }
        if (Input.GetKeyDown(KeyCode.F5) && Time.timeScale == 1 && scriptableVars.walkingMode == true)
        {
            EnviroSkyMgr.instance.SetTimeProgress(EnviroTime.TimeProgressMode.Simulated);
            EnviroSkyLite.instance.SetInternalTimeOfDay(20.0f);
        }
        if (Input.GetKeyDown(KeyCode.F6) && Time.timeScale == 1 && scriptableVars.walkingMode == true)
        {
            EnviroSkyMgr.instance.SetTimeProgress(EnviroTime.TimeProgressMode.SystemTime);
        }
        //switch between time of the day in walking mode
    }


    //Game paused
    public void PauseGame()
    {
        Time.timeScale = 0;
        //Freeze the game - Disable scripts that still work while timescale is set to 0
        fpsControler.GetComponent<FirstPersonControllerEdited>().enabled = false;
        hitBehaviour.SetActive(false);
        crossHair.SetActive(false);
        //timePanel.SetActive(false);
        //all these elements are disabled
        pausePanel.SetActive(true);
        //set pause pannel active
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        //use of mouse to click buttons
        if (timePannel.activeInHierarchy)
        {
            timePannel.SetActive(false);
        }
    }

    //Continue/resume
    public void ContinueGame()
    {
        Time.timeScale = 1;
        //unfreeze game
        fpsControler.GetComponent<FirstPersonControllerEdited>().enabled = true;
        hitBehaviour.SetActive(true);
        if (scriptableVars.walkingMode == true)
        {
            crossHair.SetActive(false);
        }
        else
        {
            crossHair.SetActive(true);
        }        
        //all these elements are enabled excepted corosshair in walking mode
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        //disable mouse to use raycast instead
        pausePanel.SetActive(false);
        //set pause pannel unactive
    }

    //Time options pannel management
    public void DropDownInput(int input)
    {
        switch (input)
        {
            case 0:
                EnviroSkyMgr.instance.SetTimeProgress(EnviroTime.TimeProgressMode.Simulated);
                EnviroSkyLite.instance.SetInternalTimeOfDay(7.0f);
                break;
            case 1:
                EnviroSkyMgr.instance.SetTimeProgress(EnviroTime.TimeProgressMode.Simulated);
                EnviroSkyLite.instance.SetInternalTimeOfDay(14.0f);
                break;
            case 2:
                EnviroSkyMgr.instance.SetTimeProgress(EnviroTime.TimeProgressMode.Simulated);
                EnviroSkyLite.instance.SetInternalTimeOfDay(20.0f);
                break;
            case 3:
                EnviroSkyMgr.instance.SetTimeProgress(EnviroTime.TimeProgressMode.SystemTime);
                break;
            default:
                print("Incorrect");
                break;
        }
    }

    public void ToggleInput(bool input)
    {
        if (input)
        {        
            timeDisplay.SetActive(true);
        }
        else
        {
            timeDisplay.SetActive(false);
        }
    }

    public void closeTimeTab()
    {
        timePannel.SetActive(false);
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        fpsControler.GetComponent<FirstPersonControllerEdited>().enabled = true;
    }
}
