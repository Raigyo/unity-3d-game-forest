using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rewire : MonoBehaviour
{
    private Dictionary<string, KeyCode> keys = new Dictionary<string, KeyCode>();
    Transform rb;
    public float jumpSpeed = 8.0f;
    public float gravity = 20.0f;
    public float speed = 10.0f;
    public float rotationSpeed = 100.0f;
    public GameObject fpsController;
    private KeyCode keyUpArrow;
    private KeyCode keyLeftArrow;
    private KeyCode keyDownArrow;
    private KeyCode keyRightArrow;
    private KeyCode keySpace;
    private KeyCode keyRun;


    void Start()
    {
        keys.Add("Up", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Up", "W")));
        keys.Add("Down", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Down", "S")));
        keys.Add("Left", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Left", "A")));
        keys.Add("Right", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Right", "D")));
        keys.Add("Run", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Run", "Backslash")));
        keys.Add("Jump", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Jump", "Space")));


        keyUpArrow = (KeyCode)System.Enum.Parse(typeof(KeyCode), "UpArrow");
        keyLeftArrow = (KeyCode)System.Enum.Parse(typeof(KeyCode), "LeftArrow");
        keyDownArrow = (KeyCode)System.Enum.Parse(typeof(KeyCode), "DownArrow");
        keyRightArrow = (KeyCode)System.Enum.Parse(typeof(KeyCode), "RightArrow");
        keySpace = (KeyCode)System.Enum.Parse(typeof(KeyCode), "Space");
        keyRun = (KeyCode)System.Enum.Parse(typeof(KeyCode), "LeftShift");
        //rb = GetComponent<Transform>();
        //KeyCode.Z = keys["Up"];
        //Input.GetKeyDown(KeyCode.Z) = Input.GetKeyDown(keys["Up"]);
    }


    // Update is called once per frame
    void Update()
    {
        //keyZ = keys["Up"];
        if (Input.GetKey(keys["Up"]))
        {
            //Do a move action
            Debug.Log("Up");
            Input.GetKey(keyUpArrow);

        }

        if (Input.GetKey(keys["Down"]))
        {
            //Do a move action
            Debug.Log("Down");

        }

        if (Input.GetKey(keys["Left"]))
        {
            //Do a move action
            Debug.Log("Left");

        }

        if (Input.GetKey(keys["Right"]))
        {
            //Do a move action
            Debug.Log("Right");

        }


        if (Input.GetKey(keys["Run"]))
        {
            //Do a move action
            Debug.Log("Run");
        }

        if (Input.GetKeyDown(keys["Jump"]))
        {
            //Do a move action
            Debug.Log("Jump");
        }

    }
}
