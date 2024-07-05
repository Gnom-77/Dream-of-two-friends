using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Combat : MonoBehaviour
{
    [SerializeField] Transform _attackPoint;
    [SerializeField] float _attackRange;
    [SerializeField] LayerMask _enemyLayers;

    
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.U))
        {
            Attack();
        }
        
    }

    private void Attack()
    {
        // Player an attack animation

        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(_attackPoint.position, _attackRange, _enemyLayers);

        foreach(Collider2D enemy in  hitEnemies)
        {
            Debug.Log("Hit enemy " + enemy.name);
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (_attackPoint != null)
        {
            Gizmos.DrawWireSphere(_attackPoint.position, _attackRange);
        }
    }
}
