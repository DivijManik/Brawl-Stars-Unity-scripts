using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttacking_Shoot : MonoBehaviour
{
    [SerializeField]
    LineRenderer LR;

    [SerializeField]
    Joystick AttackJoystick;

    [SerializeField]
    Transform AttackLookAtPoint;

    [SerializeField]
    public float TrailDistance;

    [SerializeField]
    Transform Player;

    RaycastHit hit;

    bool Shoot;

    [SerializeField]
    Transform Bullet;

    [SerializeField]
    float BulletYAxis;

    [SerializeField]
    int NoOfBullets;

    [SerializeField]
    Transform PlayerSpine;

    [SerializeField]
    Transform PlayerSpineChild;

    [SerializeField]
    Transform PlayerHips;

    [SerializeField]
    Animator PlayerAnimator;

    void Start()
    {
        PlayerSpineChild = PlayerSpine.GetChild(0);
        PlayerAnimator = Player.GetComponent<Animator>();
    }

    void Update()
    {
        if (Mathf.Abs(AttackJoystick.Horizontal) > 0.3f || Mathf.Abs(AttackJoystick.Vertical) > 0.3f)
        {
            if (LR.gameObject.activeInHierarchy == false)
            {
                LR.gameObject.SetActive(true);
            }

            transform.position = new Vector3(Player.position.x, -1.54f, Player.position.z);

            AttackLookAtPoint.position = new Vector3(AttackJoystick.Horizontal + Player.position.x, -1.54f, AttackJoystick.Vertical + Player.position.z);

            transform.LookAt(new Vector3(AttackLookAtPoint.position.x, 0, AttackLookAtPoint.position.z));

            transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);

            LR.SetPosition(0, transform.position);

            if (Physics.Raycast(transform.position, transform.forward, out hit, TrailDistance))
            {
                LR.SetPosition(1, hit.point);
            }

            else
            {
                LR.SetPosition(1, transform.position + transform.forward * TrailDistance);

            }

            if (Shoot == false)
            {
                Shoot = true;
            }
        }
        else if (Mathf.Abs(AttackJoystick.Horizontal) < 0.3f || Mathf.Abs(AttackJoystick.Vertical) < 0.3f && LR.gameObject.activeInHierarchy == true)
        {
            LR.gameObject.SetActive(false);
            Shoot = false;
        }

    }

    public void PlayerShooting()
    {
        if (Shoot)
        {
            StartCoroutine(ShootBullet());

            Shoot = false;
        }
    }

    IEnumerator ShootBullet()
    {
        float transformRot = (transform.eulerAngles.y > 180) ? transform.eulerAngles.y - 360 : transform.eulerAngles.y;

        float PlayerHipRot = (PlayerHips.eulerAngles.y > 180) ? PlayerHips.eulerAngles.y - 360 : PlayerHips.eulerAngles.y;


        PlayerAnimator.SetFloat("Blend", 1);
        

        if (transformRot - PlayerHipRot > 90)
        {
            PlayerHips.eulerAngles = new Vector3(PlayerHips.eulerAngles.x, PlayerHips.eulerAngles.y + 90, PlayerHips.eulerAngles.z);
        }
        else if (transformRot - PlayerHipRot < -90)
        {
            PlayerHips.eulerAngles = new Vector3(PlayerHips.eulerAngles.x, PlayerHips.eulerAngles.y - 90, PlayerHips.eulerAngles.z);
        }


        PlayerSpine.LookAt(AttackLookAtPoint);

        PlayerSpine.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);

        Instantiate(Bullet, new Vector3(transform.position.x, BulletYAxis, transform.position.z), transform.rotation);

        for (int i = 0; i < NoOfBullets - 1; i++)
        {
            yield return new WaitForSeconds(0.2f);

            Instantiate(Bullet, new Vector3(transform.position.x, BulletYAxis, transform.position.z), transform.rotation);
        }

        PlayerSpine.localRotation = PlayerSpineChild.localRotation;
        PlayerHips.eulerAngles = new Vector3(0f,Player.eulerAngles.y,0f);
        //StartCoroutine(ShootBullet());


        PlayerAnimator.SetFloat("Blend", 0);

    }
}
