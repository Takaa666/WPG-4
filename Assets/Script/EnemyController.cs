using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;


public class EnemyController : MonoBehaviour
{
    public Transform player; 
    //public Animator animator; 

    public float moveSpeed = 5f; 
    public float rotationSpeed = 5f; 
    public float chaseRange = 10f; 
    public float deathRange = .75f;

    private void Update()
    {
        float distance = Vector3.Distance(player.position, transform.position);

        ChasePlayer(distance); 
        //PlayerDeath(distance); 
    }

    /*private void PlayerDeath(float distance)
    {
        if (distance < deathRange)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }*/

    private void ChasePlayer(float distance)
    {
        if (distance < chaseRange)
        {
            Vector3 direction = (player.position - transform.position).normalized;

            transform.position += direction * moveSpeed * Time.deltaTime;

            Quaternion lookRotation = Quaternion.LookRotation(direction);

            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, rotationSpeed * Time.deltaTime);

            
            //animator.SetBool("isRunning", true);
        }
        else
        {
            //animator.SetBool("isRunning", false);
        }
    }

}
