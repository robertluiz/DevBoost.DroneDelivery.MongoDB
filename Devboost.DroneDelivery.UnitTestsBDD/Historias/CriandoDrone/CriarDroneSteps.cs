using System;
using TechTalk.SpecFlow;
using Xunit;

namespace Devboost.DroneDelivery.UnitTestsBDD.Historias.CriandoDrone
{
    [Binding]
    public class CriarDroneSteps
    {
        private readonly ScenarioContext _scenarioContext;

        public CriarDroneSteps(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
        }

        [Given(@"the first number is (.*)")]
        public void DadoTheFirstNumberIs(int p0)
        {
            _scenarioContext.Add("FirstNumber", p0);
        }
        
        [Given(@"the second number is (.*)")]
        public void DadoTheSecondNumberIs(int p0)
        {
            _scenarioContext.Add("SecondNumber", p0);
        }
        
        [When(@"the two numbers are added")]
        public void QuandoTheTwoNumbersAreAdded()
        {
            var v1 = _scenarioContext.Get<int>("FirstNumber");
            var v2 = _scenarioContext.Get<int>("SecondNumber");

            var v3 = v1 + v2;

            _scenarioContext.Add("ThirtyNumber", v3);
        }
        
        [Then(@"the result should be (.*)")]
        public void EntaoTheResultShouldBe(int p0)
        {
            var result = _scenarioContext.Get<int>("ThirtyNumber");

            Assert.Equal(result, p0);
        }
    }
}
