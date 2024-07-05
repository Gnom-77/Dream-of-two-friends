using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{
    [SerializeField] private GameObject _frontShield;
    [SerializeField] private GameObject _upShield;

    private void Start()
    {
        _frontShield.SetActive(false);
        _upShield.SetActive(false);
    }
    void Update()
    {
        ActivateShield();
    }

    private void ActivateShield()
    {
        if (Input.GetKey(KeyCode.K) && !Input.GetKey(KeyCode.W))
        {
            _frontShield.SetActive(true);
            _upShield.SetActive(false);
        }
        if (Input.GetKey(KeyCode.K) && Input.GetKey(KeyCode.W))
        {
            _upShield.SetActive(true);
            _frontShield.SetActive(false);
        }
        if (!Input.GetKey(KeyCode.K))
        {
            _frontShield.SetActive(false);
            _upShield.SetActive(false);
        }

    }
}
