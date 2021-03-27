using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PierreTreats.Models;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;


namespace PierreTreats.Controllers
{
  [Authorize]
  public class FlavorsController : Controller
  {
    private readonly PierreTreatsContext _db;
    private readonly UserManager<ApplicationUser> _userManager;

    public FlavorsController(UserManager<ApplicationUser> userManager, PierreTreatsContext db)
    {
      _userManager = userManager;
      _db = db;
    }

    [AllowAnonymous]
    public async Task<ActionResult> Index(string userInput)
    {
      var userId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
      var currentUser = await _userManager.FindByIdAsync(userId);
      if (User.Identity.IsAuthenticated)
      {
        if (userInput == "Rating")
        {
          var userFlavors = _db.Flavors.Where(entry => entry.User.Id == currentUser.Id).OrderByDescending(model => model.Rating).ToList();
          ModelState.Clear();
          return View(userFlavors);
        }
        else
        {
          if (!(String.IsNullOrEmpty(userInput)))
          {
            var userFlavors = _db.Flavors.Where(entry => entry.User.Id == currentUser.Id).Where(model => model.Name.Contains(userInput)).ToList();
            return View(userFlavors);
          }
          else
          {
            var userFlavors = _db.Flavors.Where(entry => entry.User.Id == currentUser.Id).ToList();
            return View(userFlavors);
          }
        }
      }
      else
      {
        if (userInput == "Rating")
        {
          var userFlavors = _db.Flavors.OrderByDescending(model => model.Rating).ToList();
          ModelState.Clear();
          return View(userFlavors);
        }
        else
        {
          if (!(String.IsNullOrEmpty(userInput)))
          {
            var userFlavors = _db.Flavors.Where(model => model.Name.Contains(userInput)).ToList();
            return View(userFlavors);
          }
          else
          {
            var userFlavors = _db.Flavors.ToList();
            return View(userFlavors);
          }
        }
      }
    }

    public ActionResult Create()
    {
      return View();
    }

    [HttpPost]
    public async Task<ActionResult> Create(Flavor flavor)
    {
      var userId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
      var currentUser = await _userManager.FindByIdAsync(userId);
      flavor.User = currentUser;
      _db.Flavors.Add(flavor);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    [AllowAnonymous]
    public ActionResult Details(int id)
    {
      var thisFlavor = _db.Flavors
          .Include(flavor => flavor.JoinEntities)
          .ThenInclude(join => join.Treat)
          .FirstOrDefault(flavor => flavor.FlavorId == id);
      return View(thisFlavor);
    }

    public ActionResult Edit(int id)
    {
      var thisFlavor = _db.Flavors.FirstOrDefault(flavor => flavor.FlavorId == id);
      return View(thisFlavor);
    }

    [HttpPost]
    public ActionResult Edit(Flavor flavor)
    {
      _db.Entry(flavor).State = EntityState.Modified;
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    public ActionResult Delete(int id)
    {
      var thisFlavor = _db.Flavors.FirstOrDefault(flavor => flavor.FlavorId == id);
      return View(thisFlavor);
    }

    [HttpPost, ActionName("Delete")]
    public ActionResult DeleteConfirmed(int id)
    {
      var thisFlavor = _db.Flavors.FirstOrDefault(flavor => flavor.FlavorId == id);
      _db.Flavors.Remove(thisFlavor);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    public async Task<ActionResult> AddTreat(int id)
    {
      var userId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
      var currentUser = await _userManager.FindByIdAsync(userId);
      var thisFlavor = _db.Flavors.FirstOrDefault(flavor => flavor.FlavorId == id);
      var userTreats = _db.Treats.Where(entry => entry.User.Id == currentUser.Id).ToList();
      ViewBag.TreatId = new SelectList(userTreats, "TreatId", "Name");
      return View(thisFlavor);
    }

    [HttpPost]
    public ActionResult AddTreat(Flavor flavor, int TreatId)
    {
      if (TreatId != 0)
      {
        _db.FlavorTreat.Add(new FlavorTreat() { TreatId = TreatId, FlavorId = flavor.FlavorId });
      }
      _db.SaveChanges();
      return RedirectToAction("Details", "Flavors", new { id = flavor.FlavorId });
    }

    [HttpPost]
    public ActionResult DeleteTreat(int joinId)
    {
      var joinEntry = _db.FlavorTreat.FirstOrDefault(entry => entry.FlavorTreatId == joinId);
      int flavorId = joinEntry.FlavorId;
      _db.FlavorTreat.Remove(joinEntry);
      _db.SaveChanges();
      return RedirectToAction("Details", "Flavors", new { id = flavorId });
    }
  }
}