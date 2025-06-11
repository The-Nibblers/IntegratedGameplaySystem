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

  public void UpdateUi(string Item, float value)
  {
    Debug.Log("switch called");
    if (Enum.TryParse<UIItems.UIItemType>(Item, true, out var uiItem))
    {
      string key = uiItem.ToString(); // "DamageUI", "HealthUI", etc.

      switch (uiItem)
      {
        case UIItems.UIItemType.DamageUI:
          if (_uiObjects.TryGetValue(key, out TextMeshProUGUI uiObjectDamage))
          {
            uiObjectDamage.text = "Damage: " + value;
          }
          else
          {
            Debug.Log("Key not found in _uiObjects: " + key);
          }
          break;

        case UIItems.UIItemType.HealthUI:
          if (_uiObjects.TryGetValue(key, out TextMeshProUGUI uiObjectHealth))
          {
            uiObjectHealth.text = "Health: " + value;
          }
          break;

        case UIItems.UIItemType.SpeedUI:
          if (_uiObjects.TryGetValue(key, out TextMeshProUGUI uiObjectSpeed))
          {
            uiObjectSpeed.text = "Speed: " + value;
          }
          break;

        case UIItems.UIItemType.FireRateUI:
          if (_uiObjects.TryGetValue(key, out TextMeshProUGUI uiObjectFirerate))
          {
            uiObjectFirerate.text = "FireRate: " + value;
          }
          break;
      }
    }
  }
}
