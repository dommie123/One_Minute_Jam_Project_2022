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
            // craftingUI.SetActive(true);
            // hud.SetActive(false);

            // TODO check for milk and change it to whipped cream
            Inventory playerInventory = player.GetInventory();
            List<Item> playerItems = playerInventory.GetItemList();

            foreach (Item item in playerItems)
            {
                bool foundItem = false;
                switch (item.itemType)
                {
                    case Item.ItemType.Milk:
                        // TODO dialogue trigger
                        foundItem = true;
                        playerInventory.RemoveItem(item);
                        IngredientBehavior.SpawnIngredient(transform.position + Vector3.up, new Item {itemType = Item.ItemType.WhippedCream, amount = 1});
                        break;
                    default:
                        break;
                }

                if (foundItem)
                    break;
            }
        }
    }    
}
