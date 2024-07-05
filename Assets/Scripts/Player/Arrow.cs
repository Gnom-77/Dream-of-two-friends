using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(ObjectPool))]
public class Arrow : MonoBehaviour
{
    [SerializeField] private float _speed = 5f;
    [SerializeField] private float _lifeTime = 5f;
    [SerializeField] private Rigidbody2D _arrowRb2D;
    [SerializeField] private Collider2D _arrowColl2D;
    [SerializeField] private Collider2D _arrowheadColl2D;
    [SerializeField] private string _groundTagName;

    private ObjectPool _poolObject;

    private void Start()
    {
        _arrowColl2D.enabled = false;
        _poolObject = GetComponent<ObjectPool>();
    }
    private void Update()
    {
        Movement();

    }

    private void OnEnable()
    {
        StartCoroutine(Destroy());
    }

    private void OnTriggerEnter2D(Collider2D hitInfo)
    {

        if (hitInfo.CompareTag("Ground"))
        {
            _arrowColl2D.enabled = true;
            _arrowRb2D.constraints = RigidbodyConstraints2D.FreezeAll;
            Debug.Log(hitInfo.name);
        }
    }

    private IEnumerator Destroy()
    {
        yield return new WaitForSeconds(_lifeTime);
        _arrowColl2D.enabled = false;
        _arrowRb2D.constraints = RigidbodyConstraints2D.None;
        _poolObject.ReturnToPool();
    }

    private void Movement()
    {
        _arrowRb2D.velocity = _arrowRb2D.transform.right * _speed;
    }
}
