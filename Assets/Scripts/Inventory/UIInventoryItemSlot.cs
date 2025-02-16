using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIInventoryItemSlot : MonoBehaviour
{
    [SerializeField]
    private Image _icon;

    [SerializeField]
    private TextMeshProUGUI _label;

    [SerializeField]
    private TextMeshProUGUI _stackLabel;

    [SerializeField]
    private GameObject _stackObj;

    public void Set(InventoryItem item)
    {
        _icon.sprite = item.Data.Icon;
        _label.text = item.Data.DisplayName;
        if (item.StackSize <= 1)
        {
            _stackObj.SetActive(false);
            return;
        }

        _stackLabel.text = item.StackSize.ToString();
    }
}
