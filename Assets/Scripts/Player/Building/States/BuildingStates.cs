using UnityEngine;
using System;
using TMPro;
using Unity.VisualScripting;
using UnityEngine.UI;
using System.Collections.Generic;



public class BuildingIdle : State<PlayerInfo>
{
    public BuildingIdle(StateMachine<PlayerInfo> machine, PlayerInfo info, State<PlayerInfo> parent) : base(machine, info, parent)
    {
    }

    protected override void OnEnter()
    {
        _info.Input.BuildModePressed = false;
        _info.Input.EditorModePressed = false;
    }
    
    protected override void OnExit() { }

    protected override State<PlayerInfo> Transition()
    {
        if (_info.Input.BuildModePressed)
        {
            return Machine.GetStateFromType<Building>();
        }
        else if(_info.Input.EditorModePressed)
        {
            return Machine.GetStateFromType<Editor>();
        }
        else
        {
            return null;
        }
    }

    protected override State<PlayerInfo> GetInitialState() => null;

    protected override void OnUpdate(float deltaTime) { }
    protected override void OnFixedUpdate(float deltaTime) { }
}

public class Editor : State<PlayerInfo>
{
    public StructureParent structure;
    public TextMeshProUGUI textMesh;

    private float repairAmount = 1;
    
    private List<UpgradeButton> buttons = new List<UpgradeButton>();
    public Editor(StateMachine<PlayerInfo> machine, PlayerInfo info, State<PlayerInfo> parent) : base(machine, info, parent)
    {
        textMesh = GameObject.Find("Player/Main Camera/Canvas/ObjDescription").GetComponent<TextMeshProUGUI>();
        for (int i = 0; i < 7; i++)
        {
            GameObject button = GameObject.Instantiate(Resources.Load<GameObject>("UI/Button"), 
                _info.Player.transform.Find("Main Camera").transform);
            button.transform.localPosition = new Vector3(6f, 4f - 1.5f*i, 10f);
            buttons.Add(button.GetComponent<UpgradeButton>());
            buttons[i].gameObject.SetActive(false);
        }
        textMesh.gameObject.SetActive(false);
    }

    protected override void OnEnter()
    {
        _info.Input.BuildModePressed = false;
        _info.Input.EditorModePressed = false;
        NullifyStructure();
        textMesh.gameObject.SetActive(true);

    }

    protected override void OnExit()
    {
        NullifyStructure();
        
        textMesh.gameObject.SetActive(false);
    }

    protected override State<PlayerInfo> Transition()
    {
        if (_info.Input.BuildModePressed)
        {
            return Machine.GetStateFromType<Building>();
        }
        else if (_info.Input.EditorModePressed)
        {
            return Machine.GetStateFromType<BuildingIdle>();
        }
        else
        {
            return null;
        }
    }
    
    protected override State<PlayerInfo> GetInitialState() => null;

    protected override void OnUpdate(float deltaTime) { }

    protected override void OnFixedUpdate(float deltaTime)
    {
        if (_info.Input.Interact)
        {
            GameObject go = _info.Input.GetObjectClicked();
            if (go != null)
            {
                Debug.Log(go.name);
                StructureParent s = go.GetComponent<StructureParent>();
                if(s != null) SetStructure(s);
                else
                {
                    UpgradeButton button = go.GetComponent<UpgradeButton>();
                    if(button != null)
                    {
                        button.Click(structure);
                        SetButtons(structure.upgrades);
                    }
                    else
                    {
                        NullifyStructure();
                    }
                }
            }
        }
        
        float maxDist = 5f;
        if (structure != null)
        {
            if (Vector2.Distance(_info.Input.Player.position, structure.gameObject.transform.position) > maxDist)
            {
                NullifyStructure();
            }
        }
    }

    private void NullifyStructure()
    {
        structure = null;
        textMesh.text = "Select structure to upgrade!";
        ClearButtons();
    }

    private void SetStructure(StructureParent s)
    {
        structure = s;
        SetButtons(s.upgrades);
        textMesh.text = structure.Description;
    }

    private void SetButtons(List<StructureUpgrade> upgrades)
    {
        for (int i = 0; i < upgrades.Count; i++)
        {
            buttons[i].gameObject.SetActive(true);
            buttons[i].OnClick = upgrades[i].UpgradeStructure;
            string nameText = upgrades[i].Name;
            if(upgrades[i].UpgradeLevel != 0) nameText += "(" + upgrades[i].UpgradeLevel.ToString() + ")";
            buttons[i].Name.text = nameText;
            string costText = "Cost: " + upgrades[i].Cost.ToString();
            buttons[i].Cost.text = costText;
            buttons[i].Description.text = upgrades[i].Description;
        }
    }

    private void ClearButtons()
    {
        foreach (UpgradeButton button in buttons)
        {
            button.OnClick = null;
            button.gameObject.SetActive(false);
        }
    }
}


public class Building : State<PlayerInfo>
{
    private GameObject displayObj;
    private SpriteRenderer displaySpriteRenderer;

    private float signIntensity = 1;
    private float numShifts = 7;
    private float signMaxTime = 0.4f;
    private float signTime;
    
    
    public Building(StateMachine<PlayerInfo> machine, PlayerInfo info, State<PlayerInfo> parent) : base(machine, info, parent)
    {
        displayObj = new GameObject("Display");
        displaySpriteRenderer = displayObj.AddComponent<SpriteRenderer>();
    }

    protected override void OnEnter()
    {
        displayObj.SetActive(true);
        _info.Input.BuildModePressed = false;
        _info.Input.EditorModePressed = false;
    }

    protected override void OnExit()
    {
        _info.StructureData.DisplayObj.SetActive(false);
        displayObj.SetActive(false);
    }

    protected override State<PlayerInfo> Transition()
    {
        if (_info.Input.BuildModePressed)
        {
            return Machine.GetStateFromType<BuildingIdle>();
        }
        else if (_info.Input.EditorModePressed)
        {
            return Machine.GetStateFromType<Editor>();
        }
        else
        {
            return null;
        }
    }
    
    protected override State<PlayerInfo> GetInitialState() => null;

    protected override void OnUpdate(float deltaTime) { }

    protected override void OnFixedUpdate(float deltaTime)
    {

        if (signTime > 0)
        {
            signTime -= deltaTime;
            if (signTime <= 0)
            {
                displaySpriteRenderer.color = Color.white;
            }
        }
        
        //_info.StructureData.DisplayObjRenderer.sprite = _info.StructureData.CurrentStructureObj.sprite;
        Vector2 placementPos = Vector2.zero;
        float maxPlacementDist = 2f;
        if (_info.Input.distToMouse > maxPlacementDist)
        {
            Vector2 pos = (_info.Input.mouseDir * maxPlacementDist) + new Vector2(_info.Input.Player.position.x, _info.Input.Player.position.y);
            placementPos = pos;
        }
        else
        {
            placementPos = _info.Input.mousePos;
        }
        placementPos.x = Mathf.RoundToInt(placementPos.x);
        placementPos.y = Mathf.RoundToInt(placementPos.y);
        
        if (_info.Input.BuildRotate)
        {
            _info.StructureData.Degrees -= 90f;
        }
        
        Quaternion rotation = Quaternion.Euler(0, 0, _info.StructureData.Degrees);
        Display(placementPos, rotation);

        if (_info.Input.Interact)
        {
            bool placed = StructureManager.Instance.Place(_info.StructureData.CurrentStructureObj, placementPos, rotation);
            if (placed)
            {
            }
            else
            {
                ErrorPlacing();
            }
        }
    }

    private void ErrorPlacing()
    {
        signTime = signMaxTime;
        displaySpriteRenderer.color = Color.red;
    }


    private void Display(Vector2 position, Quaternion rotation)
    {
        Vector3 displayPos = position;
        displayPos.z = -1;
        displayPos.x += ShakeAmount();
        displayObj.transform.position = displayPos;
        displayObj.transform.rotation = rotation;
        displaySpriteRenderer.sprite = _info.StructureData.CurrentStructureObj.sprite;
    }

    private float ShakeAmount()
    {
        if (signTime < 0)
        {
            return 0;
        }
        else
        {
            return Mathf.Sin(Mathf.PI * signTime * numShifts / signMaxTime) * signTime * signIntensity / signMaxTime;
        }
    }
}