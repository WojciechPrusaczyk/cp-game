using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.U2D;
using UnityEngine.UIElements;

public class MainMenuPanel : MonoBehaviour
{
    [SerializeField] public GameObject audioManagerObject;
    [SerializeField] private AudioManager audioManager;

    public VisualElement InterfaceRoot;
    
    private Button startButton;
    private Button settingsButton;
    private Button authorsButton;
    private Button backButton1;
    private Button backButton2;
    private VisualElement mainMenuScreenLeft;
    private VisualElement authorsScreenLeft;
    private VisualElement settingsScreenLeft;
    private VisualElement authorsScreenRight;
    private void OnEnable()
    {
        var uiDocument = GetComponent<UIDocument>();
        InterfaceRoot = uiDocument.rootVisualElement;

        
        startButton = InterfaceRoot.Q<Button>("StartBTN");
        settingsButton = InterfaceRoot.Q<Button>("SettingsBTN");
        authorsButton = InterfaceRoot.Q<Button>("AuthorsBTN");
        backButton1 = InterfaceRoot.Q<Button>("BackBTN1");
        backButton2 = InterfaceRoot.Q<Button>("BackBTN2");
        
        mainMenuScreenLeft = InterfaceRoot.Q<VisualElement>("MainMenuScreenLeft");
        settingsScreenLeft = InterfaceRoot.Q<VisualElement>("SettingsScreenLeft");
        authorsScreenLeft = InterfaceRoot.Q<VisualElement>("AuthorsScreenLeft");    
        authorsScreenRight = InterfaceRoot.Q<VisualElement>("AuthorsScreenRight");

        startButton.clicked += () => SceneManager.LoadScene(1);
        
        authorsButton.clicked += () =>
        {
            mainMenuScreenLeft.style.display = DisplayStyle.None;
            settingsScreenLeft.style.display = DisplayStyle.None;
            authorsScreenLeft.style.display = DisplayStyle.Flex;
            authorsScreenRight.style.display = DisplayStyle.Flex;
        };

        settingsButton.clicked += () =>
        {
            mainMenuScreenLeft.style.display = DisplayStyle.None;
            settingsScreenLeft.style.display = DisplayStyle.Flex;
            authorsScreenLeft.style.display = DisplayStyle.None;
            authorsScreenRight.style.display = DisplayStyle.None;
        };
        
        backButton2.clicked += () =>
        {
            mainMenuScreenLeft.style.display = DisplayStyle.Flex;
            settingsScreenLeft.style.display = DisplayStyle.None;
            authorsScreenLeft.style.display = DisplayStyle.None;
            authorsScreenRight.style.display = DisplayStyle.None;
        };
        backButton1.clicked += () =>
        {
            mainMenuScreenLeft.style.display = DisplayStyle.Flex;
            settingsScreenLeft.style.display = DisplayStyle.None;
            authorsScreenLeft.style.display = DisplayStyle.None;
            authorsScreenRight.style.display = DisplayStyle.None;
        };
        
    }

    
    private void Awake()
    {

        audioManager = audioManagerObject.GetComponent<AudioManager>();
        audioManager.SetMusicClip(0);
    }
}
