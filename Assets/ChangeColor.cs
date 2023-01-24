using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

// using this as connected host for testing purposes


public class ChangeColor : NetworkBehaviour
{
    //public GameObject cameraHolder;

    [SerializeField] public GameObject iteractWith;

    public NetworkVariable<Color> leftColor = new NetworkVariable<Color>(Color.green, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);



    SpriteRenderer button1Renderer;
    SpriteRenderer otherButton;


    public void Update()
    {

    }

    public void Start()
    {


        button1Renderer = GetComponent<SpriteRenderer>();
        button1Renderer.color = Color.red;

    }
    private void OnMouseDown()
    {
        Debug.Log("Button 2 clicked!");

 

        changeOtherColorClientRpc();
    }


    [ClientRpc]
    private void changeOtherColorClientRpc()
    {
        Debug.Log("Are we reaching server? this is host");
        otherButton = iteractWith.GetComponent<SpriteRenderer>();
        otherButton.color = Color.green;

        button1Renderer.color = Color.red;
    }



}
