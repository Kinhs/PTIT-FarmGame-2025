using UnityEngine;
using System.Collections;

public class SkeletonEnemy : MonoBehaviour
{
    public int maxHP = 3;
    public float moveSpeed = 2f;

    public float detectRange = 5f;
    public float attackRange = 1f;
    public float attackDelay = 1f;

    public float disableDelay = 1.5f;

    int currentHP;
    bool isDead;
    float attackTimer;

    public Animator animator;
    Rigidbody2D rb;
    Transform player;

    string currentAnim;

    void Start()
    {
        currentHP = maxHP;
        rb = GetComponent<Rigidbody2D>();

        if (PlayerController.instance != null)
            player = PlayerController.instance.transform;

        PlayAnim("Idle");
    }

    void Update()
    {
        if (isDead || player == null)
            return;

        attackTimer -= Time.deltaTime;

        float distance = Vector2.Distance(transform.position, player.position);

        if (distance <= attackRange)
            Attack();
        else if (distance <= detectRange)
            Chase();
        else
            Idle();
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
        rb.linearVelocity = dir * moveSpeed;

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
        if (isDead || currentHP <= 0)
            return;

        currentHP--;
        PlayAnim("TakeHit");

        if (currentHP <= 0)
            Die();
    }

    void Die()
    {
        isDead = true;
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
