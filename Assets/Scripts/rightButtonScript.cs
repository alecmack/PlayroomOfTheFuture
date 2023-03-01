using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

// using this as connected host for testing purposes


public class rightButtonScript : NetworkBehaviour
{
    //public GameObject cameraHolder;

    //[SerializeField] public GameObject iteractWith;


    public EventManager eventManager;

    SpriteRenderer button1Renderer;
    //SpriteRenderer otherButton;


    public void Update()
    {

    }

    public void Start()
    {


        button1Renderer = GetComponent<SpriteRenderer>();
       // button1Renderer.color = Color.red;

    }
    private void OnMouseDown()
    {
        Debug.Log("Button 2 clicked!");

        var ownCliID = OwnerClientId;
        var netObjID = NetworkObjectId;

        eventManager.rightButtonClicked(ownCliID, netObjID);
      //  Debug.Log(OwnerClientId);

        //eventManager.changeOtherColorClientRpc();

        // EventManager.changeOtherColorClientRpc();

        // MUST BE A NON STATIC CALL , NEED AN OBJECT ??
    }

    /*
    [ClientRpc]
    private void changeOtherColorClientRpc()
    {
        Debug.Log("Are we reaching server? this is host");
        otherButton = iteractWith.GetComponent<SpriteRenderer>();
        otherButton.color = Color.green;

        button1Renderer.color = Color.red;
    }

    */

}
