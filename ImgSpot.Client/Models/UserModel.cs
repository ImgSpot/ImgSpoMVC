using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;

namespace ImgSpot.Client.Models
{
  public class UserModel 
  {
    [Required(ErrorMessage = "Please select a file")]
    [DataType(DataType.Upload)]
    public string File { get; set; }

    [Required(ErrorMessage = "Please enter a comment")]
    [DataType(DataType.Text)]
    public string Comment { get; set; }

    [Required(ErrorMessage = "Please enter your first name")]
    [DataType(DataType.Text)]
    public string Fname { get; set; }

    [Required(ErrorMessage = "Please enter your last name")]
    [DataType(DataType.Text)]
    public string Lname { get; set; }

    public IEnumerable<ValidationResult> ValidationResults(ValidationContext validationContext)
    {
      throw new NotImplementedException();
    }
  }
}