using WarehouseApp.Warehouse;

namespace WarehouseApp;

public class InMemoryRepo: IWarehouseRepo
{
    // Dummy implementation for test
    
    private HashSet<Pallete> _palletes = new();
   
    public void SavePallete(Pallete pallete)
    {
        _palletes.Add(pallete);
    }

    public List<Pallete> LoadAllPallets()
    {
        return _palletes.ToList();
    }

    public Pallete? GetPallete(string id)
    {
        return _palletes.FirstOrDefault(pallete => pallete.Id == id);
    }

    public void DeletePallete(string id)
    {
        _palletes.RemoveWhere(pallete => pallete.Id == id);
    }
}