public class InventoryItem
{
    public InventoryItemData Data {  get; private set; }
    public int StackSize {  get; private set; }

    public InventoryItem(in InventoryItemData source)
    {
        Data = source;
        AddToStack();
    }

    public void AddToStack()
    {
        StackSize++;
    }

    public void RemoveFromStack()
    {
        StackSize--;
    }
}
