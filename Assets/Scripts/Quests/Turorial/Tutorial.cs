using System.Collections;
using TMPro;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
    [SerializeField] TextMeshPro _tutorialText;
    [SerializeField] float _reductionValue = 0.1f;


    private void Start()
    {
        gameObject.SetActive(true);
        _tutorialText.alpha = 0;
    }


    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player One") || collision.gameObject.CompareTag("Player Two"))
        {
            if (!DialogueManager.GetInstance().GetDialogueIsPlaying() && _tutorialText.alpha == 0)
            {
                StartCoroutine(ShowText());
            }
            if (DialogueManager.GetInstance().GetDialogueIsPlaying() && _tutorialText.alpha != 0)
            {
                _tutorialText.alpha = 0;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player One") || collision.gameObject.CompareTag("Player Two"))
        {
            StartCoroutine(HideText());
        }
    }

    IEnumerator ShowText()
    {
        while (_tutorialText.alpha < 1)
        {
            _tutorialText.alpha += _reductionValue;

            yield return null;
        }
    }

    IEnumerator HideText()
    {
        while (_tutorialText.alpha > 0)
        {
            _tutorialText.alpha -= _reductionValue;

            yield return null;
        }
        yield return new WaitForSeconds(0.5f);
        Destroy(gameObject);
    }
}
