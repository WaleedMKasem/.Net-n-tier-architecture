using System.Collections.Generic; 
using Arabia.Core.Lookups;

namespace Arabia.Service.Lookups
{
    public interface ICompetitionService
    {
        Competition GetCompetitionById(int competitionId);
        List<Competition> GetCompetitions();
        bool InsertCompetition(Competition competition);
        bool UpdateCompetition(Competition competition);
        bool DeleteCompetition(int competitionId);
    }
}
