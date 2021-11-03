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
            bilgiler bilgi = new bilgiler();//PASSWORD VE USERNAME BİLGİSİ BİLGİLER SINIFI İÇERİSİNDE TUTULUYOR
            
           
            IWebDriver driver = new ChromeDriver();
            driver.Navigate().GoToUrl("https://www.instagram.com/");//İNSTAGRAM SİTESİNE YÖNLENDİRİYORUM
            
            Thread.Sleep(2500);//2.5 SANİYE PROGRAMI BEKLETİYORUM
            
            IWebElement username = driver.FindElement(By.Name("username"));//USERNAME TEXT ALANININ HTML ELEMENTİ
            IWebElement password = driver.FindElement(By.Name("password"));//PASSWORD TEXT ALANININ HTML ELEMENTİ 
            IWebElement btn = driver.FindElement(By.CssSelector(".qF0y9.Igw0E.IwRSH.eGOV_._4EzTm"));//LOGİN BUTONUNUN HTML ELEMENTİ
            
            
            username.SendKeys(bilgi.getusername());//USERNAME VE PASSWORD BİLGİLERİNİ GÖNDERİYORUZ
            password.SendKeys(bilgi.getpassword());
            btn.Click();
            
            Thread.Sleep(4500);
            driver.Navigate().GoToUrl($"https://www.instagram.com/{bilgi.getusername()}");//KULLANICI PROFİL SAYFASINA YÖNLENDİRİLİYOR
            
            Thread.Sleep(5500);
            IWebElement follower = driver.FindElement(By.XPath("/ html / body / div[1] / section / main / div / header / section / ul / li[2] / a"));
            follower.Click();

            string jscommand = " "+"sayfa = document.querySelector('.isgrP');" +
                "sayfa.scrollTo(0,sayfa.scrollHeight);" +//SCROLL  EN ALTA İNDİRİLİYOR
                "var sayfaSonu = sayfa.scrollHeight;" +
                "return sayfaSonu;";//JAVASCRİPT KOMUTLARI GÖNDERİLİYOR
            

            Thread.Sleep(4000);//EĞER İNTERNETİNİZDEN KAYNAKLI BİR HATA MEVCUT İSE PROGRAM BEKLETME ZAMANINI ARTTIRABİLİRSİNİZ.
            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
            var sayfaSonu = Convert.ToInt32(js.ExecuteScript(jscommand));//jscommand DEĞİŞKENİ İÇERİSİNDEKİ JAVASCRİPT KODLARI CONSOLA YAZDIRILIYOR

            while (true)//HER 1.25 SANİYEDE SCROLU EN ALTTA İNDİRİP TAKİP EDEN KİŞİLERİN HEPSİNİ GÖSTERENE KADAR WHİLE DÖNGÜSÜ ÇALIŞIYOR
            {
                var son = sayfaSonu;
                Thread.Sleep(1250);
                sayfaSonu = Convert.ToInt32(js.ExecuteScript(jscommand));
                if (son == sayfaSonu)
                {
                    break;//EĞER TÜM TAKİPÇİLER GÖSTERİLİRSE DÖNGÜ KIRILIYOR(MAXİMUM YÜKSEKLİK SAYFA SONUNA EŞİT OLDUĞUNDA)
                }
            }

            Thread.Sleep(2000);
            IReadOnlyCollection<IWebElement> followers = driver.FindElements(By.CssSelector(".FPmhX.notranslate._0imsa "));//KULLANICI İSİMLERİNİ READONLY OLARAK BİR GENERİCTE 
            //TUTUYORUZ.
            int sayac = 1;
            foreach (IWebElement flwr in followers)
            {
                Console.WriteLine(sayac +"-->"+flwr.Text);//HER BİR FOLLOWERS HTML ELEMENTİNİN .TEXT ÖZELLİĞİ İLE KULLANICI ADINI ÇEKİYORUZ VE EKRANA YAZDIRIYORUZ.
                sayac++;
            }
        }
    }
}
