using GestionInterventions.Models.Entities;

namespace GestionInterventions.Models.ViewModels
{
  public class DashboardViewModel
  {

    // Statistiques
    public int TotalInterventions { get; set;}
    public int InterventionsEnCours { get; set;}
    public int InterventionsTerminees { get; set;}
    public int InterventionsUrgentes { get; set;}

    // Liste des interventions urgentes non terminées
    public List<Intervention> InterventionsUrgentesNonTerminees { get; set;} = new List<Intervention>();
    
    // Propriétés calculées pour l'affichage
    public double PourcentageTerminees => TotalInterventions > 0
      ? Math.Round((double)InterventionsTerminees / TotalInterventions * 100, 1)
      : 0;

      public bool ADesInterventionsUrgentes => InterventionsUrgentesNonTerminees.Any();
  }
}
