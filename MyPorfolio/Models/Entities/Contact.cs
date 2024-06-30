using System.ComponentModel.DataAnnotations;

namespace MyPorfolio.Models.Entities;

public class Contact
{
    [Key]
    public int ContactID { get; set; }
    [MaxLength(50)]
    public string Firstname { get; set; }
    [MaxLength(50)]
    public string Lastname { get; set; }
    [MaxLength(255)]
    public string Email { get; set; }
    public string Message { get; set; }
}