using System.Reflection.Metadata;

namespace Behavioural_Mediator_Demo
{
    public interface IChatRoomMediator
    {
        void ShowMessage(User user, string message);
    }
    public class ChatRoom : IChatRoomMediator
    {
        public void ShowMessage(User user, string message)
        {
            string time = DateTime.Now.ToString("HH:mm");
            string sender = user.Name;
            Console.WriteLine($"{time} [sender] : {message}");
        }
    }
    public class User
    {
        private IChatRoomMediator _chatRoom;
        public string Name { get; set; }
        public User(IChatRoomMediator mediator, string name)
        {
            _chatRoom = mediator;
            Name = name;
        }
        public void SendMessage(string message)
        {
            _chatRoom.ShowMessage(this, message);
        }

    }
}
