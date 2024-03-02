using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildManager : MonoBehaviour
{
    private TurretBlueprint turretToBuild;
    private Node selectedNode;

    [SerializeField] private NodeUI nodeUI;

    public TurretBlueprint standardTurretPrefab;

    public static BuildManager Instance;

    // Start is called before the first frame update
    void Start()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }

        turretToBuild = standardTurretPrefab;
    }

    public bool CanBuild
    {
        get { return turretToBuild != null; }
    }

    public bool HasMoney
    {
        get { return PlayerStats.Money >= turretToBuild.price; }
    }

    public void SelectNode(Node node)
    {
        if (selectedNode == node)
        {
            DeselectNode();
            return;
        }

        selectedNode = node;
        turretToBuild = null;

        nodeUI.SetTarget(selectedNode);
    }

    public void DeselectNode()
    {
        selectedNode = null;
        nodeUI.HideUI();
    }

    public void SetTurretToBuild(TurretBlueprint _turret)
    {
        turretToBuild = _turret;

        DeselectNode();
    }

    public TurretBlueprint GetTurretToBuild()
    {
        return turretToBuild;
    }
}
