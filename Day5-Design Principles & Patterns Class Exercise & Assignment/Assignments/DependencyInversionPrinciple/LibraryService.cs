namespace DependencyInversionPrinciple
{
    public class LibraryService
    {
        public void ExecuteBorrow(IBorrower borrower, string book)
        {
            borrower.BorrowBook(book);
        }

        public void ExecuteReserve(IReserver reserver, string book)
        {
            reserver.ReserveBook(book);
        }

        public void ManageInventory(IInventoryManager manager, string book, bool isAdding)
        {
            if (isAdding)
                manager.AddBook(book);
            else
                manager.RemoveBook(book);
        }
    }
}
