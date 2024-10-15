using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    [Header("Visual Cue")]
    [SerializeField] private GameObject _visualCue;

    [Header("Dialogs JSON")]
    [SerializeField] private TextAsset _dialogsJSON;
    [SerializeField] private string _dialogueName;
    
    private bool _playerInRange;

    private void Awake()
    {
        HideVisualCueAndSetPlayerInRange();
    }

    private void Update()
    {
        Trigger();
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.gameObject.CompareTag("Player"))
        {
            ShowVisualCueAndSetPlayerInRange();
        }
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Player"))
        {
            DialogueManager.GetInstance().ExitDialogueMode();
            HideVisualCueAndSetPlayerInRange();
        }
    }

    private void Trigger()
    {
        if (_playerInRange && Input.GetKeyDown(KeyCode.K))
        {   
            DialogueManager.GetInstance().EnterDialogueMode(_dialogsJSON, _dialogueName);
            HideVisualCueAndSetPlayerInRange();
        }
    }

    private void HideVisualCueAndSetPlayerInRange()
    {
        _playerInRange = false;
        _visualCue.SetActive(false);
    }

    private void ShowVisualCueAndSetPlayerInRange()
    {
        _playerInRange = true;
        _visualCue.SetActive(true);
    }

}
