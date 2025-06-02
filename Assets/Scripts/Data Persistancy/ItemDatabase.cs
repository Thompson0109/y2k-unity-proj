using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemDatabase", menuName = "Scriptable Objects/ItemDatabase")]
public class ItemDatabase : ScriptableObject
{
    [System.Serializable]
    public class ItemPrefabPair
    {
        public ItemSO item;
        public GameObject prefab;
    }

    public List<ItemPrefabPair> items;

    private Dictionary<ItemSO, GameObject> lookup;

    public GameObject GetPrefab(ItemSO item)
    {
        if (lookup == null)
        {
            lookup = new Dictionary<ItemSO, GameObject>();
            foreach (var pair in items)
            {
                if (pair.item != null && pair.prefab != null && !lookup.ContainsKey(pair.item))
                    lookup[pair.item] = pair.prefab;
            }
        }

        return lookup.TryGetValue(item, out var prefab) ? prefab : null;
    }
}
