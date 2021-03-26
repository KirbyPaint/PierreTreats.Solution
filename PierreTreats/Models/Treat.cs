using System;
using System.Collections.Generic;

namespace PierreTreats.Models
{
  public class Treat
  {
    public Treat()
    {
      this.JoinEntities = new HashSet<FlavorTreat>();
    }
    public int TreatId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public Rating Rating { get; set; }
    public string Price { get; set; }
    public virtual ApplicationUser User { get; set; }
    public virtual ICollection<FlavorTreat> JoinEntities { get; }
  }

  public enum Rating
  {
    One, // = 0
    Two,
    Three,
    Four,
    Five // = 4
  }
}