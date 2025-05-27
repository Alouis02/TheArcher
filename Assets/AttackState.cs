using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class AttackState : StateMachineBehaviour
{
    private NavMeshAgent agent;
    private Transform player;
    private float attackRange = 2f;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        agent = animator.GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player")?.transform;

        if (agent != null)
        {
            agent.isStopped = true; // Stop movement during attack
        }

        if (player != null)
        {
            animator.transform.LookAt(player);
            animator.SetTrigger("Attack"); // Trigger attack animation
        }
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (player == null || agent == null) return;

        float distance = Vector3.Distance(player.position, animator.transform.position);

        // If player moves out of attack range, transition back to chase
        if (distance > attackRange)
        {
            agent.isStopped = false;
            animator.SetBool("IsAttacking", false);
            animator.SetBool("IsChasing", true);
        }
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (agent != null)
        {
            agent.isStopped = false; // Resume movement
            agent.ResetPath(); // Clear any unwanted movement commands
        }
    }
}