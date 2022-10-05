using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemAssets : MonoBehaviour
{
    public static ItemAssets instance { get; private set; }

    private void Awake() {
        instance = this;
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
