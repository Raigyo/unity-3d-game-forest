using UnityEngine;
using System;
using UnityStandardAssets.Characters.FirstPerson;

//Source: https://gist.github.com/jawinn/f466b237c0cdc5f92d96

//SlopeDetection: Finds the slope/grade/incline angle of ground underneath a CharacterController

public class SlopeDetection : MonoBehaviour
{

    [Header("Results")]
    public float groundSlopeAngle = 0f;            // Angle of the slope in degrees
    //public Vector3 groundSlopeDir = Vector3.zero;  // The calculated slope as a vector
    
    [Header("Settings")]
    //public bool showDebug = false;                  // Show debug gizmos and lines
    private LayerMask castingMask;                  // Layer mask for casts. You'll want to ignore the player.
    private float startDistanceFromBottom = 0.2f;   // Should probably be higher than skin width
    private float sphereCastRadius = 0.25f;
    private float sphereCastDistance = 0.75f;       // How far spherecast moves down from origin point

    // Component reference
    private CharacterController controller;
    private FirstPersonControllerEdited controllerScript;

    void Awake()
    {
        // Get component on the same GameObject
        castingMask = LayerMask.GetMask("level");
        controller = GetComponent<CharacterController>();
        controllerScript = GetComponent<FirstPersonControllerEdited>();
        if (controller == null) { Debug.LogError("SlopeDetection did not find a FirstPersonController component."); }
        if (controllerScript == null) { Debug.LogError("SlopeDetection did not find a FirstPersonController component."); }
    }

    void FixedUpdate()
    {
        // Check ground, with an origin point defaulting to the bottom middle
        // of the char controller's collider. Plus a little higher 
        if (controller && controller.isGrounded)
        {
            CheckGround(new Vector3(transform.position.x, transform.position.y - (controller.height / 2) + startDistanceFromBottom, transform.position.z));
        }
    }

    /// <summary>
    /// Checks for ground underneath, to determine some info about it, including the slope angle.
    /// </summary>
    /// <param name="origin">Point to start checking downwards from</param>
    public void CheckGround(Vector3 origin)
    {
        // Out hit point from our cast(s)
        RaycastHit hit;

        // SPHERECAST
        // "Casts a sphere along a ray and returns detailed information on what was hit."
        if (Physics.SphereCast(origin, sphereCastRadius, Vector3.down, out hit, sphereCastDistance, castingMask))
        {
            // Angle of our slope (between these two vectors). 
            // A hit normal is at a 90 degree angle from the surface that is collided with (at the point of collision).
            // e.g. On a flat surface, both vectors are facing straight up, so the angle is 0.
            groundSlopeAngle = Vector3.Angle(hit.normal, Vector3.up);

            if (groundSlopeAngle >50f)
            {
                controllerScript.m_JumpSpeed = 0f;
            }
            else if (groundSlopeAngle <= 50f && groundSlopeAngle > 35f)
            {
                controllerScript.m_JumpSpeed = 0.5f;
            }
            else
            {
                controllerScript.m_JumpSpeed = 5f;
            } 
        }        
    }
}

