using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowInteraction : MonoBehaviour
{
    private Coroutine coroutine;

    private void OnEnable()
    {
        coroutine = StartCoroutine(CurCoroutine());
    }

    private void OnCollisionEnter(Collision other)
    {
        gameObject.SetActive(false);
        GetComponent<Rigidbody>().velocity = Vector3.zero;
        StopCoroutine(coroutine);
    }

    private IEnumerator CurCoroutine()
    {
        yield return new WaitForSeconds(2f);
        GetComponent<Rigidbody>().velocity = Vector3.zero;
        gameObject.SetActive(false);
    }
}
