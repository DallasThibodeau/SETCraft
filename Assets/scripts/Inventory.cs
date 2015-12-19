using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Inventory : MonoBehaviour {

    public List<Item> inventory = new List<Item>();
    public int slotsX, slotsY;
    public List<Item> slots = new List<Item>();
    public ItemDB db;
    public GUISkin skin;

    private bool showInventory;
    private bool showToolTip;
    private string toolTip;
    private bool draggingItem;
    private Item draggedItem;
    private int prevIndex;

	// Use this for initialization
	void Start () {

        //db = GameObject.FindGameObjectWithTag("Item DB").GetComponent<ItemDB>();
        for (int i = 0; i < (slotsX * slotsY); ++i)
        {
            slots.Add(new Item());
            inventory.Add(new Item());
        }

        AddItem(0);
        AddItem(1);

        print(InventoryContains(1));
    }

    void OnGUI ()
    {
        if (GUI.Button(new Rect(40, 400, 100, 40), "Craft"))
        {
            CraftHouse();
        }
        toolTip = "";
        GUI.skin = skin;
        if (showInventory)
        {
            DrawInventory();
            if (showToolTip)
            {
                GUI.Box(new Rect(Event.current.mousePosition.x, Event.current.mousePosition.y, 200, 200), toolTip);
                if (GUI.Button(new Rect(40, 400, 100, 40), "Craft"))
                {
                    CraftHouse();
                }
            }
        }
        if (draggingItem)
        {
            GUI.DrawTexture(new Rect (Event.current.mousePosition.x, Event.current.mousePosition.y, 50, 50), draggedItem.itemImage);
        }
    }

    void DrawInventory()
    {
        Event e = Event.current;
        int i = 0;
        for (int y = 0; y < slotsY; ++y)
        {
            for (int x = 0; x < slotsX; ++x)
            {
                Rect slotRect = new Rect(x * 60, y * 60, 50, 50);
                GUI.Box(new Rect(x * 60, y * 60, 50, 50), "", skin.GetStyle("Slot"));
                slots[i] = inventory[i];
                if (slots[i].itemName != null)
                {
                    GUI.DrawTexture(slotRect, slots[i].itemImage);
                    if (slotRect.Contains(e.mousePosition))
                    {
                        toolTip = CreateToolTip(slots[i]);
                        showToolTip = true;
                        if (e.button == 0 && e.type == EventType.MouseDrag && !draggingItem)
                        {
                            draggingItem = true;
                            prevIndex = i;
                            draggedItem = slots[i];
                            inventory[i] = new Item();
                        }
                        if (e.type == EventType.mouseUp &&
                            draggingItem)
                        {
                            inventory[prevIndex] = inventory[i];
                            inventory[i] = draggedItem;
                            draggingItem = false;
                            draggedItem = null;
                        }
                    }
                }
                else
                {
                    if (slotRect.Contains(e.mousePosition))
                    {
                        if (e.type == EventType.mouseUp && draggingItem)
                        {
                            inventory[i] = draggedItem;
                            draggingItem = false;
                            draggedItem = null;
                        }
                    }
                }
                if (toolTip == "")
                {
                    showToolTip = false;
                }
                
                ++i;
            }
        }
    }

    string CreateToolTip(Item item)
    {
        toolTip = "<color=#4DA4BF>" + item.itemName + "</color>\n\n" + "<color=#f2f2f2>" + item.itemDescription + "</color>";
        
        return toolTip;
    }
	
	// Update is called once per frame
	void Update () {
	
        if (Input.GetButtonDown("Inventory"))
        {
            showInventory = !showInventory;
        }
	}

    void AddItem(int id)
    {
        for (int i = 0; i < inventory.Count; ++i)
        {
            if (inventory[i].itemName == null)
            {
                for (int k = 0; k < db.items.Count; ++k)
                {
                    if (db.items[k].itemID == id)
                    {
                        inventory[i] = db.items[k];
                    }
                }
                break;
            }
        }
    }

    bool InventoryContains(int id)
    {
        foreach(Item item in inventory)
        {
            if (item.itemID == id)
            {
                return true;
            }
        }

        return false;
    }

    void RemoveItem(int id)
    {
        for (int i = 0; i < inventory.Count; ++i)
        {
            if (inventory[i].itemID == id)
            {
                inventory[i] = new Item();
                break;
            }
        }
    }

    void CraftHouse()
    {
        for (int i = 0; i < inventory.Count; ++i)
        {
            
        }
    }
}
