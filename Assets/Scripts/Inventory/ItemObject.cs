using UnityEngine;

public class ItemObject : MonoBehaviour
{
    public InventoryItemData _referenceItem;
    [SerializeField] private LayerMask _mask;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (((1 << _mask) & other.gameObject.layer) !=0)
        {
            InventorySystem.current.Add(_referenceItem);
            Destroy(this.gameObject);
        }
    }
}
