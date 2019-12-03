using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class KeyBindings
{

    public KeyCode[] up;
    public KeyCode[] down;
    public KeyCode[] left;
    public KeyCode[] right;

}
 
public class Player : MonoBehaviour
{

    public Vector2 targetAxis;
    public Vector2 inputAxis;

    public KeyBindings keys;

    void Start()
    {

        if (Application.isEditor)
        {
            keys.up[0] = KeyCode.W;
            keys.down[0] = KeyCode.S;
            keys.left[0] = KeyCode.A;
            keys.right[0] = KeyCode.D;
        }

    }

    void Update()
    {

        if (Application.isMobilePlatform)
        {

            Vector2 newTargetAxis = Vector2.zero;
            if (Input.GetMouseButton(0))
            {
                if (Mathf.Abs(Input.mousePosition.y / Screen.height - 0.5f) > Mathf.Abs(Input.mousePosition.x / Screen.width - 0.5f))
                {
                    if (Input.mousePosition.y > Screen.height / 2)
                    {
                        newTargetAxis.y = 1;
                    }
                    if (Input.mousePosition.y < Screen.height / 2)
                    {
                        newTargetAxis.y = -1;
                    }
                }
                else if (Mathf.Abs(Input.mousePosition.y / Screen.height - 0.5f) < Mathf.Abs(Input.mousePosition.x / Screen.width - 0.5f))
                {
                    if (Input.mousePosition.x > Screen.width / 2)
                    {
                        newTargetAxis.x = 1;
                    }
                    if (Input.mousePosition.x < Screen.width / 2)
                    {
                        newTargetAxis.x = -1;
                    }
                }
            }

            Debug.Log(newTargetAxis);
            inputAxis = Vector2.Lerp(newTargetAxis, inputAxis, 25f / 40f);

            targetAxis = newTargetAxis;

        }

        if (!Application.isMobilePlatform)
        {
            GetAxis();
        }

        this.transform.Translate(inputAxis.x * Time.deltaTime * 10, inputAxis.y * Time.deltaTime * 10, 0, Space.World);

    }

    public void GetAxis()
    {

        Vector2 newTargetAxis = Vector2.zero;

        foreach (KeyCode kc in keys.left)
        {
            if (Input.GetKey(kc))
            {
                newTargetAxis.x = -1;
            }
        }

        foreach (KeyCode kc in keys.right)
        {
            if (Input.GetKey(kc))
            {
                newTargetAxis.x = 1;
            }
        }

        foreach (KeyCode kc in keys.down)
        {
            if (Input.GetKey(kc))
            {
                newTargetAxis.y = -1;
            }
        }

        foreach (KeyCode kc in keys.up)
        {
            if (Input.GetKey(kc))
            {
                newTargetAxis.y = 1;
            }
        }

        inputAxis = Vector2.Lerp(newTargetAxis, inputAxis, 25f / 40f);

        targetAxis = newTargetAxis;

    }
}