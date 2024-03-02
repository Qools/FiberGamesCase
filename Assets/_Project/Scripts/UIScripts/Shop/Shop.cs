using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Shop : MonoBehaviour
{
    public List<TurretBlueprint> turretBlueprint = new List<TurretBlueprint>();
    [SerializeField] private List<TextMeshProUGUI> priceTexts = new List<TextMeshProUGUI>();
    [SerializeField] private List<Image> turretIcons = new List<Image>();
    [SerializeField] private CanvasGroup shopCanvasGroup;
    [SerializeField] private CanvasGroup infoPanelCanvasGroup;

    private void Start()
    {
        SetPriceTexts();

        SetTurretIcons();
    }

    private void OnEnable()
    {
        BusSystem.OnGameOver += HideShop;
    }

    private void OnDisable()
    {
        BusSystem.OnGameOver -= HideShop;
    }

    private void SetTurretIcons()
    {
        for (int i = 0; i < turretIcons.Count; i++)
        {
            turretIcons[i].sprite = turretBlueprint[i].turretIcon;
        }
    }

    private void SetPriceTexts()
    {
        for (int i = 0; i < priceTexts.Count; i++)
        {
            priceTexts[i].text = "$" + turretBlueprint[i].price.ToString();
        }
    }

    public void PurchaseTurret(int index)
    {
        BuildManager.Instance.SetTurretToBuild(turretBlueprint[index]);
    }

    public void HideShop(GameResult gameResult)
    {
        shopCanvasGroup.alpha = 0f;
        shopCanvasGroup.interactable = false;
        shopCanvasGroup.blocksRaycasts = false;

        infoPanelCanvasGroup.alpha = 0f;
        infoPanelCanvasGroup.interactable = false;
        infoPanelCanvasGroup.blocksRaycasts = false;
    }
}
