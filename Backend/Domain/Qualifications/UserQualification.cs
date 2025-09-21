using Domain.Users;

namespace Domain.Qualifications;

public class UserQualification
{
    private UserQualification()
    {
    }

    private UserQualification(Guid id, Guid userId, Guid qualificationId,  double grade, int position , bool hasValue, string? description = null)
    {
        Id = id;
        UserId = userId;
        QualificationId = qualificationId;
        Grade = grade;
        Description = description;
        Position = position;
        HasValue = hasValue;
    }

    public Guid Id { get; private set; }
    public Guid UserId { get; set; }
    public double Grade { get; set; }
    public string? Description {  get; set; }
    public int Position { get; set; }
    public bool HasValue { get; set; } = false;
    public Guid QualificationId { get; set; }
    public User User { get; set; }

    public static UserQualification Create(Guid userId, Guid qualificationId,double grade, int position,bool hasValue,string? description = null)
        => new(Guid.NewGuid(),userId,qualificationId,grade,position,hasValue,description);

    public static UserQualification Create(Guid userId, Guid qualificationId,double grade,int position,bool hasValue)
    => new(Guid.NewGuid(),userId,qualificationId,grade,position,hasValue);

    public void Update(double grade, int position, bool hasValue, string? description = null)
    {
        Grade = grade;
        Position = position;
        Description = description;
        HasValue = hasValue;
    }
}
