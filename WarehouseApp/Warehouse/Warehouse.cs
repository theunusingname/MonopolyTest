namespace WarehouseApp.Warehouse;

public class Warehouse(IWarehouseRepo repo)
{
    public string AddPallete(decimal width, decimal length, decimal height)
    {
        var pallete = new Pallete(width, length, height);
        repo.SavePallete(pallete);
        return pallete.Id;
    }

    public string AddBoxWithManufacturedDate(decimal width, decimal length, decimal height, decimal weight,
        DateOnly manufacturedDate,
        string palleteId)
    {
        return AddBox(width, length, height, weight, manufacturedDate, palleteId, true);
    }

    public string AddBoxWithExpireDate(decimal width, decimal length, decimal height, decimal weight,
        DateOnly expireDate, string palleteId)
    {
        return AddBox(width, length, height, weight, expireDate, palleteId, false);
    }

    private string AddBox(decimal width, decimal length, decimal height, decimal weight, DateOnly date,
        string palleteId, bool isManufacturedDate)
    {
        var pallete = repo.GetPallete(palleteId);
        if (pallete == null)
        {
            throw new ArgumentException($"No pallete with id {palleteId}");
        }

        var boxBuilder = new Box.BoxBuilder()
            .WithDimensions(width, length, height, weight);

        if (isManufacturedDate)
        {
            boxBuilder.WithManufacturedDate(date);
        }
        else
        {
            boxBuilder.WithExpireDate(date);
        }

        var box = boxBuilder.Build();
        if (pallete.AddBox(box))
        {
            repo.SavePallete(pallete);
            return box.Id;
        }

        throw new Exception($"Can't place box with specified dimensions{width}x{length} on this pallete{palleteId}");
    }

    public Pallete? GetPallete(string id)
    {
        return repo.GetPallete(id);
    }

    public Dictionary<DateOnly, List<Pallete>> GetGroupedPallets()
    {
        return repo.LoadAllPallets().GroupBy(pallete => pallete.GetExpireDate())
            .OrderBy(pair => pair.Key)
            .ToDictionary(pair => pair.Key,
                pair => pair.OrderBy(pallete => pallete.Weight).ToList());
    }

    public List<Pallete> GetTop3ByExpireDate()
    {
        return repo.LoadAllPallets()
            .Where(pallete => pallete.GetBoxes().Count > 0)
            .OrderByDescending(pallete => pallete.GetBoxes().Max(box => box.GetExpireDate()))
            .Take(3)
            .OrderBy(pallete => pallete.GetVolume())
            .ToList();
    }
}