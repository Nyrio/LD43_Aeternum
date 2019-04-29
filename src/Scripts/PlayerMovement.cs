using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public bool facingRight = true;
    public float maxSpeed = 10f;
    public GameObject conversationHolder;

    private ConversationManager conversationManager;
    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        conversationManager = conversationHolder.GetComponent<ConversationManager>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (conversationManager.blocksMovement()) return;

        float move = Input.GetAxis("Horizontal");
        GetComponent<Rigidbody2D>().velocity = new Vector2(move * maxSpeed, 0);

        bool newDirection = (move > 0);
        if (move != 0 && newDirection != facingRight) Flip();

        animator.SetFloat("speed", Mathf.Abs(move));
    }

    void Flip()
    {
        facingRight = !facingRight;
        GetComponent<Transform>().localScale = Vector3.Scale(GetComponent<Transform>().localScale, new Vector3(-1, 1, 1));
    }

    public void stopMovement()
    {
        GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
        animator.SetFloat("speed", 0f);
    }
}
