using System.ComponentModel.DataAnnotations;

namespace MyPorfolio.Models.Entities;

public class Project
{
    [Key]
    public int ProjectID { get; set; }
    [MaxLength(150)]
    public string ProjectName { get; set; }
    public string ProjectDescription { get; set; }
    public DateTime ProjectDate { get; set; }
    [MaxLength(255)]
    public string? ProjectPic { get; set; }
    [MaxLength(255)]
    public string ProjectInfo { get; set; } = "";
}
