using GestionInterventions.Data;
using GestionInterventions.Models.Entities;

namespace GestionInterventions.Services
{
    public interface IDataSeeder
    {
        Task SeedAsync();
    }

    public class DataSeeder : IDataSeeder
    {
        private readonly ApplicationDbContext _context;

        public DataSeeder(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task SeedAsync()
        {
            // Vérifie si la table Interventions est vide
            if (!_context.Interventions.Any())
            {
                Console.WriteLine("🌱 Création des données de test...");

                // Crée une liste d'interventions de test
                var interventionsTest = new List<Intervention>
                {
                    new Intervention
                    {
                        Titre = "Panne serveur principal",
                        Description = "Le serveur principal ne répond plus depuis ce matin. Intervention urgente requise.",
                        Priorite = PrioriteIntervention.Urgent,
                        Statut = StatutIntervention.EnCours
                    },
                    new Intervention
                    {
                        Titre = "Maintenance climatisation bureau 2",
                        Description = "Entretien préventif du système de climatisation au deuxième étage.",
                        Priorite = PrioriteIntervention.Normale,
                        Statut = StatutIntervention.Nouveau
                    },
                    new Intervention
                    {
                        Titre = "Réparation imprimante accueil",
                        Description = "L'imprimante de l'accueil fait du bourrage papier constant.",
                        Priorite = PrioriteIntervention.Basse,
                        Statut = StatutIntervention.Terminee
                    },
                    new Intervention
                    {
                        Titre = "Installation nouveau poste informatique",
                        Description = "Configuration et installation d'un nouveau poste pour le service comptabilité.",
                        Priorite = PrioriteIntervention.Normale,
                        Statut = StatutIntervention.EnCours
                    },
                    new Intervention
                    {
                        Titre = "Problème réseau salle de réunion",
                        Description = "Pas de connexion internet dans la salle de réunion principale.",
                        Priorite = PrioriteIntervention.Haute,
                        Statut = StatutIntervention.Nouveau
                    },
                    new Intervention
                    {
                        Titre = "Changement écran défaillant",
                        Description = "L'écran du poste 15 affiche des lignes verticales.",
                        Priorite = PrioriteIntervention.Basse,
                        Statut = StatutIntervention.Annulee
                    },
                    new Intervention
                    {
                        Titre = "Mise à jour système de sécurité",
                        Description = "Mise à jour critique des systèmes de sécurité informatique.",
                        Priorite = PrioriteIntervention.Urgent,
                        Statut = StatutIntervention.Terminee
                    },
                    new Intervention
                    {
                        Titre = "Maintenance ascenseur",
                        Description = "Contrôle technique obligatoire de l'ascenseur principal.",
                        Priorite = PrioriteIntervention.Haute,
                        Statut = StatutIntervention.EnCours
                    },
                    new Intervention
                    {
                        Titre = "Réparation fuite d'eau cuisine",
                        Description = "Fuite sous l'évier de la cuisine du personnel.",
                        Priorite = PrioriteIntervention.Normale,
                        Statut = StatutIntervention.Nouveau
                    },
                    new Intervention
                    {
                        Titre = "Remplacement néons bureau",
                        Description = "Plusieurs néons sont grillés dans le bureau principal.",
                        Priorite = PrioriteIntervention.Basse,
                        Statut = StatutIntervention.Terminee
                    },
                    new Intervention
                    {
                        Titre = "Configuration firewall",
                        Description = "Mise à jour des règles de sécurité du firewall principal.",
                        Priorite = PrioriteIntervention.Haute,
                        Statut = StatutIntervention.Nouveau
                    },
                    new Intervention
                    {
                        Titre = "Problème téléphone ligne 3",
                        Description = "Le téléphone de la ligne 3 ne fonctionne plus.",
                        Priorite = PrioriteIntervention.Normale,
                        Statut = StatutIntervention.EnCours
                    }
                };

                // Ajoute toutes les interventions au contexte
                _context.Interventions.AddRange(interventionsTest);

                // Sauvegarde en base de données (version async)
                await _context.SaveChangesAsync();

                Console.WriteLine($"✅ {interventionsTest.Count} interventions de test créées !");
            }
            else
            {
                Console.WriteLine("ℹ️ Des données existent déjà, pas de seeding nécessaire.");
            }
        }
    }
}