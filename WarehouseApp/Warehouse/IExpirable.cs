namespace WarehouseApp.Warehouse;

public interface IExpirable
{
    public DateOnly GetExpireDate();
}