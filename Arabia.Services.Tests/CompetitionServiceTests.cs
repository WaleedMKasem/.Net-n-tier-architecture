using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Arabia.Core.Lookups;
using Arabia.Service.Lookups;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Arabia.Services.Tests
{
    [TestClass]
    public class CompetitionServiceTests
    {
        private Mock<ICompetitionService> _mockCompetitionService;
        private IList<Competition> _competitions;
        private Competition _competition;

       [TestInitialize]
        public void Init()
        {
               // setup
           _competitions = new List<Competition>()
            {
                new Competition(){Competition_Id = 1,Competition_Name = "WorldCup"},
                new Competition(){Competition_Id = 2,Competition_Name = "AsiaCup"},
                new Competition(){Competition_Id = 3,Competition_Name = "AfricaCup"},
            };

           _competition = new Competition() {Competition_Id = 1, Competition_Name = "WorldCup"};

            _mockCompetitionService = new Mock<ICompetitionService>();

            _mockCompetitionService.Setup(obj => obj.GetCompetitions()).Returns(_competitions.ToList());
            _mockCompetitionService.Setup(obj => obj.GetCompetitionById(1)).Returns(_competition);

        }
       [TestMethod]
       public void GetCompetitionById_Get_Details_Successful_And_Return_Object()
       {
           // act
           var result = _mockCompetitionService.Object.GetCompetitionById(1);

           //assert
           Assert.IsInstanceOfType(result, typeof(Competition));
           Assert.AreEqual(_competition, result);
       }
       [TestMethod]
       public void GetCompetitions_Get_All_Successful_And_Return_List()
       {
           // act
           var result = _mockCompetitionService.Object.GetCompetitions();

           //assert
           Assert.IsInstanceOfType(result, typeof(IList<Competition>));
           Assert.AreEqual(_competitions.Count,3);
       }
    }
}
