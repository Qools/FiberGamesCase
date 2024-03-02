using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class NodeUI : MonoBehaviour
{
    [SerializeField] private GameObject ui;

    [SerializeField] private Button upgradeButton;

    [SerializeField] private TextMeshProUGUI upgradeButtonText;
    [SerializeField] private TextMeshProUGUI sellButtonText;

    private Node target;

    public void SetTarget(Node _target)
    {
        target = _target;

        transform.position = target.GetOffsetPosition();

        EnableUpradeButton(target.isUpgraded);
        
        SetUpgradeButtonText();
        SetSellButtonText();

        EnableUI();
    }

    public void UpgradeTurretButton()
    {
        target.UpgradeTurret();

        BuildManager.Instance.DeselectNode();
    }

    public void SellTurretButton()
    {
        target.SellTurret();

        BuildManager.Instance.DeselectNode();
    }

    public void HideUI()
    {
        ui.SetActive(false);
    }

    public void EnableUI()
    {
        ui.SetActive(true);
    }

    private void SetUpgradeButtonText()
    {
        if (!target.isUpgraded)
        {
            upgradeButtonText.text = "<b>UPGRADE</b>" + "<br>" + "$" + target.turretBlueprint.upgradePrice.ToString();
        }

        else
        {
            upgradeButtonText.text = "<b>NO UPGRADE</b>";
        }

    }

    private void SetSellButtonText()
    {
        sellButtonText.text = "<b>SELL</b>" + "<br>" + "$" + target.turretBlueprint.GetSellAmount().ToString();
    }

    private void EnableUpradeButton(bool enable)
    {
        upgradeButton.interactable = !enable;
    }
}
