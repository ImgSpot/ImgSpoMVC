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
    public List<string> SelectedPicture { get; set; }

    [Required(ErrorMessage = "Please enter a comment")]
    [DataType(DataType.Text)]
    public string SelectedComment { get; set; }

    [Required(ErrorMessage = "Please enter your username")]
    [DataType(DataType.Text)]
    public string SelectedUser { get; set; }

    public IEnumerable<ValidationResult> ValidationResults(ValidationContext validationContext)
    {
      throw new NotImplementedException();
    }
  }
}