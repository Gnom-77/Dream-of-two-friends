using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [SerializeField]
    private Rigidbody2D _rigidBody2D;
    private void Awake()
    {
        if ( _rigidBody2D == null )
        {
            _rigidBody2D = GetComponent<Rigidbody2D>();
        }
    }

    public Rigidbody2D GetRigidBody2D
    {
        get { return _rigidBody2D; }
    }

    public void ReturnToPool()
    {
        gameObject.SetActive(false);
    }
}
