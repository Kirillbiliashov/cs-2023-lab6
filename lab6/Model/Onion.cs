using System;
namespace lab6.Model
{
    public class Onion : Vegetable
    {
        public Onion(double quantity) : base(quantity)
        {
            CaloriesPerPiece = 124.08;
            Color = "Yellow";
        }
    }
}

