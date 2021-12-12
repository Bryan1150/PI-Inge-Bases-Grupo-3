using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace PruebasUIPlanetario.UITesting
{
    [TestClass]
    public class CuestionarioEvaluacionTest
    {
        IWebDriver driver;
        
        [TestMethod]
        public void TituloVistaCuestionarioEsCorrecto()
        {
            driver = new ChromeDriver();
            string URL = "https://localhost:44368/Evaluacion/CuestionarioEvaluacion";
            
            
            driver.Url = URL;
            IWebElement titulo = driver.FindElement(By.ClassName("titulo"));
            
            Assert.AreEqual("Califica tu experiencia", titulo.Text);
        }

        [TestMethod]
        public void EnviarFormularioVacioDaError()
        {
            driver = new ChromeDriver();
            string URL = "https://localhost:44368/Evaluacion/CuestionarioEvaluacion";
            

            driver.Url = URL;
            IWebElement botonSubmit = driver.FindElement(By.CssSelector("input[type=submit]"));
            botonSubmit.Click();
            IWebElement titulo = driver.FindElement(By.CssSelector(".alert.alert-warning"));

            Assert.AreEqual("El cuestionario tiene errores. Por favor revise sus respuestas.", titulo.Text);
        }


        [TestMethod]
        public void EviarFormularioIncorrectoDaError()
        {
            driver = new ChromeDriver();
            string URL = "https://localhost:44368/Evaluacion/CuestionarioEvaluacion";
            

            driver.Url = URL;
            IWebElement botonSubmit1 = driver.FindElement(By.CssSelector("input[type=radio]"));
            botonSubmit1.Click();

            IWebElement botonSubmit = driver.FindElement(By.CssSelector("input[type=submit]"));
            botonSubmit.Click();
            IWebElement titulo = driver.FindElement(By.CssSelector(".alert.alert-warning"));

            Assert.AreEqual("El cuestionario tiene errores. Por favor revise sus respuestas.", titulo.Text);
        }

        [TestCleanup]
        public void TearDown()
        {
            if (driver != null)
            {
                driver.Quit();
            }
        }
    }
}