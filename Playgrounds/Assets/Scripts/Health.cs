using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public int health;

    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void TakeDamage(int damage)
    {
        health -= damage;

        CheckIfWeDead();
    }

    private void CheckIfWeDead()
    {
        if(health <= 0)
        {
            health = 0;
            if (gameObject.name == "kucuk_legorg")
            {
                Debug.Log("You are died " + gameObject.name);
            }
            else
            {
                animator.SetBool("Destroying", true);
                Destroy(gameObject, 1f);
            }
        }
    }


}
