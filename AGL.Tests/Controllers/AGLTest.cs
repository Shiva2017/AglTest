using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AGL.Controllers;
using System.Threading.Tasks;
using AGL.Orchestration;
using AGL.Models;
using Newtonsoft.Json;
using AGL.Orchestration.Interfaces;
using Microsoft.Practices.Unity;
using Moq;

namespace AGL.Tests.Controllers
{
    [TestClass]
    public class AGLTest
    {
        const string type = "cat";
        const string maleGender = "male";
        const string femaleGender = "female";
        ProcessAglData agl;
        AGLData aglData;

        #region JsonData
        const string jsonData = @"[{'name':'Bob','gender':'Male','age':23,'pets':[{'name':'Garfield','type':'Cat'},{'name':'Fido','type':'Dog'}]},{'name':'Jennifer','gender':'Female','age':18,'pets':[{'name':'Garfield','type':'Cat'}]},{'name':'Steve','gender':'Male','age':45,'pets':null},{'name':'Fred','gender':'Male','age':40,'pets':[{'name':'Tom','type':'Cat'},{'name':'Max','type':'Cat'},{'name':'Sam','type':'Dog'},{'name':'Jim','type':'Cat'}]},{'name':'Samantha','gender':'Female','age':40,'pets':[{'name':'Tabby','type':'Cat'}]},{'name':'Alice','gender':'Female','age':64,'pets':[{'name':'Simba','type':'Cat'},{'name':'Nemo','type':'Fish'}]}]";
        #endregion

        public AGLTest()
        {
            var repMock = new Mock<IProcessAglData>();
            var container = new UnityContainer();
            container.RegisterInstance<IProcessAglData>(repMock.Object);
            agl = container.Resolve<ProcessAglData>();
            aglData = new AGLData();
        }
        public AGLTest(IUnityContainer container)
        {
            var mock = new Mock<IProcessAglData>();
           // this.unityContainer = container;
        }
        [TestMethod]
        public void AGLAction()
        {
            HomeController controller = new HomeController(agl);

            Task<ActionResult> result = controller.AGL() as Task<ActionResult>;

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void ProcessData_sucess()
        { 
            var pets = GetData();  
            Assert.IsNotNull(pets);
            Assert.AreEqual(pets.Persons.Count, 5);
            foreach (var person in pets.Persons)
            {
                var pet = pets.Persons.FirstOrDefault(p => p.Pets.Any(a => a.Type.ToLower() == type)).Pets.FirstOrDefault(pt => pt.Type.ToLower() == type).Name;
                Assert.IsNotNull(pet);
            }           
        }

        [TestMethod]
        public void ProcessData_Male_Pets_sucess()
        {
            var pets = GetData();
            Assert.IsNotNull(pets);
            Assert.AreEqual(pets.Persons.Count, 5);
            var malePets = pets.Persons.Where(p => p.Gender.ToLower() == maleGender).ToList();
            Assert.AreEqual(malePets.Count,2);
            foreach (var person in malePets)
            {
                var pet = pets.Persons.FirstOrDefault(p => p.Pets.Any(a => a.Type.ToLower() == type)).Pets.FirstOrDefault(pt => pt.Type.ToLower() == type).Name;
                Assert.IsNotNull(pet);
            }
        }

        [TestMethod]
        public void ProcessData_FeMale_Pets_sucess()
        {
            var pets = GetData();
            Assert.IsNotNull(pets);
            Assert.AreEqual(pets.Persons.Count, 5);
            var malePets = pets.Persons.Where(p => p.Gender.ToLower() == femaleGender).ToList();
            Assert.AreEqual(malePets.Count, 3);
            foreach (var person in malePets)
            {
                var pet = pets.Persons.FirstOrDefault(p => p.Pets.Any(a => a.Type.ToLower() == type)).Pets.FirstOrDefault(pt => pt.Type.ToLower() == type).Name;
                Assert.IsNotNull(pet);
            }
        }
        private AGLData GetData()
        {
            var person = JsonConvert.DeserializeObject<List<Person>>(jsonData);
            aglData.Persons = agl.ProcessData(person, type);
            return aglData;
        }
    }
}
