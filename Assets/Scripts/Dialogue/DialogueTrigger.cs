using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    [Header("Visual Cue")]
    [SerializeField] private GameObject _visualCue;

    [Header("Dialogs JSON")]
    [SerializeField] private TextAsset _dialogsJSON;
    [SerializeField] private string _dialogueName;
    [SerializeField] private string _nextDialogueName;

    [Header("Dialogue Settings")]
    [SerializeField] private bool _isAutomaticActivation = false;
    [SerializeField] private bool _destroyAfterUsing = false;

    private bool _wasUsed;
    private bool _wasChanged;
    private bool _playerInRange;
    private int _playerCounter;

    private void Awake()
    {
        HideVisualCueAndSetPlayerInRange();
        _playerCounter = 0;
        _wasUsed = false;
        _wasChanged = false;
    }

    private void Update()
    {
        Trigger();
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (CheckPlayerCollider(collider) && !_isAutomaticActivation)
        {
            ShowVisualCueAndSetPlayerInRange();
            _playerCounter++;
        }
        if (CheckPlayerCollider(collider) && _isAutomaticActivation)
        {
            DialogueManager.GetInstance().EnterDialogueMode(_dialogsJSON, _dialogueName);
            _playerCounter++;
            _wasUsed = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        if (CheckPlayerCollider(collider) && (_playerCounter - 1 == 0) && !_isAutomaticActivation)
        {
            DialogueManager.GetInstance().ExitDialogueMode();
            HideVisualCueAndSetPlayerInRange();
        }
        if (CheckPlayerCollider(collider) && _isAutomaticActivation)
        {
            DialogueManager.GetInstance().ExitDialogueMode();
        }
        _playerCounter--;
        if (_wasUsed && !_wasChanged)
        {
            SetDialogueName(_nextDialogueName);
        }
        if (_wasUsed && _destroyAfterUsing)
        {
            Destroy(this.gameObject);
        }
        _wasUsed = false;
    }

    private void Trigger()
    {
        if (_playerInRange && Input.GetKeyDown(KeyCode.K))
        {   
            DialogueManager.GetInstance().EnterDialogueMode(_dialogsJSON, _dialogueName);
            _wasUsed = true;
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

    private bool CheckPlayerCollider(Collider2D collider)
    {
        return collider.gameObject.CompareTag("Player One") || collider.gameObject.CompareTag("Player Two");
    }

    public void SetDialogueName(string newDialogueName)
    {
        _wasChanged = true;
        _dialogueName = newDialogueName;
    }

    public string GetDialogueName()
    {
        return _dialogueName;
    }

    public bool GetDialogueIsUsed()
    {
        return _wasUsed;
    }

}
