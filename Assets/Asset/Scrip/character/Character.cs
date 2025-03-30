using System.Collections;
using UnityEngine;
using Fusion;

public class Character : NetworkBehaviour
{
    public CharacterController characterController;
    public float speed = 2f;
    public Vector3 movementVelocity;
    public Animator animator;
    public DamageZone damageZone;
    public HP HP;
    public float gravity = 10f;
    public float jumpHeight = 2f;
    public GameObject sword;
    public float attackRange = 2.5f;
    public int attackDamage = 30;

    public enum CharacterState
    {
        Normal,
        Attack,
        Jump,
        Laugh,
        Hurt,
        Die
    }

    [Networked] public CharacterState curState { get; set; }

    public override void FixedUpdateNetwork()
    {
        if (!Object.HasStateAuthority) return;

        if (HP.currentHP <= 0)
        {
            RpcChangeState(CharacterState.Die);
            return;
        }

        HandleInput();
        HandleMovement();
    }

    void HandleInput()
    {
        if (curState != CharacterState.Normal) return;

        if (Input.GetKey(KeyCode.K))
        {
            StartCoroutine(PerformAttack());
        }
        else if (Input.GetKey(KeyCode.J))
        {
            StartCoroutine(PerformAction(CharacterState.Jump, 1.0f));
        }
        else if (Input.GetKey(KeyCode.X))
        {
            StartCoroutine(PerformAction(CharacterState.Laugh, 1.5f));
        }
    }

    IEnumerator PerformAttack()
    {
        RpcChangeState(CharacterState.Attack);
        yield return new WaitForSeconds(0.3f);
        AttackNearestEnemy();
        yield return new WaitForSeconds(0.5f);
        RpcChangeState(CharacterState.Normal);
    }

    void AttackNearestEnemy()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, attackRange);
        float closestDistance = float.MaxValue;
        Health closestEnemy = null;

        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.CompareTag("Enemy"))
            {
                float distance = Vector3.Distance(transform.position, hitCollider.transform.position);
                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    closestEnemy = hitCollider.GetComponent<Health>();
                }
            }
        }

        if (closestEnemy != null)
        {
            closestEnemy.TakeDamage(attackDamage);
        }
    }

    IEnumerator PerformAction(CharacterState state, float duration)
    {
        RpcChangeState(state);
        yield return new WaitForSeconds(duration);
        RpcChangeState(CharacterState.Normal);
    }

    void HandleMovement()
    {
        if (curState != CharacterState.Normal) return;

        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical");
        bool sprintInput = Input.GetKey(KeyCode.LeftShift);

        float currentSpeed = sprintInput ? speed * 2 : speed;
        movementVelocity = new Vector3(horizontalInput, 0, verticalInput).normalized * currentSpeed;
        animator.SetFloat("Speed", movementVelocity.magnitude);

        if (movementVelocity != Vector3.zero)
        {
            transform.rotation = Quaternion.LookRotation(movementVelocity);
        }

        if (characterController.isGrounded)
        {
            movementVelocity.y = -gravity * Runner.DeltaTime;
        }
        else
        {
            movementVelocity.y += gravity * Runner.DeltaTime;
        }

        characterController.Move(movementVelocity * Runner.DeltaTime);
    }

    [Rpc(RpcSources.InputAuthority, RpcTargets.StateAuthority)]
    public void RpcChangeState(CharacterState newState, RpcInfo info = default)
    {
        if (curState == newState) return;
        curState = newState;

        animator.ResetTrigger("Jump");
        animator.ResetTrigger("Attack");
        animator.ResetTrigger("Laugh");
        animator.ResetTrigger("Hurt");
        animator.ResetTrigger("Die");

        switch (newState)
        {
            case CharacterState.Jump:
                animator.SetTrigger("Jump");
                movementVelocity.y = Mathf.Sqrt(2 * jumpHeight * gravity);
                break;
            case CharacterState.Attack:
                animator.SetTrigger("Attack");
                break;
            case CharacterState.Laugh:
                animator.SetTrigger("Laugh");
                break;
            case CharacterState.Hurt:
                animator.SetTrigger("Hurt");
                break;
            case CharacterState.Die:
                sword.transform.SetParent(null);
                sword.GetComponent<Rigidbody>().isKinematic = false;
                animator.SetTrigger("Die");
                break;
        }
    }
}
