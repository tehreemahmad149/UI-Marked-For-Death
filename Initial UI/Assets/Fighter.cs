using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fighter : MonoBehaviour
{
    public Collider[] attackHitboxes;

    private Animator animator; // Assuming you have an Animator component
    public bool isAttacking = false;
    private float attackStartTime = 0.0f;
    private int startupFrames, attackFrames, recoveryFrames;
    public Collider ongoingAttack;
    public float stunDuration, damage, dash; // Default stun duration (adjustable)
    public bool isHit = false;
    private float hitCooldownDuration = 1;
    public bool isGrounded;
    public bool isAlive = true;
    public bool parrying = false;
    public bool parrySuccess = false;
    public bool inFront;

    private CharacterStats characterStats;
    private NewMovement characterController;
    private GameObject Enemy0;
    private float distance;

    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        characterStats = GetComponent<CharacterStats>();
        characterController = GetComponent<NewMovement>();
    }

    void Update()
    {
        // Check if grounded using a raycast or physics overlap

        if (Enemy0 == null)
        {
            Enemy0 = GameObject.FindWithTag("Enemy");
        }
        else {
            distance = (float)(Enemy0.transform.position - transform.position).x;
            CheckPosition();
            if (inFront)
            {
                transform.rotation = Quaternion.Euler(0f, 0f, 0f);
            }
            else
            {
                // Rotate player 180 degrees if enemy is behind
                transform.rotation = Quaternion.Euler(0f, 180f, 0f);
            }
        }
        isGrounded = Physics.Raycast(transform.position, -Vector3.up, 3f);
        if (characterStats.currentHealth <= 0)
        {
            isAlive = false;
        }
        if (isAlive)
        {

            if (isAttacking || parrying)
            {
                return; // Don't process further input if already attacking
            }
            CheckPosition();
            if (isGrounded)
            {
                if (Input.GetKeyDown(KeyCode.G))
                {
                    QuickSlash();
                }
                else if (Input.GetKeyDown(KeyCode.H))
                {
                    StrongSlash();
                }
                else if (Input.GetKeyDown(KeyCode.J))
                {
                    LowSweep();
                }
                else if (Input.GetKeyDown(KeyCode.K))
                {
                    WideSlash();
                }
                else if (Input.GetKeyDown(KeyCode.D))
                { 
                    animator.SetTrigger("Run");
                    characterController.MoveForward(4,(float)0.8);
                }
                else if (Input.GetKeyDown(KeyCode.A))
                {
                    animator.SetTrigger("BackDash");
                    if (inFront)
                    {
                        characterController.ForwardDash(-15);
                    }
                    else {
                        characterController.ForwardDash(15);
                    }
                }
                else if (Input.GetKeyDown(KeyCode.W))
                {
                    characterController.Jump(15);
                }
                else if (Input.GetKeyDown(KeyCode.P))
                {
                    StartCoroutine(Parry());
                }
            }
            else
            {
                if (Input.GetKeyDown(KeyCode.L))
                {
                    OverheadSlash();
                }
            }
        }
    }
    public void MoveForth()
    {
        if (inFront)
        {
            animator.SetTrigger("Walk");
            characterController.MoveForward2(15);
        }
        else {
            animator.SetTrigger("Walk");
            characterController.MoveForward2(-15);
        }
    }
    public void MoveBack()
    {
        if (inFront)
        {
            animator.SetTrigger("BackDash");
            characterController.MoveBackward(-15);
        }
        else
        {
            animator.SetTrigger("BackDash");
            characterController.MoveBackward(15);
        }
    }
    public void jump()
    {
        if (isGrounded)
        {
            characterController.Jump(15);
        }
    }

    IEnumerator WaitForDelay(float delay)
    {
        yield return new WaitForSeconds(3);
    }
    private void CheckPosition()
    {
        if (Enemy0 != null)
        {
            // Calculate direction vector from player to enemy
            Vector3 enemyDirection = Enemy0.transform.position - transform.position;

            // Check if enemy is in front (positive X) or behind (negative X)
            inFront = enemyDirection.x > 0f;
        }
        else
        {
            // No enemy detected, set flag to true
            inFront = true;
        }
    }
    public void parry()
    {
        StartCoroutine(Parry());
    }

    public void QuickSlash()
    {
        if (characterStats.currentStamina >= 15)
        {
            animator.SetTrigger("Attack1");
            LaunchAttack(attackHitboxes[0], 1.0f, 10, 20, 15, 1f, 15, 1, 15); // Add attack type
        }
    }
    public void StrongSlash()
    {
        if (characterStats.currentStamina >= 25)
        {
            animator.SetTrigger("Heavy Attack");
            if (inFront)
            {
                characterController.ForwardDash(20f);
            }
            else {
                characterController.ForwardDash(-20f);
            }
            LaunchAttack(attackHitboxes[1], 1.5f, 15, 15, 15, 2.5f, 20, 3, 20); // Add attack type
        }
    }
    private void LowSweep()
    {
        if (characterStats.currentStamina >= 15)
        {
            animator.SetTrigger("Leg Attack");
            LaunchAttack(attackHitboxes[2], 1.0f, 10, 20, 15, 0.7f, 15, -1, 15); // Add attack type
        }
    }
    private void OverheadSlash()
    {
        if (characterStats.currentStamina >= 20)
        {
            animator.SetTrigger("Attack2");
            LaunchAttack(attackHitboxes[3], 1.0f, 10, 20, 15, 0.6f, 20, 0, 25); // Add attack type
        }
    }

    public void WideSlash()
    {
        if (characterStats.currentStamina >= 30)
        {
            animator.SetTrigger("WideAttack");
            if(inFront)
            {
                characterController.MoveForward2(20);
            }
            else
            {
            
                characterController.MoveForward2(-20);
                
            }
            LaunchAttack(attackHitboxes[4], 2.6f, 20, 25, 5, 2f, 30, 5, 35); // Add attack type
        }
    }

    // Add similar functions for other attacks (LaunchSpecialAttack2, LaunchSpecialAttack3, etc.)

    private void LaunchAttack(Collider col, float duration, int startupFrames, int attackFrames, int recoveryFrames, float stunDuration, float stamina, float dash, float damage)
    {
            isAttacking = true;
            attackStartTime = Time.time;
            this.startupFrames = startupFrames;
            this.attackFrames = attackFrames;
            this.recoveryFrames = recoveryFrames;
            this.stunDuration = stunDuration;
            this.damage = damage;
            this.dash = dash;
            ongoingAttack = col;
            characterStats.ConsumeStamina(stamina);
            StartCoroutine(AttackCoroutine(duration));
    }

    public void GetHit(float damageTaken, float stunDuration1) // Public method for enemies to call
    {
        // Apply stun regardless of isAttacking
        animator.SetTrigger("Damage");
        isHit = true;
        StartCoroutine(HitCooldown());
        StartCoroutine(HandleStun(stunDuration1));

        float finalDamage = damageTaken * (100 / characterStats.armor);

        // Deduct health
        characterStats.TakeDamage(finalDamage);
    }
    IEnumerator HitCooldown()
    {
        yield return new WaitForSeconds(hitCooldownDuration); // Replace with desired cooldown time
        isHit = false; // Reset isHit flag after cooldown
    }
    IEnumerator Parry()
    {
        animator.SetTrigger("Parry");
        parrying = true;
        parrySuccess = false; // Reset at the start
        float elapsedTime = 0f;

        while (elapsedTime < 1f)
        {
            elapsedTime += Time.deltaTime;

            if (parrySuccess)
            {
                animator.SetTrigger("ParrySuccess");
                parrying = false;
                yield return new WaitForSeconds(0.5f);
                parrySuccess = false; // Reset after a successful parry
                yield break; // Exit the coroutine
            }

            yield return null; // Wait for the next frame
        }

        parrying = false;
        parrySuccess = false; // Reset in case parry window ended without success
    }


    IEnumerator HandleStun(float stunDue)
    {
        isAttacking = true;
        Debug.Log("Stun started");

        yield return new WaitForSeconds(stunDue);

        isAttacking = false;
        Debug.Log("Stun ended");
    }

    private IEnumerator AttackCoroutine(float duration)
    {
        float elapsedTime = 0f;
        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;

            float normalizedTime = elapsedTime / duration;
            // Handle attack phases based on normalized time
            if (normalizedTime < (float)startupFrames / (startupFrames + attackFrames + recoveryFrames))
            {
                // Startup frames - perform visual/audio cues (optional)
            }
            else if (normalizedTime < (float)(startupFrames + attackFrames) / (startupFrames + attackFrames + recoveryFrames))
            {
                // Attack frames - check for enemy hitboxes
                Collider[] cols = Physics.OverlapBox(ongoingAttack.bounds.center, ongoingAttack.bounds.extents, ongoingAttack.transform.rotation, LayerMask.GetMask("Enemy")); // Assuming "Enemy" layer for enemies
                HashSet<EnemyFighter> hitEnemies = new HashSet<EnemyFighter>(); // Track hit enemies

                foreach (Collider c in cols)
                {
                    EnemyFighter enemy = c.GetComponent<EnemyFighter>();
                    if (enemy != null && !hitEnemies.Contains(enemy) && !enemy.isHit) // Check for enemy existence and if not already hit
                    {
                        hitEnemies.Add(enemy);
                        enemy.GetHit(characterStats.strength * damage / 100, stunDuration);
                    }
                }
            }
            else
            {
                // Recovery frames - no attack functionality
            }

            yield return null;
        }
        isAttacking = false;
    }
}
