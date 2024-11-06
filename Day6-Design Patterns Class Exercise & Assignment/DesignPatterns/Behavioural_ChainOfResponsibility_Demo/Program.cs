using Behavioural_ChainOfResponsibility_Demo;

ISupportHandler levelOne = new LevelOneSupport();
ISupportHandler levelTwo = new LevelTwoSupport();
ISupportHandler levelThree = new LevelThreeSupport();
ISupportHandler levelFour = new LevelFourSupport();

levelOne.SetNext(levelTwo).SetNext(levelThree).SetNext(levelFour);

levelOne.HandleRequest("Simple issue");
levelOne.HandleRequest("Complex issue");
levelOne.HandleRequest("Very Complex issue");
levelOne.HandleRequest("Unkown issue");

Console.ReadLine();
