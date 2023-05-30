using UnityEngine;
using UnityEngine.AI;

public class Test : MonoBehaviour
{
    public NavMeshAgent agent;

    public Transform player;

    public LayerMask whatIsGround, whatIsPlayer;

    public float health;
    public bool isDead;
    
    public int damages = 20;

    Animator animator;

    const string WALK_STATE = "Walk";
    const string DAMAGE_STATE = "TakeDamage";
    const string DEATH_STATE = "Death";
    const string ATTACK1_STATE = "Attack1";
    int maxHealth = 200;
    
    public string currentAction;

   
    public Vector3 walkPoint;
    bool walkPointSet;
    public float walkPointRange;


    public float timeBeforeDeath;
    public float timeBetweenAttacks;
    bool alreadyAttacked;
    

   
    public float sightRange, attackRange;
    public bool playerInSightRange, playerInAttackRange,receiveDamage;

    private void Awake()
    {
        
        animator = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        
        agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

        if (!playerInSightRange && !playerInAttackRange && !isDead)
        {
            
            ResetAnimation();
            currentAction = WALK_STATE;
            animator.SetBool("Walk", true);
            
            Patroling();
        }
        if (playerInSightRange && !playerInAttackRange && !isDead)
        {
            
            ResetAnimation();

            currentAction = WALK_STATE;
            animator.SetBool("Walk", true);
            ChasePlayer();
            
        }
        if (playerInAttackRange && playerInSightRange)
        {
            
            ResetAnimation();
            currentAction = ATTACK1_STATE;
            animator.SetBool("Attack1", true);
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

        
        if (distanceToWalkPoint.magnitude < 1f)
            walkPointSet = false;
    }
    private void SearchWalkPoint()
    {
      
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
        agent.SetDestination(transform.position);

        transform.LookAt(player);

        if (!alreadyAttacked && health > 0)
        {
            PlayerStats.instance.Damages(damages);

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
        
        receiveDamage = true;
        health -= damage;
        if (health <= 0)
        {
            Invoke(nameof(DestroyEnemy), 0);
            isDead = true;
        }
        else Invoke(nameof(dmg), 0);


    }
    private void dmg()
    {
        ResetAnimation();
        currentAction = DAMAGE_STATE;
        animator.SetBool("TakeDamage", true);
    }
    
    private void DestroyEnemy()
    {
        ResetAnimation();
        currentAction = DEATH_STATE;
        animator.SetBool("Death", true);
        Invoke("TimeBD", 4);
    }

    
    void TimeBD()
    {
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
        animator.SetBool(WALK_STATE, false);
        animator.SetBool(DEATH_STATE, false);
        animator.SetBool(DAMAGE_STATE, false);
        animator.SetBool(ATTACK1_STATE, false);
    }
}

