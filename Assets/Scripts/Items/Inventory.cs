using System.Collections.Generic;

public class Inventory
{
    private List<Item> items;

    public Inventory()
    {
        items = new List<Item>();
    }

    public void AddItem(Item item)
    {
        Item exist = GetItem(item.GetName());

        if (exist == null)
            items.Add(item);
        else
            exist.AddQty(item.GetQty());
    }

    public Item GetItem(string name)
    {
        int size = items.Count;
        for (int i = 0; i < size; i++)
        {
            if (items[i].GetName() == name)
                return items[i];
        }
        return null;
    }

    public void RemoveItem(string name)
    {
        Item item = GetItem(name);
        if (item != null)
            items.Remove(item);
    }

    public bool Has(string name)
    {
        int size = items.Count;
        for (int i = 0; i < size; i++)
        {
            if (items[i].GetName() == name)
                return true;
        }
        return false;
    }

    public int Size()
    {
        return items.Count;
    }
}
