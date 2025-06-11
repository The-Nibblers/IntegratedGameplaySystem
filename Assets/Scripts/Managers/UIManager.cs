using System;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager
{
  private Dictionary<string, TextMeshProUGUI > _uiObjects;

  private GameObject _deathUI;
  private Button _restartButton; 
  private Button _quitButton; 
  public UIManager(Dictionary<string, TextMeshProUGUI > uiObjects, GameObject deathUI, Button restartButton, Button quitButton)
  {
    _uiObjects = uiObjects;
    _deathUI = deathUI;
    _restartButton = restartButton;
    _quitButton = quitButton;
    
    _restartButton.onClick.AddListener(() => SceneManager.LoadScene(SceneManager.GetActiveScene().name));
    _quitButton.onClick.AddListener(() => Application.Quit());
  }

  public void UpdateUi(string Item, float value)
  {
    if (Enum.TryParse<UIItems.UIItemType>(Item, true, out var uiItem))
    {
      string key = uiItem.ToString();

      switch (uiItem)
      {
        case UIItems.UIItemType.DamageUI:
          if (_uiObjects.TryGetValue(key, out TextMeshProUGUI uiObjectDamage))
          {
            uiObjectDamage.text = "Damage: " + value;
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

  public void EnableDeathUI()
  {
    _deathUI.gameObject.SetActive(true);

  }
}
