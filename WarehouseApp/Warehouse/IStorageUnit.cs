namespace WarehouseApp.Warehouse;

public interface IStorageUnit
{
    string Id { get; }
    decimal ShortSide { get; }
    decimal LongSide { get; }
    decimal Height { get; }
    decimal Weight { get; }

    public bool IsFit(StorageUnit other);
    public decimal GetVolume();
}