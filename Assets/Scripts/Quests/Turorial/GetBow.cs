using TMPro;
using UnityEngine;

public class GetBow : MonoBehaviour
{
    [SerializeField] GameObject _witchDialogueTrigger;
    [SerializeField] GameObject _princeDialogueTrigger;
    [SerializeField] Bow _bowPrinceComponent;

    private void Start()
    {
        _witchDialogueTrigger.SetActive(false);
        _princeDialogueTrigger.SetActive(false);
        _bowPrinceComponent.enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player Two") && _witchDialogueTrigger!=null && !_bowPrinceComponent.isActiveAndEnabled)
        {
            _witchDialogueTrigger.SetActive(true);
        }
        if (collision.gameObject.CompareTag("Player One"))
        {
            _princeDialogueTrigger.SetActive(true);
            _bowPrinceComponent.enabled = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player One"))
        {
            Destroy(gameObject);
        }
    }
}
