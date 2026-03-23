namespace SageOwl.UI.ViewModels.Users;

public class UpdateUserViewModel
{
    public string Name { get; set; }
    public string Email { get; set; }
    public string Surname { get; set; }

    //Birth
    public int Day { get; set; }
    public int Month { get; set; }
    public int Year { get; set; }

    //Account
    public string Username { get; set; }
    public string Password { get; set; }
    public string ConfirmPassword { get; set; }

    public DateTime BirthDay => new(Year, Month, Day);
}
