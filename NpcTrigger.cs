using UnityEngine;

public class NpcTrigger : MonoBehaviour
{
    public NpcDialogue npcDialogue;
    public GameObject dialogueUI;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            dialogueUI.SetActive(true);
            npcDialogue.StartDialogue();
        }
    }
}
