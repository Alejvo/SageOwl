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

    public Guid Id {  get; private set; }
    public Guid TeamId { get; private set; }
    public double MinimumGrade { get; private set; }
    public double MaximumGrade { get; private set; }
    public double PassingGrade { get; private set; }
    public string Period { get; private set; }
    public int TotalGrades { get; private set; }

    public List<UserQualification> UserQualifications { get; set; } = new();
    public Team Team { get; set; }

    public static Qualification Create(Guid teamId, double minimumGrade, double maximumGrade, double passingGrade,string period,int totalGrades)
        => new(Guid.NewGuid(),teamId,minimumGrade,maximumGrade,passingGrade,period,totalGrades);

    public void ChangeMinGrade(double newValue) => MinimumGrade = newValue; 
    public void ChangeMaxGrade(double newValue) => MaximumGrade = newValue; 
    public void ChangePassingGrade(double newValue) => PassingGrade = newValue; 
    public void ChangePeriod(string newValue) => Period = newValue; 
    public void ChangeTotalGrades(int newValue) => TotalGrades = newValue; 

    public void SyncUserQualifications(IEnumerable<UserQualificationInput> incoming)
    {
        var existing = UserQualifications
            .ToDictionary(x => (x.UserId, x.Description));

        var incomingKeys = incoming
            .Select(x => (x.UserId, x.Description))
            .ToHashSet();

        UserQualifications.RemoveAll(x =>
            !incomingKeys.Contains((x.Id, x.Description)));

        foreach (var uq in incoming)
        {
            var key = (uq.UserId,uq.Description);
            if (existing.TryGetValue(key, out var entity))
            {
                // Update
                entity.Description = uq.Description;
                entity.Grade = uq.Grade;
            }
            else
            {
                // Insert
                UserQualifications.Add(UserQualification.Create(
                    uq.UserId,
                    Id,
                    uq.Grade,
                    uq.Description
                    ));
            }
        }
    }

    public void AddUserQualification(Guid userId, double grade, string description)
    {
        var uq = UserQualification.Create(userId, Id,grade, description);

        UserQualifications.Add(uq); 
    }
}
