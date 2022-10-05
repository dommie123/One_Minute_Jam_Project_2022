using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CowBehavior : MonoBehaviour
{
    public PlayerController player;
    public AudioSource sfx;

    // Update is called once per frame
    void Update()
    {
        bool playerIsInRange = Vector2.Distance(player.transform.position, this.transform.position) <= 2;

        if (playerIsInRange && player.IsInteracting)
        {
            Inventory playerInventory = player.GetInventory();
            List<Item> playerItems = playerInventory.GetItemList();
            int numMilkBuckets = 0;

            if (playerItems.Count <= 0)
            {
                GatherMilk();
                return;
            }

            foreach (Item item in playerItems)
            {
                switch (item.itemType)
                {
                    case Item.ItemType.Milk:
                        numMilkBuckets++;
                        // TODO dialogue trigger
                        break;
                    default:
                        break;
                }

                if (numMilkBuckets >= 2)
                {
                    break;
                }
            }

            if (numMilkBuckets < 2)
            {
                GatherMilk();
            }
        }
    }

    private void GatherMilk()
    {
        IngredientBehavior.SpawnIngredient(transform.position + Vector3.down, new Item {itemType = Item.ItemType.Milk, amount = 1});
        sfx.Play();
    }  
}
