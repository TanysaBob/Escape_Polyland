using UnityEngine;

public class NewSceneSwitch : MonoBehaviour
{
      public string sceneToLoad;
      public GameObject player;
      public Vector3 spawnPosition;
   
      private void OnTriggerEnter(Collider other)
      {
         if (other.CompareTag("Player"))
         {
   
               // Load the new scene
               UnityEngine.SceneManagement.SceneManager.LoadScene(6);
         }
      }
}
