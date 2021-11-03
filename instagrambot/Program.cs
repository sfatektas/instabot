using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Threading;
using System.Collections.Generic;

namespace instagrambot
{
    class Program
    {
        static void Main(string[] args)
        {  
            bilgiler bilgi = new bilgiler();
           
            IWebDriver driver = new ChromeDriver();
            driver.Navigate().GoToUrl("https://www.instagram.com/");
            
            Thread.Sleep(2500);
            
            IWebElement username = driver.FindElement(By.Name("username"));
            IWebElement password = driver.FindElement(By.Name("password"));
            IWebElement btn = driver.FindElement(By.CssSelector(".qF0y9.Igw0E.IwRSH.eGOV_._4EzTm"));
            
            
            username.SendKeys(bilgi.getusername());
            password.SendKeys(bilgi.getpassword());
            btn.Click();
            
            Thread.Sleep(4500);
            driver.Navigate().GoToUrl($"https://www.instagram.com/{bilgi.getusername()}");
            
            Thread.Sleep(5500);
            IWebElement follower = driver.FindElement(By.XPath("/ html / body / div[1] / section / main / div / header / section / ul / li[2] / a"));
            follower.Click();

            string jscommand = " "+"sayfa = document.querySelector('.isgrP');" +
                "sayfa.scrollTo(0,sayfa.scrollHeight);" +
                "var sayfaSonu = sayfa.scrollHeight;" +
                "return sayfaSonu;";

            Thread.Sleep(4000);
            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
            var sayfaSonu = Convert.ToInt32(js.ExecuteScript(jscommand));

            while (true)
            {
                var son = sayfaSonu;
                Thread.Sleep(1250);
                sayfaSonu = Convert.ToInt32(js.ExecuteScript(jscommand));
                if (son == sayfaSonu)
                {
                    break;
                }
            }

            Thread.Sleep(2000);
            IReadOnlyCollection<IWebElement> followers = driver.FindElements(By.CssSelector(".FPmhX.notranslate._0imsa "));
            int sayac = 1;
            foreach (IWebElement flwr in followers)
            {
                Console.WriteLine(sayac +"-->"+flwr.Text);
                sayac++;
            }


            // IWebElement username = driver.FindElement(By.Name());

            //Thread.Sleep(22000);
            //IWebElement takipciler;
            //takipciler = driver.FindElement(By.CssSelector("#react-root > section > main > div > header > section > ul > li:nth-child(2) > a"));
            //takipciler.Click();

        }
    }
}
