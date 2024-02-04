using System;
namespace lab6.Model
{
    public abstract class Vegetable
    {

        public double Quantity { get; init; }

        public double CaloriesPerPiece { get; init; }

        public string Color { get; init; }


        public double Calories
        {
            get
            {
                return Quantity * CaloriesPerPiece;
            }
        }

        public Vegetable(double quantity)
        {
            if (quantity <= 0)
            {
                throw new ArgumentException("Quantity cannot be negative", nameof(quantity));
            }
            Quantity = quantity;
        }

        public override string ToString()
        {
            return $"{Quantity} of {Color} {GetType().Name}";
        }

        public override bool Equals(object? obj)
        {
            if (obj == null || obj is not Vegetable)
                return false;
            return obj.GetHashCode() == GetHashCode();
        }

        public override int GetHashCode()
        {
            var result = Quantity.GetHashCode();
            result = 31 * result + CaloriesPerPiece.GetHashCode();
            result = 31 * result + Color.GetHashCode();
            return result;
        }
    }
}

