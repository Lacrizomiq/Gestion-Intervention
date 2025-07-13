using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using GestionInterventions.Models;
using GestionInterventions.Data;
using GestionInterventions.Models.ViewModels;
using GestionInterventions.Models.Entities;
using Microsoft.EntityFrameworkCore;

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
  public async Task<IActionResult> Index(string? searchText, StatutIntervention? statut, PrioriteIntervention? priorite)
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

    // Récupérer les interventions avec les filtres
    var interventions = await query.ToListAsync();

    // Passer les valeurs des filtres à la vue
    ViewBag.SearchText = searchText;
    ViewBag.Statut = statut;
    ViewBag.Priorite = priorite;

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
    catch (System.Exception)
    {
      // Log l'erreur
      return RedirectToAction("Index");
    }
  }  
}