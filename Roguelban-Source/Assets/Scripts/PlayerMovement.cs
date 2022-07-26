using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody2D Player;
    public float speed;

    // Start is called before the first frame update
    void Start()
    {
        Player=gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //If we switch to the new input system then this may change but should not be a difficult migration
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical");

        Player.velocity = new Vector2(horizontalInput, verticalInput).normalized * speed;
    }
}
