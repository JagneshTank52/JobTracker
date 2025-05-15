using Microsoft.AspNetCore.Mvc.Rendering;

namespace TestAssignment.Repository.ViewModel;

public class ReviewModal
{
    public int StatusId { get; set; }
    public string StatusName { get; set; } = string.Empty;
    public List<SelectListItem> StatusList = new List<SelectListItem>();
    public string Comment { get; set; } = string.Empty;
    public int JobUserMappingId { get; set; }
}
