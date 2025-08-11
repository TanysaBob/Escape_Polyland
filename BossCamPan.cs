using UnityEngine;
using Unity.Cinemachine;
public class BossCamPan : MonoBehaviour
{
    public CinemachineSplineCart dollyCart;
    public float dollySpeed = 0.1f; // Adjust for speed
    private bool playPan = false;

    public void StartPan()
    {
        dollyCart.SplinePosition = 0f;
        playPan = true;
    }

    void Update()
    {
        if (playPan && dollyCart.SplinePosition < 1f)
        {
            dollyCart.SplinePosition += dollySpeed * Time.deltaTime;
        }
    }
}
