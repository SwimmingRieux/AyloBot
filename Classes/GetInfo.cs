using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using OpenQA.Selenium;
using Newtonsoft.Json.Linq;
using HtmlAgilityPack;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Chrome;

namespace AyloBot.Classes
{
    class GetInfo
    {
        
        public static void MainUpdateFiltereds(IWebDriver driver, IWebDriver driver2, AyloBotEntities db)
        {
            foreach (var e in db.Filtereds.ToList())
            {
                db.Filtereds.Remove(e);
            }
            db.SaveChanges();
            
            driver2.Manage().Window.Maximize();
            driver2.Navigate().GoToUrl("https://asanbourse.ir/Home");
            WebDriverWait wait2 = new WebDriverWait(driver2,System.TimeSpan.FromSeconds(4));
            wait2.Until(dr => dr.FindElement(By.XPath("//button[@data-target='#login']")));
            try
            {
                driver2.FindElement(By.XPath("//button[@data-target='#login']")).Click();
            }
            catch (NoSuchElementException)
            {
                MainUpdateFiltereds(driver, driver2, db);
            }
            catch (ElementNotVisibleException)
            {
                MainUpdateFiltereds(driver, driver2, db);
            }
            catch (WebDriverException)
            {
                MainUpdateFiltereds(driver, driver2, db);
            }
            Thread.Sleep(500);
            driver2.FindElement(By.Id("mobile")).SendKeys("___________");
            driver2.FindElement(By.Id("pass")).SendKeys("____________");
            driver2.FindElement(By.XPath("//button[@value='Login' and @type='submit']")).Click();
            Thread.Sleep(4000);
            driver2.FindElement(By.Id("phoneMenuBtn")).Click();
            Thread.Sleep(100);
            driver2.FindElement(By.XPath("//a[@href='/filter']")).Click();
            Thread.Sleep(4000);
            driver2.FindElement(By.Name("marked")).Click();
            
            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
            for (int i = 0; i < db.FilterTypes.Where(ft => ft.FTType == 1).ToList().Count; i++)
            {
                js.ExecuteScript("mw.QueryWindow()");
                Thread.Sleep(250);
                js.ExecuteScript("mw.SelectFilter(" + i + ");mw.ShowSettings();mw.SaveParams();mw.QueryWindowFilterNames()");
                Thread.Sleep(250);
                js.ExecuteScript("mw.QueryWindowSaveFilter()");
                Thread.Sleep(250);
                js.ExecuteScript("$('#ModalWindowOuter22').remove()");
                var main = driver.FindElement(By.Id("main")).Text.Replace("\r\n", "#") + "#";
                while (main != "")
                {
                    int inOf = main.IndexOf("#");
                    string toSav = main.Substring(0, inOf);
                    if (toSav != "")
                    {
                        var syI = db.Symbols.Where(s => s.SName == toSav).ToList();
                        if (syI.Count() != 0)
                        {
                            int sId = syI.SingleOrDefault().SID;
                            int ftId = db.FilterTypes.ToList()[i].FTID;
                            db.Filtereds.Add(new Filtered()
                            {
                                SID = sId,
                                FTID = ftId
                            });
                        }

                    }
                    main = main.Remove(0, toSav.Length + 1);
                }
            }
            db.SaveChanges();
            List<FilterType> filTy2 = db.FilterTypes.Where(ft => ft.FTType == 2).ToList();
            for (int i = 0; i < filTy2.Count; i++)
            {
                driver2.FindElement(By.Name(filTy2[i].FTCode)).Click();
                driver2.FindElement(By.ClassName("infoContentRunBtn")).Click();
                Thread.Sleep(1500);
                var sDiv = driver2.FindElement(By.Id("buttonsDiv"));
                var divs = sDiv.FindElements(By.TagName("button")).ToList();
                divs.Remove(sDiv.FindElement(By.ClassName("first_page")));
                divs.Remove(sDiv.FindElement(By.ClassName("last_page")));
                for (int m = 0; m < divs.Count(); m++)
                {
                    try
                    {
                        divs[m].Click();
                    }
                    catch (Exception)
                    {

                    }
                    Thread.Sleep(1500);
                    var trs = driver2.FindElement(By.ClassName("abGrid-table")).FindElement(By.TagName("tbody")).FindElements(By.TagName("tr")).ToList();
                    foreach (var itm in trs)
                    {
                        var syN = itm.FindElements(By.TagName("td"))[1].Text;
                        var selS = db.Symbols.Where(s => s.SRahName == syN).ToList();
                        if (selS.Count() != 0)
                        {
                            db.Filtereds.Add(new Filtered()
                            {
                                FTID = filTy2[i].FTID,
                                SID = selS[0].SID
                            });
                        }
                    }
                }
                db.SaveChanges();
            }
            driver2.FindElement(By.XPath("//a[@href='/dashboard?open=diagram']")).Click();

            List<FilterType> filTy3 = db.FilterTypes.Where(ft => ft.FTType == 3).ToList();
            IJavaScriptExecutor js3 = (IJavaScriptExecutor)driver2;
            for (int i = 0; i < db.FilterTypes.Where(ft => ft.FTType == 3).ToList().Count; i++)
            {
                driver2.FindElement(By.XPath("//button[@data-target='#openFileModal']")).Click();
                Thread.Sleep(300);
                driver2.FindElement(By.Id(filTy3[i].FTCode)).Click();
                Thread.Sleep(500);
                driver2.FindElement(By.XPath("//button[@data-bind='event: { click: container.windows.fileManager.openSelectedFile}']")).Click();
                Thread.Sleep(1500);
                driver2.FindElement(By.Id("runIcon")).Click();
                Thread.Sleep(5000);
                var sDiv = driver2.FindElement(By.Id("buttonsDiv"));
                var divs = sDiv.FindElements(By.TagName("button")).ToList();
                divs.Remove(sDiv.FindElement(By.ClassName("first_page")));
                divs.Remove(sDiv.FindElement(By.ClassName("last_page")));
                for (int m = 0; m < divs.Count(); m++)
                {
                    divs[m].Click();
                    var trs = driver2.FindElement(By.ClassName("abGrid-table")).FindElement(By.TagName("tbody")).FindElements(By.TagName("tr")).ToList();
                    foreach (var itm in trs)
                    {
                        var syN = itm.FindElements(By.TagName("td"))[1].Text;
                        var selS = db.Symbols.Where(s => s.SRahName == syN).ToList();
                        if (selS.Count() != 0)
                        {
                            db.Filtereds.Add(new Filtered()
                            {
                                FTID = filTy3[i].FTID,
                                SID = selS[0].SID
                            });
                        }
                    }
                }
                driver2.FindElement(By.ClassName("backToDesign")).Click();
                Thread.Sleep(100);
                driver2.FindElement(By.ClassName("lm_close_tab")).Click();
                db.SaveChanges();
            }
            foreach (var s in db.Symbols.ToList())
            {
                s.SUpdateTime = null;
            }
        }
        
        public static string GetEnglishNumber(string persianNumber)
        {
            string englishNumber = "";
            foreach (char ch in persianNumber)
            {
                englishNumber += char.GetNumericValue(ch);
            }
            return englishNumber;
        }
    }
}
