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
    [SerializeField] private Button serverBtn;
    [SerializeField] private Button hostBtn;
    [SerializeField] private Button clientBtn;
    public GameObject TurtleCamera;
    public GameObject LionCamera;
    public GameObject MonkeyCamera;

    private NetworkVariable<int> numClients = new NetworkVariable<int>();

    private const int initial = 1;



    [SerializeField] TextMeshProUGUI ipAddressText;
    [SerializeField] TMP_InputField ip;

    [SerializeField] string ipAddress;
    [SerializeField] UnityTransport transport;

    private void Awake()
    {

        
        serverBtn.onClick.AddListener(() =>
        {
            NetworkManager.Singleton.StartServer(); 
        });

        hostBtn.onClick.AddListener(() =>
        {
            NetworkManager.Singleton.StartHost();
            GetLocalIPAddress();
           
            TurtleCamera.SetActive(false);
            MonkeyCamera.SetActive(false);
            Debug.Log("Host started");
            numClients.Value = initial;
        });

        clientBtn.onClick.AddListener(() =>
        {
            ipAddress = ip.text;
            SetIpAddress();
            NetworkManager.Singleton.StartClient();
            if(numClients.Value == 1)
            {
                LionCamera.SetActive(false);
                MonkeyCamera.SetActive(false);
                numClients.Value++;
            }

            if(numClients.Value == 2)
            {
                LionCamera.SetActive(false);
                TurtleCamera.SetActive(false);
                numClients.Value++;
            }

        });
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
