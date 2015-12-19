using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ItemDB : MonoBehaviour {

    public List<Item> items = new List<Item>();

    void Start()
    {
        items.Add(new Item("Old Wood", 0, "Old wood common to this area.", Item.ItemType.Wood));
        items.Add(new Item("Smooth Stone", 1, "Stone smoothed by erosion.", Item.ItemType.Rock));
    }
}
