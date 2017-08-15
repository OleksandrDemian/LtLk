public class Item
{
    protected string name;
    protected int qty = 0;

    public Item(string name, int qty)
    {
        this.name = name;
        this.qty = qty;
    }

    public int GetQty()
    {
        return qty;
    }

    public virtual void SetQty(int qty)
    {
        this.qty = qty;
    }

    public virtual void AddQty(int qty)
    {
        this.qty += qty;
    }

    public virtual void Decrease()
    {
        qty--;
    }

    public virtual void Increase()
    {
        qty++;
    }

    public string GetName()
    {
        return name;
    }
}
