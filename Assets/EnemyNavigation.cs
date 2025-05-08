using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyNavigation : MonoBehaviour
{
    [Header("Movement")]
    public Transform target;
    public float distance = 10;
    public float force = 1;
    
    [Header("Attack")]
    public GameObject arrow;
    public Transform arrowPoint;
    
    public float attackDelay = 2;  
    private float lastAttackTime = 0;
    
    private GameObject currentArrow;
    
    private NavMeshAgent agent;
    private Animator animator;

    private void Start()
    {
        currentArrow = Instantiate(arrow, transform.position, arrow.transform.rotation);
        currentArrow.SetActive(false);
        
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (EnemyStats.isAlive)
        {
            if (Vector3.Distance(transform.position, target.position) <= distance)
            {
                if (Time.time > lastAttackTime + attackDelay)
                {
                    lastAttackTime = Time.time;
                    Shoot();
                }
               
                agent.isStopped = true;
                transform.LookAt(target);
            }
            else
            {
                agent.isStopped = false;
                agent.SetDestination(target.position);
            }
            
            animator.SetFloat("Speed", agent.velocity.magnitude);
        }
        else
        {
            agent.isStopped = true;
        }
    }

    private void Shoot()
    {
        if (!currentArrow.activeSelf)
        {
            animator.SetTrigger("Shoot");
            
            if (arrowPoint != null)
            {
                currentArrow.transform.position = arrowPoint.position;
                currentArrow.SetActive(true);
            }
            
            Vector3 targetDir = new Vector3(target.position.x - transform.position.x, 0, target.position.z - transform.position.z);
            currentArrow.GetComponent<Rigidbody>().AddForce(targetDir * force , ForceMode.Impulse);
        }
       
    }
}
