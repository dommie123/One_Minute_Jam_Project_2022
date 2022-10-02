using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory
{
    public event EventHandler OnItemListChanged;

    private List<Item> itemList;

    public Inventory()
    {
        itemList = new List<Item>();

        // Debug.Log("Inventory");
        // AddItem(new Item {itemType = Item.ItemType.Milk, amount = 1});
        // AddItem(new Item {itemType = Item.ItemType.Torch, amount = 1});
        // AddItem(new Item {itemType = Item.ItemType.CoffeeBeans, amount = 1});
        // Debug.Log(itemList.Count);
    }

    public void AddItem(Item item)
    {
        itemList.Add(item);
        OnItemListChanged?.Invoke(this, EventArgs.Empty);
    }

    public List<Item> GetItemList()
    {
        return itemList;
    }
}
