using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class ItemDropper
{
    private List<GameObject> _spawnedItems = new List<GameObject>();

    private List<GameObject> _ItemPrefabs;
    private Player _playerScript;

    public ItemDropper(List<GameObject> ItemPrefabs, Player playerScript)
    {
        _ItemPrefabs = ItemPrefabs;   
        _playerScript = playerScript;
    }
    
    public void ItemUpdate()
    {
        CheckItemHitboxes();
    }

    public void SpawnItem(Vector3 position)
    {
        if (_ItemPrefabs == null || _ItemPrefabs.Count == 0)
        {
            Debug.LogWarning("No item prefabs to spawn!");
            return;
        }

        int randomIndex = Random.Range(0, _ItemPrefabs.Count);
        GameObject item = UnityEngine.Object.Instantiate(_ItemPrefabs[randomIndex], position, Quaternion.identity);
        _spawnedItems.Add(item);
    }

    private void CheckItemHitboxes()
    {
        for (int i = _spawnedItems.Count - 1; i >= 0; i--)
        {
            GameObject item = _spawnedItems[i];
            if (item == null)
            {
                _spawnedItems.RemoveAt(i);
                continue;
            }

            Collider[] hits = Physics.OverlapSphere(item.transform.position, 0.5f);
            foreach (var hit in hits)
            {
                if (hit.CompareTag("Player"))
                {
                    ApplyItemEffect(item.tag);
                    UnityEngine.Object.Destroy(item);
                    _spawnedItems.RemoveAt(i);
                    break;
                }
            }
        }
    }

    private void ApplyItemEffect(string tag)
    {
        if (Enum.TryParse<Items.ItemTypes>(tag, true, out var itemType))
        {
            switch (itemType)
            {
                case Items.ItemTypes.Shoe:
                    _playerScript.ChangeSpeed(1f);
                    break;
                case Items.ItemTypes.Bullet:
                    _playerScript.ChangeDamage(5f);
                    break;
                case Items.ItemTypes.Magazine:
                    _playerScript.ChangeFireRate(0.5f);
                    break;
                case Items.ItemTypes.Syringe:
                    _playerScript.ChangeMaxHealth(20f);
                    break;
                default:
                    break;
            }
        }
    }
}
