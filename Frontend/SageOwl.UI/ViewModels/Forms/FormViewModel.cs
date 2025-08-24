namespace SageOwl.UI.ViewModels.Forms;
public class FormViewModel
{
    public Guid Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public Guid TeamId { get; set; }
    public DateTime Deadline { get; set; }
    public List<QuestionViewModel> Questions { get; set; }
    public QuestionType FormType { get; set; }
}
