using System;
namespace lab6.Model
{
    public class Tomato : Vegetable
    {
        public Tomato(double quantity) : base(quantity)
        {
            CaloriesPerPiece = 22;
            Color = "Red";
        }
    }
}

