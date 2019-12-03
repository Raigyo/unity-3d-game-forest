using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;
using UnityEngine.SceneManagement;

public class CursorVisibility : MonoBehaviour
{
    public Texture2D cursorTexture;
    public CursorMode cursorMode = CursorMode.Auto;
    public Vector2 hotSpot = Vector2.zero;

    void Awake()
    {
        SceneManager.sceneLoaded += LoadScene;
    }
    // when scene loaded check if FirstPersonController exists to disable mouse cursor
    void LoadScene(Scene scene, LoadSceneMode mode)
    {

        if (FindObjectOfType<FirstPersonControllerEdited>() != null)
        {
            //print("FirstPersonController found");
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
        else
        {
            //print("FirstPersonController not found");
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            //Cursor.SetCursor(cursorTexture, hotSpot, cursorMode);
        }
    }
}