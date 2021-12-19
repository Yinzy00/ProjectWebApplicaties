namespace Project_WebApp.ViewModels.Auth
{
    public class RestorePasswordViewModel
    {
        public string UserId { get; set; }
        public string RestoreKey { get; set; }
        public string NewPass { get; set; }
        public string NewPassRepeat { get; set; }
    }
}
