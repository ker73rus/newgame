using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PistolController : MonoBehaviour
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
        showammo.text = ammo + "/7";
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
            showammo.text = ammo + "/7";
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
        Player.GetComponent<CharacterContol>().AddAmmo(-7+ammo);
        ammo = 7;
        showammo.text = ammo + "/7";
        reload = false;
    }
}
