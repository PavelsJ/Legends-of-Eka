using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMouseControl : MonoBehaviour
{
    public float moveSpeed = 10f;
    public LayerMask layerMask;
    
    private CharacterController characterController;
    private Vector3 targetPos;
    private Animator animator;
    private bool isRunning = false;

    private void Start()
    {
        characterController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        
        targetPos = transform.position;
    }
    
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            
            if (Physics.Raycast(ray, out hit, 500, layerMask))
            {
                targetPos = hit.point;
            }
        }

        if (Vector3.Distance(transform.position, targetPos) > 1f)
        {
            isRunning = true;
            Vector3 targetDir = Vector3.Normalize(targetPos - transform.position);
            characterController.Move(targetDir * (moveSpeed * Time.deltaTime));
            
            transform.LookAt(targetPos);
        }
        else
        {
            isRunning = false;
        }
        
        animator.SetBool("Run", isRunning);
    }
}
