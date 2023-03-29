using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using TMPro;
using System.Net;
using System.Net.Sockets;

using Unity.Netcode.Transports.UTP;



public class OldEventManager : NetworkBehaviour
{
    public  GameObject leftButton;
    public  GameObject rightButton;

    SpriteRenderer leftButtonSprite;
    SpriteRenderer rightButtonSprite;

    [SerializeField] TextMeshProUGUI numberOfClients;

    public NetworkManagerUI NetworkManagerUI;

    // Start is called before the first frame update
    void Start()
    {

        leftButtonSprite = leftButton.GetComponent<SpriteRenderer>();
        rightButtonSprite = rightButton.GetComponent<SpriteRenderer>();
      //  leftButtonSprite.color = Color.red;
       // rightButtonSprite.color = Color.green;

    }

    // Update is called once per frame
    void Update()
    {
        numberOfClients.text = NetworkManagerUI.getNumClients().ToString();
       // Debug.Log(NetworkManagerUI.getNumClients().ToString());
    }



    public void leftButtonClicked(ulong ownCliID, ulong netObjID)
    {
        leftButtonSprite.color = Color.red;
        rightButtonSprite.color = Color.green;
        changeOtherColorServerRpc(ownCliID, netObjID);


        Debug.Log("ownerClientID: " + ownCliID + "\t networkObjectID: " + netObjID);


    }


    public void rightButtonClicked(ulong ownCliID, ulong netObjID)
    {
        leftButtonSprite.color = Color.green;
        rightButtonSprite.color = Color.red;
        changeOtherColorClientRpc(ownCliID, netObjID);

        Debug.Log("ownerClientID: " + ownCliID + "\t networkObjectID: " + netObjID);
    }

    [ServerRpc(RequireOwnership = false)]
    public void changeOtherColorServerRpc(ulong ownCliID, ulong netObjID)  // Is a server RPC because it is being sent from the client to the server/host
    {
        Debug.Log("Are we reaching server from client?????");
        Debug.Log("ownerClientID: " + ownCliID + "\t networkObjectID: " + netObjID);

        //otherButton = iteractWith.GetComponent<SpriteRenderer>();
        //leftButtonSprite = leftButton.GetComponent<SpriteRenderer>();
        leftButtonSprite.color = Color.red;

        rightButtonSprite.color = Color.green;

    }

    [ClientRpc]
    public void changeOtherColorClientRpc(ulong ownCliID, ulong netObjID)     // The host is both a client and a server. If a host invokes a client RPC, it triggers the on all clients, including the host
    {
        Debug.Log("Are we reaching server? this is the host");
        Debug.Log("ownerClientID: " + ownCliID + "\t networkObjectID: " + netObjID);
        //otherButton = leftButton.GetComponent<SpriteRenderer>();
        //leftButtonSprite = leftButton.GetComponent<SpriteRenderer>();
        leftButtonSprite.color = Color.green;

        rightButtonSprite.color = Color.red;
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
