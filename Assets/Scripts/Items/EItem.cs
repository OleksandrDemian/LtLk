public delegate void OnItemQuantityChange(int qty);

public class EItem : Item
{
    private OnItemQuantityChange listener;

    public EItem(string name, int qty) : base(name, qty)
    {

    }

    public void SetListener(OnItemQuantityChange listener)
    {
        this.listener = listener;
    }

    public override void AddQty(int qty)
    {
        base.AddQty(qty);
        if (listener != null)
            listener(this.qty);
    }

    public override void Decrease()
    {
        base.Decrease();
        if (listener != null)
            listener(qty);
    }

    public override void Increase()
    {
        base.Increase();
        if (listener != null)
            listener(qty);
    }

    public override void SetQty(int qty)
    {
        base.SetQty(qty);
        if (listener != null)
            listener(qty);
    }
}
