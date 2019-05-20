namespace WebApplication
{
    public class WelcomServices : IWelcomServices
    {
        public string GetMessage()
        {
            return "come from iwelcomservices";
        }
    }
}