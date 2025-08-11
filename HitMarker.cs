using UnityEngine;
using UnityEngine.UI;

public class HitMarker : MonoBehaviour
{
    
    public Image hitMarker;
    public float showTime = 0.1f;
    public float timer;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if(hitMarker != null)
            hitMarker.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        {
            if (timer > 0) //When the hitmarker is not shown
            {
                timer -= Time.deltaTime;
                if (timer <= 0 && hitMarker != null)
                {
                    hitMarker.enabled = false;
                }
            }
        }
    }

    public void ShowHitMarker()
        {
            if (hitMarker != null) // SHows the htmarker when hit
            {
                hitMarker.enabled = true;
                timer = showTime;
            }
        }
}

