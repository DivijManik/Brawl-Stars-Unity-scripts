using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    [SerializeField]
    Joystick joystick;

    [SerializeField]
    Transform PlayerSprite;

    [SerializeField]
    Animator animator;

    bool Movement;

    // Start is called before the first frame update
    void Start()
    {
        PlayerSprite.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        

        if(Mathf.Abs(joystick.Direction.x) > 0f || Mathf.Abs(joystick.Direction.y) > 0f)
        {
            PlayerSprite.gameObject.SetActive(true);
            PlayerSprite.position = new Vector3(joystick.Horizontal + transform.position.x, -1.54f, joystick.Vertical + transform.position.z);

            transform.LookAt(new Vector3(PlayerSprite.position.x, 0, PlayerSprite.position.z));

            transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);

            transform.Translate(Vector3.forward * Time.deltaTime);

            if (animator.GetBool("Walking") != true)
            {
                animator.SetBool("Walking", true);
            }

            Movement = true;
        }
        else if(Movement == true)
        {
            PlayerSprite.gameObject.SetActive(false);
            animator.SetBool("Walking", false);

            Movement = false;
        }

    }
}
