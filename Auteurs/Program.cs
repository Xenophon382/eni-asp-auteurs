﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Auteurs.BO;

namespace Auteurs
{
    class Program
    {

        private static List<Auteur> ListeAuteurs = new List<Auteur>();
        private static List<Livre> ListeLivres = new List<Livre>();

        private static void InitialiserDatas()
        {
            ListeAuteurs.Add(new Auteur("GROUSSARD", "Thierry"));
            ListeAuteurs.Add(new Auteur("GABILLAUD", "Jérôme"));
            ListeAuteurs.Add(new Auteur("HUGON", "Jérôme"));
            ListeAuteurs.Add(new Auteur("ALESSANDRI", "Olivier"));
            ListeAuteurs.Add(new Auteur("de QUAJOUX", "Benoit"));
            ListeLivres.Add(new Livre(1, "C# 4", "Les fondamentaux du langage", ListeAuteurs.ElementAt(0), 533));
            ListeLivres.Add(new Livre(2, "VB.NET", "Les fondamentaux du langage", ListeAuteurs.ElementAt(0), 539));
            ListeLivres.Add(new Livre(3, "SQL Server 2008", "SQL, Transact SQL", ListeAuteurs.ElementAt(1), 311));
            ListeLivres.Add(new Livre(4, "ASP.NET 4.0 et C#", "Sous visual studio 2010", ListeAuteurs.ElementAt(3), 544));
            ListeLivres.Add(new Livre(5, "C# 4", "Développez des applications windows avec visual studio 2010", ListeAuteurs.ElementAt(2), 452));
            ListeLivres.Add(new Livre(6, "Java 7", "les fondamentaux du langage", ListeAuteurs.ElementAt(0), 416));
            ListeLivres.Add(new Livre(7, "SQL et Algèbre relationnelle", "Notions de base", ListeAuteurs.ElementAt(1), 216));
            ListeAuteurs.ElementAt(0).addFacture(new Facture(3500, ListeAuteurs.ElementAt(0)));
            ListeAuteurs.ElementAt(0).addFacture(new Facture(3200, ListeAuteurs.ElementAt(0)));
            ListeAuteurs.ElementAt(1).addFacture(new Facture(4000, ListeAuteurs.ElementAt(1)));
            ListeAuteurs.ElementAt(2).addFacture(new Facture(4200, ListeAuteurs.ElementAt(2)));
            ListeAuteurs.ElementAt(3).addFacture(new Facture(3700, ListeAuteurs.ElementAt(3)));
        }

        static void Main(string[] args)
        {
            InitialiserDatas();

            // Afficher la liste des prénoms des auteurs dont le nom commence par "G"
            {
                Console.WriteLine("Afficher la liste des prénoms des auteurs dont le nom commence par \"G\" :");
                var listeDesAuteursCommancantParG = ListeAuteurs.Where(s => s.Nom.StartsWith("G"));
                foreach (var auteur in listeDesAuteursCommancantParG)
                {
                    Console.WriteLine($"- {auteur.Prenom}");
                }
                Console.WriteLine();
            }
            // Afficher l’auteur ayant écrit le plus de livres
            {
                Console.WriteLine("Afficher l’auteur ayant écrit le plus de livres :");
                var auteurAyantEcritPlusDeLivre = ListeLivres.GroupBy(l => l.Auteur).OrderByDescending(g => g.Count()).FirstOrDefault().Key;
                Console.WriteLine($" - {auteurAyantEcritPlusDeLivre.Prenom} {auteurAyantEcritPlusDeLivre.Nom}");
                Console.WriteLine();
            }
            // Afficher le nombre moyen de pages par livre par auteur
            {
                Console.WriteLine("Afficher le nombre moyen de pages par livre par auteur :");
                var livreParAuteur = ListeLivres.GroupBy(l => l.Auteur);
                foreach (var lvpa in livreParAuteur)
                {
                    Console.WriteLine($" - {lvpa.Key.Nom} {lvpa.Key.Prenom} : moyenne de pages = {lvpa.Average(l => l.NbPages)}");
                }
                Console.WriteLine();
            }
            // Afficher le titre du livre avec le plus de pages
            {
                Console.WriteLine("Afficher le titre du livre avec le plus de pages :");
                var livreAvecPlusDePages = ListeLivres.OrderByDescending(l => l.NbPages).First();
                Console.WriteLine($" - {livreAvecPlusDePages.Titre} - {livreAvecPlusDePages.NbPages}");
            }
            // Afficher combien ont gagné les auteurs en moyenne(moyenne des factures)
            {
                Console.WriteLine("Afficher combien ont gagné les auteurs en moyenne(moyenne des factures) :");
                var gainMoyen = ListeAuteurs.Average(a => a.Factures.Sum(f => f.Montant));
                Console.WriteLine($" - Les auteurs ont gagnés en moyenne {gainMoyen}");
                Console.WriteLine();
            }
            // Afficher les auteurs et la liste de leurs livres
            {
                Console.WriteLine("Afficher les auteurs et la liste de leurs livres :");
                var livreParAuteur = ListeLivres.GroupBy(l => l.Auteur);
                foreach (var lvpa in livreParAuteur)
                {
                    Console.WriteLine($"    - Auteur : {lvpa.Key.Nom} {lvpa.Key.Prenom}");
                    foreach (var livre in lvpa)
                    {
                        Console.WriteLine($"        - {livre.Titre}");
                    }
                }
                Console.WriteLine();
            }
            // Afficher les titres de tous les livres triés par ordre alphabétique
            {
                Console.WriteLine("Afficher les titres de tous les livres triés par ordre alphabétique");
                ListeLivres.Select(l => l.Titre).OrderBy(t => t).ToList().ForEach(Console.WriteLine);
                Console.WriteLine();
            }
            // Afficher la liste des livres dont le nombre de pages est supérieur à la moyenne
            {
                Console.WriteLine("Afficher la liste des livres dont le nombre de pages est supérieur à la moyenne :");
                var moyennePages = ListeLivres.Average(l => l.NbPages);
                var livresNbPagesSupMoyenne = ListeLivres.Where(l => l.NbPages > moyennePages);
                foreach (var livre in livresNbPagesSupMoyenne)
                {
                    Console.WriteLine($" - {livre.Titre}");
                }
                Console.WriteLine();
            }
            // Afficher l'auteur ayant écrit le moins de livres
            {
                Console.WriteLine("Afficher l'auteur ayant écrit le moins de livres :");
                var auteurAyantEcritLeMoinsDeLivres = ListeAuteurs.OrderBy(a => ListeLivres.Count(l => l.Auteur == a)).FirstOrDefault();
                Console.WriteLine($" - {auteurAyantEcritLeMoinsDeLivres.Prenom} {auteurAyantEcritLeMoinsDeLivres.Nom}");
                Console.ReadKey();
            }

            Console.ReadKey();
        }
    }
}
