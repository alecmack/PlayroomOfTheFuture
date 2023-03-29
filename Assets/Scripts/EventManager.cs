using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using TMPro;
using System.Net;
using System.Net.Sockets;

using Unity.Netcode.Transports.UTP;

public class EventManager : NetworkBehaviour
{
    public GameObject lionButton;
    public GameObject turtleButton;
    public GameObject monkeyButton;

    SpriteRenderer lionSprite;
    SpriteRenderer turtleSprite;
    SpriteRenderer monkeySprite;

    [SerializeField] TextMeshProUGUI numberOfClients;

    public NetworkManagerUI NetworkManagerUI;

    // Start is called before the first frame update
    void Start()
    {
        lionSprite = lionButton.GetComponent<SpriteRenderer>();
        turtleSprite = turtleButton.GetComponent<SpriteRenderer>();
        monkeySprite = monkeyButton.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        numberOfClients.text = NetworkManagerUI.getNumClients().ToString();
    }

    public void lionClicked(ulong ownCliID, ulong netObjID)
    {
        lionSprite.color = Color.red;
        turtleSprite.color = Color.red;
        monkeySprite.color = Color.red;
        lionToClientRpc(ownCliID, netObjID);
    }

    public void turtleClicked(ulong ownCliID, ulong netObjID)
    {
        lionSprite.color = Color.green;
        turtleSprite.color = Color.green;
        monkeySprite.color = Color.green;
        turtleToServerRpc(ownCliID, netObjID);
    }

    public void monkeyClicked(ulong ownCliID, ulong netObjID)
    {
        lionSprite.color = Color.blue;
        turtleSprite.color = Color.blue;
        monkeySprite.color = Color.blue;
        monkeyToServerRpc(ownCliID, netObjID);
    }

    [ClientRpc]
    public void lionToClientRpc(ulong ownCliID, ulong netObjID)
    {
        lionSprite.color = Color.red;
        turtleSprite.color = Color.red;
        monkeySprite.color = Color.red;
    }

    [ServerRpc(RequireOwnership = false)]
    public void turtleToServerRpc(ulong ownCliID, ulong netObjID)
    {
        turtleToClientRpc(ownCliID, netObjID);
    }

    [ClientRpc]
    public void turtleToClientRpc(ulong ownCliID, ulong netObjID)
    {
        lionSprite.color = Color.green;
        turtleSprite.color = Color.green;
        monkeySprite.color = Color.green;
    }

    [ServerRpc(RequireOwnership = false)]
    public void monkeyToServerRpc(ulong ownCliID, ulong netObjID)
    {
        monkeyToClientRpc(ownCliID, netObjID);
    }

    [ClientRpc]
    public void monkeyToClientRpc(ulong ownCliID, ulong netObjID)
    {
        lionSprite.color = Color.blue;
        turtleSprite.color = Color.blue;
        monkeySprite.color = Color.blue;
    }

    [ServerRpc(RequireOwnership = false)]
    public void syncNetVarServerRpc()
    {
        numberOfClients.text = NetworkManagerUI.numClients.Value.ToString();
        Debug.Log("Sync Net getting calledd");
    }

    [ClientRpc]
    public void syncNetVarClientRpc()
    {
        numberOfClients.text = NetworkManagerUI.numClients.Value.ToString();
        Debug.Log("Sync Net getting calledd");
    }


}
