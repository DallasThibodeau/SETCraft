using UnityEngine;
using System.Collections;

[System.Serializable]
public class Item {

    public string itemName;
    public int itemID;
    public string itemDescription;
    public Texture2D itemImage;
    public ItemType itemType;

    public enum ItemType
    {
        Wood,
        Rock
    }

    public Item()
    {

    }

    public Item(string name, int id, string desc, ItemType type)
    {
        itemName = name;
        itemID = id;
        itemDescription = desc;
        itemImage = Resources.Load<Texture2D>("Item Icons/" + name);
        itemType = type;
    }
}
