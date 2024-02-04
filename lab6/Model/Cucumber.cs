using System;
namespace lab6.Model
{
    public class Cucumber : Vegetable
    {
        public Cucumber(double quantity) : base(quantity)
        {
            CaloriesPerPiece = 45;
            Color = "Green";
        }
    }
}

