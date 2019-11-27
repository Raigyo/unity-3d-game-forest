using UnityEngine;
using System.Collections;

// GrabObject: animation / sound / set the object unactive

public class GrabObject : MonoBehaviour {

	public Transform guide;
    private GameObject itemSelected;
    public GameObject tempParent;
    private GameObject itemToRelease;
    AudioSource audioData;

    private void Update()
    {
        //listener to know if the mouse button is released and if an item is still grabed
       if (Input.GetMouseButtonUp(0) && itemToRelease)
        {
            UnGrabObjectFct(itemToRelease);
        }
    }

    void Start()
    {
        audioData = GetComponent<AudioSource>();
    }

    //Grabbed (called from HitBehaviour.cs)
    public void GrabObjectFct(GameObject itemSelected)
    {
        //play sound
        audioData.Play(0);
        itemSelected.transform.GetChild(0).gameObject.SetActive(false);
        //private assign to be used in update
        itemToRelease = itemSelected;
        //grab the object animation
        itemSelected.GetComponent<Rigidbody>().useGravity = false;
        itemSelected.GetComponent<Rigidbody>().isKinematic = true;
        itemSelected.transform.position = guide.transform.position;
        itemSelected.transform.rotation = guide.transform.rotation;
        itemSelected.transform.parent = tempParent.transform;        
    }

    //Ungrabbed (called from Update): set the GO inactive
    private void UnGrabObjectFct(GameObject itemSelected)
    { 
        StartCoroutine(DestroyGO());
        IEnumerator DestroyGO()
        {                
            yield return new WaitForSeconds(1);
            itemSelected.SetActive(false);
        }
    }
}

