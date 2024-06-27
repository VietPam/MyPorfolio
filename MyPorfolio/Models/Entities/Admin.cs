using System.ComponentModel.DataAnnotations;

namespace MyPorfolio.Models.Entities;

public class Admin
{
    [Key]
    public int UserID { get; set; }
    [MaxLength(50)]
    public string UserName { get; set; } = "";
    [MaxLength(50)]
    public string Password { get; set; } = "";
}
