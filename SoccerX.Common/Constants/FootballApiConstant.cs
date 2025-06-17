using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoccerX.Common.Constants
{
    public class FootballApiConstant
    {        
        /// <summary>
        /// Countries
        /// </summary>
        public const string FootballApi_Countries = "/countries";

        /// <summary>
        /// Leagues
        /// </summary>        
        public const string FootballApi_Leagues = "/leagues";
        public const string FootballApi_Leagues_Seasons = "/leagues/seasons";

        /// <summary>
        /// Teams
        /// </summary>
        public const string FootballApi_Teams = "/teams";
        public const string FootballApi_Teams_Statistics = "/teams/statistics";
        public const string FootballApi_Teams_Season = "/teams/seasons";
        public const string FootballApi_Teams_Countries = "/teams/countries";

        /// <summary>
        /// Venues
        /// </summary>
        public const string FootballApi_Venues = "/venues";

        /// <summary>
        /// Standings
        /// </summary>
        public const string FootballApi_Standings = "/standings";

        //Fixtures
        public const string FootballApi_Fixtures = "/fixtures";
        public const string FootballApi_Fixtures_Rounds = "/fixtures/rounds";
        public const string FootballApi_Fixtures_HeadtoHead = "/fixtures/headtohead";
        public const string FootballApi_Fixtures_Statistics = "/fixtures/statistics";
        public const string FootballApi_Fixtures_Events = "/fixtures/events";
        public const string FootballApi_Fixtures_Lineups = "/fixtures/lineups";
        public const string FootballApi_Fixtures_Players = "/fixtures/players";

        /// <summary>
        /// Injuries
        /// </summary>
        public const string FootballApi_Injuries = "/injuries";

        /// <summary>
        /// Predictions
        /// </summary>
        public const string FootballApi_Predictions = "/predictions";

        /// <summary>
        /// Coachs
        /// </summary>
        public const string FootballApi_Coachs = "/coachs";

        /// <summary>
        /// Players
        /// </summary>
        public const string FootballApi_Players_Seasons = "/players/seasons";
        public const string FootballApi_Players_Profiles = "/players/profiles";
        public const string FootballApi_Players_Statistics = "/players";
        public const string FootballApi_Players_Squads = "/players/squads";
        public const string FootballApi_Players_Team = "/players/teams";
        public const string FootballApi_Players_Topscorers = "/players/topscorers";
        public const string FootballApi_Players_Topassists = "/players/topassists";
        public const string FootballApi_Players_TopYellowCards = "/players/topyellowcards";
        public const string FootballApi_Players_TopRedCards = "/players/topredcards";

        /// <summary>
        /// Transfers
        /// </summary>
        public const string FootballApi_Transfers = "/transfers";

        /// <summary>
        /// Trophies
        /// </summary>
        public const string FootballApi_Trophies = "/trophies";

        /// <summary>
        /// Sidelined
        /// </summary>
        public const string FootballApi_Sidelined = "/sidelined";

        /// <summary>
        /// Odds
        /// </summary>
        public const string FootballApi_Odds_InPlay = "/odds/live";
        public const string FootballApi_Odds_Live_Bets = "/odds/live/bets";

        /// <summary>
        /// Odds Pre-Match
        /// </summary>
        public const string FootballApi_Odds = "/odds";
        public const string FootballApi_Odds_Mapping = "/odds/mapping";
        public const string FootballApi_Odds_Bookmakers = "/odds/bookmakers";
        public const string FootballApi_Odds_Bets = "/odds/bets";
    }
}
