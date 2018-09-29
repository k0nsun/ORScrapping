using EasyOR.DataAccess.SqlServer;
using EasyOR.DTO;
using EasyOR.MIgration.DTOOld;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EasyOR.MIgration
{
    class Program
    {

        public static List<int> QueteAlreadyProcess = new List<int>();
        public static List<int> QuestError = new List<int>();
        public static List<int> QuestAddBug = new List<int>();
        public static bool turnOn = true;
        static void Main(string[] args)
        {

            int countLast = 0;
            while (turnOn)
            {
                countLast = QueteAlreadyProcess.Count;
                MergeBDD();
                if (countLast == QueteAlreadyProcess.Count)
                {
                    turnOn = false;
                }
            }

           


            //  db.GetAllQuestDetails()
            // POur chaque planete, vérifier si on a son nom dans la base de données
            // The code provided will print ‘Hello World’ to the console.
            // Press Ctrl+F5 (or go to Debug > Start Without Debugging) to run your app.
            Console.WriteLine("Hello World!");
            Console.ReadKey();

            // Go to http://aka.ms/dotnet-get-started-console to continue learning how to build a console app! 
        }


        public static void MergeBDD()
        {
            DataAccess db = new DataAccess();

            Dictionary<int, IList<Planet>> QuestMaybe = new Dictionary<int, IList<Planet>>();

            var allQuest = db.GetAllQuestDetails();
            var count2 = allQuest.Count();
            allQuest = allQuest.Where(x => !QueteAlreadyProcess.Any(y => y == x.IDQuete));
            count2 = allQuest.Count();
            var allQuestGroupBy = allQuest.GroupBy(x => x.IDQuete);
            QuestError.Clear();

          
            foreach (IEnumerable<QueteDetails> queteDetails in allQuestGroupBy)
            {
                List<Planet> temp = new List<Planet>();
                bool add = false;
                int groupby = 1;
                int count = 1;
                foreach (QueteDetails details in queteDetails)
                {
                    string sanitiseName = string.Empty;
                    var temp2 = details.Nom.Split('.');
                    if (temp2.Count() > 2)
                    {
                        details.Nom = temp2[0].Substring(0, temp2[0].Count() - 2);
                    }

                     var planets = new PlanetAction().SearchByNameQuest(details.Nom.Trim());
                    if (planets.Count() == 0)
                    {
                        // planete non reconnu
                        continue;
                    }
                    else if (planets.Count() > 1)
                    {
                        if (temp.Count == 0)
                        {
                            temp.AddRange(planets.ToList());
                            continue;
                        }
                        else
                        {
                            count++;
                            temp.AddRange(planets.ToList());
                            var groupByPlayerTemp = temp.GroupBy(x => x.PlayerId);
                            var player2 = groupByPlayerTemp.Where(x => x.Count() > groupby);
                            var player3 = player2.Where(x => x.Any(b => b.Name.ToLower() == details.Nom));
                            if (player2.Count() == 0)
                            {
                                continue;
                            }
                            else if (player2.Count() == 1)
                            {
                                planets = player2.First();
                                add = true;
                            }
                            else
                            {

                                if (count == queteDetails.Count())
                                {
                                    planets = player2.First();
                                    add = true;
                                }

                                temp = new List<Planet>();
                                foreach (var item in player2)
                                {
                                    temp.AddRange(item);
                                }
                            }
                        }
                    }
                    else
                    {
                        add = true;
                    }



                    if (add)
                    {
                        var quete = db.GetQuestById(details.IDQuete);
                        Quest quest = new Quest()
                        {
                            Name = quete.Nom,
                            RewardTypeId = quete.IDTypeRecompense,
                            ProfitId = quete.IDNomRecompense,
                            ProfitTypeId = quete.IDTypeGain,
                            ValueProfit = (Int16)quete.Valeur,
                            DurationProfil = (Int16)quete.Duree,
                            HasSoldier = quete.Soldat,
                            HasSpaceship = quete.Flotte,
                            HasDefense = quete.Defense,
                            HasExploration = quete.Exploration,
                            Comment = quete.Commentaire,
                            Visible = quete.Visible,
                            ProfilOldDatabase = quete.RecompenseDetail,
                            PlayerId = planets.First().PlayerId
                        };

                        try
                        {
                            new QuestAction().AddQuest(quest);
                            QueteAlreadyProcess.Add(quete.ID);
                            break;
                        }
                        catch
                        {
                            if (!QuestAddBug.Contains(quete.ID))
                            {
                                QuestAddBug.Add(quete.ID);
                            }
                        }
                    }

                }

                if (!add)
                {
                    if (!QuestError.Contains(queteDetails.First().IDQuete))
                    {
                        QuestError.Add(queteDetails.First().IDQuete);
                    }
                }
            }

        }

        //static void Main(string[] args)
        //{
        //    List<int> QueteAlreadyProcess = new List<int>();
        //    List<int> QuestError = new List<int>();
        //    DataAccess db = new DataAccess();

        //    Dictionary<int, IList<Planet>> QuestMaybe = new Dictionary<int, IList<Planet>>();

        //    var allQuest = db.GetAllQuestDetails();
        //    foreach (QueteDetails quetedetails in allQuest)
        //    {
        //        bool alreadyprocess = QueteAlreadyProcess.Contains(quetedetails.IDQuete);
        //        bool add = false;
        //        if (alreadyprocess)
        //            continue;

        //        var planets = new PlanetAction().SearchByName(quetedetails.Nom);

        //        if (planets.Count() == 0)
        //        {
        //            // planete non reconnu
        //            continue;
        //        }
        //        else if (planets.Count() > 1)
        //        {
        //            IList<Planet> quetesTemp;
        //            QuestMaybe.TryGetValue(quetedetails.IDQuete, out quetesTemp);
        //            if (quetesTemp == null)
        //            {
        //                QuestMaybe.Add(quetedetails.IDQuete, planets);
        //            }
        //            else
        //            {


        //                //var result = lChildren.Where(s => rChildren.Select(p => getKey(p)).Contains(getKey(s))).ToList();
        //                var temp =  quetesTemp.Where(q => planets.All(p => p.PlayerId == q.PlayerId));

        //                if (temp.Count() == 0)
        //                {


        //                    if (!QuestError.Contains(quetedetails.IDQuete))
        //                    {
        //                        QuestError.Add(quetedetails.IDQuete);
        //                    }
        //                }
        //                else if (temp.Count() == 1)
        //                {
        //                    planets = temp;
        //                    add = true;
        //                }
        //                else
        //                {
        //                    QuestMaybe[quetedetails.IDQuete] = temp;                          
        //                }
        //            }

        //        }
        //        else
        //        {
        //            add = true;
        //        }


        //        if (add)
        //        {
        //            var quete = db.GetQuestById(quetedetails.IDQuete);
        //            Quest quest = new Quest()
        //            {
        //                Name = quete.Nom,
        //                RewardTypeId = quete.IDTypeRecompense,
        //                ProfitId = quete.IDNomRecompense,
        //                ProfitTypeId = quete.IDTypeGain,
        //                ValueProfit = (Int16)quete.Valeur,
        //                DurationProfil = (Int16)quete.Duree,
        //                HasSoldier = quete.Soldat,
        //                HasSpaceship = quete.Flotte,
        //                HasDefense = quete.Defense,
        //                HasExploration = quete.Exploration,
        //                Comment = quete.Commentaire,
        //                Visible = quete.Visible,
        //                ProfilOldDatabase = quete.RecompenseDetail,
        //                PlayerId = planets.First().PlayerId
        //            };

        //            try
        //            {
        //                new QuestAction().AddQuest(quest);
        //                QueteAlreadyProcess.Add(quete.ID);
        //            }
        //            catch
        //            {
        //                if (!QuestError.Contains(quete.ID))
        //                {
        //                    QuestError.Add(quete.ID);
        //                }
        //            }
        //        }
        //    }

        //    //  db.GetAllQuestDetails()
        //    // POur chaque planete, vérifier si on a son nom dans la base de données
        //    // The code provided will print ‘Hello World’ to the console.
        //    // Press Ctrl+F5 (or go to Debug > Start Without Debugging) to run your app.
        //    Console.WriteLine("Hello World!");
        //    Console.ReadKey();

        //    // Go to http://aka.ms/dotnet-get-started-console to continue learning how to build a console app! 
        //}



    }
}
