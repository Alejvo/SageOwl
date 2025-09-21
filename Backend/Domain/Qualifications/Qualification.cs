using Domain.Teams;

namespace Domain.Qualifications;

public class Qualification
{
    private Qualification()
    {
    }

    private Qualification(Guid teamId, double minimumGrade, double maximumGrade, double passingGrade, int period)
    {
        TeamId = teamId;
        MinimumGrade = minimumGrade;
        MaximumGrade = maximumGrade;
        PassingGrade = passingGrade;
        Period = period;
    }

    public Guid Id {  get; set; }
    public Guid TeamId { get; set; }
    public double MinimumGrade { get; set; }
    public double MaximumGrade { get; set; }
    public double PassingGrade { get; set; }
    public int Period { get; set; }

    private readonly List<UserQualification> _usersQualifications = new();
    public IReadOnlyCollection<UserQualification> UsersQualifications => _usersQualifications.AsReadOnly();
    public Team Team { get; set; }

    public static Qualification Create(Guid teamId, double minimumGrade, double maximumGrade, double passingGrade,int period)
        => new(teamId,minimumGrade,maximumGrade,passingGrade,period);

    public void AddUserQualification(Guid userId,double grade,int position,bool hasValue, string? description = null)
    {
        var existingUQ = _usersQualifications.FirstOrDefault(q => q.UserId == userId && q.Position == position);

        if(existingUQ is null)
        {
            if (description != null)
                _usersQualifications.Add(UserQualification.Create(userId,Id,grade, position, hasValue,description));
            else
                _usersQualifications.Add(UserQualification.Create(userId,Id,grade, position, hasValue));
        }
        else
        {
            existingUQ.Update(grade, position, hasValue,description);
        }
    }
}
