using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class LaneSpritesController : MonoBehaviour
{
    public Sprite lane1;
    public Sprite lane2;
    public Sprite lane3;
    public Sprite laneLight1;
    public Sprite laneLight2;
    public Sprite laneLight3;
    private MainUserInterfaceBehaviour mainUserInterfaceBehaviour;
    public VisualElement InterfaceRoot;
    private Sprite laneSprite1;
    private Sprite laneSprite2;
    private Sprite laneSprite3;
    public SpriteRenderer laneGameObject1;
    public SpriteRenderer laneGameObject2;

    public SpriteRenderer laneGameObject3;
    // private List<Button> lineButtons;

    private void OnEnable()
    {
        var uiDocument = GameObject.Find("SidePanels").GetComponent<UIDocument>();
        InterfaceRoot = uiDocument.rootVisualElement;
        var lineButtons = InterfaceRoot.Query<Button>(className: "LineButton").ToList();

        foreach (var button in lineButtons)
        {
            button.clicked += () => { ChangeLane(); };
        }

        foreach (var button in lineButtons)
        {
            int laneIndex = lineButtons.IndexOf(button);
            button.clicked += () =>
            {
                mainUserInterfaceBehaviour.selectedLane = laneIndex;
                ChangeLane();
            };
        }
    }

    private void Awake()
    {
        mainUserInterfaceBehaviour = GameObject.Find("SidePanels").GetComponent<MainUserInterfaceBehaviour>();

        laneSprite1 = lane1;
        laneSprite2 = lane2;
        laneSprite3 = lane3;

        laneGameObject1 = gameObject.transform.Find("Lane1").transform.Find("LaneTexture1")
            .GetComponent<SpriteRenderer>();
        laneGameObject2 = gameObject.transform.Find("Lane2").transform.Find("LaneTexture2")
            .GetComponent<SpriteRenderer>();
        laneGameObject3 = gameObject.transform.Find("Lane3").transform.Find("LaneTexture3")
            .GetComponent<SpriteRenderer>();

        laneGameObject1.sprite = lane1;
        laneGameObject2.sprite = lane2;
        laneGameObject3.sprite = lane3;
    }

    private void Update()
    {
        laneGameObject1.sprite = laneSprite1;
        laneGameObject2.sprite = laneSprite2;
        laneGameObject3.sprite = laneSprite3;
    }

    private void ChangeLane()
    {
        Debug.Log($"Selected Lane: {mainUserInterfaceBehaviour.selectedLane}");
        if (mainUserInterfaceBehaviour.selectedLane == 0)
        {
            laneSprite1 = laneLight1;
            laneSprite2 = lane2;
            laneSprite3 = lane3;
        }
        else if (mainUserInterfaceBehaviour.selectedLane == 1)
        {
            laneSprite1 = lane1;
            laneSprite2 = laneLight2;
            laneSprite3 = lane3;
        }
        else if (mainUserInterfaceBehaviour.selectedLane == 2)
        {
            laneSprite1 = lane1;
            laneSprite2 = lane2;
            laneSprite3 = laneLight3;
        }
        else
        {
            laneSprite1 = lane1;
            laneSprite2 = lane2;
            laneSprite3 = lane3;
        }
    }
}