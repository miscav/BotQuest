using UnityEngine;
using UnityEngine.AI;

public class Test : MonoBehaviour
{
    public NavMeshAgent agent;

    public Transform player;

    public LayerMask whatIsGround, whatIsPlayer;

    public float health;

    public Player playerBis;

    Animator animator;

    const string STAND_STATE = "Stand";
    const string RUN_STATE = "Run";
    const string DAMAGE_BOT_STATE = "DamageBot";
    const string ISDEFEATED_STATE = "IsDefeated";
    const string WALK_STATE = "Walk";
    const string DAMAGE_PLAYER_STATE = "DamagePlayer";

    public string currentAction;

    //Patroling
    public Vector3 walkPoint;
    bool walkPointSet;
    public float walkPointRange;

    //Attacking
    public float timeBetweenAttacks;
    bool alreadyAttacked;
    public GameObject projectile;

    //States
    public float sightRange, attackRange;
    public bool playerInSightRange, playerInAttackRange;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        player = GameObject.Find("Player").transform;
        agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        //Check for sight and attack range
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

        if (!playerInSightRange && !playerInAttackRange)
        {
            ResetAnimation();
            currentAction = WALK_STATE;
            animator.SetBool("Walk", true);
            Patroling();
        }
        if (playerInSightRange && !playerInAttackRange)
        {
            ResetAnimation();
            currentAction = RUN_STATE;
            animator.SetBool("Run", true);
            ChasePlayer();
            
        }
        if (playerInAttackRange && playerInSightRange)
        {
            ResetAnimation();
            currentAction = DAMAGE_PLAYER_STATE;
            animator.SetBool("DamagePlayer", true);
            AttackPlayer();
        }

    }

    private void Patroling()
    {
        if (!walkPointSet)
        {
            SearchWalkPoint();
            
        }

        if (walkPointSet)
        {
            
            agent.SetDestination(walkPoint);
        }

        Vector3 distanceToWalkPoint = transform.position - walkPoint;

        //Walkpoint reached
        if (distanceToWalkPoint.magnitude < 1f)
            walkPointSet = false;
    }
    private void SearchWalkPoint()
    {
        //Calculate random point in range
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        if (Physics.Raycast(walkPoint, -transform.up, 2f, whatIsGround))
            walkPointSet = true;
    }

    private void ChasePlayer()
    {
        agent.SetDestination(player.position);
    }

    private void AttackPlayer()
    {
        
        //Make sure enemy doesn't move
        agent.SetDestination(transform.position);

        transform.LookAt(player);

        if (!alreadyAttacked)
        {
            ///Attack code here
            Rigidbody rb = Instantiate(projectile, transform.position, Quaternion.identity).GetComponent<Rigidbody>();
            rb.AddForce(transform.forward * 20f, ForceMode.Impulse);
            rb.AddForce(transform.up * 6f, ForceMode.Impulse);
            ///End of attack code
            
            //ENLEVER DE LA VIE A PLAYERBIS

            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
    }
    private void ResetAttack()
    {
        alreadyAttacked = false;
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        ResetAnimation();
        currentAction = DAMAGE_BOT_STATE;
        animator.SetBool("DamageBot", true);

        if (health <= 0) Invoke(nameof(DestroyEnemy), 0.5f);
    }
    private void DestroyEnemy()
    {
        ResetAnimation();
        currentAction = ISDEFEATED_STATE;
        animator.SetBool("IsDefeated", true);
        Destroy(gameObject);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, sightRange);
    }

    private void ResetAnimation()
    {
        animator.SetBool(RUN_STATE, false);
        animator.SetBool(WALK_STATE, false);
        animator.SetBool(ISDEFEATED_STATE, false);
        animator.SetBool(DAMAGE_BOT_STATE, false);
        animator.SetBool(DAMAGE_PLAYER_STATE, false);
        animator.SetBool(STAND_STATE, false);
    }
}

