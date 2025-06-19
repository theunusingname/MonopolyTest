using WarehouseApp;
using WarehouseApp.Warehouse;

namespace Tests;

public class Tests
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void WHEN_put_big_box_THEN_throw_exception()
    {
        var warehouse = new Warehouse(new InMemoryRepo());

        var smallPalleteId = warehouse.AddPallete(1, 1, 1);
        Assert.Throws<Exception>(() =>
            warehouse.AddBoxWithExpireDate(2, 2, 2, 10, DateOnly.FromDateTime(DateTime.Now), smallPalleteId));
    }
    
    [Test]
    public void WHEN_create_storage_unit_with_negative_dimension_THEN_throw_exception()
    {
        var warehouse = new Warehouse(new InMemoryRepo());

        Assert.Throws<ArgumentException>(() =>warehouse.AddPallete(-1, 1, 1));
        
    }
    
    [Test]
    public void WHEN_put_big_box_into_unexisting_pallete_THEN_throw_exception()
    {
        var warehouse = new Warehouse(new InMemoryRepo());

        warehouse.AddPallete(1, 1, 1);
        Assert.Throws<ArgumentException>(() =>
            warehouse.AddBoxWithExpireDate(2, 2, 2, 10, DateOnly.FromDateTime(DateTime.Now), "smallPalleteId"));
    }
}