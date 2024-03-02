using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Node : MonoBehaviour
{
    private Renderer nodeRenderer;

    [SerializeField] private Vector3 positionOffset = new Vector3(0f, 0.5f, 0f);
    [SerializeField] private Color highLightColor;
    [SerializeField] private Color notEnoughMoneyColor;
    private Color startColor;

    public GameObject turret;
    public TurretBlueprint turretBlueprint;
    [HideInInspector] public bool isUpgraded = false;
    [SerializeField] private bool _isOccupied = false;

    // Start is called before the first frame update
    void Start()
    {
        nodeRenderer = GetComponent<Renderer>();
        startColor = nodeRenderer.material.color;

        _isOccupied = false;
    }


    private void OnMouseEnter()
    {

        if (EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }

        if (!BuildManager.Instance.CanBuild)
        {
            return;
        }

        if (BuildManager.Instance.HasMoney)
        {
            nodeRenderer.material.color = highLightColor;
        }
        else
        {
            nodeRenderer.material.color = notEnoughMoneyColor;
        }
        
    }

    private void OnMouseExit()
    {
        nodeRenderer.material.color = startColor;
    }

    private void OnMouseDown()
    {
        

        if (EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }

        if (turret != null)
        {
            BuildManager.Instance.SelectNode(this);
            return;
        }

        if (!BuildManager.Instance.CanBuild)
        {
            return;
        }

        BuildTurret(BuildManager.Instance.GetTurretToBuild());
    }

    private void BuildTurret(TurretBlueprint _turretBlueprint)
    {
        if (_isOccupied)
        {
            return;
        }

        if (PlayerStats.Money < _turretBlueprint.price)
        {
            return;
        }

        PlayerStats.Money -= _turretBlueprint.price;

        GameObject newTurret = Instantiate(_turretBlueprint.turretPrefab, GetOffsetPosition(), Quaternion.identity, this.transform.parent.parent);
        turret = newTurret;

        GameObject buildEffect = Instantiate(_turretBlueprint.buildEffect, transform.position, Quaternion.identity, this.transform.parent.parent);
        Destroy(buildEffect, 2f);

        turretBlueprint = _turretBlueprint;

        _isOccupied = true;
    }

    public void UpgradeTurret()
    {
        if (PlayerStats.Money < turretBlueprint.upgradePrice)
        {
            return;
        }

        PlayerStats.Money -= turretBlueprint.upgradePrice;

        Destroy(turret);

        GameObject newTurret = Instantiate(turretBlueprint.upgradeTurretPrefab, GetOffsetPosition(), Quaternion.identity, this.transform.parent.parent);
        turret = newTurret;

        Vector3 offset = Vector3.up * 1.5f;
        GameObject upgradeEffect = Instantiate(turretBlueprint.upgradeEffect, transform.position + offset, Quaternion.identity, this.transform.parent.parent);
        Destroy(upgradeEffect, 1f);

        isUpgraded = true;
    }

    public void SellTurret()
    {
        PlayerStats.Money += turretBlueprint.GetSellAmount();

        GameObject sellEffect = Instantiate(turretBlueprint.sellEffect, transform.position, Quaternion.identity, this.transform.parent.parent);
        Destroy(sellEffect, 2f);

        Destroy(turret);
        turretBlueprint = null;

        _isOccupied = false;
    }

    public Vector3 GetOffsetPosition()
    {
        return transform.position + positionOffset;
    }
}
