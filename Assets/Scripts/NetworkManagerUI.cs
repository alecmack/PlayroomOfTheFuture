using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Unity.Netcode;
using System.Net;

using Unity.Netcode.Transports.UTP;
using TMPro;
using System.Net.Sockets;


public class NetworkManagerUI : NetworkBehaviour
{
    [SerializeField] private Button hostBtn;
    [SerializeField] private Button clientBtn;

    [SerializeField] private GameObject networkButtons;


    public GameObject TurtleCamera;
    public GameObject LionCamera;
    public GameObject MonkeyCamera;

    public GameObject MainCamera;

    public NetworkVariable<int> numClients = new NetworkVariable<int>();

    private const int initial = 1;

    public EventManager EventManager;



    [SerializeField] TextMeshProUGUI ipAddressText;
    [SerializeField] TMP_InputField ip;

    [SerializeField] string ipAddress;
    [SerializeField] UnityTransport transport;
    // [SerializeField] string numClientsString;
    [SerializeField] TextMeshProUGUI numClientsGUI;

    public int getNumClients()
    {
        return numClients.Value;
    }


    private void Awake()
    {
        MainCamera.SetActive(true);
        TurtleCamera.SetActive(false);
        LionCamera.SetActive(false);
        MonkeyCamera.SetActive(false);

        hostBtn.onClick.AddListener(() =>
        {
            NetworkManager.Singleton.StartHost();
            GetLocalIPAddress();

            LionCamera.SetActive(true);
            MainCamera.SetActive(false);

            networkButtons.SetActive(false);

            Debug.Log("Host started");
            numClients.Value = initial;
            Debug.Log("Num Clients: " + numClients.Value);
            numClientsGUI.text = numClients.Value.ToString();
        });

        clientBtn.onClick.AddListener(() =>
        {
            numClientsGUI.text = numClients.Value.ToString();
            ipAddress = ip.text;
            SetIpAddress();

            bool temp = false;
            temp = NetworkManager.Singleton.StartClient();

            // while (!temp) {}
            // changeCameras();
            
            Invoke("changeCameras", 1);  // THIS WORKED !!!
        });
    }

    public void changeCameras()
    {
        if (numClients.Value == 1)
        {
            TurtleCamera.SetActive(true);
            MainCamera.SetActive(false);
            
            updateNumClientsServerRpc();
            Debug.Log("Num Clients: " + numClients.Value);
            numClientsGUI.text = numClients.Value.ToString();
        }

        if (numClients.Value == 2)
        {
            MonkeyCamera.SetActive(true);
            MainCamera.SetActive(false);
            
            updateNumClientsServerRpc();
            Debug.Log("Num Clients: " + numClients.Value);
            numClientsGUI.text = numClients.Value.ToString();
        }

        networkButtons.SetActive(false);
    }

    [ServerRpc(RequireOwnership = false)]
    public void updateNumClientsServerRpc()
    {
        numClients.Value = numClients.Value + 1; 
    }

    public string GetLocalIPAddress()
    {
        var host = Dns.GetHostEntry(Dns.GetHostName());
        foreach (var ip in host.AddressList)
        {
            if (ip.AddressFamily == AddressFamily.InterNetwork)
            {
                ipAddressText.text = ip.ToString();
                ipAddress = ip.ToString();
                return ip.ToString();
            }
        }
        throw new System.Exception("No network adapters with an IPv4 address in the system!");
    }

    /* Sets the Ip Address of the Connection Data in Unity Transport
     to the Ip Address which was input in the Input Field */
    // ONLY FOR CLIENT SIDE
    public void SetIpAddress()
    {
        transport = NetworkManager.Singleton.GetComponent<UnityTransport>();
        transport.ConnectionData.Address = ipAddress;
    }

}
