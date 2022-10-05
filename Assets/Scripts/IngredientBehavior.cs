using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngredientBehavior : MonoBehaviour
{
    public AudioSource sfx;

    public static IngredientBehavior SpawnIngredient(Vector3 position, Item item) {
        Transform transform = Instantiate(ItemAssets.instance.pfIngredient, position, Quaternion.identity);

        IngredientBehavior ingredient = transform.GetComponent<IngredientBehavior>();
        ingredient.SetItem(item);

        return ingredient;
    }

    public static IngredientBehavior DropItem(Vector3 dropPosition, Item item)
    {
        IngredientBehavior ingredient = SpawnIngredient(dropPosition + Vector3.right * 2f, item);

        return ingredient;
    }

    private Item item;
    private SpriteRenderer spriteRenderer;

    private void Awake() 
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void SetItem(Item item)
    {
        this.item = item;
        spriteRenderer.drawMode = SpriteDrawMode.Sliced;
        spriteRenderer.sprite = item.GetSprite();
    }

    public Item GetItem()
    {
        return item;
    }

    public void PlaySFX()
    {
        sfx.Play();
    }
}
