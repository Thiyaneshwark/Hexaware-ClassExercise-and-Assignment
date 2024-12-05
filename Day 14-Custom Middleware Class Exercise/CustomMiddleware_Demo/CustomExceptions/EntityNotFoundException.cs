namespace CustomMiddleware_Demo.CustomExceptions
{
    public class EntityNotFoundException : Exception
    {
        public EntityNotFoundException(string message) : base(message)
        {

        }
    }
}