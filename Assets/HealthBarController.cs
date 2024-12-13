using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class HealthBarController : MonoBehaviour
{
    public VisualElement InterfaceRoot;
    
    void OnEnable()
    {
        var uiDocument = GetComponent<UIDocument>();
        InterfaceRoot = uiDocument.rootVisualElement;
    }
}
