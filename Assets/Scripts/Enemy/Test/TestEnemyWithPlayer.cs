using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestEnemyWithPlayer : MonoBehaviour
{
    [SerializeField, Range(0f, 10f)] private float moveSpeed = 5.0f;



    private void Update()
    {

        float forward = Input.GetAxis("Horizontal");
        float right = Input.GetAxis("Vertical");


        Vector3 direction = new Vector3(right, 0.0f, forward);

        transform.position += direction * moveSpeed * Time.deltaTime;


    }


}
