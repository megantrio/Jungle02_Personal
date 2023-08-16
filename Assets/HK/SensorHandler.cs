using UnityEngine;

public class SensorHandler : MonoBehaviour
{
    public int sensorID; // �� ���� ������Ʈ�� ������ ID �ο�

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) // �÷��̾�� �浹���� ���� ����
        {
            TriggerSensor();
        }
    }

    public void TriggerSensor()
    {
        TutorialManager tutorialManager = FindObjectOfType<TutorialManager>();
        if (tutorialManager != null)
        {
            tutorialManager.GoToSensorStep(sensorID);
        }
    }
}
