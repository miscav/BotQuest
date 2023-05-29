using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    [SerializeField]
    private Animator animator;
    const string ATTACK_STATE = "Attack";

    [SerializeField]
    private Armes equipmentSystem;

    private bool isAttacking;

    [SerializeField]
    private float attackRange;

    [SerializeField]
    private LayerMask layerMask;

    [SerializeField]
    private Vector3 attackOffSet;


    void Update()
    {
        Debug.DrawRay(transform.position + attackOffSet, transform.forward * attackRange, Color.red);

        if (Input.GetMouseButtonDown(0))
        {
            isAttacking = true;
            SendAttack();
            //animator.SetTrigger("Attack");
        }
    }

    void SendAttack()
    {
        RaycastHit hit;

        if(Physics.Raycast(transform.position + attackOffSet, transform.forward, out hit, attackRange))
        {
            if (hit.transform.CompareTag("Enemy"))
            {
                Debug.Log("AHHHHHHHHHHHHHHHH");
                Test enemy = hit.transform.GetComponent<Test>();
                enemy.TakeDamage(200);
            }
        }

    }

    public void AttackFinished()
    {
        isAttacking = false;
    }
}
