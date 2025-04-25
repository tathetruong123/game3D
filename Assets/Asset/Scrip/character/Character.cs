using System.Collections;
using UnityEngine;
using Fusion;

public class Character : NetworkBehaviour
{
    public CharacterController characterController;
    public float speed = 2f;
    private Vector3 velocity;
    public Animator animator;
    public float gravity = 20f;
    public float jumpForce = 8f;
    public float attackRange = 2.5f;
    public int attackDamage = 30;

    public enum CharacterState
    {
        Normal,
        Attack,
        Jump,
        Laugh,
        Die
    }

    [Networked] public CharacterState CurrentState { get; set; }

    public override void FixedUpdateNetwork()
    {
        if (!HasInputAuthority || CurrentState == CharacterState.Die) return;

        HandleInput();
        HandleMovement();
    }

    void HandleInput()
    {
        if (CurrentState != CharacterState.Normal) return;

        if (Input.GetKeyDown(KeyCode.K))
        {
            StartCoroutine(PerformAttack());
        }
        else if (Input.GetKeyDown(KeyCode.J) && characterController.isGrounded)
        {
            Jump();
        }
        else if (Input.GetKeyDown(KeyCode.X))
        {
            StartCoroutine(PerformAction(CharacterState.Laugh, 1.5f));
        }
    }

    void HandleMovement()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");
        bool isSprinting = Input.GetKey(KeyCode.LeftShift);
        float moveSpeed = isSprinting ? speed * 2 : speed;

        Vector3 inputDir = new Vector3(h, 0, v).normalized;

        if (inputDir.magnitude > 0)
        {
            Vector3 move = inputDir * moveSpeed;
            move.y = velocity.y;
            velocity = move;

            characterController.Move(velocity * Runner.DeltaTime);
            transform.rotation = Quaternion.LookRotation(inputDir);

            animator.SetFloat("Speed", moveSpeed);
        }
        else
        {
            velocity.x = 0;
            velocity.z = 0;
            characterController.Move(velocity * Runner.DeltaTime);
            animator.SetFloat("Speed", 0f);
        }

        if (characterController.isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }
        else
        {
            velocity.y += -gravity * Runner.DeltaTime;
        }
    }

    void Jump()
    {
        velocity.y = jumpForce;
        RpcChangeState(CharacterState.Jump);
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
        Collider[] hits = Physics.OverlapSphere(transform.position, attackRange);
        foreach (var hit in hits)
        {
            if (hit.CompareTag("Enemy"))
            {
                var hp = hit.GetComponent<Health>();
                hp?.TakeDamage(attackDamage);
                break;
            }
        }
    }

    IEnumerator PerformAction(CharacterState state, float duration)
    {
        RpcChangeState(state);
        yield return new WaitForSeconds(duration);
        RpcChangeState(CharacterState.Normal);
    }

    [Rpc(RpcSources.InputAuthority, RpcTargets.All)]
    void RpcChangeState(CharacterState state)
    {
        CurrentState = state;
        UpdateAnimation(state);
    }

    void UpdateAnimation(CharacterState state)
    {
        animator.ResetTrigger("Jump");
        animator.ResetTrigger("Attack");
        animator.ResetTrigger("Laugh");
        animator.ResetTrigger("Die");

        switch (state)
        {
            case CharacterState.Jump: animator.SetTrigger("Jump"); break;
            case CharacterState.Attack: animator.SetTrigger("Attack"); break;
            case CharacterState.Laugh: animator.SetTrigger("Laugh"); break;
            case CharacterState.Die: animator.SetTrigger("Die"); break;
        }
    }
}
