using System.Runtime;
using Unity.Cinemachine;
using UnityEngine;
using Unity.Collections;
using UnityEngine.Rendering;

public class BossWalk : MonoBehaviour
{
    public Transform targetPoint;          // where to walk to
    public float speed = 2f;
    private Animator anim;
    private bool isWalking = false;
    private bool hasLookedAround = false;
    public GameObject bossText;
    
    private CapsuleCollider bossCollider;

    public CinemachineCamera bossCam;
    
    void Start()
    {
        anim = GetComponent<Animator>();
        bossCollider = GetComponent<CapsuleCollider>();
        
        if(bossCollider != null)
            bossCollider.enabled = false;
    }

    void Update()
    {
        if (isWalking)
        {
            Vector3 direction = (targetPoint.position - transform.position).normalized;
            transform.position += direction * speed * Time.deltaTime;
            transform.rotation = Quaternion.LookRotation(direction);

            float distance = Vector3.Distance(transform.position, targetPoint.position);
            if (distance < 0.5f && !hasLookedAround)
            {
                anim.Play("Idle");
                isWalking = false;
                hasLookedAround = true;
                
                StartCoroutine(BossLookSequence());
            }
        }
    }

    public void StartWalking()
    {
        if (anim != null) anim.SetTrigger("StartWalk");
        //bossText.ShowText();
        isWalking = true;
    }
    
    private System.Collections.IEnumerator BossLookSequence() {
        if (bossCollider != null)
            bossCollider.enabled = true;
        if (bossText != null)
            bossText.SetActive(true);
        
        yield return new WaitForSeconds(5f); // Let Idle pose settle

        anim.Play("Skill");
        yield return new WaitForSeconds(2f); // Assume "Skill" lasts ~2 seconds
        bossText.SetActive(false);
        
        anim.SetTrigger("BackToStand");
        yield return new WaitForSeconds(1f); // Assume transition takes 1 second

        // Reset camera or do next thing
        Debug.Log("Animations done. Resetting camera.");
        bossCam.Priority = 0;
        GetComponent<BossFollow>()?.EnableFollow();
    }
    
}
