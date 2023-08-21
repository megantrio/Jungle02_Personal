using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class PanelController : MonoBehaviour
{
    public GameObject parent;
    public UIManager UIManager { get; set; }

    void Start()
    {
        UIManager = UIManager.instance;
    }


    public void OpenPanel()
    {
        parent.SetActive(true);
        UIManager.UpdatePocketPanelUI();
    }

    public void ClosePanel()
    {
        parent.SetActive(false);
    }
}
