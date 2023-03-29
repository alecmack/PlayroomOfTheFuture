using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

// A Host

public class LionButton : NetworkBehaviour
{
    public EventManager eventManager;

    SpriteRenderer lionRenderer;

    public void Update()
    {
    }

    public void Start()
    {
        lionRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnMouseDown()
    {
        Debug.Log("Lion Clicked!");

        var ownCliID = OwnerClientId;
        var netObjID = NetworkObjectId;

        eventManager.lionClicked(ownCliID, netObjID);
    }
}
