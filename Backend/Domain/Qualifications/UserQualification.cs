using Domain.Users;

namespace Domain.Qualifications;

public class UserQualification
{
    private UserQualification()
    {
    }

    private UserQualification(Guid id, Guid userId, double grade, string description)
    {
        Id = id;
        UserId = userId;
        Grade = grade;
        Description = description;
    }

    public Guid Id { get; private set; }
    public Guid UserId { get; set; }
    public double Grade { get; set; }
    public string Description {  get; set; }
    public Guid QualificationId { get; set; }
    public Qualification Qualification { get; set; }
    public User User { get; set; }

    public static UserQualification Create(Guid userId, double grade, string description)
    => new(Guid.NewGuid(),userId,grade,description);

}
