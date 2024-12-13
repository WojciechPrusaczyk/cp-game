using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class MainUserInterfaceBehaviour : MonoBehaviour
{
    [SerializeField] public GameObject audioManagerObject;
    private AudioManager audioManager;
    public int selectedLane = 0;
    
    public VisualElement InterfaceRoot;
    
    public UnitSpawner unitSpawner;
    
    private Button upgradesButton;
    private Button unitsButton;
    private Button linesButton;
    
    private VisualElement upgrades;
    private VisualElement units;

    private void Awake()
    {
        audioManager = audioManagerObject.GetComponent<AudioManager>();
        audioManager.SetMusicClip(0);
    }

    private void Update()
    {
        var lineButtons = InterfaceRoot.Query<Button>(className: "LineButton").ToList();
        foreach (var button in lineButtons)
        {
            button.clicked += () =>
            {
                int lineId = Int32.Parse(button.text);
                if (lineId == 0)
                {
                    selectedLane = 0;
                }
                else if (lineId == 1)
                {
                    selectedLane = 1;
                }
                else if (lineId == 2)
                {
                    selectedLane = 2;
                    
                }
                
            };
        }
    }

    // Update is called once per frame
    private void OnEnable()
    {
        var uiDocument = GetComponent<UIDocument>();
        InterfaceRoot = uiDocument.rootVisualElement;
        Debug.Log(InterfaceRoot);
        Debug.Log(gameObject.name);
        
        // Przyciski
        upgradesButton = InterfaceRoot.Q<Button>("UpgradesButton");
        unitsButton = InterfaceRoot.Q<Button>("UnitsButton");
        
        
        // Containery
        upgrades = InterfaceRoot.Q<VisualElement>("Upgrades");
        upgrades.style.visibility = Visibility.Hidden;
        
        units = InterfaceRoot.Q<VisualElement>("Units");
        //units.style.visibility = Visibility.Hidden;
        
        upgradesButton.clicked += () =>
        {
            if (upgrades.style.visibility == Visibility.Visible)
                upgrades.style.visibility = Visibility.Hidden;
            else
                upgrades.style.visibility = Visibility.Visible;
        };
        
        unitsButton.clicked += () =>
        {
            if (units.style.visibility == Visibility.Visible)
                units.style.visibility = Visibility.Hidden;
            else
                units.style.visibility = Visibility.Visible;
        };
        
        var lineButtons = InterfaceRoot.Query<Button>(className: "UpgradeButton").ToList();
        var upgradeButtons = InterfaceRoot.Query<Button>(className: "UpgradeButton").ToList();
        foreach (var button in upgradeButtons)
        {
            button.clicked += () =>
            {
                // TODO: osłużyć kliknięcie kazdego guzika
                Debug.Log("UpgradeButton: "+button.text);
            };
        }
        
        var unitButtons = InterfaceRoot.Query<Button>(className: "UnitButton").ToList();
        foreach (var button in unitButtons)
        {
            button.clicked += () =>
            {
                int unitId = Int32.Parse(button.text);
                unitSpawner.QueueUnit(unitId, selectedLane);
            };
        }
    }
}
