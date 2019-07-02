using System;
using System.Linq;

namespace XboxStatistics
{
    class Program
    {
        private static readonly MyXboxOneGames Xbox = new MyXboxOneGames();
        MyXboxOneGames myXboxOneGames = new MyXboxOneGames();

        static void Main(string[] args)
        {

            Question("How many games do I have?", HowManyGamesDoIHave);
            Question("How many games have I completed?", HowManyGamesHaveICompleted);
            Question("How much Gamerscore do I have?", HowMuchGamescoreDoIHave);
            Question("How many days did I play?", HowManyDaysDidIPlay);
            Question("Which game have I spent the most hours playing?", WhichGameHaveISpentTheMostHoursPlaying);
            Question("In which game did I unlock my latest achievement?", InWhichGameDidIUnlockMyLatestAchievement);
            Question("List all of my statistics in Binding of Isaac:", ListAllOfMyStatisticsInBindingOfIsaac);
            Question("How many achievements did I earn per year?", HowManyAchievementsDidIEarnPerYear);
            Question("List all of my games where I have earned a rare achievement", ListAllOfMyGamesWhereIHaveEarnedARareAchievement);
            Question("List the top 3 games where I have earned the most rare achievements", ListTheTop3GamesWhereIHaveEarnedTheMostRareAchievements);
            Question("Which is my rarest achievement?", WhichIsMyRarestAchievement);

            Console.ReadLine();
        }

        static void Question(string question, Func<string> answer)
        {
            Console.WriteLine($"Q: {question}");
            Console.WriteLine($"A: {answer()}");
            Console.WriteLine();
        }


        static string HowManyGamesDoIHave()
        {
            string quantityOfGames;
            quantityOfGames = Xbox.MyGames.Count().ToString();
            return quantityOfGames;
        }

        static string HowManyGamesHaveICompleted()
        {
            //HINT: you need to count the games where I reached the maximum Gamerscore
            var completedGames = from game in Xbox.MyGames
                                 where game.CurrentGamerscore == game.MaxGamerscore
                                 select game;
            string quantityOfCompletedGames = completedGames.Count().ToString();
            return quantityOfCompletedGames;
        }

        static string HowManyDaysDidIPlay()
        {
            //HINT: there's a game stat property called MinutesPlayed, and as the name suggests it stored total minutes
            var statCollection = Xbox.GameStats.SelectMany(x => x.Value)
                .Where(x => x.Name == "MinutesPlayed" && x.Value != null)
                .Sum(x => int.Parse(x.Value));

            return (statCollection/1440).ToString();
        }

        static string WhichGameHaveISpentTheMostHoursPlaying()
        {
            //HINT: there's a game stat property called MinutesPlayed, and as the name suggests it stored total minutes
            var statCollection = Xbox.GameStats.SelectMany(x => x.Value)
                .Where(x => x.Name == "MinutesPlayed" && x.Value != null);

            var statCollectionMax = Xbox.GameStats.SelectMany(x => x.Value)
                 .Where(x => x.Name == "MinutesPlayed" && x.Value != null)
                 .Max(x => int.Parse(x.Value));

            var gameId = statCollection
                         .Where(x => x.Name == "MinutesPlayed" && x.Value == statCollectionMax.ToString())
                         .Select(x => x.Titleid);

            var name = Xbox.MyGames
                     .Where(x => x.TitleId == gameId.First())
                     .Select(x => x.Name);

            return name.First();
        }

        static string HowMuchGamescoreDoIHave()
        {
            var gameScoreTotal = Xbox.MyGames
                .Sum(x => x.CurrentGamerscore);

            return gameScoreTotal.ToString();
        }

        static string InWhichGameDidIUnlockMyLatestAchievement()
        {
            var lastUnlockedAchievement= Xbox.Achievements.SelectMany(x => x.Value)
                .Max(x => x.Progression.TimeUnlocked);
            var nameOfLastGameUnblAch = Xbox.MyGames
                .Where(x => x.LastUnlock == lastUnlockedAchievement)
                .Select(x => x.Name);
            var result = nameOfLastGameUnblAch.First().ToString() + " on " + lastUnlockedAchievement.ToString();

            return result;
        }

        static string ListAllOfMyStatisticsInBindingOfIsaac()
        {
            var gameScId = Xbox.MyGames.Select(x => x)
                            .Where(x => x.Name.EndsWith("Rebirth"))
                            .Select(x => x.ServiceConfigId);                                                                                                                                                                                                            

            var stats = Xbox.GameStats.SelectMany(x => x.Value)
                .Where(x => x.Scid == gameScId.First())
                .Select(x => x);
            
            string result = string.Join(string.Empty, stats.Select(s =>$"{s.Name} = {s.Value}\n"));

            return result;
        }

        static string HowManyAchievementsDidIEarnPerYear()
        {

            //HINT: unlocked achievements have an "Achieved" progress state
            var quantityOfAchievementsByYear = Xbox.Achievements.SelectMany(x => x.Value)
                .Where(x => x.ProgressState == "Achieved")
                .GroupBy(x => x.Progression.TimeUnlocked.Year)
                .Select(x => x);

            string result = string.Join(string.Empty, quantityOfAchievementsByYear
                                                        .OrderBy(q => q.Key)
                                                        .Select(q => $"{q.Key} = {q.Count()}\n"));
            return result;
        }

        static string ListAllOfMyGamesWhereIHaveEarnedARareAchievement()
        {
            //HINT: rare achievements have a rarity category called "Rare"
            var rareAchievementsIds = Xbox.Achievements.SelectMany(x => x.Value)
                                                             .Where(x => x.Rarity.CurrentCategory == "Rare" && x.ProgressState == "Achieved")
                                                             .GroupBy(x => x.ServiceConfigId)
                                                             .Select(x => x);
            var games = Xbox.MyGames;
            var query = from achievement in rareAchievementsIds
                        join game in games
                        on achievement.Key equals game.ServiceConfigId
                        select game.Name;
            var count = query.Count();
            string result = string.Join("\n", query);
            return result;

        }

        static string ListTheTop3GamesWhereIHaveEarnedTheMostRareAchievements()
        {
            var rareAchievementsIds = Xbox.Achievements.SelectMany(x => x.Value)
                                                 .Where(x => x.Rarity.CurrentCategory == "Rare" && x.ProgressState == "Achieved")
                                                 .OrderBy(x => x.Rarity.CurrentPercentage)
                                                 .GroupBy(x => x.ServiceConfigId)
                                                 .OrderByDescending(x => x.Count())
                                                 .Select(x => x);

            var firstThreeGames = rareAchievementsIds.Take(3);

            var games = Xbox.MyGames;
            var query = from game in games
                        join achievement in firstThreeGames
                        on  game.ServiceConfigId equals achievement.Key
                        select game.Name;
            //This query returns the counts of rare achievements in selected games
            var firstThreeGamesCounts = firstThreeGames
                                        .Select(x => x.Count());

            // The output is [query.Name] but it should be like [query.Name] [firstThreeGamesCounts[i]]
            string result = string.Join("\n", query);

            return result;

        }

        static string WhichIsMyRarestAchievement()
        {
            var theRarestAchievementsIds = Xbox.Achievements.SelectMany(x => x.Value)
                                                .Where(x => x.Rarity.CurrentCategory == "Rare" && x.ProgressState == "Achieved")
                                                .OrderBy(x => x.Rarity.CurrentPercentage)
                                                .GroupBy(x => x.ServiceConfigId)
                                                .Select(x => x)
                                                .First();
    
            string result = $"You are among the " + string.Join(string.Empty, theRarestAchievementsIds.Select(x => x.Rarity.CurrentPercentage).First() 
                + $"% of gamers who earned \"{theRarestAchievementsIds.Select(x => x.Name).First()}\" achievement in " +
                $"{Xbox.MyGames.Where(x => x.ServiceConfigId == theRarestAchievementsIds.First().ServiceConfigId).Select(x => x.Name).First()}");

            return result;
        }
    }
}