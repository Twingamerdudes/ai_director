using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    [Header("Movement Settings")]
    [Tooltip("The speed at which the character moves.")]
    [SerializeField]
    private float moveSpeed = 3f;

    public IEnumerator MoveCharacterTo(float x, float z)
    {

        transform.LookAt(new Vector3(x, 0.67f, z));
        //while the character is not at the coords
        while (Math.Abs(transform.position.x - x) > 0.05 && Math.Abs(transform.position.z - z) > 0.05)
        {
            //move the character to the coords
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(x, 0.67f, z), moveSpeed * Time.deltaTime);
            yield return null;
        }
    }
}
