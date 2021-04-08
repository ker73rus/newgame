using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectInteraction : MonoBehaviour
{
    [SerializeField]
    private bool box = false;
    private Collider curbox;
    [SerializeField]
    private GameObject Player;
    private void OnTriggerStay(Collider other)
    {
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("ammobox"))
        {
            box = true;
            curbox = other;
            other.GetComponent<AmmoBox>().OutLine.SetActive(true);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("ammobox"))
        {
            box = false;
            curbox = null;
            other.GetComponent<AmmoBox>().OutLine.SetActive(false);
        }    
    }
    private void Update()
    {
        if(box && Input.GetKeyDown(KeyCode.E))
        {
            Player.GetComponent<CharacterContol>().AddAmmo(curbox.gameObject.GetComponent<AmmoBox>().countAmmo);

        }
    }
}
