namespace Demo.PresentaionLayer.ViewModels
{
    public class ForgetPasswordViewModel
    {
        [EmailAddress]
        [Required]
        public string Email { get; set; }
    }
}
