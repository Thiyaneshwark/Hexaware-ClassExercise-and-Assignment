using System.Text.RegularExpressions;


////Example 1:Email Validation:
//string emailPattern =@"^[^@\s]+@[^@\s]+\.(com|net|org|gov|in)$";
//string[] emails = { "test@example.com", "hello@gmail.in", "example@gmail" };
//foreach(String email in emails)
//{
//    if(Regex.IsMatch(emailPattern, email))
//    {
//        Console.WriteLine($"{email} is Vaild");
//    }
//    else
//    {
//        Console.WriteLine($"{email} is Invalid");
//    }
//}


////Example 2: Number Matches
//Console.WriteLine("Enter the text to extract number from string");
//string input =Console.ReadLine();
//string numberPattern = @"\d+";
//foreach (Match match in Regex.Matches(input, numberPattern))
//{
//    Console.WriteLine($" Found Number: { match.Value}");
//}


////Example 3: Replaces
//string message = "you can reach me @ 123-456-2738 (or) 987-789-7352";
//string mobilePattern = @"\d{3}-\d{3}-\d{4}";
//string replaceWith = "***-***-****";
//string result =Regex.Replace(message, mobilePattern, replaceWith);
//Console.WriteLine(result);


//Example 4: Find and HighLight the specific pattern
string content = "I forget My lunch Box at Black Box";
string wordPattern = @"\b\w+?ox\b";
string findResult = Regex.Replace(content, wordPattern, match => $"[{match.Value}]");
Console.WriteLine(findResult);
Console.ReadLine(); 