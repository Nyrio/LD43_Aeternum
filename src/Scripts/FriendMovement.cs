using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FriendMovement : MonoBehaviour
{
    public GameObject player;
    public bool facingRight = true;
    public float maxSpeed = 10f;

    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        animator.SetBool("idle", true);
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 friendPosition = GetComponent<Transform>().position;
        Vector3 playerPosition = player.GetComponent<Transform>().position;
   
        bool nowFacingRight = facingRight;

        if (friendPosition.x > playerPosition.x + 5)
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(-maxSpeed, 0);
            nowFacingRight = false;
            animator.SetBool("idle", false);
        }
        else if (friendPosition.x < playerPosition.x - 3)
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(maxSpeed, 0);
            nowFacingRight = true;
            animator.SetBool("idle", false);
        }
        else if(friendPosition.x > playerPosition.x - 2 && friendPosition.x < playerPosition.x + 3)
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
            animator.SetBool("idle", true);
        }

        if (facingRight != nowFacingRight) Flip();
    }

    void Flip()
    {
        facingRight = !facingRight;
        GetComponent<Transform>().localScale = Vector3.Scale(GetComponent<Transform>().localScale, new Vector3(-1, 1, 1));
    }
}
