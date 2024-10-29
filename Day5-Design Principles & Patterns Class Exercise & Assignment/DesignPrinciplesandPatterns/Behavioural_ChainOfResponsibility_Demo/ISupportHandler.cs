namespace Behavioural_ChainOfRespons_Demo
{
    public interface ISupportHandler
    {
        void HandleRequest(string issue);
        ISupportHandler SetNext(ISupportHandler nextHandler);
    }
    public abstract class SupportHandler : ISupportHandler
    {
        private ISupportHandler _nextHandler;
        public ISupportHandler SetNext(ISupportHandler nextHandler)
        {
            _nextHandler = nextHandler;
            return nextHandler;
        }
        public virtual void HandleRequest(string issue)
        {
            if (_nextHandler != null)
            {
                _nextHandler.HandleRequest(issue);
            }
        }
    }
    public class LevelOneSupport : SupportHandler
    {
        public override void HandleRequest(string issue)
        {
            if (issue == "Simple issue")
            {
                Console.WriteLine("Level One Support Handle Simple Issue");
            }
            else
            {
                base.HandleRequest(issue);
            }
        }
    }

    public class LevelTwoSupport : SupportHandler
    {
        public override void HandleRequest(string issue)
        {
            if (issue == "Complex issue")
            {
                Console.WriteLine("Level One Support Handle Complex Issue");
            }
            else
            {
                base.HandleRequest(issue);
            }
        }
    }
    public class LevelThreeSupport : SupportHandler
    {
        public override void HandleRequest(string issue)
        {
            if (issue == "Very Complex issue")
            {
                Console.WriteLine("Level One Support Handle Very Complex Issue");
            }
            else
            {
                base.HandleRequest(issue);
            }
        }
    }
    public class LevelFourSupport : SupportHandler
    {
        public override void HandleRequest(string issue)
        {
            if (issue == "Unkown issue")
            {
                Console.WriteLine("Level One Support Handle Unkown Issue");
            }
            else
            {
                base.HandleRequest(issue);
            }
        }
    }
}
