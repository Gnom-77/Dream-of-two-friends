using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Pool;

public class ObjectPoolManager : MonoBehaviour 
{
    [SerializeField] private ObjectPool _prefab;
    [Space(10)][SerializeField] private Transform _container;
    [SerializeField] private int _minCapacity;
    [SerializeField] private int _maxCapacity;
    [Space(10)]
    [SerializeField] private bool _autoExpand;

    private Queue<ObjectPool> _pool;

    private void OnValidate()
    {
        if(_maxCapacity <= _minCapacity)
        {
            _maxCapacity = _minCapacity + 1;
        }
        if(_autoExpand)
        {
            _maxCapacity = Int32.MaxValue;
        }
    }

    private void Start()
    {
        CreatePool();
    }

    private void CreatePool()
    {
        _pool = new Queue<ObjectPool>(_minCapacity);

        for (int i = 0; i < _minCapacity; i++) 
        {
            CreateElement();
        }
    }

    private ObjectPool CreateElement(bool isActiveByDefault = false)
    {
        ObjectPool createdObject = Instantiate(_prefab, _container);
        createdObject.gameObject.SetActive(isActiveByDefault);

        _pool.Enqueue(createdObject);

        return createdObject;
    }

    public bool TryGetElement(out ObjectPool element)
    {
        foreach (ObjectPool item in _pool)
        {
            if (!item.gameObject.activeInHierarchy)
            {
                element = item;
                item.gameObject.SetActive(true);
                return true;
            }
        }

        element = null;
        return false;
    }

    public ObjectPool GetFreeElement(Vector3 position, Quaternion rotation)
    {
        ObjectPool element = GetFreeElement(position);
        element.transform.rotation = rotation;
        return element;
    }
    public ObjectPool GetFreeElement(Vector3 position)
    {
        ObjectPool element = GetFreeElement();
        element.transform.position = position;
        return element;
    }

    public ObjectPool GetFreeElement()
    {
        if (TryGetElement(out ObjectPool element))
        {
            return element;
        }

        if (_autoExpand)
        {
            return CreateElement(true);
        }

        if (_pool.Count < _maxCapacity)
        {
            return CreateElement(true);
        }

        throw new Exception("Pool is over!");
    }


}
