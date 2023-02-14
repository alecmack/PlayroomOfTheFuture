using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class disableEnable : NetworkBehaviour
{
    public GameObject objectBeingActivated;
    private bool deactivated = false;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (IsHost && !deactivated)
        {
            Debug.Log("setting camera 1 to false");

            objectBeingActivated.SetActive(false);
            deactivated = true;
        }

    }

    //public virtual void OnStartServer()
    //{
        
    //}

    //public virtual void OnNetworkSpawn()
    //{

    //}
}
