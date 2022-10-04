using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PedestalBehavior : MonoBehaviour
{
    public PlayerController player;
    
    private Item currentItem;
    private DialogueTrigger dialogueTrigger;

    private void Awake() 
    {
        currentItem = null;
        dialogueTrigger = GetComponent<DialogueTrigger>();
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
                        StartPedestalDialogue(item);
                        foundItem = true;
                        currentItem = item;
                        playerInventory.RemoveItem(item);
                        IngredientBehavior.SpawnIngredient(transform.position + Vector3.back, item);
                        break;
                    default:
                        StartPedestalDialogue();
                        break;
                }

                if (foundItem)
                    break;
            }
        }
        else if (playerIsInRange && player.IsInteracting && currentItem != null)
        {
            StartPedestalDialogue(true);
        }
    }

    private void StartPedestalDialogue(Item item)
    {
        dialogueTrigger.SetDialogue(new Dialogue {sentences = new string[] {
            $"Placed the {GetItemTypeAsString(item)}."
        }});
        dialogueTrigger.TriggerDialogue();
    }

    private void StartPedestalDialogue()
    {
        StartPedestalDialogue(false);
    }

    private void StartPedestalDialogue(bool isFull)
    {
        if (!isFull)
        {
            dialogueTrigger.SetDialogue(new Dialogue {sentences = new string[] {
                "Man, this sure is one spooky pedestal.",
                "But it looks like I can put something on it that the creepy monster likes...",
            }});
        }
        else
        {
            dialogueTrigger.SetDialogue(new Dialogue {sentences = new string[] {
                "I already put something on this pedestal.",
            }});
        }

        dialogueTrigger.TriggerDialogue();
    }

    private string GetItemTypeAsString(Item item)
    {
        switch (item.itemType)
        {
            case Item.ItemType.Milk: 
                return "Milk";
            case Item.ItemType.WhippedCream:
                return "Whipped Cream";
            case Item.ItemType.Pumpkin:
                return "Pumpkin";
            case Item.ItemType.GroundCoffee:
                return "Ground Coffee";
            case Item.ItemType.VanillaExtract:
                return "Vanilla Extract";
            default:
                return "Item of unknown type";
        }
    }
}
