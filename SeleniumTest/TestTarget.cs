using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace SeleniumTest
{
    class TestTarget
    {
        private IWebDriver _driver;
        private const string User = "fbb0916";

        private const string Username = "17649818647";
        private const string Password = "qwerty123456.";
        private const string Url = "https://weibo.cn/";
        private const string LoginUrl = "https://passport.weibo.cn/signin/Login";

        private void Init()
        {
            _driver = new ChromeDriver();
        }

        private void Login()
        {
            _driver.Navigate().GoToUrl(LoginUrl);
            var userName = _driver.FindElement(By.Id("loginName"));
            userName.SendKeys(Username);
            var password = _driver.FindElement(By.Id("loginPassword"));
            password.SendKeys(Password);
            _driver.FindElement(By.Id("loginAction")).Click();
            Thread.Sleep(7500);
        }

        public void RunWeibo()
        {
            Init();
            Login();
            int page = 1;
            int weiboCount = 0;
            String rawUrl = Url + User + "?page=";

            _driver.Navigate().GoToUrl(rawUrl + page);

            int maxPage = 1;
            var rawMaxPage = _driver.FindElement(By.XPath("//*[@id=\"pagelist\"]/form/div/input[1]"));
            maxPage = int.Parse(rawMaxPage.GetAttribute("value"));
            put("===================maxPage:" + maxPage);
            Random rand = new Random(); //
            while (true)
            {
                try
                {
                    var elements = _driver.FindElements(By.ClassName("c"));
                    for(int i = 0; i < elements.Count -2; ++i)
                    {
                        weiboCount++;
                        put(elements[i].Text);
                    }
                    _driver.FindElement(By.LinkText("下页"));
                    _driver.Navigate().GoToUrl(rawUrl + ++page);
                }
                catch (NoSuchElementException e)
                {
                    Console.WriteLine("no such element");
                    _driver.Navigate().Refresh();
                    Thread.Sleep(3000);
                }
                if (maxPage == page)
                {
                    break;
                }
                Thread.Sleep(1200 + rand.Next(0, 400));
            }
            put("Complete, total Weibo count:" + weiboCount);
        }

        public void RunJLU()
        {
            Init();
            string url = "https://oa.jlu.edu.cn/defaultroot/PortalInformation!jldxList.action?1=1&startPage=";
            string xpath = "//*[@id=\"rightIframe\"]/div/div[3]/div[2]/table/tbody/tr/td/table/tbody/tr/td[2]";
            int page = 1;
            int maxPage = 1;
            while (true)
            {
                _driver.Navigate().GoToUrl(url + page++);
                var elements = _driver.FindElements(By.XPath("//*[@id=\"itemContainer\"]/div"));
                foreach (IWebElement element in elements)
                {
                    put(element.Text);
                }
            }

        }

        public static void put(string s)
        {
            Console.WriteLine(s);
        }

//        public static void Main()
//        {
//           new TestTarget().RunWeibo();
//        }
    }
}