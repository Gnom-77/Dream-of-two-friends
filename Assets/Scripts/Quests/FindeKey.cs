using UnityEngine;

public class FindeKey : MonoBehaviour
{
    [SerializeField] private InventoryItemData _item;
    [SerializeField] private string _endQuestDialogue;
    [SerializeField] private string _dialogueAfterQuest;
    [SerializeField] private DialogueTrigger _dialogueTrigger;
    [SerializeField] private GameObject _closedDoor;
    [SerializeField] private GameObject _openDoors;

    private void Awake()
    {
        _closedDoor.SetActive(true);
        _openDoors.SetActive(false);
    }

    private void Update()
    {
        if (_endQuestDialogue == _dialogueTrigger.GetDialogueName() && _dialogueTrigger.GetDialogueIsUsed())
        {
            _dialogueTrigger.SetDialogueName(_dialogueAfterQuest);
            InventorySystem.current.Remove(_item);
            _closedDoor.SetActive(false);
            _openDoors.SetActive(true);
            Destroy(this.gameObject);
        }
    }
}
