using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    public String newSceneName;
    void OnTriggerEnter(Collider other) {
        if (other.GetComponent<Player>() != null) {
            SceneManager.LoadScene(newSceneName);
        }
    }
}
