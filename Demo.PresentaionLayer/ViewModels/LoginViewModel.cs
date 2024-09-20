namespace Demo.PresentaionLayer.ViewModels
{
	public class LoginViewModel
	{
		[Required]
		[EmailAddress(ErrorMessage = "Invalid Email")]
		public string Email { get; set; }
		[Required]
		[DataType(DataType.Password)]
		public string Password { get; set; }

		public bool RememberMe { get; set; }
	}
}
