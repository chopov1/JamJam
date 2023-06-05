using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemDisplay : MonoBehaviour
{
    public Image icon;
    public TextMeshProUGUI titleText;
    public TextMeshProUGUI statisticsText;
    public TextMeshProUGUI buttonText;
    public MagicItem item;

    private void OnEnable()
    {
        SetItem(MagicItem.GenerateMagicItem());
    }

    public void SetItem(MagicItem _item)
    {
        item = _item;
        UpdateUI();
    }

    public void UpdateUI()
    {
        if (item == null) return;

        if (item.icon != null) icon.sprite = item.icon;

        titleText.text = item.itemName;


        statisticsText.text = 
$@"+{Mathf.Floor((item.stats.GetStat(item.buffedStat) - 1) * 100)}% {item.buffedStat}
-{Mathf.Floor((1 - item.stats.GetStat(item.debuffedStat)) * 100)}% {item.debuffedStat}";

        buttonText.text = $"Purchase ({item.price} souls)";
    }

}
