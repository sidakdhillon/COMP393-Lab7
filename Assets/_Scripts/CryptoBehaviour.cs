using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.AI;

public enum CryptoState
{
    IDLE,
    RUN,
    JUMP,
    KICK
}





public class CryptoBehaviour : MonoBehaviour
{
    [Header("Line of Sight")] 
    public bool HasLOS;

    public GameObject player;
    private NavMeshAgent agent;
    private Animator animator;


    [Header("Attack")] 
    public float distance;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        if (HasLOS)
        {
            agent.SetDestination(player.transform.position);
        }


        if(HasLOS && Vector3.Distance(transform.position, player.transform.position) < distance)
        {
                // could be an attack
            animator.SetInteger("AnimState", (int)CryptoState.KICK );
            transform.LookAt(transform.position - player.transform.forward);

            if (agent.isOnOffMeshLink)
            {
                animator.SetInteger("AnimState", (int)CryptoState.JUMP);
            }
        }
        else
        {
            animator.SetInteger("AnimState", (int)CryptoState.RUN);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            HasLOS = true;
            player = other.transform.gameObject;
        }
    }

}

