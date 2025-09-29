using Domain.Teams;

namespace Domain.Qualifications;

public class Qualification
{
    private Qualification()
    {
    }

    private Qualification(Guid id,Guid teamId, double minimumGrade, double maximumGrade, double passingGrade, string period, int totalGrades)
    {
        Id = id;
        TeamId = teamId;
        MinimumGrade = minimumGrade;
        MaximumGrade = maximumGrade;
        PassingGrade = passingGrade;
        Period = period;
        TotalGrades = totalGrades;
    }

    public Guid Id {  get; set; }
    public Guid TeamId { get; set; }
    public double MinimumGrade { get; set; }
    public double MaximumGrade { get; set; }
    public double PassingGrade { get; set; }
    public string Period { get; set; }
    public int TotalGrades { get; set; }

    private List<UserQualification> _userQualifications = new();
    public IReadOnlyCollection<UserQualification> UserQualifications => _userQualifications.AsReadOnly();
    public Team Team { get; set; }

    public static Qualification Create(Guid teamId, double minimumGrade, double maximumGrade, double passingGrade,string period,int totalGrades)
        => new(Guid.NewGuid(),teamId,minimumGrade,maximumGrade,passingGrade,period,totalGrades);

    public void AddUserQualification(Guid userId,double grade,int position,bool hasValue, string? description = null)
    {
        var existingUQ = _userQualifications.FirstOrDefault(q => q.UserId == userId && q.Position == position);

        if(existingUQ is null)
        {
            if (description != null)
                _userQualifications.Add(UserQualification.Create(userId,grade, position, hasValue,description));
            else
                _userQualifications.Add(UserQualification.Create(userId,grade, position, hasValue));
        }
        else
        {
            existingUQ.Update(grade, position, hasValue,description);
        }
    }
}
