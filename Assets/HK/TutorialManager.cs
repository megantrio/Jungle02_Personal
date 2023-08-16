using UnityEngine;
using UnityEngine.UI;

public class TutorialManager : MonoBehaviour
{
    public GameObject playerPrefab;
    public Vector2 fixedPlayerPosition;
    public GameObject[] tutorialObjects;
    public string[] soundEvent;
    public Button tutorialButton;
    private int currentStep = 0;

    private int[] sensorStepMapping = { 20, 21, 22};


    private void Start()
    {
        ShowStep(currentStep);
    }

    public void NextStep()
    {
        HideStep(currentStep);
        currentStep++;
        ShowStep(currentStep);
        Debug.Log("NextStep - stepIndex: " + currentStep);
    }

    private void ShowStep(int stepIndex)
    {
        if (stepIndex >= 0 && stepIndex < tutorialObjects.Length)
        {
            for (int i = 0; i < tutorialObjects.Length; i++)
            {
                tutorialObjects[i].SetActive(i == stepIndex);
            }

            if ((stepIndex >= 1 && stepIndex <= 8))
            {
                tutorialObjects[1].SetActive(true); // Step 1 Ȱ��ȭ
                Debug.Log("Previous Step - stepIndex: 1");
            }

            if ((stepIndex >= 12 && stepIndex <= 15))
            {
                tutorialObjects[12].SetActive(true);
                Debug.Log("Previous Step - stepIndex: 1");
            }

            if (stepIndex == 17)
            {
                // Ʃ�丮�� ���� �÷��̾� ��ġ�� ����
                GameObject playerInstance = Instantiate(playerPrefab, fixedPlayerPosition, Quaternion.identity);
                
                // �� ���� ī�޶�
                Camera.main.orthographicSize /= 4.0f;
            }

            if ((stepIndex >= 17 && stepIndex <= 23))
            {
                tutorialObjects[17].SetActive(true); // Step 1 Ȱ��ȭ
                Debug.Log("Previous Step - stepIndex: 1");
            }

            if(stepIndex == 18)
            {
                tutorialButton.gameObject.SetActive(false);
            }

            else if ((stepIndex >= 9 && stepIndex <= 11))
            {
                tutorialObjects[9].SetActive(true); 
            }
        }

    }


    private void HideStep(int stepIndex)
    {
        if (stepIndex != 1 && stepIndex != 8)
        {
            tutorialObjects[stepIndex].SetActive(false);
            Debug.Log("HideStep - stepIndex: " + currentStep);
        }

        if (stepIndex != 12 && stepIndex != 15)
        {
            tutorialObjects[stepIndex].SetActive(false);
            Debug.Log("HideStep - stepIndex: " + currentStep);
        }

        if (stepIndex == 8)
        {
            tutorialObjects[1].SetActive(false);
            tutorialObjects[8].SetActive(false);
            Debug.Log("HideStep - stepIndex: " + currentStep);
        }

        else if (stepIndex == 16)
        {
            tutorialObjects[12].SetActive(false);
            tutorialObjects[15].SetActive(false);
            Debug.Log("HideStep - stepIndex: " + currentStep);
        }
    }

    public void GoToSensorStep(int sensorID)
    {
        if (sensorID >= 0 && sensorID < sensorStepMapping.Length)
        {
            int targetStep = sensorStepMapping[sensorID];
            HideStep(currentStep);
            ShowStep(targetStep);
        }
    }

}

