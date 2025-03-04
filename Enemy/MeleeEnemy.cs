using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeEnemy : MonoBehaviour
{
    [SerializeField] private float attackCooldown;
    [SerializeField] private float range;
    [SerializeField] private float colliderDistance;
    [SerializeField] private int damage;
    [SerializeField] private BoxCollider2D box;
    [SerializeField] private LayerMask playerLayer;
    private float cooldownTimer = Mathf.Infinity;
    private Animator anim;
    private Player_Health playerHealth;
    private Enemy_Patrol enemyPatrol;
    [SerializeField] private AudioClip attackSound;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        enemyPatrol = GetComponentInParent<Enemy_Patrol>();
    }

    private void Update()
    {
        cooldownTimer += Time.deltaTime;
        if(PlayerInSight())
        {
            if (cooldownTimer >= attackCooldown && playerHealth.currentHealth > 0)
            {
                cooldownTimer = 0;
                anim.SetTrigger("meleeAttack");
                Sound_Manager.instance.PlaySound(attackSound);
            }
        }

        if(enemyPatrol != null)
        {
            enemyPatrol.enabled = !PlayerInSight();
        }
    }

    private bool PlayerInSight()
    {
        RaycastHit2D hit = Physics2D.BoxCast(box.bounds.center + transform.right * range * transform.localScale.x * colliderDistance, new Vector3(box.bounds.size.x * range, box.bounds.size.y, box.bounds.size.z), 0, Vector2.left, 0, playerLayer);
        if(hit.collider != null)
        {
            playerHealth = hit.transform.GetComponent<Player_Health>();
        }
        return hit.collider != null;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(box.bounds.center + transform.right * range * transform.localScale.x * colliderDistance, new Vector3(box.bounds.size.x * range, box.bounds.size.y, box.bounds.size.z));
    }

    private void DamagePlayer()
    {
        if(PlayerInSight())
        {
            playerHealth.TakeDamage(damage);
        }
    }

}
