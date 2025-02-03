using System.Collections;
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
    [SerializeField] private float _invulnerabilityArrowTime = 0.5f;

    private ObjectPool _poolObject;
    private int _groundAndWallLayerMask;
    private float _timeAfterShot; 

    private void Start()
    {
        _arrowColl2D.enabled = false;
        _poolObject = GetComponent<ObjectPool>();
        _groundAndWallLayerMask = LayerMask.GetMask("Ground", "Wall");
    }
    private void Update()
    {
        Movement();
        _timeAfterShot += Time.deltaTime;
    }

    private void OnEnable()
    {
        _timeAfterShot = 0;
        StartCoroutine(Destroy());
    }

    private void OnTriggerEnter2D(Collider2D hitInfo)
    {

        if ((_groundAndWallLayerMask & (1 << hitInfo.gameObject.layer)) != 0)
        {
            if (_timeAfterShot > _invulnerabilityArrowTime)
            {
            _arrowColl2D.enabled = true;
            _arrowRb2D.constraints = RigidbodyConstraints2D.FreezeAll;
            }
        }
    }

    private IEnumerator Destroy()
    {
        yield return new WaitForSeconds(_lifeTime);
        HideAndReturnToPoolArrow();
    }

    private void HideAndReturnToPoolArrow()
    {
        _arrowColl2D.enabled = false;
        _arrowRb2D.constraints = RigidbodyConstraints2D.None;
        _poolObject.ReturnToPool();
    }

    private void Movement()
    {
        _arrowRb2D.linearVelocity = _arrowRb2D.transform.right * _speed;
    }
}
