using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RifleController : MonoBehaviour
{
    [SerializeField]
    private GameObject bulletPrefab;
    [SerializeField]
    private float shotforce;
    [SerializeField]
    private GameObject Player;
    [SerializeField]
    private Transform spawnposition;
    [SerializeField]
    private Transform shotposition;
    [SerializeField]
    private int ammo = 30;
    private int otherammo = 100;
    private bool reload = false;
    private bool autoshot = false;
    private Animator animator;
    [SerializeField]
    private Text showammo;
    [SerializeField]
    private GameObject lefthand;
    private void Start()
    {
        animator = GetComponent<Animator>();
        showammo.text = ammo + "/30";
    }
    private void Update()
    {
        if (!GetComponentInParent<CharacterContol>().panelact)
        {
            otherammo = Player.GetComponent<CharacterContol>().ammo;
            showammo.text = ammo + "/30";
            if (!autoshot)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    if (ammo > 0 && !reload)
                    {
                        Transform bullet = Instantiate(bulletPrefab.transform, spawnposition.position, Quaternion.identity);
                        bullet.Rotate(new Vector3(90f, 0, 0));
                        Rigidbody rigidbody = bullet.GetComponent<Rigidbody>();
                        rigidbody.AddForce(shotposition.forward * -shotforce, ForceMode.Impulse);
                        animator.Play("Shot");
                        ammo--;

                    }
                    if (ammo < 1)
                    {
                        animator.Play("Empty");
                    }
                }
            }
            else
            {
                if (Input.GetMouseButton(0))
                {
                    if (ammo > 0 && !reload)
                    {
                        Transform bullet = Instantiate(bulletPrefab.transform, spawnposition.position, Quaternion.identity);
                        bullet.Rotate(new Vector3(90f, 0, 0));
                        Rigidbody rigidbody = bullet.GetComponent<Rigidbody>();
                        rigidbody.AddForce(shotposition.forward * -shotforce, ForceMode.Impulse);
                        animator.Play("Shot");
                        ammo--;

                    }
                    if (ammo < 1)
                    {
                        animator.Play("Empty");
                    }
                }
            }
            if (Input.GetKeyDown(KeyCode.R))
            {
                if (otherammo > 0)
                {
                    StartCoroutine("Reload");
                    reload = true;
                }
                else
                {
                    //нет патронов
                }
            }
            if (Input.GetKeyDown(KeyCode.V))
            {
                autoshot = true;
            }
        }
    }
    IEnumerator Reload()
    {
        animator.Play("Reloading");
        lefthand.GetComponent<Animator>().Play("ReloadPistol");
        yield return new WaitForSeconds(3);
        Player.GetComponent<CharacterContol>().AddAmmo(-30+ammo);
        ammo = 30;
        showammo.text = ammo + "/30";
        reload = false;
    }

}
