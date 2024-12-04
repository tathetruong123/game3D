using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enermy : MonoBehaviour
{
    public NavMeshAgent navMeshAgent;
    public Transform target; // mục tiêu

    public float radius = 10f; // bán kính tìm kiếm mục tiêu
    public Vector3 originalePosition; // vị trí ban đầu
    public float maxDistance = 50f; // khoảng cách tối đa



    public Animator animator; // khai báo component


    public DamageZone damageZone;
    public BoxCollider damBox;

    public Health health;



    // state machine
    public enum CharacterState
    {
        Normal,
        Attack,
        Die
    }
    public CharacterState currentState; // trạng thái hiện tại


    void Start()
    {
        damBox.enabled = false;
        originalePosition = transform.position;

        // Tự động tìm đối tượng với tag "Player"
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            target = player.transform;
        }
        else
        {
            Debug.LogError("Không tìm thấy đối tượng có tag 'Player'. Hãy gán target thủ công trong Inspector.");
        }
    }


    void Update()
    {
        if (health.currenHP <= 0)
        {
            ChangeState(CharacterState.Die);
        }
        if (currentState == CharacterState.Die)
        {
            return;
        }
        // khoảng cách từ vị trí hiện tại đến vị trí ban đầu
        var distanceToOriginal = Vector3.Distance(originalePosition, transform.position);
        // khoảng cách từ vị trí hiện tại đến mục tiêu
        var distance = Vector3.Distance(target.position, transform.position);
        if (distance <= radius && distanceToOriginal <= maxDistance)
        {
            // di chuyển đến mục tiêu
            navMeshAgent.SetDestination(target.position);
            animator.SetFloat("Speed", navMeshAgent.velocity.magnitude);

            distance = Vector3.Distance(target.position, transform.position);
            if (distance < 2f)
            {
                // tấn công
                ChangeState(CharacterState.Attack);
            }
        }

        if (distance > radius || distanceToOriginal > maxDistance)
        {
            // quay về vị trí ban đầu
            navMeshAgent.SetDestination(originalePosition);
            animator.SetFloat("Speed", navMeshAgent.velocity.magnitude);

            // chuyển sang trạng thái đứng yên
            distance = Vector3.Distance(originalePosition, transform.position);
            if (distance < 1f)
            {
                animator.SetFloat("Speed", 0);
            }

            // bình thường
            ChangeState(CharacterState.Normal);
        }


    }

    // chuyển đổi trạng thái
    private void ChangeState(CharacterState newState)
    {
        // exit current state
        switch (currentState)
        {
            case CharacterState.Normal:
                break;
            case CharacterState.Attack:
                break;
        }

        // enter new state
        switch (newState)
        {
            case CharacterState.Normal:
                damageZone.EndAttack();
                break;
            case CharacterState.Attack:
                animator.SetTrigger("Attack");
                damageZone.BeginAttack();
                break;
            case CharacterState.Die:
                animator.SetTrigger("Die");
                Destroy(gameObject, 5f);
                break;
        }

        // update current state
        currentState = newState;
    }

    public void ZomAttack()
    {
        damBox.enabled = true;
    }
    public void EndAttack()
    {
        damBox.enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("katana"))
        {
            health.TakeDamage(20);
        }
    }
}
