using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class ArmorManager : MonoBehaviour
{
    [System.Serializable]
    public class ArmorPiece
    {
        public string name;
        public GameObject model;
    }

    [SerializeField] private List<ArmorPiece> armorPieces;

    // References to body parts that should be hidden when armor is equipped
    [Header("Body Parts to Hide")]
    [SerializeField] private GameObject baseLegs;
    [SerializeField] private GameObject baseChest;
    [SerializeField] private GameObject baseArms;
    [SerializeField] private GameObject baseHands;

    private void Start()
    {
        EquipArmorForScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void EquipArmorForScene(int sceneIndex)
    {
        Debug.Log($"Equipping armor for scene index {sceneIndex}");

        bool hasHelmet = false;
        bool hasChest = false;
        bool hasPants = false;
        bool hasGloves = false;
        bool hasShoulders = false;
        bool hasBoots = false;
        

        foreach (var piece in armorPieces)
        {
            if (piece.model == null) continue;

            bool active = false;

            // Determine what to equip based on scene index
            switch (sceneIndex)
            {
                case 3: // Level 1: Gloves
                    active = piece.name == "Gloves";
                    break;
                case 4: // Level 2: Gloves + Helmet 
                    active = piece.name == "Gloves" || piece.name== "Helmet";
                    break;
                case 5: // Level 3: Gloves + Helmet + Chest
                    active = piece.name == "Gloves" || piece.name == "Helmet" || piece.name == "Chest";
                    break;
                case 8: // Level 4: Gloves + Helmet + Chest + Pants
                    active = piece.name == "Gloves" || piece.name == "Helmet" || piece.name == "Chest" || piece.name == "Pants";
                    break;
                case 6: // Level 5: Full Armor
                    active = true;
                    break;
            }

            piece.model.SetActive(active);

            // Track what's equipped
            switch (piece.name)
            {
                case "Helmet": hasHelmet = active; break;
                case "Chest": hasChest = active; break;
                case "Pants": hasPants = active; break;
                case "Gloves": hasGloves = active; break;
                case "Shoulders": hasShoulders = active; break;
                case "Boots": hasBoots = active; break;
            }
        }

        // Disable body parts based on what's equipped
        if (baseLegs != null) baseLegs.SetActive(!hasPants);
        if (baseChest != null) baseChest.SetActive(!hasChest);
        if (baseArms != null) baseArms.SetActive(!hasChest);
        if (baseHands != null) baseHands.SetActive(!hasGloves);
    }
}
