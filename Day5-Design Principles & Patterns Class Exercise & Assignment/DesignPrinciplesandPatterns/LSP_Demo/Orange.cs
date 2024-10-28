namespace LiskovSegregationPrinciple_Demo
{
    public class Orange : IFruit
    {
        public string GetColor()
        {
            return "Orange";
        }
    }
}