using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterContol : MonoBehaviour
{
    //передвижение
    [SerializeField]
    private float speed = 4f;
    private float sspeed = 24f;
    [SerializeField]
    private float stamina = 400;  //выносливость !!!!!!поправить!!!!!!!
    [SerializeField]
    private float jspeed = 16f;
    [SerializeField]
    private float gravity = 20f;
    [SerializeField]
    private Vector3 moveDir = Vector3.zero;
    private CharacterController controller;
    [SerializeField]
    private int doublejump = 0;

    public bool panelact = false;
    [SerializeField]
    private GameObject panel;
    private bool reload = false;

    [SerializeField]
    private Image heals;
    private float hp = 100;

    //оружия
    public int ammo = 100;
    public GameObject pistol;
    public GameObject rifle;
    public GameObject shotgun;
    public GameObject snipe;
    public GameObject hand;
    public Transform handposition;
    [SerializeField]
    private Text showammo;
    void Start()
    {
        handposition = hand.transform;
        controller = GetComponent<CharacterController>();
        showammo.text = "" + ammo;
    }
    public void AddAmmo(int ammo)
    {
        this.ammo += ammo;
        showammo.text = "" + this.ammo;
    }
    private void FixedUpdate()
    {
        if(stamina < 400)
        stamina++;
    }
    // Update is called once per frame
    void Update()
    {
        if (panel.activeSelf == false)
        {
            panelact = false;
        }
        if (Input.GetKey(KeyCode.Escape) && panelact == false)
        {
            Cursor.lockState = CursorLockMode.Confined;
            panelact = true;
            panel.SetActive(true);
        }
        if (!panelact)
        {   
            if(!reload)
                if (Input.GetKeyDown(KeyCode.Alpha1))
                {
                    WeaponSwitch("fist");
                }
                else if (Input.GetKeyDown(KeyCode.Alpha2))
                {
                    WeaponSwitch("pistol");
                }
                else if (Input.GetKeyDown(KeyCode.Alpha3))
                {
                    WeaponSwitch("shotgun");
                }
                else if (Input.GetKeyDown(KeyCode.Alpha4))
                {
                    WeaponSwitch("rifle");
                }
                else if (Input.GetKeyDown(KeyCode.Alpha5))
                {
                    WeaponSwitch("snipe");
                }


            if (controller.isGrounded)
            {
                doublejump = 0;
                if ((Input.GetKey(KeyCode.LeftShift)) && stamina > 0)
                {
                    moveDir = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
                    moveDir = transform.TransformDirection(moveDir);
                    moveDir *= sspeed;
                    stamina -= 2;
                }
                else
                {
                    moveDir = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
                    moveDir = transform.TransformDirection(moveDir);
                    moveDir *= speed;
                }
            }
            if ((Input.GetKeyDown(KeyCode.Space)) && doublejump != 2)
            {
                moveDir.y = jspeed;
                doublejump++;
            }
            moveDir.y -= gravity * Time.deltaTime;
            controller.Move(moveDir * Time.deltaTime);
            if (Input.GetKeyDown(KeyCode.R))
            {
                StartCoroutine("Reload");
            }
        }
    }
    public void WeaponSwitch(string weaponame)
    {
        //hand.SetActive(false);
        shotgun.SetActive(false);
        rifle.SetActive(false);
        pistol.SetActive(false);
        snipe.SetActive(false);
        
        if (weaponame == "fist")
        {
            hand.SetActive(true);
        }
        else if(weaponame == "pistol")
        {
            pistol.SetActive(true);
        }
        else if(weaponame == "rifle")
        {
            rifle.SetActive(true);
        }
        else if(weaponame == "shotgun")
        {
            shotgun.SetActive(true);
        }
        else if(weaponame == "snipe")
        {
            snipe.SetActive(true);
        }
    }
    IEnumerator Reload()
    {
        reload = true;
        yield return new WaitForSeconds(3);
        reload = false;
    }
    public void Damage()
    {
        hp -= 10;
        heals.fillAmount = hp / 100;
        print("ok");
    }
}
