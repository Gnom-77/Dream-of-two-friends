using System.Collections.Generic;
using UnityEngine;

public class InventorySystem : MonoBehaviour
{
    private Dictionary<InventoryItemData, InventoryItem> _itemDictionary;
    public List<InventoryItem> Inventory { get; private set; }
    static public InventorySystem current;
    public delegate void InventoryChangedAction();
    public event InventoryChangedAction OnInventoryChanged;


    private void Awake()
    {
        Inventory = new List<InventoryItem>();
        _itemDictionary = new Dictionary<InventoryItemData, InventoryItem>(); 

        if(current == null )
        {
            current = this;
        }
        else if (current == this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }

    public void Add(InventoryItemData referenceData)
    {
        if(_itemDictionary.TryGetValue(referenceData, out InventoryItem value))
        {
            value.AddToStack();
        }
        else
        {
            InventoryItem newItem = new (referenceData);
            Inventory.Add(newItem);
            _itemDictionary.Add(referenceData, newItem);
        }
        OnInventoryChanged();
    }

    public void Remove(InventoryItemData referenceData)
    {
        if (_itemDictionary.TryGetValue(referenceData, out InventoryItem value))
        {
            value.RemoveFromStack();
            if (value.StackSize == 0)
            {
                Inventory.Remove(value);
                _itemDictionary.Remove(referenceData);
            }
            //OnInventoryChanged();
        }
    }

    public InventoryItem Get(InventoryItemData referenceData)
    {
        if (_itemDictionary.TryGetValue(referenceData, out InventoryItem value))
        {
            return value;
        }
        return null;
    }
}
