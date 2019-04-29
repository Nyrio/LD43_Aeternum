using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudMovement : MonoBehaviour
{
    public float maxSpeed = 1f;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Rigidbody2D>().velocity = new Vector2(maxSpeed, 0);
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 position = GetComponent<Transform>().position;

        if (position.x > 60)
        {
            position.x = -15;
        }

        GetComponent<Transform>().position = position;
    }
}
