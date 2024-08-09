using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyFighter : MonoBehaviour
{
    public EnemyAttackData attackData; // Reference to the EnemyAttackData script

    private Animator animator;
    public bool isAttacking = false;
    private float attackStartTime = 0.0f;
    private int startupFrames, attackFrames, recoveryFrames;
    public Collider ongoingAttack;
    public float stunDuration, damage, dash;
    public bool isHit = false;
    private float hitCooldownDuration = 1;
    public bool isGrounded;
    public bool isAlive = true;
    public bool parrying = false;
    public bool inFront;
    public bool HitPlayer;
    private bool isWaitingForDelay = false;

    private CharacterStats characterStats;
    private PlayerMovement characterController;

    public enum EnemyState
    {
        Idle,
        Moving,
        Attacking,
        Jumping
    }

    private EnemyState currentState;
    private bool playerDetected = false;

    public bool enableHandleStun = true;

    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        characterStats = GetComponent<CharacterStats>();
        characterController = GetComponent<PlayerMovement>();
        currentState = EnemyState.Idle;
    }

    void Update()
    {
        if (!playerDetected)
        {
            DetectPlayer();
            if (!playerDetected)
            {
                return;
            }
        }
        CheckPosition();
        if (inFront)
        {
            transform.rotation = Quaternion.Euler(0f, 0f, 0f);
        }
        else
        {
            transform.rotation = Quaternion.Euler(0f, 180f, 0f);
        }

        isGrounded = Physics.Raycast(transform.position, -Vector3.up, 3f);
        if (characterStats.currentHealth <= 0)
        {
            isAlive = false;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
        if (isAlive)
        {
            if (isAttacking)
            {
                return;
            }
            else
            {
                switch (currentState)
                {
                    case EnemyState.Idle:
                        currentState = EnemyState.Moving;
                        break;

                    case EnemyState.Moving:
                        HitPlayer = CanHitPlayer();
                        if (HitPlayer)
                        {
                            currentState = EnemyState.Attacking;
                        }
                        else
                        {
                            GameObject player0 = GameObject.FindWithTag("Player");
                            if (player0 != null)
                            {
                                if (player0.transform.position.x < transform.position.x)
                                {
                                    characterController.MoveBackward();
                                }
                                else
                                {
                                    characterController.MoveForward();
                                }
                                animator.SetTrigger("walk");
                            }
                        }
                        break;

                    case EnemyState.Attacking:
                        if (!isWaitingForDelay)
                        {
                            int randomAttack = Random.Range(0, 6);
                            switch (randomAttack)
                            {
                                case 0: QuickSlash(); animator.SetTrigger("attack03"); break;
                                case 1: StrongSlash(); animator.SetTrigger("attack04"); break;
                                case 2: QuickSlash(); animator.SetTrigger("attack03"); break;
                                case 3:
                                case 4:
                                case 5:
                                    StartCoroutine(WaitForDelay());
                                    isWaitingForDelay = true;
                                    Debug.Log("am active");
                                    break;
                                default:
                                    break;
                            }
                        }
                        break;
                }
                return;
            }
        }
    }

    private void DetectPlayer()
    {
        GameObject player0 = GameObject.FindWithTag("Player");
        if (player0 != null)
        {
            playerDetected = true;
            Debug.Log("Player detected.");
        }
        else
        {
            Debug.Log("Looking for player...");
        }
    }

    IEnumerator WaitForDelay()
    {
        yield return new WaitForSeconds(2f);
        currentState = EnemyState.Idle;
        isWaitingForDelay = false;
    }

    private void CheckPosition()
    {
        GameObject Enemy0 = GameObject.FindWithTag("Player");
        if (Enemy0 != null)
        {
            Vector3 enemyDirection = Enemy0.transform.position - transform.position;
            inFront = enemyDirection.x > 0f;
        }
        else
        {
            inFront = true;
        }
    }

    private void QuickSlash()
    {
        LaunchAttack(attackData.quickSlashData);
    }

    private void StrongSlash()
    {
        LaunchAttack(attackData.strongSlashData);
    }

    private bool CanHitPlayer()
    {
        GameObject player0 = GameObject.FindWithTag("Player");
        float distanceToPlayer = Mathf.Abs(player0.transform.position.x - transform.position.x);
        return distanceToPlayer <= 2.2f;
    }

    private void LaunchAttack(AttackData data)
    {
        isAttacking = true;
        attackStartTime = Time.time;
        startupFrames = data.startupFrames;
        attackFrames = data.attackFrames;
        recoveryFrames = data.recoveryFrames;
        stunDuration = data.stunDuration;
        damage = data.damage;
        dash = data.dash;
        ongoingAttack = data.attackHitbox;

        StartCoroutine(AttackCoroutine(data.duration));
    }

    public void GetHit(float damageTaken, float stunDuration1)
    {
        isHit = true;
        StartCoroutine(HitCooldown());

        if (enableHandleStun)
        {
            StartCoroutine(HandleStun(stunDuration1));
        }

        float finalDamage = damageTaken * (100 / characterStats.armor);
        characterStats.TakeDamage(finalDamage);
    }

    IEnumerator HitCooldown()
    {
        yield return new WaitForSeconds(hitCooldownDuration);
        isHit = false;
    }

    IEnumerator HandleStun(float stunDue)
    {
        animator.SetTrigger("Damage");
        isAttacking = true;
        yield return new WaitForSeconds(stunDue);
        isAttacking = false;
    }

    private IEnumerator AttackCoroutine(float duration)
    {
        Debug.Log("Attack has Started");
        bool continueCoroutine = true;
        float elapsedTime = 0f;

        while (elapsedTime < duration && continueCoroutine)
        {
            elapsedTime += Time.deltaTime;
            float normalizedTime = elapsedTime / duration;

            if (normalizedTime < (float)startupFrames / (startupFrames + attackFrames + recoveryFrames))
            {
                // Startup phase - nothing to do
            }
            else if (normalizedTime < (float)(startupFrames + attackFrames) / (startupFrames + attackFrames + recoveryFrames))
            {
                // Attack phase
                Collider[] cols = Physics.OverlapBox(ongoingAttack.bounds.center, ongoingAttack.bounds.extents, ongoingAttack.transform.rotation, LayerMask.GetMask("Player"));
                HashSet<Fighter> hitEnemies = new HashSet<Fighter>();

                foreach (Collider c in cols)
                {
                    Fighter player = c.GetComponent<Fighter>();
                    if (player != null && !hitEnemies.Contains(player) && !player.isHit && !player.parrying)
                    {
                        hitEnemies.Add(player);
                        player.GetHit(characterStats.strength * damage / 100, stunDuration);
                    }
                    if (player.parrying)
                    {
                        player.parrySuccess = true;
                        GetHit(20, 1f);
                        continueCoroutine = false; // Stop the attack after a successful parry
                        break;
                    }
                }
            }
            else
            {
                // Recovery phase - nothing to do
            }

            yield return null;
        }

        Debug.Log("Attack has ended");
        isAttacking = false;
        currentState = EnemyState.Idle;
    }

}
