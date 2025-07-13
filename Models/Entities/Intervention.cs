using System.ComponentModel.DataAnnotations;

namespace GestionInterventions.Models.Entities
{
  // Classe pour l'intervention
  public class Intervention
  {
    public int Id { get; set;}

    [Required, MaxLength(200)] // Titre de l'intervention, obligatoire et max 200 caractères
    public string Titre { get; set;} = null!; // null! est un opérateur de nullité, il signifie que la propriété ne peut pas être null

    [Required, MaxLength(1000)] // Description de l'intervention, obligatoire et max 1000 caractères
    public string Description { get; set;} = null!; // null! est un opérateur de nullité, il signifie que la propriété ne peut pas être null
    
    public DateTime DateCreation { get; set; } // Date de création de l'intervention
    public PrioriteIntervention Priorite { get; set; } // Priorité de l'intervention
    public StatutIntervention Statut { get; set; } // Statut de l'intervention

    public Intervention()
    {
      DateCreation = DateTime.UtcNow;
      Statut = StatutIntervention.Nouveau;
      Priorite = PrioriteIntervention.Normale;
    }
  }

  // Enumération pour la priorité de l'intervention
  public enum PrioriteIntervention
  {
    Basse,
    Normale,
    Haute,
    Urgent
  }

  // Enumération pour le statut de l'intervention
  public enum StatutIntervention
  {
    Nouveau,
    EnCours,
    Terminee,
    Annulee
  }
}