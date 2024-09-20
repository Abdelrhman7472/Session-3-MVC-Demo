namespace Demo.PresentaionLayer.ViewModels
{
    public class ResetPasswordViewModel
    {
        public string Password { get; set; }
        [DataType(DataType.Password)]
        [Compare(nameof(Password), ErrorMessage = "Password & Confirm Password Doesn't Match")]
        public string ConfirmPassword { get; set; }
        public string Email { get; set; }
        public string Token { get; set; }


    }
}
