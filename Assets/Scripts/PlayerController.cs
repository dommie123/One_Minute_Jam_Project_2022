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

    
}
