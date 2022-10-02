using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftingTableBehavior : MonoBehaviour
{
    public PlayerController player;
    public GameObject craftingUI;
    public GameObject hud;

    // Update is called once per frame
    void Update()
    {
        bool playerIsInRange = Vector2.Distance(player.transform.position, this.transform.position) <= 2;

        if (playerIsInRange && player.IsInteracting)
        {
            craftingUI.SetActive(true);
            hud.SetActive(false);
        }
    }    
}
