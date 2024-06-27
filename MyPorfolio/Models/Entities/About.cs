using System.ComponentModel.DataAnnotations;

namespace MyPorfolio.Models.Entities;

public class About
{
    [Key]
    public int ID { get; set; }

    [MaxLength(100)]
    public string Header { get; set; } = "";
    public string Text { get; set; } = "";
    public DateTime TextDate { get; set; }
    [MaxLength(255)]
    public string? TextPic { get; set; }
}
