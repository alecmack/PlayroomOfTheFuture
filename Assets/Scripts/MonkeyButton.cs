using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

// A Client

public class MonkeyButton : NetworkBehaviour
{
    public EventManager eventManager;

    SpriteRenderer monkeyRenderer;

    public void Update()
    {
    }

    public void Start()
    {
        monkeyRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnMouseDown()
    {
        Debug.Log("Monkey Clicked!");

        var ownCliID = OwnerClientId;
        var netObjID = NetworkObjectId;

        eventManager.monkeyClicked(ownCliID, netObjID);
    }
}
