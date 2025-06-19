namespace WarehouseApp.Warehouse;

public abstract class StorageUnit : IStorageUnit
{
    protected StorageUnit(decimal width, decimal length, decimal height, decimal weight)
    {
        if (width<=0 || length <= 0 || height <= 0 || weight <= 0)
        {
            throw new ArgumentException("Dimensions and weight must be bigger than 0");
        }
        Id = Guid.NewGuid().ToString();
        ShortSide = width >= length ? length : width;
        LongSide = width >= length ? width : length;
        Height = height;
        Weight = weight;
    }

    public string Id { get; }
    public decimal ShortSide { get; }
    public decimal LongSide { get; }
    public decimal Height { get; }
    public decimal Weight { get; }

    public bool IsFit(StorageUnit other)
    {
        if (LongSide < other.LongSide)
        {
            return false;
        }

        return ShortSide >= other.ShortSide;
    }

    public abstract decimal GetVolume();
}