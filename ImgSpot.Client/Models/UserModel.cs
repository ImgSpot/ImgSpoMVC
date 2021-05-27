using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using static System.Net.WebRequestMethods;

namespace ImgSpot.Client.Models
{
  public class UserModel 
  {
    [Required(ErrorMessage = "Please select a file")]
    [DataType(DataType.Upload)]
    public List<string> Filename { get; set; }

    [Required(ErrorMessage = "Please enter a comment")]
    [DataType(DataType.Text)]
    public string Body { get; set; }

    [Required(ErrorMessage = "Please enter your first name")]
    [DataType(DataType.Text)]
    public string FirstName { get; set; }

    [Required(ErrorMessage = "Please enter your last name")]
    [DataType(DataType.Text)]
    public string LastName { get; set; }
    public string Username { get; set; }
    public int CountLikes { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }

    public IEnumerable<ValidationResult> ValidationResults(ValidationContext validationContext)
    {
      throw new NotImplementedException();
    }
  }
}