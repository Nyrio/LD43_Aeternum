using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{

    public GameObject dBox;
    public Text dText;

    private bool highlightState;

    // Start is called before the first frame update
    void Start()
    {
        highlightState = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void showMessage(string message)
    {
        dText.text = message;
        if (!string.IsNullOrEmpty(message)) dBox.SetActive(true);
    }

    public void hide()
    {
        dBox.SetActive(false);
    }

    public void hightlight(bool newHighlightState)
    {
        if (newHighlightState == highlightState) return;
        highlightState = newHighlightState;
        if(highlightState)
        {
            dBox.GetComponent<RectTransform>().localScale *= 1.15f;
        } else {
            dBox.GetComponent<RectTransform>().localScale /= 1.15f;
        }
    }
}
