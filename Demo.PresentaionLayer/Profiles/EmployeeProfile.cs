namespace Demo.PresentaionLayer.Profiles
{
    public class EmployeeProfile:Profile
    {
        public EmployeeProfile()
        {
            CreateMap<Employee,EmployeeViewModel>().ReverseMap();
        }
    }
}
