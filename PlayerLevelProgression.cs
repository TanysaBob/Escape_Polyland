using Progression;
using UnityEngine;
using ProgressBar = UnityEngine.UIElements.ProgressBar;
    public class PlayerLevelProgression : MonoBehaviour 
    {
        private int level; 
        private int xp;
        private int xpToNextLevel;
        private int xpForLevelComplete;
        private Health health;
        private Armour armour;
        private ProgressBar healthBar;
        private ProgressBar xpBar;
        
        void Start()
        {
            level = 1;
            xp = 0;
            xpToNextLevel = 100;
            health = GetComponent<Health>();
            armour = GetComponent<Armour>();
        }
        
        void Update()
        {
            
        }
        
        public void GainXP(int xp)
        {
            this.xp += xp;
            
            if (this.xp >= xpToNextLevel)
            {
                level++;
                this.xp -= xpToNextLevel;
                xpToNextLevel = 100 * level;
                health.AddMaxHP();
                armour.AddArmourPts();
            }
        }
    }