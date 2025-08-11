using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NpcDialogue : MonoBehaviour
{
    public TextMeshProUGUI textComponent;
    public string[] lines;
    public float textSpeed;
    

    private int index;
    private bool dialogueStarted = false;
    [SerializeField] private ArmorManager armorManager; // Assign in inspector

    void Start()
    {
        textComponent.text = string.Empty;
    }

    void Update()
    {
        if (!dialogueStarted) return;

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

   public void StartDialogue() // now public
    {
        dialogueStarted = true;
        index = 0;
        StartCoroutine(TypeLine());
    }

    IEnumerator TypeLine()
    {
        textComponent.text = "";
        foreach (char c in lines[index])
        {
            textComponent.text += c;
            yield return new WaitForSeconds(textSpeed);
        }
    }

    void NextLine()
    {
        if (index < lines.Length - 1)
        {
            index++;
            textComponent.text = string.Empty;
            StartCoroutine(TypeLine());
        }
        else
        {
            // Replace with your scene name or build index
            SceneManager.LoadScene(8);
         
            
            
        }
    }
    
   
    
  
}
