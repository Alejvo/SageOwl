using SageOwl.UI.ViewModels.Forms;

namespace SageOwl.UI.ViewModels.Teams.UI;

public class TeamFormsPageViewModel
{
    public Guid TeamId {  get; set; }
    public List<FormViewModel> Forms { get; set; } = [];
}
