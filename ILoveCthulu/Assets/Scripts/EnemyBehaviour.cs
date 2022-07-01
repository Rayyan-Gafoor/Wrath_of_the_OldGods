using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBehaviour : MonoBehaviour
{
    [Header("Object Refs")]
    public NavMeshAgent agent;
    public Transform player;
    public GameObject souls;
    public LayerMask ground_mask, player_mask;
    SoulSystem soul_system;
    Projectile_type projectile_type;

    public enum enemy__type { Scout, Runner }
    public enemy__type EnemyType;
    public float health;
    public int soul_value;
    public float enemy_damage;
    //enemy attacks
    [Header("Weapon Type")]
    public GameObject projectile;
    //patrolling 
    [Header("Patroling stats")]
    public Vector3 walk_point;
    public float walk_point_range;
    public bool walk_point_set;

    //Attacking
    [Header("Attack stats")]
    public float time_between_attacks;
    bool attacked;

    //States
    [Header("Enemy States")]
    public float sight_range, attack_range;
    public bool player_insight, player_in_attack;
    

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").transform;
        soul_system = player.GetComponent<SoulSystem>();
        projectile_type = projectile.GetComponent<Projectile_type>();
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        player_insight = Physics.CheckSphere(transform.position, sight_range, player_mask);
        player_in_attack = Physics.CheckSphere(transform.position, attack_range, player_mask);
        projectile_type.enemy_damage = enemy_damage;
        if(!player_in_attack && !player_insight)
        {
            patrol();
        }
        if (!player_in_attack && player_insight)
        {
            chase();
        }
        if (player_in_attack && player_insight)
        {
            attack();
        }
    }
    void patrol()
    {
        if (!walk_point_set)
        {
            search_walk_point();
        }
        if (walk_point_set)
        {
            agent.SetDestination(walk_point);
        }
        Vector3 distance_to_walk = transform.position - walk_point;

        if (distance_to_walk.magnitude< 0.1f)
        {
            walk_point_set = false;
            Debug.Log("dis to small");

        }
    }
    void search_walk_point()
    {
        Debug.Log("is Searching");

        float randomZ = Random.Range(-walk_point_range, walk_point_range);
        float randomX = Random.Range(-walk_point_range, walk_point_range);
        walk_point = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);
       
        if (Physics.CheckSphere(walk_point, 5f, ground_mask))
        {
            walk_point_set = true;
        }
       /* if (Physics.Raycast(walk_point,-transform.up, 2f, ground_mask))
        {

            Debug.Log("is Set");
            walk_point_set = true;
        }*/
    }
    void attack()
    {
        agent.SetDestination(transform.position);
        transform.LookAt(player);
        if (!attacked)
        {
            //attack type
            Rigidbody rb = Instantiate(projectile, transform.position, Quaternion.identity).GetComponent<Rigidbody>();
            rb.AddForce(transform.forward * 32f, ForceMode.Impulse);
            rb.AddForce(transform.up * 8f, ForceMode.Impulse);
            attacked = true;
            Invoke(nameof(reset_attack), time_between_attacks);
        }
    }
    void reset_attack()
    {
        attacked = false;
    }
    void chase()
    {
        agent.SetDestination(player.position);
    }
    public void take_damage(float damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Invoke(nameof(destroy__enemy), 0.5f);
        }
    }
    private void destroy__enemy()
    {
        souls.SetActive(true);
        souls.transform.parent = null;
        soul_system.add_souls(soul_value);
        DestroyObject(gameObject);
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attack_range);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, sight_range);

    }
}
