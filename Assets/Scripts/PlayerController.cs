using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public bool IsInteracting {get; private set;}

    [SerializeField] private float moveSpeed;
    [SerializeField] private UI_InventoryBehavior uiInventory;
    private DialogueTrigger hintTrigger;
    private Rigidbody2D body;
    private Inventory inventory;

    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        IsInteracting = false;

        inventory = new Inventory(UseItem);
        uiInventory.SetInventory(inventory);
        uiInventory.SetPlayerController(this);

        hintTrigger = GetComponent<DialogueTrigger>();

        IngredientBehavior.SpawnIngredient(new Vector3(5, 5), new Item {itemType = Item.ItemType.Milk, amount = 1});
        IngredientBehavior.SpawnIngredient(new Vector3(-5, 5), new Item {itemType = Item.ItemType.CoffeeBeans, amount = 1});
        IngredientBehavior.SpawnIngredient(new Vector3(5, 0), new Item {itemType = Item.ItemType.GroundCoffee, amount = 1});
        IngredientBehavior.SpawnIngredient(new Vector3(7, 0), new Item {itemType = Item.ItemType.VanillaExtract, amount = 1});
        IngredientBehavior.SpawnIngredient(new Vector3(4, 1), new Item {itemType = Item.ItemType.WhippedCream, amount = 1});
        IngredientBehavior.SpawnIngredient(new Vector3(4, -3), new Item {itemType = Item.ItemType.Pumpkin, amount = 1});
        IngredientBehavior.SpawnIngredient(new Vector3(3, 0), new Item {itemType = Item.ItemType.Torch, amount = 1});
    }

    // Update is called once per frame1
    void Update()
    {
        UpdatePlayerInputs();
    }

    void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.gameObject.tag == "Ingredient") 
        {
            Debug.Log($"{other.gameObject.name} Grabbed");

            IngredientBehavior ingredient = other.GetComponent<IngredientBehavior>();
            if (ingredient != null)
            {
                inventory.AddItem(ingredient.GetItem());
            }

            Destroy(other.gameObject);
        }
    }

    public Vector3 GetPosition() 
    {
        return this.transform.position;
    }

    private void UpdatePlayerInputs() 
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        body.velocity = new Vector2(horizontal * moveSpeed, vertical * moveSpeed);

        IsInteracting = Input.GetKey(KeyCode.E);

        if (Input.GetKeyDown(KeyCode.H))
        {
            SetNextHint();
            hintTrigger.TriggerDialogue();
        }
    }

    private void UseItem(Item item)
    {
        switch (item.itemType) {
            case Item.ItemType.Torch: 
                // TODO spawn torch and light the area
                Debug.Log("Used Torch!");
                inventory.RemoveItem(item);
                break;
            default: 
                Debug.Log("???");
                break;
        }
    }

    private void SetNextHint()
    {
        List<string> itemTypesNeeded = new List<string> {
            "Milk",
            "Ground Coffee",
            "Whipped Cream",
            "Vanilla Extract",
            "Pumpkin"
        };

        foreach (Item item in inventory.GetItemList())
        {
            switch (item.itemType) {
                case Item.ItemType.Milk:
                    itemTypesNeeded.Remove("Milk");
                    break;
                case Item.ItemType.Pumpkin:
                    itemTypesNeeded.Remove("Pumpkin");
                    break;
                case Item.ItemType.GroundCoffee:
                    itemTypesNeeded.Remove("Ground Coffee");
                    break;
                case Item.ItemType.WhippedCream:
                    itemTypesNeeded.Remove("Whipped Cream");
                    break;
                case Item.ItemType.VanillaExtract:
                    itemTypesNeeded.Remove("Vanilla Extract");
                    break;
                default:
                    break;
            }
        }

        Dialogue newHintDialogue;
        if (itemTypesNeeded.Count <= 0)
        {
            newHintDialogue = new Dialogue {
                sentences = new string[] {
                    "Okay, that should be everything!", 
                    "All I need to do is put the ingredients on those creepy pedestals over yonder."
                }
            };

            hintTrigger.SetDialogue(newHintDialogue);
            return;
        }

        System.Random rng = new System.Random();
        int randHintIndex = rng.Next(itemTypesNeeded.Count);
        string typeHinted = itemTypesNeeded[randHintIndex];

        switch (typeHinted) {
            case "Milk":
                newHintDialogue = new Dialogue {
                    sentences = new string[] {
                        "Oh yeah, I just remembered I was a dairy farmer!",
                        "I should be able to get some milk from these cows over yonder."
                    }
                };
                break;
            case "Pumpkin":
                newHintDialogue = new Dialogue {
                    sentences = new string[] {
                        "Oh yeah, I just remembered I was also a pumpkin farmer!",
                        "That pumpkin patch over yonder should have what I need."
                    }
                };
                break;
            case "Ground Coffee":
                newHintDialogue = new Dialogue {
                    sentences = new string[] {
                        "Hmm, what else did that demon say I needed?",
                        "Oh yeah, some coffee!",
                        "I should have some still in my kitchen!"
                    }
                };
                break;
            case "Whipped Cream":
                newHintDialogue = new Dialogue {
                    sentences = new string[] {
                        "Shoot! I don't have any whipped cream!",
                        "Oh well, maybe I can try making it myself.",
                        "Gonna need some milk first.",
                        "Then, I can make the whipped cream at my workshop."
                    }
                };
                break;
            case "Vanilla Extract":
                newHintDialogue = new Dialogue {
                    sentences = new string[] {
                        "Oh yeah, I just remembered I know a vanilla farmer!",
                        "Good thing he gave me some of his vanilla extract for ice cream night!",
                        "I should have some over yonder."
                    }
                };
                break;
            default:
                newHintDialogue = new Dialogue {
                    sentences = new string[] {
                        "Okay, that should be everything!", 
                        "All I need to do is put the ingredients on those creepy pedestals over yonder."
                    }
                };
                break;
        }

        hintTrigger.SetDialogue(newHintDialogue);
    }

    public Inventory GetInventory()
    {
        return inventory;
    }
}
