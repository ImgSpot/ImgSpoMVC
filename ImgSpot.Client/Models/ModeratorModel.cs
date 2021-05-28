namespace ImgSpot.Client.Models
{
  public class ModeratorModel
  {
  public Classification Classification { get; set; }
  }
  public class Classification
  {
    public Category Category3 { get; set; }
  }
  public class Category
  {
    public int Score { get; set; }
  }
}