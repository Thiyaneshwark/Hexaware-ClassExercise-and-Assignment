using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Structural_Decorator_Pattern_Demo
{
    public interface Pizza
    {
        string MakePizza();
    }
    public class PlainPizza : Pizza
    {
        public string MakePizza()
        {
            return "Plian Pizza";
        }
    }
    public abstract class PizzaDecorator : Pizza
    {
        protected Pizza pizza;
        public PizzaDecorator(Pizza pizza)
        {
            this.pizza = pizza;
        }
        public virtual string MakePizza()
        {
            return pizza.MakePizza();
        }
    }
    public class ChickenPizza : PizzaDecorator
    {
        public ChickenPizza(Pizza pizza) : base(pizza) { }

        public override string MakePizza()
        {
            return base.MakePizza() + ", Chicken";
        }
    }

    public class VegetablePizza : PizzaDecorator
    {
        public VegetablePizza(Pizza pizza) : base(pizza) { }

        public override string MakePizza()
        {
            return base.MakePizza() + ", Vegetable";
        }
    }
}
