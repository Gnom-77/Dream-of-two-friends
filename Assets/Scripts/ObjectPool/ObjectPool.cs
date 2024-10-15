using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public void ReturnToPool()
    {
        gameObject.SetActive(false);
    }
}
