using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
public class TestBis : MonoBehaviour
{
    public GameObject player;
    NavMeshAgent navMeshAgent;
    Animator animator;

    const string STAND_STATE = "Stand";
    const string TAKE_DAMAGE_STATE = "Damages";
    const string DEFEATED_STATE = "IsDefeated";
    const string WALK_STATE = "Walk";

    public string currentAction;

    public float attackRange;
    public bool playerInAttackRange;

    private void Awake()
    {
        currentAction = STAND_STATE;

        animator = GetComponent<Animator>();
        navMeshAgent = GetComponent<NavMeshAgent>();    
        player = FindObjectOfType<Player>().gameObject;
    }

    private void Update()
    {
        //playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, player.layer);

        if (currentAction == DEFEATED_STATE)
        {
            navMeshAgent.ResetPath();
            TakingDamage();
            return;
        }
        if(currentAction == TAKE_DAMAGE_STATE)
        {
            navMeshAgent.ResetPath();
            TakingDamage();
            return;
        }

        if(player != null/* && playerInAttackRange*/)
        {
            if (MovingToTarget()) //est ce que le robot se déplace vers le joueur ?
            {
                return;
            }
        }
    }


    private void Stand()
    {
        ResetAnimation();
        currentAction = STAND_STATE;
        animator.SetBool("Stand", true);
    }

    private void Walk()
    {
        ResetAnimation();
        currentAction = WALK_STATE;
        animator.SetBool("Walk", true);
    }

    private void TakingDamage()
    {
        
    }

    private bool MovingToTarget()
    {
        navMeshAgent.SetDestination(player.transform.position);

        if (navMeshAgent.remainingDistance > navMeshAgent.stoppingDistance)
        {
            if (currentAction != WALK_STATE)
            {
                Walk();
            }
        }
        else
        {
            RotateToTarget(player.transform);
            return false;
        }
        return true;
    }

    private void RotateToTarget(Transform target)
    {
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z)); 
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 3f);
    }

    
    private void ResetAnimation()
    {
        animator.SetBool(STAND_STATE, false);
        animator.SetBool(WALK_STATE, false);
        animator.SetBool(DEFEATED_STATE, false);
        animator.SetBool(TAKE_DAMAGE_STATE, false);
    }
}