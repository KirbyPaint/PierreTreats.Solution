using System.Collections.Generic;

namespace PierreTreats.Models
{
  public class Flavor
  {
    public Flavor()
    {
      this.JoinEntities = new HashSet<FlavorTreat>();
    }

    public int FlavorId { get; set; }
    public string Name { get; set; }
    public FlavorRating Rating { get; set; }
    public virtual ApplicationUser User { get; set; }
    public virtual ICollection<FlavorTreat> JoinEntities { get; set; }
  }

  public enum FlavorRating
  {
    One, // = 0
    Two,
    Three,
    Four,
    Five // = 4
  }
}