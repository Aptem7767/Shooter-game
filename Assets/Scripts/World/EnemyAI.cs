using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public float detectionRange = 20f;
    public float attackRange = 10f;
    public float moveSpeed = 3f;
    public float damage = 10f;
    public float attackCooldown = 1f;
    
    private Transform player;
    private float lastAttackTime;
    
    void Start()
    {
        var playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj) player = playerObj.transform;
    }
    
    void Update()
    {
        if (player == null) return;
        
        float distance = Vector3.Distance(transform.position, player.position);
        
        if (distance <= attackRange)
        {
            Attack();
        }
        else if (distance <= detectionRange)
        {
            ChasePlayer();
        }
    }
    
    void ChasePlayer()
    {
        Vector3 direction = (player.position - transform.position).normalized;
        direction.y = 0;
        transform.position += direction * moveSpeed * Time.deltaTime;
        transform.LookAt(new Vector3(player.position.x, transform.position.y, player.position.z));
    }
    
    void Attack()
    {
        if (Time.time - lastAttackTime < attackCooldown) return;
        
        lastAttackTime = Time.time;
        var playerHealth = player.GetComponent<PlayerHealth>();
        if (playerHealth) playerHealth.TakeDamage(damage);
    }
}

public class Health : MonoBehaviour
{
    public float maxHealth = 100f;
    public float currentHealth;
    
    void Start()
    {
        currentHealth = maxHealth;
    }
    
    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
            Destroy(gameObject);
    }
}
