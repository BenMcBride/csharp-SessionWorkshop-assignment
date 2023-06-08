#pragma warning disable CS8618
using System.ComponentModel.DataAnnotations;
namespace SessionWorkshop.Models;
public class User
{
  [Required]
  [MinLength(2)]
  [Display(Name = "Your Name:")]
  public string Name { get; set; }
}