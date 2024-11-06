namespace Abstract_Factory_Pattern_Demo
{
    public interface IButton
    {
        void Render();
    }
    public interface ITextField
    {
        void Render();
    }
    public interface IUFactory
    {
        IButton CreateButton();
        ITextField CreateTextField();
    }
    //Light Theme:
    public class LightButton : IButton
    {
        public void Render()
        {
            Console.WriteLine("Rendering Light Theme Button!....");
        }
    }
    public class LightTextField : ITextField
    {
        public void Render()
        {
            Console.WriteLine("Rendering Light Theme Text Field!...");
        }
    }
    //Dark Theme:
    public class DarkButton : IButton
    {
        public void Render()
        {
            Console.WriteLine("Rendering Dark Theme Button!....");
        }
    }
    public class DarkTextField : ITextField
    {
        public void Render()
        {
            Console.WriteLine("Rendering Dark Theme Text Field!...");
        }
    }
    public class LightUIFactory : IUFactory
    {
        public IButton CreateButton()
        {
            return new LightButton();
        }
        public ITextField CreateTextField()
        { 
            return new  LightTextField(); 
        }
    }
    public class DarkUIFactory : IUFactory
    {
        public IButton CreateButton()
        {
            return new DarkButton();
        }
        public ITextField CreateTextField()
        {
            return new DarkTextField();
        }
    }
    public class Client
    {
        private readonly IButton _button;
        private readonly ITextField _textField;
        public Client(IUFactory factory)
        {
            _button = factory.CreateButton();
            _textField = factory.CreateTextField();
        }
        public void RenderUI()
        {
            _button.Render();
            _textField.Render();
        }
    }
}
