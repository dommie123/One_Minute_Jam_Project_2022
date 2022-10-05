using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item
{
    public enum ItemType
    {
        // Non-craftable itmes
        Milk, 
        Pumpkin,
        CoffeeBeans,
        VanillaExtract,

        // Craftable Items
        Torch,
        WhippedCream,
        GroundCoffee
    }

    public ItemType itemType;
    public int amount;

    public Sprite GetSprite() 
    {
        switch (itemType) {
            case ItemType.Milk:             return ItemAssets.instance.milkSprite;
            case ItemType.Pumpkin:          return ItemAssets.instance.pumpkinSprite;
            case ItemType.CoffeeBeans:      return ItemAssets.instance.coffeeBeansSprite;
            case ItemType.VanillaExtract:   return ItemAssets.instance.vanillaSprite;
            case ItemType.Torch:            return ItemAssets.instance.torchSprite;
            case ItemType.WhippedCream:     return ItemAssets.instance.whippedCreamSprite;
            case ItemType.GroundCoffee:     return ItemAssets.instance.coffeeSprite;
            default:                        return null;            
        }
    }
}
