using UnityEngine;

public class PanelController : MonoBehaviour
{
    public GameObject parent;

    public void OpenPanel()
    {
        parent.SetActive(true);
    }

    public void ClosePanel()
    {
        parent.SetActive(false);
    }
}
