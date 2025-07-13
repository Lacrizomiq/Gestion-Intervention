using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using GestionInterventions.Models;
using GestionInterventions.Data;
using GestionInterventions.Models.ViewModels;
using GestionInterventions.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace GestionInterventions.Controllers;

public class HomeController : Controller
{
    // Injection de dépendance pour le contexte de la base de données
    private readonly ApplicationDbContext _context;

    // Constructeur pour l'injection de dépendance
    public HomeController(ApplicationDbContext context)
    {
        _context = context;
    }

    // Action pour afficher le dashboard
    public async Task<IActionResult> Index()
    {
        // Créer le ViewModel pour le dashboard
        var dashboardViewModel = new DashboardViewModel();

        // Calculer les statistiques avec des requêtes optimisées
        // Récupérer le nombre total d'interventions
        dashboardViewModel.TotalInterventions = await _context.Interventions.CountAsync();

        // Récupérer le nombre d'interventions en cours
        dashboardViewModel.InterventionsEnCours = await _context.Interventions
            .CountAsync(i => i.Statut == StatutIntervention.EnCours);
        
        // Récupérer le nombre d'interventions terminées
        dashboardViewModel.InterventionsTerminees = await _context.Interventions
            .CountAsync(i => i.Statut == StatutIntervention.Terminee);

        // Récupérer le nombre d'interventions urgentes
        dashboardViewModel.InterventionsUrgentes = await _context.Interventions
            .CountAsync(i => i.Priorite == PrioriteIntervention.Urgent && 
                           i.Statut != StatutIntervention.Terminee);

        // ✅ Récupérer les interventions urgentes non terminées (Limité à 5)
        // Récupérer les interventions urgentes non terminées
        dashboardViewModel.InterventionsUrgentesNonTerminees = await _context.Interventions
            .Where(i => (i.Priorite == PrioriteIntervention.Urgent || i.Priorite == PrioriteIntervention.Haute) &&
                       i.Statut != StatutIntervention.Terminee && 
                       i.Statut != StatutIntervention.Annulee)
            .OrderByDescending(i => i.Priorite)
            .ThenBy(i => i.DateCreation)
            .Take(5)
            .ToListAsync();

        // Retourner la vue avec le ViewModel
        return View(dashboardViewModel);
    }

    // Action pour afficher la page de confidentialité
    public IActionResult Privacy()
    {
        return View();
    }

    // Action pour afficher la page d'erreur
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}