using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[System.Serializable]
public class DialogueOption
{
    public string text;
    public int next;
}

[System.Serializable]
public class DialogueLine
{
    public int n;
    public string type;
    public string text;
    public DialogueOption[] options;
    public int next;
}

[System.Serializable]
public class DialogueInfo
{
    public DialogueLine[] dialogue;
}

public class ConversationManager : MonoBehaviour
{
    public TextAsset dialogueJson;
    public GameObject myMessage;
    public GameObject otherMessage;
    public GameObject myChoiceTop;
    public GameObject myChoiceBottom;

    private int state;
    private DialogueLine[] dialogue;
    private bool waitingToPass;
    private bool waitingForChoice;
    private int currentChoice;
    private int nextState;
    private bool submitDown;

    // Start is called before the first frame update
    void Start()
    {
        string json = dialogueJson.ToString();
        DialogueInfo dialogueInfo = JsonUtility.FromJson<DialogueInfo>(json);
        dialogue = dialogueInfo.dialogue;

        setState(0);
        submitDown = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!Input.GetButton("Submit")) submitDown = false;

        if(waitingToPass && !submitDown && Input.GetButton("Submit"))
        {
            setState(nextState);
            submitDown = true;
        }
        else if(waitingForChoice)
        {
            if(Input.GetAxis("Vertical") > 0.2 && currentChoice != 0)
            {
                currentChoice = 0;
                myChoiceTop.GetComponent<DialogueManager>().hightlight(true);
                myChoiceBottom.GetComponent<DialogueManager>().hightlight(false);
            }
            else if(Input.GetAxis("Vertical") < -0.2 && currentChoice != 1)
            {
                currentChoice = 1;
                myChoiceTop.GetComponent<DialogueManager>().hightlight(false);
                myChoiceBottom.GetComponent<DialogueManager>().hightlight(true);
            }
            if (!submitDown && Input.GetButton("Submit"))
            {
                setState(dialogue[state].options[currentChoice].next);
                submitDown = true;
            }
        }
    }

    public void setState(int newState)
    {
        // Clear current state
        myMessage.GetComponent<DialogueManager>().hide();
        otherMessage.GetComponent<DialogueManager>().hide();
        myChoiceTop.GetComponent<DialogueManager>().hide();
        myChoiceBottom.GetComponent<DialogueManager>().hide();
        waitingToPass = false;
        waitingForChoice = false;

        state = newState;

        switch (dialogue[state].type) {
            case "npcline":
                otherMessage.GetComponent<DialogueManager>().showMessage(dialogue[state].text);
                waitingToPass = true;
                nextState = dialogue[state].next;
                break;
            case "playerline":
                myMessage.GetComponent<DialogueManager>().showMessage(dialogue[state].text);
                waitingToPass = true;
                nextState = dialogue[state].next;
                break;
            case "choice":
                otherMessage.GetComponent<DialogueManager>().showMessage(dialogue[state].text);
                myChoiceTop.GetComponent<DialogueManager>().showMessage(dialogue[state].options[0].text);
                myChoiceBottom.GetComponent<DialogueManager>().showMessage(dialogue[state].options[1].text);
                waitingForChoice = true;
                currentChoice = 0;
                myChoiceTop.GetComponent<DialogueManager>().hightlight(true);
                myChoiceBottom.GetComponent<DialogueManager>().hightlight(false);
                break;
            case "changescene":
                SceneManager.LoadScene(dialogue[state].text, LoadSceneMode.Single);
                break;
            default:
                break;
        }
    }

    public bool blocksMovement()
    {
        return (waitingForChoice || waitingToPass);
    }

    public int getState()
    {
        return state;
    }
}
