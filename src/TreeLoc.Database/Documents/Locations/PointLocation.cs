namespace TreeLoc.Database.Documents.Locations
{
  public class PointLocation: LocationBase
  {
    public override string type { get; set; } = "Point";

    public int[] coordinates { get; set; } = default!;
  }
}
