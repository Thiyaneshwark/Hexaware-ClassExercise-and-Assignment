namespace ASSIGNMENT_DIP
{

    public interface IUser
    {
        string GetUserType();
    }

    public interface IBorrower
    {
        void BorrowBook(string book);
    }

    public interface IReserver
    {
        void ReserveBook(string book);
    }

    public interface IInventoryManager
    {
        void AddBook(string book);
        void RemoveBook(string book);
    }
}
