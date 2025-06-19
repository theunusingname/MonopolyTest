namespace WarehouseApp.Warehouse;

public interface IWarehouseRepo
{
    public void SavePallete(Pallete pallete);
    
    public List<Pallete> LoadAllPallets();

    public Pallete? GetPallete(string id);
}