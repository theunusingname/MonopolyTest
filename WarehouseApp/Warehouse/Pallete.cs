namespace WarehouseApp.Warehouse;

public class Pallete(decimal width, decimal length, decimal height)
    : StorageUnit(width, length, height, PalleteWeight), IExpirable
{
    private const decimal PalleteWeight = 30;

    private readonly List<Box> _content = new();

    public new decimal Weight => PalleteWeight + _content.Sum(box => box.Weight);

    public bool AddBox(Box box)
    {
        if (IsFit(box))
        {
            _content.Add(box);
            return true;
        }

        return false;
    }

    public DateOnly GetExpireDate()
    {
        return _content.MinBy(box => box.GetExpireDate())?.GetExpireDate() ?? DateOnly.MaxValue;
    }

    public override decimal GetVolume()
    {
        return _content.Sum(box => box.GetVolume()) + LongSide * ShortSide * Height;
    }

    public IReadOnlyList<Box> GetBoxes()
    {
        return _content;
    }

    public override string ToString()
    {
        return $"PalleteId: {Id}, Box count:{_content.Count}, Weight:{Weight}, Volume:{GetVolume()} Expire: {GetExpireDate()} ";
    }
}