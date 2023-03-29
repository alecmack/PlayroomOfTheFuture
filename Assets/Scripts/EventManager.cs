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

    public GameObject lionText;
    public GameObject turtleText;
    public GameObject monkeyText;

    [SerializeField] TextMeshProUGUI numberOfClients;

    public NetworkManagerUI NetworkManagerUI;

    // Start is called before the first frame update
    void Start()
    {
        lionSprite = lionButton.GetComponent<SpriteRenderer>();
        turtleSprite = turtleButton.GetComponent<SpriteRenderer>();
        monkeySprite = monkeyButton.GetComponent<SpriteRenderer>();

        lionSprite.color = new Color(1f, 1f, 1f, 0.5f);
        turtleSprite.color = new Color(1f, 1f, 1f, 0.5f);
        monkeySprite.color = new Color(1f, 1f, 1f, 0.5f);

        index = 0;
        stepNumber = 0;
        steps = JsonUtility.FromJson<AllStepsList>(textJSON.text);
        currentStep = steps.steps[stepNumber];

        lionText.GetComponent<TextMeshPro>().text = currentStep.devices[0].initialText;
        turtleText.GetComponent<TextMeshPro>().text = currentStep.devices[1].initialText;
        monkeyText.GetComponent<TextMeshPro>().text = currentStep.devices[2].initialText;
    }

    // Update is called once per frame
    void Update()
    {
        numberOfClients.text = NetworkManagerUI.getNumClients().ToString();
    }

    // Scenario Manager
    private AllStepsList steps;
    private int index;

    public TextAsset textJSON;
    public int stepNumber;
    public StepInfo currentStep;

    [System.Serializable]
    public class StepInfo
    {
        public string type;
        public int deviceCount;
        public int correctInput;
        public DeviceInfo[] devices;
    }

    [System.Serializable]
    public class DeviceInfo
    {
        public string asset;
        public string initialText;
        public string incorrectText;
    }

    [System.Serializable]
    public class AllStepsList
    {
        public StepInfo[] steps;
    }

    void getNextStep()
    {
        stepNumber++;
        currentStep = steps.steps[stepNumber];
        lionText.GetComponent<TextMeshPro>().text = currentStep.devices[0].initialText;
        turtleText.GetComponent<TextMeshPro>().text = currentStep.devices[1].initialText;
        monkeyText.GetComponent<TextMeshPro>().text = currentStep.devices[2].initialText;
    }

    // Starting
    private bool lionStart = false;
    private bool turtleStart = false;
    private bool monkeyStart = false;

    public void lionClicked(ulong ownCliID, ulong netObjID)
    {
        lionToClientRpc(ownCliID, netObjID);
    }

    public void turtleClicked(ulong ownCliID, ulong netObjID)
    {
        turtleToServerRpc(ownCliID, netObjID);
    }

    public void monkeyClicked(ulong ownCliID, ulong netObjID)
    {
        monkeyToServerRpc(ownCliID, netObjID);
    }

    [ClientRpc]
    public void lionToClientRpc(ulong ownCliID, ulong netObjID)
    {
        if (currentStep.type == "Initialization")
        {
            lionStart = true;
            lionSprite.color = new Color(1f, 1f, 1f, 1f);
            if (lionStart && turtleStart && monkeyStart)
            {
                getNextStep();
            }
        }
        else if (currentStep.type == "Cutscene")
        {
            getNextStep();
        }
        else if (currentStep.type == "Activity")
        {
            if (currentStep.correctInput == 0)
            {
                getNextStep();
            }
            else
            {
                lionText.GetComponent<TextMeshPro>().text = currentStep.devices[0].incorrectText;
            }
        }
    }

    [ServerRpc(RequireOwnership = false)]
    public void turtleToServerRpc(ulong ownCliID, ulong netObjID)
    {
        turtleToClientRpc(ownCliID, netObjID);
    }

    [ClientRpc]
    public void turtleToClientRpc(ulong ownCliID, ulong netObjID)
    {
        if (currentStep.type == "Initialization")
        {
            turtleStart = true;
            turtleSprite.color = new Color(1f, 1f, 1f, 1f);
            if (lionStart && turtleStart && monkeyStart)
            {
                getNextStep();
            }
        }
        else if (currentStep.type == "Cutscene")
        {
            getNextStep();
        }
        else if (currentStep.type == "Activity")
        {
            if (currentStep.correctInput == 1)
            {
                getNextStep();
            }
            else
            {
                turtleText.GetComponent<TextMeshPro>().text = currentStep.devices[1].incorrectText;
            }
        }
    }

    [ServerRpc(RequireOwnership = false)]
    public void monkeyToServerRpc(ulong ownCliID, ulong netObjID)
    {
        monkeyToClientRpc(ownCliID, netObjID);
    }

    [ClientRpc]
    public void monkeyToClientRpc(ulong ownCliID, ulong netObjID)
    {
        if (currentStep.type == "Initialization")
        {
            monkeyStart = true;
            monkeySprite.color = new Color(1f, 1f, 1f, 1f);
            if (lionStart && turtleStart && monkeyStart)
            {
                getNextStep();
            }
        }
        else if (currentStep.type == "Cutscene")
        {
            getNextStep();
        }
        else if (currentStep.type == "Activity")
        {
            if (currentStep.correctInput == 2)
            {
                getNextStep();
            }
            else
            {
                monkeyText.GetComponent<TextMeshPro>().text = currentStep.devices[2].incorrectText;
            }
        }
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
