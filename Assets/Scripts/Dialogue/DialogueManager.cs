using UnityEngine;
using TMPro;
using System.Linq;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{

    [Header("Dealogue UI")]
    [SerializeField] private GameObject _dialoguePanel;
    [SerializeField] private Image _dialogueIcon;
    [SerializeField] private TextMeshProUGUI _dialogueTitle;
    [SerializeField] private TextMeshProUGUI _dialogueText;
    [SerializeField] private GameObject _dialogueContinueIcon;

    [Space, Header("Character Icon")]
    [SerializeField] private CharactersIcons _charactersIcons;
    [Space, Header("Timer before dialogue")]
    [SerializeField] private float _timeBeforeNextDialogue = 1.0f;
    [Space, Header("Block alements then start dialogue")]
    [SerializeField] private MonoBehaviour[] _blockAlementsThenDialogue;


    private static DialogueManager _instance;
    private bool _dialogueIsPlaying;
    private DialoguesData _dialoguesData;
    private int _dialogueListIndex;
    Dialogue _currentDialogue;
    private float _timer = 0f;

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }
        else if (_instance != this)
        {
            Debug.LogWarning("Found more than one Dialogue Manager in the scene");
            Destroy(this);
        }   
    }

    public static DialogueManager GetInstance()
    {
        return _instance;
    }

    private void Start()
    {
        Close();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.K) && _dialogueIsPlaying && _timer <= 0)
        {
            Next();
        }
        else
        {
            Timer();
        }
    }
    public void EnterDialogueMode(TextAsset dialogueJSON, string dialogueName)
    {
        _dialoguesData = JsonUtility.FromJson<DialoguesData>(dialogueJSON.text);

        bool containsItem = _dialoguesData.dialogues.Any(item => item.dialogue_name == dialogueName);
        if (containsItem)
        {
            _timer = _timeBeforeNextDialogue;
            _dialogueListIndex = 0;
            _currentDialogue = _dialoguesData.dialogues.Where(item => item.dialogue_name == dialogueName).FirstOrDefault();
            BlockAlements();
            Show();
        }
        else
        {
            Debug.LogError($"Dialogue '{dialogueName}' is not found.");
        }
    }

    private void Show()
    {
        
        _dialogueIsPlaying = true;
        _dialoguePanel.SetActive(true);
        _dialogueContinueIcon.SetActive(true);
        ChangeText();
    }

    private void Next()
    {
        if (_dialogueIsPlaying && (_dialogueListIndex != _currentDialogue.entries.Count))
        {
            ChangeText();
        }
        else
        {
            UnBlockAlements();
            Close();
        }
        if (_dialogueIsPlaying && (_dialogueListIndex == _currentDialogue.entries.Count))
        {
            _dialogueContinueIcon.SetActive(false);
        }
    }

    private void ChangeText()
    {
        _dialogueTitle.text = _currentDialogue.entries[_dialogueListIndex].character[0];
        _dialogueText.text = _currentDialogue.entries[_dialogueListIndex].text;
        Debug.Log(_currentDialogue.entries[_dialogueListIndex].character[0] +
            _currentDialogue.entries[_dialogueListIndex].character[1]);
        Debug.Log(_dialogueListIndex);
        _dialogueIcon.sprite = _charactersIcons.GetCharacterIcon(_currentDialogue.entries[_dialogueListIndex].character[0]+
            _currentDialogue.entries[_dialogueListIndex].character[1]);
        _dialogueListIndex++;
    }

    public void ExitDialogueMode()
    {
        Close();
    }

    private void Close()
    {
        _dialogueIsPlaying = false;
        _dialoguePanel.SetActive(false);
    }    

    private void Timer()
    {
        _timer -= Time.deltaTime;
    }

    private void BlockAlements()
    {
        if (_blockAlementsThenDialogue != null)
        {
            foreach (var item in _blockAlementsThenDialogue)
            {
                item.enabled = false;
            }
        }
    }
    private void UnBlockAlements()
    {
        if (_blockAlementsThenDialogue != null)
        {
            foreach (var item in _blockAlementsThenDialogue)
            {
                item.enabled = true;
            }
        }
    }

    public bool GetDialogueIsPlaying()
    {
        return _dialogueIsPlaying;
    }
}
