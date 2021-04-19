using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SnipeController : MonoBehaviour
{
    [SerializeField]
    private GameObject bulletPrefab;
    [SerializeField]
    private GameObject Player;
    [SerializeField]
    private float shotforce;
    [SerializeField]
    private Transform spawnposition;
    [SerializeField]
    private Transform shotposition;
    [SerializeField]
    private int ammo = 7;
    private int otherammo;
    private bool reload = false;
    private Animator animator;
    [SerializeField]
    private Text showammo;
    [SerializeField]
    private GameObject lefthand;
    private void Start()
    {
        animator = GetComponent<Animator>();
        showammo.text = ammo + "/10";
    }
    private void Update()
    {
        if (!GetComponentInParent<CharacterContol>().panelact)
        {
            otherammo = Player.GetComponent<CharacterContol>().ammo;
            if (Input.GetMouseButtonDown(0))
            {
                if (ammo > 0 && !reload)
                {
                    Ray ray = new Ray(spawnposition.position ,(spawnposition.position - shotposition.position).normalized);
                    RaycastHit raycast;
                    if (Physics.Raycast(ray, out raycast, 1000))
                    {
                        if (raycast.transform.CompareTag("enemyhead"))
                        {

                        }
                    }
                    animator.Play("Shot");
                    ammo--;
                }
                if (ammo < 1)
                {
                    animator.Play("Empty");
                }
            }
            showammo.text = ammo + "/10";
            if (Input.GetKeyDown(KeyCode.R))
            {
                StartCoroutine("Reload");
                reload = true;
            }
        }
    }
    IEnumerator Reload()
    {
        animator.Play("Reloading");
        lefthand.GetComponent<Animator>().Play("ReloadPistol");
        yield return new WaitForSeconds(3);
        Player.GetComponent<CharacterContol>().AddAmmo(-10+ammo);
        ammo = 10;
        showammo.text = ammo + "/10";
        reload = false;
    }
}
