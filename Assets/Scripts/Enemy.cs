using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private float speed;
    [SerializeField]
    private float hittime = 3;
    [SerializeField]
    private float distanceVisibility;
    [SerializeField]
    private float distanceAttack;
    [SerializeField]
    private Transform player;
    private Animator animator;
    private bool isAttack;
    private bool wait = false;
    [SerializeField]
    private Transform[] points;
    private int curIndexPoint;
    public bool IsAttack {
        get {
            return isAttack;
        }
    }

    private void Start()
    {
        animator = GetComponent<Animator>();
        curIndexPoint = 0;
    }

    private void Update()
    {
        
        float distance =
            Vector3.Distance(transform.position, player.position);
        if(distance <= distanceAttack)
        {
            if (!isAttack)
            { 
                animator.SetTrigger("Attack");
                StartCoroutine(Attack());
            }
            animator.SetBool("IsWalk", false);
        }
        else if (distance <= distanceVisibility)
        {
            transform.LookAt(new Vector3(
                player.position.x,
                transform.position.y,
                player.position.z));
            if (!isAttack)
            {
                transform.Translate(Vector3.forward * speed * Time.deltaTime);
                animator.SetBool("IsWalk", true);
            }
        }
        else
        {
            if (!wait)
            {
                Transform point = points[curIndexPoint];
                transform.LookAt(new Vector3(point.position.x, transform.position.y, point.position.z));
                transform.Translate(Vector3.forward * speed * Time.deltaTime);
                animator.SetBool("IsWalk", true);
                if (Vector3.Distance(transform.position, point.position) < 2)
                {
                    StartCoroutine(Next());
                }
            }
        }
    }
    IEnumerator Attack()
    {
        isAttack = true;
        animator.SetTrigger("Attack");
        yield return new WaitForSeconds(hittime);
        isAttack = false;
    }
   IEnumerator Next()
    {
        wait = true;
        animator.SetBool("IsWalk", false);
        yield return new WaitForSeconds(2);
        curIndexPoint = (curIndexPoint + 1) % points.Length;
        wait = false;
    }
}

