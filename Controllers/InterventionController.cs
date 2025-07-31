using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using GestionInterventions.Models;
using GestionInterventions.Data;
using GestionInterventions.Models.ViewModels;
using GestionInterventions.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace GestionInterventions.Controllers;

public class InterventionController : Controller
{
  // Injection de dépendance pour le contexte de la base de données
  private readonly ApplicationDbContext _context;

  // Constructeur pour l'injection de dépendance
  public InterventionController(ApplicationDbContext context)
  {
    _context = context;
  }

  // Action pour afficher les interventions
  public async Task<IActionResult> Index(string? searchText, StatutIntervention? statut, PrioriteIntervention? priorite, string? dateCreation)
  {
    // Créer une requête pour les interventions
    var query = _context.Interventions.AsQueryable();

    // Condition pour la recherche
    if (!string.IsNullOrWhiteSpace(searchText))
    {
      query = query.Where(i => i.Titre.Contains(searchText) || i.Description.Contains(searchText));
    }

    // Condition pour le statut
    if (statut.HasValue)
    {
      query = query.Where(i => i.Statut == statut.Value);
    }

    // Condition pour la priorité
    if (priorite.HasValue)
    {
      query = query.Where(i => i.Priorite == priorite.Value);
    }

    // Condition pour la date de création
    if (!string.IsNullOrEmpty(dateCreation))
    {
      if (dateCreation == "asc")
      {
        query = query.OrderBy(i => i.DateCreation);
      }
      else if (dateCreation == "desc")
      {
        query = query.OrderByDescending(i => i.DateCreation);
      }
    }
    

    // Récupérer les interventions avec les filtres
    var interventions = await query.ToListAsync();

    // Passer les valeurs des filtres à la vue
    ViewBag.SearchText = searchText;
    ViewBag.SelectedStatut = statut;
    ViewBag.SelectedPriorite = priorite;
    ViewBag.SelectedDateCreation = dateCreation;

    return View(interventions);
  }

  // Action pour afficher les détails d'une intervention
  public async Task<IActionResult> Details(int id)
  {
    try
    {
      
    // Récupérer l'intervention par son ID
    var intervention = await _context.Interventions.FindAsync(id);

    if (intervention == null)
    {
      return NotFound();
    }

    return View(intervention);
    }
    catch (Exception)
    {
      // Log l'erreur
      return RedirectToAction("Index");
    }
  }  

  // Action pour afficher le formulaire de création d'une intervention
  public IActionResult Create()
  {
    return View();
  }

  // Action pour créer une intervention
  [HttpPost]
  public async Task<IActionResult> Create(Intervention intervention)
  {
    if (ModelState.IsValid)
    {
      try {
        // Si le modèle est valide, on ajoute l'intervention à la base de données
        intervention.DateCreation = DateTime.Now;
        _context.Interventions.Add(intervention);
        await _context.SaveChangesAsync();
        return RedirectToAction("Details", new { id = intervention.Id });
      }
      catch (Exception)
      {
        // Log l'erreur
        ModelState.AddModelError(string.Empty, "Une erreur est survenue lors de la création de l'intervention");
        return View(intervention);
      }
    }
    // Si le modèle n'est pas valide, on retourne la vue avec les erreurs
    return View(intervention);
  }
}