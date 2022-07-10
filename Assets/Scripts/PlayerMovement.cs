using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed;
    [HideInInspector] public bool isMoving = true;
    private Rigidbody2D rb;
    public Vector2 movement;
    public Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    private void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        anim.SetFloat("PlayerXDirection", movement.x);
        anim.SetFloat("PlayerYDirection", movement.y);
        anim.SetFloat("Speed", movement.magnitude);

        if(movement.magnitude > 0)
        {
            anim.SetFloat("LastXDirection", movement.x);
            anim.SetFloat("LastYDirection", movement.y);
        }
    }

    private void FixedUpdate()
    {
        if (isMoving)
            rb.MovePosition(rb.position + movement.normalized * speed * Time.deltaTime);
    }
}
