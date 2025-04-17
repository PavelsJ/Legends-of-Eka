using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyNavigation : MonoBehaviour
{
    public Transform target;
    public float distance = 10;
    public float force = 1;
    public GameObject arrow;
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
        if (Vector3.Distance(transform.position, target.position) < distance)
        {
            Shoot();
            agent.isStopped = true;
            transform.LookAt(target);
        }
        else
        {
            agent.isStopped = false;
            agent.SetDestination(target.position);
        }
    }

    private void Shoot()
    {
        if (!currentArrow.activeSelf)
        {
            animator.SetTrigger("Shoot");
            currentArrow.transform.position = transform.position + new Vector3(0,2);
            currentArrow.SetActive(true);
            
            Vector3 targetDir = new Vector3(target.position.x - transform.position.x, 0, target.position.z - transform.position.z);
            currentArrow.GetComponent<Rigidbody>().AddForce(targetDir * force , ForceMode.Impulse);
        }
       
    }
}
