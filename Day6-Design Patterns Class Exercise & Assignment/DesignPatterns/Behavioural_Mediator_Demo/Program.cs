using Behavioural_Mediator_Demo;

IChatRoomMediator chatRoom = new ChatRoom();
User user1=new User(chatRoom,"sam");
User user2 = new User(chatRoom, "Pam");

user1.SendMessage("Hi Pam");
user2.SendMessage("Hi Sam, Are you available for Quick Call");

Console.ReadLine();