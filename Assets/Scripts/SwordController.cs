using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordController : MonoBehaviour
{
    public Enemy enemy;
    private bool attack = false;
    private void OnTriggerEnter(Collider other)
    {
        if (enemy.IsAttack && !attack)
        {
            other.GetComponent<CharacterContol>().Damage();
            StartCoroutine(Attack());
        }
    }
    IEnumerator Attack()
    {
        attack = true;
        yield return new WaitForSeconds(1);
        attack = false;
    }
}
