using UnityEngine;

public class UIInventoryManager : MonoBehaviour
{
    [SerializeField] 
    private GameObject _slotPrefab;

    private void Start()
    {
        if (InventorySystem.current != null)
        {
            InventorySystem.current.OnInventoryChanged += OnUpdateInventory;
        }
    }

    private void OnUpdateInventory()
    {
        foreach (Transform t in transform)
        {
            Destroy(t.gameObject);
        }

        DrawInventory();
    }

    private void DrawInventory()
    {
        foreach (InventoryItem item in InventorySystem.current.Inventory)
        {
            AddInventorySlot(item);
        }
    }

    private void AddInventorySlot(InventoryItem item)
    {
        GameObject obj = Instantiate(_slotPrefab);
        obj.transform.SetParent(transform, false);

        UIInventoryItemSlot slot = obj.GetComponent<UIInventoryItemSlot>();
        slot.Set(item);
    }

    private void OnDisable()
    {
        InventorySystem.current.OnInventoryChanged -= OnUpdateInventory;
    }
}
