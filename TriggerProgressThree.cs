using UnityEngine;

namespace Progression
{
    public class TriggerProgressThree : MonoBehaviour
    {
        private ProgressBar _progressBar; // Declare ProgressBar reference
        public string tag; // The tag of the point attached to this trigger.
        private bool canSpawn = true; // Flag to control spawning.
        void Start()
        {
            // Assign the ProgressBar component from the scene
            _progressBar = FindObjectOfType<ProgressBar>();

            // Optional: Log error if no ProgressBar is found
            if (_progressBar == null)
            {
                Debug.LogError("No ProgressBar found in the scene! Make sure one exists.");
            }
        }
            void OnTriggerEnter(Collider other) 
            {
                if (other.CompareTag("Player") && canSpawn) 
                {
                    _progressBar.IncrementProgress(1f); // Increments 
                    pdatexp();
                }

            canSpawn = false; // Prevents sequential spawns on this trigger (can be reset).
            
        }

        void pdatexp()
        {
            if (_progressBar != null)
            {
                _progressBar.IncrementProgress(1f); // Increments by 10%
            }
        }
        
    }
}
