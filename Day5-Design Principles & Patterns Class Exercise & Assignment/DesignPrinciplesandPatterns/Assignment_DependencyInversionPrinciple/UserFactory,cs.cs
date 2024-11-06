namespace ASSIGNMENT_DIP
{
    public static class UserFactory
    {
        public static IUser CreateUser(string userType)
        {
            return userType switch
            {
                "Student" => new Student(),
                "Teacher" => new Teacher(),
                "Librarian" => new Librarian(),
                _ => throw new ArgumentException("Invalid user type.")
            };
        }
    }
}
