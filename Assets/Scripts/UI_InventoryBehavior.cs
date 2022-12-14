using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using CodeMonkey.Utils;

public class UI_InventoryBehavior : MonoBehaviour
{
    private Inventory inventory;
    private Transform itemSlotContainer;
    private Transform itemSlot;
    private PlayerController player;

    public void Awake() 
    {
        itemSlotContainer = transform.Find("Item Slot Background").Find("Item Slot Container");
        itemSlot = itemSlotContainer.Find("Item Slot");

        Debug.Log(itemSlotContainer.ToString());
        Debug.Log(itemSlot.ToString());
    }

    public void SetInventory(Inventory inventory)
    {
        this.inventory = inventory;

        inventory.OnItemListChanged += OnItemListChanged;

    }

    public void SetPlayerController(PlayerController player)
    {
        this.player = player;
    }

    private void OnItemListChanged(object sender, System.EventArgs e)
    {
        RefreshInventoryItems();
    }

    private void RefreshInventoryItems()
    {
        foreach (Transform child in itemSlotContainer)
        {
            if (child == itemSlot) continue;
            Destroy(child.gameObject);
        }

        int x = 0;
        int y = 0;
        float itemSlotCellSize = 70f;

        foreach (Item item in inventory.GetItemList())
        {
            RectTransform itemSlotRectTransform = Instantiate(itemSlot, itemSlotContainer).GetComponent<RectTransform>();
            itemSlotRectTransform.gameObject.SetActive(true);

            itemSlotRectTransform.GetComponent<Button_UI>().ClickFunc = () => {
                // Use Item
                inventory.UseItem(item);
            };

            itemSlotRectTransform.GetComponent<Button_UI>().MouseRightClickFunc = () => {
                // Drop Item
                inventory.RemoveItem(item);
                IngredientBehavior.DropItem(player.GetPosition(), item);
            };

            itemSlotRectTransform.anchoredPosition = new Vector2(x * itemSlotCellSize, y * itemSlotCellSize);
            Image image = itemSlotRectTransform.GetComponent<Image>();
            image.sprite = item.GetSprite();

            x++;
            if (x >= 2)
            {
                x = 0;
                y--;
            }
        }
    }
}
