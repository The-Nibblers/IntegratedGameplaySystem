using System;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager
{
  private Dictionary<string, TextMeshProUGUI > _uiObjects;
  public UIManager(Dictionary<string, TextMeshProUGUI > uiObjects)
  {
    _uiObjects = uiObjects;
  }

  public void UpdateUi(string Item, int value)
  {
    if (Enum.TryParse<UIItems.UIItemType>(Item, true, out var uiItem))
    {
      switch (uiItem)
      {
        case UIItems.UIItemType.DamageUI:
          _uiObjects.TryGetValue(Item, out TextMeshProUGUI  uiObjectDamage);
          uiObjectDamage.text = "Damage: " + value;
          break;
        case UIItems.UIItemType.HealthUI:
          _uiObjects.TryGetValue(Item, out TextMeshProUGUI  uiObjectHealth);
          uiObjectHealth.text = "Health: " + value;
          break;
        case UIItems.UIItemType.SpeedUI:
          _uiObjects.TryGetValue(Item, out TextMeshProUGUI  uiObjectSpeed);
          uiObjectSpeed.text = "Speed: " + value;
          break;
        case UIItems.UIItemType.FireRateUI:
          _uiObjects.TryGetValue(Item, out TextMeshProUGUI  uiObjectFirerate);
          uiObjectFirerate.text = "FireRate: " + value;
          break;
      }
    }
  }
}
