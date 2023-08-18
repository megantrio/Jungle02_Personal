using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//0818 

public class CanvasManager : MonoBehaviour
{
    public static CanvasManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public Canvas GetCanvas()
    {
        return GetComponent<Canvas>();
    }
}