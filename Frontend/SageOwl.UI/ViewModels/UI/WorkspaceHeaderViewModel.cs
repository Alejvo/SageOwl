namespace SageOwl.UI.ViewModels.UI;

public class WorkspaceHeaderViewModel
{
    public string Url { get; set; } = string.Empty;
    public string Tooltip { get; set; } = string.Empty;
    public string Title {  get; set; } = string.Empty;
    public ProfileInfoViewModel ProfileInfo { get; set; }
}
