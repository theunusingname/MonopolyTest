namespace WarehouseApp.Warehouse;

public class Box : StorageUnit, IExpirable
{
    private readonly DateOnly _expireDate;
    
    private Box(decimal width, decimal length, decimal height, decimal weight, DateOnly expireDate) : 
        base(width, length, height, weight)
    {
        _expireDate = expireDate;
    }

    public DateOnly GetExpireDate()
    {
        return _expireDate;
    }

    public override decimal GetVolume()
    {
        return ShortSide * LongSide * Height;
    }
    
    public override string ToString()
    {
        return $"BoxId: {Id}, Weight:{Weight}, Volume:{GetVolume()} ";
    }
    
    public class BoxBuilder
    {
        private DateOnly _date;
        private bool _isManufacturedSpecified = false;

        private decimal _width;
        private decimal _length;
        private decimal _height;
        private decimal _weight;
        
        public BoxBuilder WithDimensions(decimal width, decimal length, decimal height, decimal weight)
        {
            _width = width;
            _length = length;
            _height = height;
            _weight = weight;
            return this;
        }

        public BoxBuilder WithManufacturedDate(DateOnly date)
        {
            _isManufacturedSpecified = true;
            _date = date;
            return this;
        }
        
        public BoxBuilder WithExpireDate(DateOnly date)
        {
            _isManufacturedSpecified = false;
            _date = date;
            return this;
        }
        
        public Box Build()
        {
            if (_isManufacturedSpecified)
            {
                return new Box(_width, _length, _height, _weight, _date.AddDays(100));
            }
            return new Box(_width, _length, _height, _weight, _date);

        }
    }
}

