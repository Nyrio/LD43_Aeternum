using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 cameraPosition = GetComponent<Transform>().position;
        Vector3 playerPosition = player.GetComponent<Transform>().position;

        if(cameraPosition.x > playerPosition.x + 3)
        {
            cameraPosition.x = playerPosition.x + 3;
        }
        else if (cameraPosition.x < playerPosition.x - 3)
        {
            cameraPosition.x = playerPosition.x - 3;
        }

        GetComponent<Transform>().position = cameraPosition;
    }
}
