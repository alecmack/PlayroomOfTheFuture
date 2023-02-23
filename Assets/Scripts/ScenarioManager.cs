using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScenarioManager : MonoBehaviour
{
    private AllStepsList steps;
    private int index;

    public TextAsset textJSON;
    public int stepNumber;
    public bool isNextReady;
    public StepInfo currentStep;

    [System.Serializable]
    public class StepInfo
    {
        public string type;
        public int deviceCount;
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

    void Start()
    {
        index = 0;
        stepNumber = 0;
        steps = JsonUtility.FromJson<AllStepsList>(textJSON.text);
        currentStep = steps.steps[stepNumber];
        isNextReady = true;
    }

    void Update()
    {
        if (stepNumber != index)
        {
            isNextReady = false;
            if (stepNumber < steps.steps.Length)
            {
                currentStep = steps.steps[stepNumber];
                index = stepNumber;
            }
            isNextReady = true;
        }
    }
}
