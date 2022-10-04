using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PedestalBehavior : MonoBehaviour
{
    public PlayerController player;
    
    private Item currentItem;

    private void Awake() 
    {
        currentItem = null;
    }

    // Update is called once per frame
    void Update()
    {
        bool playerIsInRange = Vector2.Distance(player.transform.position, this.transform.position) <= 2;

        if (playerIsInRange && player.IsInteracting && currentItem == null)
        {
            Inventory playerInventory = player.GetInventory();
            List<Item> playerItems = playerInventory.GetItemList();

            foreach (Item item in playerItems)
            {
                bool foundItem = false;
                switch (item.itemType)
                {
                    case Item.ItemType.Milk:
                    case Item.ItemType.WhippedCream:
                    case Item.ItemType.Pumpkin:
                    case Item.ItemType.GroundCoffee:
                    case Item.ItemType.VanillaExtract:
                        // TODO dialogue trigger
                        foundItem = true;
                        currentItem = item;
                        playerInventory.RemoveItem(item);
                        IngredientBehavior.SpawnIngredient(transform.position + Vector3.back, item);
                        break;
                    default:
                        // TODO prompt the player to place a valid ingredient on the pedestal.
                        break;
                }

                if (foundItem)
                    break;
            }
        }
    }
}
