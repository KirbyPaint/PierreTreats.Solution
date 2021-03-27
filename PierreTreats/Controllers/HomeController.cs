using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using PierreTreats.Models;

namespace PierreTreats.Controllers
{
  public class HomeController : Controller
  {
    private readonly PierreTreatsContext _db;
    public HomeController(PierreTreatsContext db)
    {
      _db = db;
    }
    [HttpGet("/")]
    public ActionResult Index()
    {
      List<Flavor> model = _db.Flavors.ToList();
      return View(model);
    }
  }
}
