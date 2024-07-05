using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ObjectPoolManager))]
public class Bow : MonoBehaviour
{
    [SerializeField] GameObject _arrowPrefab;
    [SerializeField] Transform _bowPosition;

    private ObjectPoolManager _pool;

    private void Start()
    {
        _pool = GetComponent<ObjectPoolManager>();
    }

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.H))
        {
            Shot();
        }
    }

    private void Shot()
    {
        _pool.GetFreeElement(_bowPosition.position, _bowPosition.rotation);
        //Instantiate(_arrowPrefab, _bowPosition.position, _bowPosition.rotation);
    }

}
