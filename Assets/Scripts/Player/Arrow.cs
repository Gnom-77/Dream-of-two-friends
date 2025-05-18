using System.Collections;
using UnityEngine;

[RequireComponent(typeof(ObjectPool))]
public class Arrow : Sounds
{
    [SerializeField] private Rigidbody2D _arrowRb2D;
    [SerializeField] private Collider2D _arrowColl2D;
    [SerializeField] private Collider2D _arrowheadColl2D;
    [SerializeField] private string _groundTagName;
    [SerializeField] private float _lifeTime = 5f;
    [SerializeField] private float _invulnerabilityArrowTime = 0.5f;
    [SerializeField] private string _princeTag;
    [Header("Sound Settings")]
    [SerializeField] private float _volume = 1f;

    private ObjectPool _poolObject;
    private int _groundAndWallLayerMask;
    private float _timeAfterShot;
    private bool _isTrigger;

    private void Start()
    {
        _arrowColl2D.enabled = false;
        _poolObject = GetComponent<ObjectPool>();
        _groundAndWallLayerMask = LayerMask.GetMask("Ground", "Wall", "Platform");
    }
    private void OnEnable()
    {
        _isTrigger = false;
        _timeAfterShot = 0;
        StartCoroutine(Destroy());
    }

    private void Update()
    {
        if (!_isTrigger)
            Movement();
        _timeAfterShot += Time.deltaTime;
    }


    private void OnTriggerEnter2D(Collider2D hitInfo)
    {

        if ((_groundAndWallLayerMask & (1 << hitInfo.gameObject.layer)) != 0)
        {
            if (_timeAfterShot > _invulnerabilityArrowTime)
            {
                PlaySound(0,_volume);
                _isTrigger = true;
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
        float angle = Mathf.Atan2(_arrowRb2D.linearVelocity.y, _arrowRb2D.linearVelocity.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }
}
