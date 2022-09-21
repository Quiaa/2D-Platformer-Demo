using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterCombat : MonoBehaviour
{

    [SerializeField]
    int attackDamage;
    [SerializeField]
    float attackRange;
    [SerializeField]
    GameObject attackPoint;
    [SerializeField]
    LayerMask targetLayer;
    [SerializeField]
    CharacterMovementController movementController;

    public bool isAttacking = false;

    private void Update()
    {
        if (movementController.facingDirection == CharacterMovementController.FacingDirection.Right)
        {
            attackPoint.transform.position = transform.position + new Vector3(2.4f, 0f, 0f);
        }
        else
        {
            attackPoint.transform.position = transform.position - new Vector3(2.4f, 0f, 0f);
        }
    }

    public void Attack()
    {
        Collider2D[] hitResults = Physics2D.OverlapCircleAll(attackPoint.transform.position, attackRange, targetLayer);

        if (hitResults == null)
            return;

        foreach(Collider2D hit in hitResults)
        {
            if( hit.GetComponent<Health>() != null)
            {
                hit.GetComponent<Health>().TakeDamage(attackDamage);
            }
        }

    }

    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;

        Gizmos.DrawWireSphere(attackPoint.transform.position, attackRange);
    }

}
