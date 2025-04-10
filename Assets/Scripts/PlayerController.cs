using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Animator animator;
    public CharacterController characterController;

    public float moveSpeed = 20;

    public Collider[] swordColliders;

    public LayerMask layerMask;
    public GameObject laserDot;

    void Start()
    {
        EndAttack();
    }
    
    void Update()
    {
        Vector3 moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        characterController.SimpleMove(moveDirection.normalized * moveSpeed);
        animator.SetFloat("Speed", (moveDirection * moveSpeed).magnitude);

        //searching for laser hit
        RaycastHit hit;
        //installing laser
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        //Debug.Log("Sveiki! Te bus teksts");
        Debug.DrawRay(ray.origin, ray.direction * 500, Color.red);
        //Shooting laser
        if (Physics.Raycast(ray, out hit, 500, layerMask, QueryTriggerInteraction.Ignore))
        {
            laserDot.transform.position = hit.point;


            Vector3 targetPosition = new Vector3(hit.point.x, transform.position.y, hit.point.z);
            Quaternion rotation = Quaternion.LookRotation(targetPosition - transform.position);
            //transform.rotation = rotation;
            //make it slow
            transform.rotation = Quaternion.Lerp(transform.rotation, rotation, Time.deltaTime*10f);
        }


        //left
        if (Input.GetMouseButtonDown(0))
        {
            animator.Play("Stab");
        }
        //right
        if (Input.GetMouseButtonDown(1))
        {
            animator.Play("Spin Attack");
        }
        //middle
        if (Input.GetMouseButtonDown(2))
        {
            
        }



    }


    public void BeginAttack()
    {
        foreach (Collider swordCollider in swordColliders)
        {
            swordCollider.enabled = true;
        }
    }
    public void EndAttack()
    {
        foreach (Collider swordCollider in swordColliders)
        {
            swordCollider.enabled = false;
        }
    }

}
