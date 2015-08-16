using System;
using System.Linq;
using System.Collections.Generic; 
using Arabia.Core.Data;
using Arabia.Data;
using Arabia.Core.Lookups;

namespace Arabia.Service.Lookups
{
    public partial class CompetitionService : ICompetitionService
    {
        #region Fields
        
        private readonly IRepository<Competition> _competitionRepository;
        
        #endregion
        
        #region Ctor

        public CompetitionService(IRepository<Competition> competitionRepository)
        {
            this._competitionRepository = competitionRepository;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Gets a competition
        /// </summary>
        /// <param name="competitionId">The competition identifier</param>
        /// <returns>Competition</returns>
        public Competition GetCompetitionById(int competitionId)
        {
            if (competitionId == 0)
                return null;
            return _competitionRepository.GetById(competitionId);
        }

        /// <summary>
        /// Gets competition collection
        /// </summary>
        /// <returns>Competition collection</returns>
        public List<Competition> GetCompetitions()
        {
            return _competitionRepository.Table.ToList();
        }
        
        /// <summary>
        /// Inserts a competition
        /// </summary>
        /// <param name="competition">Competition</param>
        public bool InsertCompetition(Competition competition)
        {
            if (competition == null)
                throw new ArgumentNullException("Invalid Competition");
           return _competitionRepository.Insert(competition);
        }

        /// <summary>
        /// Updates the competition
        /// </summary>
        /// <param name="competition">Competition</param>
        public bool UpdateCompetition(Competition competition)
        {
            if (competition == null)
                throw new ArgumentNullException("Invalid Competition");
           return _competitionRepository.Update(competition);
        }

        /// <summary>
        /// Deletes a competition
        /// </summary>
        /// <param name="competition">The competition identifier</param>
        public bool DeleteCompetition(int competitionId)
        {
            var competition = GetCompetitionById(competitionId);
            if (competition == null)
                throw new ArgumentNullException("Invalid Competition");
            return _competitionRepository.Delete(competition);
        }

        #endregion
    }
}
