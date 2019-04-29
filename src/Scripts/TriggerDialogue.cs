using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerDialogue : MonoBehaviour
{
    public int entryState;
    public int nextState;
    public GameObject conversationHolder;
    public GameObject player;

    private ConversationManager conversationManager;

    // Start is called before the first frame update
    void Start()
    {
        conversationManager = conversationHolder.GetComponent<ConversationManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.name == player.name && conversationManager.getState() == entryState)
        {
            conversationManager.setState(nextState);
            player.GetComponent<PlayerMovement>().stopMovement();
        }
    }
}
