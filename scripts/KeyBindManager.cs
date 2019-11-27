using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KeyBindManager : MonoBehaviour
{
    private Dictionary<string, KeyCode> keys = new Dictionary<string, KeyCode>();

    public Text up, left, down, right, jump, run;

    private GameObject currentKey;

    void Start()
    {
        keys.Add("Up", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Up","W"))); //convert string to keycode
        keys.Add("Down", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Down", "S")));
        keys.Add("Left", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Left", "A")));
        keys.Add("Right", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Right", "D")));
        keys.Add("Run", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Run", "Backslash")));
        keys.Add("Jump", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Jump", "Space")));

        up.text = keys["Up"].ToString();
        down.text = keys["Down"].ToString();
        left.text = keys["Left"].ToString();
        right.text = keys["Right"].ToString();
        run.text = keys["Run"].ToString();
        jump.text = keys["Jump"].ToString();
    }

    void Update()
    {
        if (Input.GetKeyDown(keys["Up"]))
        {
            //Do a move action
            Debug.Log("Up");
        }

        if (Input.GetKeyDown(keys["Down"]))
        {
            //Do a move action
            Debug.Log("Down");
        }

        if (Input.GetKeyDown(keys["Left"]))
        {
            //Do a move action
            Debug.Log("Left");
        }

        if (Input.GetKeyDown(keys["Right"]))
        {
            //Do a move action
            Debug.Log("Right");
        }

        if (Input.GetKeyDown(keys["Run"]))
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

    private void OnGUI()
    {
        if (currentKey != null)
        {
            Event e = Event.current;
            if (e.isKey)
            {
                keys[currentKey.name] = e.keyCode;
                currentKey.transform.GetChild(0).GetComponent<Text>().text = e.keyCode.ToString();               
                currentKey = null;
            }
        }
    }

    public void ChangeKey(GameObject clicked)
    {
        currentKey = clicked;
    }

    public void SaveKeys()
    {
        foreach (var key in keys)
        {
            PlayerPrefs.SetString(key.Key, key.Value.ToString());
        }

        PlayerPrefs.Save();
    }

}
