using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PierreTreats.Models;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Claims;

namespace PierreTreats.Controllers
{
  [Authorize]
  public class TreatsController : Controller
  {
    private readonly PierreTreatsContext _db;
    private readonly UserManager<ApplicationUser> _userManager;

    public TreatsController(PierreTreatsContext db, UserManager<ApplicationUser> userManager)
    {
      _db = db;
      _userManager = userManager;
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
          var userTreats = _db.Treats.Where(entry => entry.User.Id == currentUser.Id).OrderByDescending(model => model.Rating).ToList();
          ModelState.Clear();
          return View(userTreats);
        }
        else
        {
          if (!(String.IsNullOrEmpty(userInput)))
          {
            var userTreats = _db.Treats.Where(entry => entry.User.Id == currentUser.Id).Where(model => model.Name.Contains(userInput)).ToList();
            return View(userTreats);
          }
          else
          {
            var userTreats = _db.Treats.Where(entry => entry.User.Id == currentUser.Id).ToList();
            return View(userTreats);
          }
        }
      }
      else
      {
        if (userInput == "Rating")
        {
          var userTreats = _db.Treats.OrderByDescending(model => model.Rating).ToList();
          ModelState.Clear();
          return View(userTreats);
        }
        else
        {
          if (!(String.IsNullOrEmpty(userInput)))
          {
            var userTreats = _db.Treats.Where(model => model.Name.Contains(userInput)).ToList();
            return View(userTreats);
          }
          else
          {
            var userTreats = _db.Treats.ToList();
            return View(userTreats);
          }
        }
      }
    }

    public ActionResult Create()
    {
      ViewBag.FlavorId = new SelectList(_db.Flavors, "FlavorId", "Name");
      return View();
    }

    [HttpPost]
    public async Task<ActionResult> Create(Treat treat, int FlavorId)
    {
      var userId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
      var currentUser = await _userManager.FindByIdAsync(userId);
      treat.User = currentUser;
      _db.Treats.Add(treat);
      _db.SaveChanges();
      if (FlavorId != 0)
      {
        _db.FlavorTreat.Add(new FlavorTreat() { FlavorId = FlavorId, TreatId = treat.TreatId });
      }
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    [AllowAnonymous]
    public ActionResult Details(int id)
    {
      var thisTreat = _db.Treats
        .Include(treat => treat.JoinEntities)
        .ThenInclude(join => join.Flavor)
        .FirstOrDefault(treat => treat.TreatId == id);
      return View(thisTreat);
    }

    public ActionResult Edit(int id)
    {
      var thisTreat = _db.Treats.FirstOrDefault(treat => treat.TreatId == id);
      ViewBag.FlavorId = new SelectList(_db.Flavors, "FlavorId", "Name");
      return View(thisTreat);
    }

    [HttpPost]
    public ActionResult Edit(Treat treat, int FlavorId)
    {
      if (FlavorId != 0)
      {
        _db.FlavorTreat.Add(new FlavorTreat() { FlavorId = FlavorId, TreatId = treat.TreatId });
      }
      _db.Entry(treat).State = EntityState.Modified;
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    public async Task<ActionResult> AddFlavor(int id)
    {
      var userId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
      var currentUser = await _userManager.FindByIdAsync(userId);
      var thisTreat = _db.Treats.FirstOrDefault(treat => treat.TreatId == id);
      var userFlavors = _db.Flavors.Where(entry => entry.User.Id == currentUser.Id).ToList();
      ViewBag.FlavorId = new SelectList(userFlavors, "FlavorId", "Name");
      return View(thisTreat);
    }

    [HttpPost]
    public ActionResult AddFlavor(Treat treat, int FlavorId)
    {
      if (FlavorId != 0)
      {
        _db.FlavorTreat.Add(new FlavorTreat() { FlavorId = FlavorId, TreatId = treat.TreatId });
      }
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    public ActionResult Delete(int id)
    {
      var thisTreat = _db.Treats.FirstOrDefault(treat => treat.TreatId == id);
      return View(thisTreat);
    }

    [HttpPost, ActionName("Delete")]
    public ActionResult DeleteConfirmed(int id)
    {
      var thisTreat = _db.Treats.FirstOrDefault(treat => treat.TreatId == id);
      _db.Treats.Remove(thisTreat);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    [HttpPost]
    public ActionResult DeleteFlavor(int joinId)
    {
      var joinEntry = _db.FlavorTreat.FirstOrDefault(entry => entry.FlavorTreatId == joinId);
      int treatId = joinEntry.TreatId;
      _db.FlavorTreat.Remove(joinEntry);
      _db.SaveChanges();
      return RedirectToAction("Details", "Treats", new { id = treatId });
    }
  }
}