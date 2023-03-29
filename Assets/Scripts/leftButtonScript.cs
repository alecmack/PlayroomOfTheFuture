using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

// This is the client for testing purposes

public class leftButtonScript : NetworkBehaviour
{
    //  public GameObject cameraHolder;

    // [SerializeField] public GameObject iteractWith;

    public EventManager eventManager;

    SpriteRenderer button1Renderer;
    // SpriteRenderer otherButton;


    public void Update()
    {



    }

    public void Start()
    {

        button1Renderer = GetComponent<SpriteRenderer>();
        //button1Renderer.color = Color.green;


    }
    private void OnMouseDown()
    {
        Debug.Log("Left button clicked!");


        //otherButton = iteractWith.GetComponent<SpriteRenderer>();
        // otherButton.color = Color.green;

        //button1Renderer.color = Color.red;

        var ownCliID = OwnerClientId;
        var netObjID = NetworkObjectId;

        //eventManager.leftButtonClicked(ownCliID, netObjID);
        Debug.Log(OwnerClientId);

        // eventManager.changeOtherColorServerRpc();

        //   myobject.EventManager.changeOtherColorServerRpc();

        // NEED TO FIGURE OUT HOW TO CALL IT SINCE IT MUST BE NONSTATIC


    }
    /*
    [ServerRpc(RequireOwnership = false)]
    private void changeOtherColorServerRpc()
    {
        Debug.Log("Are we reaching server from client?????");

        otherButton = iteractWith.GetComponent<SpriteRenderer>();
        otherButton.color = Color.green;

        button1Renderer.color = Color.red;

    }
    */

}
