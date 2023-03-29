using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

// A Client

public class TurtleButton : NetworkBehaviour
{
    public EventManager eventManager;

    SpriteRenderer turtleRenderer;

    public void Update()
    {
    }

    public void Start()
    {
        turtleRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnMouseDown()
    {
        Debug.Log("Turtle Clicked!");

        var ownCliID = OwnerClientId;
        var netObjID = NetworkObjectId;

        eventManager.turtleClicked(ownCliID, netObjID);
    }
}

