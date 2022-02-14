namespace NotAlone.WebApp.Models
{
    public class MessagingRequest
    {
        public string FirstPersonInfo { get; set; }
        public string SecondPersonInfo { get; set; } 
        public string IsBlindDate { get; set; } 
        public string LinkBlindDate { get; set; }
        public string Action { get; set; }
    }
}