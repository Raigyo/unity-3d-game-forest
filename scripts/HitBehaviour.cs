using UnityEngine;
using TMPro;

//HitBehaviour: Raycast on GO that can be picked-up / Highlight GO 

public class HitBehaviour : MonoBehaviour
{
    //Reference to cam
    Camera cam;
    public GameObject playerCameraFPS;

    //color for selected material
    [SerializeField] private Material highlightMaterial;

    //reference to the object selected to come back to its default state
    private Transform _selection;
    private Material defaultMaterial;
    //ref to ScoreAndTimeController to know the time and score
    public GameObject countdownScript;


    void Update()
    {
        //Camera raycast management
        cam = playerCameraFPS.GetComponent<Camera>();
        Ray ray = cam.ViewportPointToRay(new Vector3(0.5F, 0.5F, 0)); //center pointer with camera
        LayerMask mask = LayerMask.GetMask("rubbish"); // mask with interractable objects / layer 8

        //Object highlighting: if out, back to the default renderer
        if (_selection != null && _selection.GetComponent<Renderer>() != null)
        {
            var selectionRenderer = _selection.GetComponent<Renderer>();
            selectionRenderer.material = defaultMaterial;
            _selection = null;
        }

        //Check if camera hit interractable objects
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 10, mask))
        {
            //Object highlighting: if on, highlighting
            var selection = hit.transform;
            //print("GO: " + selection.gameObject);
            if (selection.GetComponent<Renderer>() != null)
            {
                var selectionRenderer = selection.GetComponent<Renderer>();
                defaultMaterial = selection.GetComponent<Renderer>().material;
                if (selectionRenderer != null)
                {
                    selectionRenderer.material = highlightMaterial;
                }
            }
 
            _selection = selection;

            //Actions avalaible with mouse
            //Push
            if (Input.GetMouseButtonDown(0))
            {
                //check if component script exists then call the function in GrabObjectWGL.cs
                if (hit.transform.gameObject.GetComponent("GrabObject"))
                {
                    //send the GO to layer zero so it's not detected by the raycast anymore and so cannnot be clicked
                    selection.gameObject.layer = 0;
                    countdownScript.GetComponent<ScoreAndTimeController>().SetCountText();
                    //GrabObjectWGL.cs => animation / sound / destroy the object
                    hit.transform.gameObject.GetComponent<GrabObject>().GrabObjectFct(hit.transform.gameObject);                    
                }
            }
        }
    }
}
