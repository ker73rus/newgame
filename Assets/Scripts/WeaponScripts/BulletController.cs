using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BulletController : MonoBehaviour
{
    [SerializeField]
    private Text scoretext;
    private void Start()
    {
        StartCoroutine("Destroing");
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "score100")
        {
            other.GetComponent<TargetController>().Hit(100);
            Destroy(gameObject);
        }
        else if(other.tag == "score10")
        {
            other.GetComponent<TargetController>().Hit(10);
            Destroy(gameObject);
        }
        else if(other.tag == "score50")
        {
            other.GetComponent<TargetController>().Hit(50);
            Destroy(gameObject);
        }
        if (other.CompareTag("shield"))
        {
            Destroy(gameObject);
        }
    }
    IEnumerator Destroing()
    {
        yield return new WaitForSeconds(2);
        Destroy(gameObject);
    }
}
