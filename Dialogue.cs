using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class NewMonoBehaviourScript : MonoBehaviour
{
    //get the text component
    public TextMeshProUGUI textComponent;
    //lines of text
    public string[] lines;
    public float textSpeed;
    
    private int index;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        textComponent.text = string.Empty;
        startDialogue();
    }

    // Update is called once per frame
    void Update()
    {
        //if the player clicks the mouse button, check if the full line of text has been displayed
        if (Input.GetMouseButtonDown(0))
        {
            if (textComponent.text == lines[index])
            {
                NextLine();
            }
            else
            {
                StopAllCoroutines();
                textComponent.text = lines[index];
            }
        }
}
        //method to start the dialogue
    void startDialogue()
    {
        index = 0;
        StartCoroutine(TypleLine());
    }
    
    //This function handles the "typing" animation for one line of text.
    IEnumerator TypleLine()
    {
        //loop through each character in the line
        foreach (char c in lines[index].ToCharArray())
        {
            //add the character to the text, chharacter by character
            textComponent.text += c;
            //wait for a short amount of time before adding the next character
            yield return new WaitForSeconds(textSpeed);
        }
    }
    
    //This function is called when the player clicks the mouse button to move to the next line of text.
    void NextLine()
    {
        //if there are more lines of text, increase the index and start typing the next line
        if (index < lines.Length - 1)
        {
            //increase the index
            index++;
            //clear the text
            textComponent.text = string.Empty;
            //start typing the next line
            StartCoroutine(TypleLine());
        }
        else
        {
            // Replace with your scene name or build index
            SceneManager.LoadScene(3);
        }
    }
}
