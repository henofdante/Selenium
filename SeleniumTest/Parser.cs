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
    class Parser
    {
        private Pipeline _pipeline;
        public Parser()
        {
            _pipeline = new Pipeline();
        }
        public bool Parse(IWebElement element)
        {
            try
            {
                var elements = element.FindElements(By.ClassName("c"));
                for (var i = 1; i < elements.Count - 2; ++i)
                {
                    var time = elements[i].FindElement(By.XPath(".//span[@class='ct']")).Text;
                    var content = elements[i].FindElement(By.XPath(".")).Text;
                    content = content.Replace(time, "");
                    _pipeline.Push(time, content);
                }
                element.FindElement(By.LinkText("下页"));
            }
            catch (NoSuchElementException)
            {
                return false;
            }
            return true;
        }

        public static void Out(String s)
        {
            Console.WriteLine(s);
        }
    }
}
