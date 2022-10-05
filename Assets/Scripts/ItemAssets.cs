using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemAssets : MonoBehaviour
{
    public static ItemAssets instance { get; private set; }

    // [SerializeField] private Transform beansSpawnPos;
    // [SerializeField] private Transform vanillaSpawnPos;
    // [SerializeField] private Transform pumpkinSpawnPos;

    void Awake() {
        Debug.Log("In Awake method");
        instance = this;

                // Spawn ingredients for player to pick up
        // // IngredientBehavior.SpawnIngredient(new Vector3(5, 5), new Item {itemType = Item.ItemType.Milk, amount = 1});
        // // IngredientBehavior.SpawnIngredient(new Vector3(-5, 5), new Item {itemType = Item.ItemType.CoffeeBeans, amount = 1});
        // IngredientBehavior.SpawnIngredient(beansSpawnPos.position, new Item {itemType = Item.ItemType.GroundCoffee, amount = 1});
        // IngredientBehavior.SpawnIngredient(vanillaSpawnPos.position, new Item {itemType = Item.ItemType.VanillaExtract, amount = 1});
        // // IngredientBehavior.SpawnIngredient(new Vector3(4, 1), new Item {itemType = Item.ItemType.WhippedCream, amount = 1});
        // IngredientBehavior.SpawnIngredient(pumpkinSpawnPos.position, new Item {itemType = Item.ItemType.Pumpkin, amount = 1});
        // // IngredientBehavior.SpawnIngredient(new Vector3(3, 0), new Item {itemType = Item.ItemType.Torch, amount = 1});
    }

    public Transform pfIngredient;

    public Sprite milkSprite;
    public Sprite pumpkinSprite;
    public Sprite coffeeBeansSprite;
    public Sprite vanillaSprite;
    public Sprite torchSprite;
    public Sprite whippedCreamSprite;
    public Sprite coffeeSprite;
}
