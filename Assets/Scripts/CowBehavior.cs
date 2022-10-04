using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CowBehavior : MonoBehaviour
{
    public PlayerController player;

    // Update is called once per frame
    void Update()
    {
        bool playerIsInRange = Vector2.Distance(player.transform.position, this.transform.position) <= 2;

        if (playerIsInRange && player.IsInteracting)
        {
            Inventory playerInventory = player.GetInventory();
            List<Item> playerItems = playerInventory.GetItemList();
            bool foundItem = false;

            if (playerItems.Count <= 0)
            {
                IngredientBehavior.SpawnIngredient(transform.position + Vector3.down, new Item {itemType = Item.ItemType.Milk, amount = 1});
                return;
            }

            foreach (Item item in playerItems)
            {
                switch (item.itemType)
                {
                    case Item.ItemType.Milk:
                        foundItem = true;
                        // TODO dialogue trigger
                        break;
                    default:
                        break;
                }

                if (foundItem)
                {
                    break;
                }
            }

            if (!foundItem)
            {
                IngredientBehavior.SpawnIngredient(transform.position + Vector3.down, new Item {itemType = Item.ItemType.Milk, amount = 1});
            }
        }
    }  
}
