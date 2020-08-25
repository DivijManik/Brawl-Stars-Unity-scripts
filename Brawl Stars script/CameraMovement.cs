using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField]
    Transform Player;

    [SerializeField]
    float offsetZ = -2.43f;

    [SerializeField]
    float offsetY = -2.43f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position = new Vector3( Player.position.x, offsetY, Player.position.z + offsetZ);
    }
}
