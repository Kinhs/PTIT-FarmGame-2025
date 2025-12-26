using UnityEngine;
using System.Collections;

public class SkeletonEnemy : MonoBehaviour
{
    [Range(0f, 1f)]
    public float spawnChance = 1f;

    public int maxHP = 3;

    public float minMoveSpeed = 1.5f;
    public float maxMoveSpeed = 3f;
    public float speedChangeInterval = 0.5f;

    public float detectRange = 5f;
    public float attackRange = 1f;
    public float attackDelay = 1f;

    public float hitStunTime = 0.3f;
    public float knockbackForce = 3f;

    public float disableDelay = 1.5f;

    int currentHP;
    bool isDead;
    bool isHit;
    float attackTimer;

    float currentMoveSpeed;
    float speedTimer;

    public Animator animator;
    Rigidbody2D rb;
    Transform player;

    string currentAnim;

    void Start()
    {
        if (Random.value > spawnChance)
        {
            gameObject.SetActive(false);
            return;
        }

        currentHP = maxHP;
        rb = GetComponent<Rigidbody2D>();

        if (PlayerController.instance != null)
            player = PlayerController.instance.transform;

        RandomizeSpeed();
        PlayAnim("Idle");
    }

    void Update()
    {
        if (isDead || isHit || player == null)
            return;

        attackTimer -= Time.deltaTime;
        speedTimer -= Time.deltaTime;

        if (speedTimer <= 0f)
            RandomizeSpeed();

        float distance = Vector2.Distance(transform.position, player.position);

        if (distance <= attackRange)
            Attack();
        else if (distance <= detectRange)
            Chase();
        else
            Idle();
    }

    void RandomizeSpeed()
    {
        currentMoveSpeed = Random.Range(minMoveSpeed, maxMoveSpeed);
        speedTimer = speedChangeInterval;
    }

    void Idle()
    {
        rb.linearVelocity = Vector2.zero;
        PlayAnim("Idle");
    }

    void Chase()
    {
        PlayAnim("Walk");

        Vector2 dir = ((Vector2)player.position - rb.position).normalized;
        rb.linearVelocity = dir * currentMoveSpeed;

        if (Mathf.Abs(dir.x) > 0.01f)
            transform.localScale = new Vector3(dir.x > 0 ? 1 : -1, 1, 1);
    }

    void Attack()
    {
        rb.linearVelocity = Vector2.zero;

        if (attackTimer > 0f)
            return;

        attackTimer = attackDelay;
        PlayerController.instance.TakeHit();
    }

    public void TakeHit()
    {
        if (isDead || isHit || currentHP <= 0)
            return;

        currentHP--;
        isHit = true;

        PlayAnim("TakeHit");

        Vector2 knockDir = ((Vector2)transform.position - (Vector2)player.position).normalized;
        rb.linearVelocity = knockDir * knockbackForce;

        if (currentHP <= 0)
        {
            Die();
            return;
        }

        StartCoroutine(HitStunCoroutine());
    }

    IEnumerator HitStunCoroutine()
    {
        yield return new WaitForSeconds(hitStunTime);
        isHit = false;
    }

    void Die()
    {
        isDead = true;
        isHit = false;
        rb.linearVelocity = Vector2.zero;
        PlayAnim("Die");
        StartCoroutine(DisableAfterDelay());
    }

    void PlayAnim(string animName)
    {
        if (currentAnim == animName)
            return;

        currentAnim = animName;
        animator.Play(animName);
    }

    IEnumerator DisableAfterDelay()
    {
        yield return new WaitForSeconds(disableDelay);
        gameObject.SetActive(false);
    }
}
