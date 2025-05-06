using UnityEngine;

public class Key : MonoBehaviour
{
    [SerializeField] private DialogueTrigger _dialogueTrigger;
    [SerializeField] private string _newDialogue;

    private void OnDestroy()
    {
        _dialogueTrigger.SetDialogueName(_newDialogue);
        Debug.Log(_newDialogue);
    }
}
