using HtmlAgilityPack;
using Newtonsoft.Json.Linq;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace AyloBot
{
    public partial class Form1 : Form
    {
        private static string Token = "token here";
        private Thread botThread;
        private Telegram.Bot.TelegramBotClient bot;
        private ReplyKeyboardMarkup mainKM;
        Thread UpThread;
        AyloBotEntities db = new AyloBotEntities();
        HtmlWeb web;
        bool firstCh;
        bool secondCh;
        IWebDriver driver;
        IWebDriver driver2;
        TimeSpan en1 = new TimeSpan(8, 29, 59);
        TimeSpan st2 = new TimeSpan(12, 29, 0);
        TimeSpan en2 = new TimeSpan(12, 45, 0);

        public Form1()
        {
            InitializeComponent();
        }
        private void btnStart_Click(object sender, EventArgs e)
        {
            driver = new ChromeDriver("",new ChromeOptions(),new TimeSpan(0,2,30));
            web = new HtmlWeb();
            driver2 = new ChromeDriver("", new ChromeOptions(), new TimeSpan(0, 2, 30));
            Token = txtToken.Text;
            botThread = new Thread(new ThreadStart(runBot));
            botThread.Start();
            UpThread = new Thread(new ThreadStart(mainUpdates));
            #region fir
            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
            driver.Manage().Window.Maximize();
            driver.Navigate().GoToUrl("http://www.tsetmc.com/Loader.aspx?ParTree=15131F");
            Thread.Sleep(10000);
            while (driver.PageSource.Contains("Server is too busy"))
            {
                driver.Navigate().Refresh();
                Thread.Sleep(10000);
            }
            js.ExecuteScript("mw.ShowAllSettings()");
            Thread.Sleep(200);
            js.ExecuteScript("mw.Settings.GroupBySector=0;mw.SaveParams();mw.ShowSettings();HideAllWindow();mw.RemoveAllData();mw.RenderData();mw.SortData();");
            Thread.Sleep(200);
            js.ExecuteScript("mw.ShowAllSettings()");
            Thread.Sleep(200);
            js.ExecuteScript("mw.Settings.ShowPayeFarabourse=1-mw.Settings.ShowPayeFarabourse;mw.SaveParams();mw.ShowSettings();HideAllWindow();mw.ShowAllSettings();mw.RemoveAllData();mw.RenderData();mw.SortData();");
            Thread.Sleep(150);
            js.ExecuteScript("mw.Settings.ShowHousingFacilities=1-mw.Settings.ShowHousingFacilities;mw.SaveParams();mw.ShowSettings();HideAllWindow();mw.ShowAllSettings();mw.RemoveAllData();mw.RenderData();mw.SortData();");
            Thread.Sleep(150);
            js.ExecuteScript("mw.Settings.ShowHaghTaghaddom=1-mw.Settings.ShowHaghTaghaddom;mw.SaveParams();mw.ShowSettings();HideAllWindow();mw.ShowAllSettings();mw.RemoveAllData();mw.RenderData();mw.SortData();");
            Thread.Sleep(150);
            js.ExecuteScript("mw.Settings.ShowOraghMosharekat=1-mw.Settings.ShowOraghMosharekat;mw.SaveParams();mw.ShowSettings();HideAllWindow();mw.ShowAllSettings();mw.RemoveAllData();mw.RenderData();mw.SortData();");
            Thread.Sleep(150);
            js.ExecuteScript("mw.Settings.ShowEkhtiarForoush=1-mw.Settings.ShowEkhtiarForoush;mw.SaveParams();mw.ShowSettings();HideAllWindow();mw.ShowAllSettings();mw.RemoveAllData();mw.RenderData();mw.SortData();");
            Thread.Sleep(150);
            js.ExecuteScript("mw.Settings.ShowAti=1-mw.Settings.ShowAti;mw.SaveParams();mw.ShowSettings();HideAllWindow();mw.ShowAllSettings();mw.RemoveAllData();mw.RenderData();mw.SortData();");
            Thread.Sleep(150);
            js.ExecuteScript("mw.Settings.ShowKala=1-mw.Settings.ShowKala;mw.SaveParams();mw.ShowSettings();HideAllWindow();mw.ShowAllSettings();mw.RemoveAllData();mw.RenderData();mw.SortData();");
            Thread.Sleep(150);
            js.ExecuteScript("mw.Settings.ShowSandoogh=1-mw.Settings.ShowSandoogh;mw.SaveParams();mw.ShowSettings();HideAllWindow();mw.ShowAllSettings();mw.RemoveAllData();mw.RenderData();mw.SortData();");
            Thread.Sleep(150);
            js.ExecuteScript("mw.Settings.LoadClientType=1-mw.Settings.LoadClientType;mw.SaveParams();HideAllWindow();mw.LoadClientType();");
            Thread.Sleep(150);
            js.ExecuteScript("mw.ShowAllSettings()");
            Thread.Sleep(200);
            js.ExecuteScript("mw.Settings.LoadInstStat=1-mw.Settings.LoadInstStat;mw.SaveParams();HideAllWindow();mw.LoadInstStat();");
            Thread.Sleep(200);
            js.ExecuteScript("mw.ShowAllSettings()");
            Thread.Sleep(200);
            js.ExecuteScript("mw.Settings.LoadInstHistory=1-mw.Settings.LoadInstHistory;mw.SaveParams();HideAllWindow();mw.LoadInstHistory();");
            Thread.Sleep(200);
            js.ExecuteScript("mw.ShowAllSettings()");
            Thread.Sleep(1000);
            js.ExecuteScript("$('#ModalWindowOuter12').remove()");
            Thread.Sleep(200);
            js.ExecuteScript("mw.ShowTemplateWindow()");
            Thread.Sleep(200);
            js.ExecuteScript("mw.BuildTemplate()");
            Thread.Sleep(500);
            var toClear = driver.FindElement(By.Id("TemplateColNo"));
            toClear.SendKeys(OpenQA.Selenium.Keys.Control + "a");
            toClear.SendKeys(OpenQA.Selenium.Keys.Delete);
            toClear.SendKeys("1");
            Thread.Sleep(500);
            driver.FindElement(By.CssSelector("[href*='javascript:mw.SaveTemplate()']")).Click();
            Thread.Sleep(1000);
            js.ExecuteScript("$('#ModalWindowOuter15').remove()");
            Thread.Sleep(200);
            js.ExecuteScript("mw.ShowTemplateWindow()");
            Thread.Sleep(500);
            js.ExecuteScript("mw.changeTemplate(11)");
            Thread.Sleep(1000);
            js.ExecuteScript("$('#ModalWindowOuter16').remove()");
            Thread.Sleep(200);
            js.ExecuteScript("mw.QueryWindow()");
            Thread.Sleep(200);
            for (int k = 1; k <= db.FilterTypes.Where(f => f.FTType == 1).ToList().Count; k++)
            {
                js.ExecuteScript("mw.QueryWindowNewFilter();mw.QueryWindowFilterNames()");
                Thread.Sleep(200);
            }
            var ftL = db.FilterTypes.Where(ft => ft.FTType == 1).ToList();
            js.ExecuteScript("$('#ModalWindowOuter3').remove()");
            for (int j = 0; j < db.FilterTypes.Where(ft => ft.FTType == 1).ToList().Count; j++)
            {
                var selFTL = ftL[j];
                js.ExecuteScript("mw.SelectFilter(" + j + ");mw.ShowSettings();mw.SaveParams();mw.QueryWindowFilterNames()");
                Thread.Sleep(200);
                js.ExecuteScript("$('#ModalWindowOuter6').remove()");
                Thread.Sleep(200);
                js.ExecuteScript("mw.QueryWindow()");
                Thread.Sleep(200);
                driver.FindElement(By.Id("InputFilterName")).SendKeys(OpenQA.Selenium.Keys.Control + "a");
                driver.FindElement(By.Id("InputFilterName")).SendKeys(OpenQA.Selenium.Keys.Delete);
                driver.FindElement(By.Id("InputFilterName")).SendKeys(selFTL.FTName);
                driver.FindElement(By.Id("InputFilterCode")).SendKeys(selFTL.FTCode);
                js.ExecuteScript("mw.QueryWindowSaveFilter()");
            }
            #endregion
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            mainKM = new ReplyKeyboardMarkup();

            List<KeyboardButton> row1 = new List<KeyboardButton>();
            List<KeyboardButton> row2 = new List<KeyboardButton>();
            row1.Add(new KeyboardButton("راهنما" + "\U0001F4C3"));
            row1.Add(new KeyboardButton("لیست افزایش سرمایه ها" + "\U0001F3C1"));
            row2.Add(new KeyboardButton("ارتباط با مدیر" + "\U0000260E"));
            row2.Add(new KeyboardButton("درباره ما" + "\U0001F4E2"));
            var buttons = new List<List<KeyboardButton>>();
            buttons.Add(row2);
            buttons.Add(row1);
            foreach (var item in db.FilterTypes.ToList())
            {
                buttons.Add(new List<KeyboardButton>() { new KeyboardButton("فیلتر" + " " + item.FTName) });
            }
            mainKM.Keyboard = buttons;
        }
        void mainUpdates()
        {
            Classes.GetInfo.MainUpdateFiltereds(driver, driver2, db);
        }
        void runBot()
        {
            bot = new TelegramBotClient(Token);

            this.Invoke(new Action(() =>
            {
                lblStatus.Text = "Online";
                lblStatus.ForeColor = Color.Green;
            }));
            int offset = 0;

            while (true)
            {
                if (DateTime.Now.TimeOfDay >= new TimeSpan(12, 30, 0) && DateTime.Now.TimeOfDay <= new TimeSpan(12, 45, 0) && DateTime.Now.DayOfWeek != DayOfWeek.Friday && DateTime.Now.DayOfWeek != DayOfWeek.Thursday && secondCh != true)
                {
                    UpThread.Start();
                    firstCh = false;
                    secondCh = true;
                }
                if (DateTime.Now.TimeOfDay >= new TimeSpan(6, 0, 0) && DateTime.Now.TimeOfDay <= new TimeSpan(6, 10, 0) && DateTime.Today.DayOfWeek != DayOfWeek.Friday && DateTime.Today.DayOfWeek != DayOfWeek.Thursday && firstCh != true)
                {
                    firstCh = true;
                    secondCh = false;
                }
                Update[] upd = bot.GetUpdatesAsync(offset).Result;
                foreach (var u in upd)
                {
                    offset = u.Id + 1;
                    if (u.Message == null)
                        continue;
                    if (u.Message.Text == "@--------------")
                        continue;
                    var nowT = DateTime.Now.TimeOfDay;
                    var msgID = u.Message.MessageId;
                    var text = u.Message.Text;
                    var userID = u.Message.From.Id;
                    var chatID = u.Message.Chat.Id;
                    var date = u.Message.Date;
                    var userName = u.Message.From.Username;

                    Message msgToAdd = new Message()
                    {
                        MID = msgID,
                        MText = text,
                        MCID = (int)chatID,
                        MUID = userID,
                        MDate = date.ToString("yyyy/MM/dd-HH:mm"),
                        MUName = userName
                    };
                    db.Messages.Add(msgToAdd);
                    db.SaveChanges();
                    var rsl = bot.GetChatMemberAsync("@--------------", userID).Result;
                    if (rsl.Status.ToString() == "Left")
                    {
                        StringBuilder sb = new StringBuilder();
                        sb.AppendLine("کاربر محترم،برای استفاده رایگان از خدمات این بات می بایست در کانال ما عضو شوید");
                        sb.AppendLine("روی دکمه پایین کلیک کنید و سپس پس از ورود به کانال در آن عضو شوید،سپس به بات برگردید و دوباره درخواست خود را ارسال کنید");
                        var cKBa = new InlineKeyboardButton[][]
                            {
                            new InlineKeyboardButton[]
                            {
                                InlineKeyboardButton.WithUrl("ورود به کانال","https://t.me/--------------")
                            }
                            };
                        var cKMa = new InlineKeyboardMarkup(cKBa);
                        bot.SendTextMessageAsync(u.Message.From.Id, sb.ToString(), ParseMode.Default, false, false, 0, cKMa);
                    }
                    else
                    {
                        if (text.Contains("/start"))
                        {
                            StringBuilder sb = new StringBuilder();

                            sb.AppendLine("سلام" + u.Message.From.Username);
                            sb.AppendLine("به آیلوبات خوش آمدید");
                            sb.AppendLine("هر چه بخواهید اینجا هست");

                            bot.SendTextMessageAsync(chatID, sb.ToString(), ParseMode.Default, false, false, 0, mainKM);

                        }
                        else if (text.Contains("درباره ما"))
                        {
                            
                        }
                        else if (text.Contains("ارتباط با مدیر"))
                        {
                            var cIKB = new InlineKeyboardButton[][]
                            {
                            new InlineKeyboardButton[]
                            {
                                InlineKeyboardButton.WithUrl("ارتباط با مدیر","http://t.me/---------")
                            }
                            };
                            var cMK = new InlineKeyboardMarkup(cIKB);
                            StringBuilder sb = new StringBuilder();
                            sb.AppendLine("برای ارتباط با مدیر دکمه زیر را لمس کنید");
                            bot.SendTextMessageAsync(chatID, sb.ToString(), ParseMode.Default, false, false, 0, cMK);

                        }
                        else if (text.Contains("لیست افزایش سرمایه ها"))
                        {
                            StringBuilder sb = new StringBuilder();
                            sb.AppendLine("برای دیدن لیست افزایش سرمایه ها بر اساس سایت کدال روی دکمه پایین کلیک کنید");
                            var cKB = new InlineKeyboardButton[][]
                            {
                            new InlineKeyboardButton[]
                            {
                                InlineKeyboardButton.WithUrl("لیست افزایش سرمایه ها","https://www.codal.ir/ReportList.aspx?search&LetterCode=%D9%86-%DB%B6%DB%B2&LetterType=-1&AuditorRef=-1&PageNumber=1&Audited&NotAudited&IsNotAudited=false&Childs&Mains&Publisher=false&CompanyState=-1&Category=-1&CompanyType=-1&Consolidatable&NotConsolidatable")
                            }
                            };
                            var cKM = new InlineKeyboardMarkup(cKB);
                            bot.SendTextMessageAsync(chatID, sb.ToString(), ParseMode.Default, false, false, 0, cKM);
                        }
                        else if (text == "راهنما")
                        {
                            StringBuilder sb = new StringBuilder();
                            sb.AppendLine("راهنما در اینجا قرار میگیرد");
                            bot.SendTextMessageAsync(chatID, sb.ToString(), ParseMode.Default, false, false, 0, mainKM);
                        }
                        else if (text.StartsWith("فیلتر "))
                        {
                            StringBuilder sb = new StringBuilder();
                            if (DateTime.Now.TimeOfDay >= st2 && DateTime.Now.TimeOfDay <= en2 && DateTime.Today.DayOfWeek != DayOfWeek.Thursday && DateTime.Today.DayOfWeek != DayOfWeek.Friday)
                            {
                                sb.AppendLine("کاربر گرامی در این ساعت امکان استفاده از این مورد وجود ندارد،بعد از ساعت 12:45 تلاش کنید");
                            }
                            else
                            {
                                text = text.Remove(0, 6);
                                FilterType ft = db.FilterTypes.Where(f => f.FTName == text).ToList().SingleOrDefault();
                                sb.AppendLine(ft.FTName);
                                sb.AppendLine(ft.FTDesc);
                                if (DateTime.Now.TimeOfDay > new TimeSpan(8, 29, 59) && DateTime.Now.TimeOfDay <= new TimeSpan(12, 30, 00))
                                {
                                    sb.AppendLine("در حال حاظر اطلاعات بات در زمان بازار آپدیت نمیشود،این اطلاعات از روز قبل است،اطلاعات جدید در ساعت 12:45 به روزرسانی میشود");
                                }
                                foreach (var d in db.Filtereds.Where(fq => fq.FTID == ft.FTID).ToList())
                                {
                                    sb.AppendLine(d.SName);
                                }
                            }
                            bot.SendTextMessageAsync(chatID, sb.ToString(), ParseMode.Default, false, false, 0, mainKM);

                        }
                        else if (text.StartsWith("نماد "))
                        {
                            text = text.Remove(0, 5);
                            StringBuilder sb = new StringBuilder();
                            Symbol selSy = db.Symbols.Where(s => s.SRahName == text).SingleOrDefault();
                            if (selSy == null)
                            {
                                sb.AppendLine("متاسفانه چنین سهمی یافت نشد، لطفا نام سهم را دقیق وارد کنید و یا مشکل را به ادمین گزارش دهید");
                            }
                            else
                            {
                                bot.SendTextMessageAsync(chatID, "لطفا چند ثانیه صبور باشید،در حال پردازش اطلاعات");
                                if ((!(DateTime.Now.TimeOfDay >= new TimeSpan(8, 30, 0) && DateTime.Now.TimeOfDay <= en2)) || DateTime.Today.DayOfWeek == DayOfWeek.Thursday || DateTime.Today.DayOfWeek == DayOfWeek.Friday)
                                {
                                    List<Filtered> filds = db.Filtereds.Where(fd => fd.SID == selSy.SID).ToList();
                                    sb.AppendLine("فیلتر های امروز این سهم:");
                                    if (filds == null)
                                    {
                                        sb.AppendLine("هیچ فیلتری یافت نشد");
                                    }
                                    else
                                    {
                                        foreach (var fild in filds)
                                        {
                                            sb.AppendLine("___________");
                                            sb.AppendLine(fild.FTName);
                                            sb.AppendLine(fild.FTDesc);
                                        }
                                    }
                                }
                                else
                                {
                                    sb.AppendLine("فیلتر های این سهم را پس از 12:45 میتوانید مشاهده کنید");
                                }
                                int upTime = 4;
                                if (DateTime.Now.TimeOfDay > new TimeSpan(8, 30, 0) && DateTime.Now.TimeOfDay <= new TimeSpan(10, 0, 0) && DateTime.Today.DayOfWeek != DayOfWeek.Thursday && DateTime.Today.DayOfWeek != DayOfWeek.Friday)
                                {
                                    upTime = 1;
                                }
                                else if (DateTime.Now.TimeOfDay > new TimeSpan(10, 0, 0) && DateTime.Now.TimeOfDay <= new TimeSpan(11, 30, 0) && DateTime.Today.DayOfWeek != DayOfWeek.Thursday && DateTime.Today.DayOfWeek != DayOfWeek.Friday)
                                {
                                    upTime = 2;
                                }
                                else if (DateTime.Now.TimeOfDay > new TimeSpan(11, 30, 0) && DateTime.Now.TimeOfDay <= new TimeSpan(12, 30, 0) && DateTime.Today.DayOfWeek != DayOfWeek.Thursday && DateTime.Today.DayOfWeek != DayOfWeek.Friday)
                                {
                                    upTime = 3;
                                }
                                JObject details;
                                if (upTime == selSy.SUpdateTime)
                                {
                                    details = JObject.Parse(selSy.SJsonCode);
                                }
                                else
                                {
                                    HtmlAgilityPack.HtmlDocument doc = web.Load("http://rahavard365.com/asset/" + selSy.SRahID + "/indicator");
                                    Thread.Sleep(4000);
                                    var scTg = doc.DocumentNode.SelectNodes("//script[@type='text/javascript']").First();
                                    var sCon = scTg.InnerText.Remove(0, 23);
                                    string tmp = "    Global = {};";
                                    var jsonDataF = sCon.Replace(tmp, "");
                                    var jsonData = jsonDataF.Replace(";", "");
                                    details = JObject.Parse(jsonData);
                                    selSy.SJsonCode = details.ToString();
                                    selSy.SUpdateTime = upTime;
                                    db.Symbols.Attach(selSy);
                                    db.Entry(selSy).State = System.Data.Entity.EntityState.Modified;
                                    db.SaveChanges();
                                }
                                var pSL = details.SelectToken("technical_sum_List").ToList();
                                var SSIndic = Convert.ToInt32(pSL[2]);
                                var SNIndic = Convert.ToInt32(pSL[1]);
                                var SBIndic = Convert.ToInt32(pSL[0]);
                                sb.AppendLine("___________________________________________");
                                if (SSIndic == 0 && SNIndic == 0 && SBIndic == 0)
                                {
                                    sb.AppendLine("اطلاعات دیگری برای این نماد یافت نشد");
                                }
                                else
                                {
                                    List<JToken> Ichi = new List<JToken>();
                                    List<JToken> adx = new List<JToken>();
                                    List<JToken> macd = new List<JToken>();
                                    List<JToken> so = new List<JToken>();
                                    List<JToken> strsi = new List<JToken>();
                                    var Ichi1 = details.SelectToken("moving_averages").Where(j => j.SelectToken("type").ToString() == "indicator_ichimoku_cloud_d_last");
                                    if (Ichi1.Count() != 0)
                                    {
                                        Ichi = Ichi1.SingleOrDefault().SelectToken("values").ToList();
                                    }
                                    var adx1 = details.SelectToken("indicators").Where(j => j.SelectToken("type").ToString() == "indicator_adx_14_d_last");
                                    var macd1 = details.SelectToken("indicators").Where(j => j.SelectToken("type").ToString() == "indicator_macd_26_12_9_d_last");
                                    var so1 = details.SelectToken("oscillators").Where(j => j.SelectToken("type").ToString() == "indicator_so_14_3_3_d_last");
                                    if (so1.Count() != 0)
                                    {
                                        so = so1.SingleOrDefault().SelectToken("values").ToList();
                                    }
                                    var strsi1 = details.SelectToken("indicators").Where(j => j.SelectToken("type").ToString() == "indicator_stochrsi_14_d_last");
                                    if (strsi1.Count() != 0)
                                    {
                                        strsi = strsi1.SingleOrDefault().SelectToken("values").ToList();
                                    }
                                    sb.AppendLine("___________________________________");
                                    sb.Append("تعداد اندیکاتور های خرید:");
                                    sb.AppendLine(SBIndic.ToString());
                                    sb.Append("تعداد اندیکاتور های فروش:");
                                    sb.AppendLine(SSIndic.ToString());
                                    sb.Append("تعداد اندیکاتور های خنثی:");
                                    sb.AppendLine(SNIndic.ToString());
                                    var tSL = details.SelectToken("pivot_sum_list").ToList();
                                    sb.Append("تعداد اندیکاتور های حمایت:");
                                    sb.AppendLine(Convert.ToInt32(tSL[1]).ToString());
                                    sb.Append("تعداد اندیکاتور های مقاومت:");
                                    sb.AppendLine(Convert.ToInt32(tSL[0]).ToString());
                                    sb.Append("تعداد اندیکاتور های نقطه عطف:");
                                    sb.AppendLine(Convert.ToInt32(tSL[2]).ToString());
                                    sb.AppendLine("\n");
                                    sb.AppendLine("\n");
                                    sb.AppendLine("\n");
                                    sb.AppendLine("اندیکاتور های اشباع خرید و فروش");
                                    sb.AppendLine("\n");
                                    var osc = details.SelectToken("oscillators");
                                    sb.Append("rsi:");
                                    sb.AppendLine(osc.Where(j => j.SelectToken("type").ToString() == "indicator_rsi_14_d_last").SingleOrDefault().SelectToken("values")[0].SelectToken("value").ToString());
                                    sb.Append("stoch rsi");
                                    sb.AppendLine(osc.Where(j => j.SelectToken("type").ToString() == "indicator_stochrsi_14_d_last").SingleOrDefault().SelectToken("values")[0].SelectToken("value").ToString());
                                    sb.Append("mfi:");
                                    sb.AppendLine(osc.Where(j => j.SelectToken("type").ToString() == "indicator_mfi_14_d_last").SingleOrDefault().SelectToken("values")[0].SelectToken("value").ToString());
                                    sb.Append("cci:");
                                    sb.AppendLine(osc.Where(j => j.SelectToken("type").ToString() == "indicator_cci_14_d_last").SingleOrDefault().SelectToken("values")[0].SelectToken("value").ToString());
                                    if (adx1.Count() != 0)
                                    {
                                        adx = adx1.SingleOrDefault().SelectToken("values").ToList();
                                        sb.Append("adx:");
                                        sb.AppendLine(adx[2].SelectToken("value").ToString());
                                    }
                                    if (macd1.Count() != 0)
                                    {
                                        macd = macd1.SingleOrDefault().SelectToken("values").ToList();
                                        sb.Append("macd:");
                                        sb.AppendLine(macd[0].SelectToken("value").ToString());
                                        sb.Append("macd signal:");
                                        sb.AppendLine(macd[1].SelectToken("value").ToString());
                                    }
                                    sb.AppendLine("\n");
                                    sb.AppendLine("\n");
                                    sb.AppendLine("\n");
                                    sb.Append("سیگنال های اندیکاتور ها و روند ها:");
                                    sb.AppendLine("\n");
                                    sb.AppendLine("adx:");
                                    foreach (var iw in adx)
                                    {
                                        if (iw.SelectToken("long_name_fa").Contains("روند"))
                                        {
                                            continue;
                                        }
                                        if (iw.SelectToken("value").ToString() == "True")
                                        {
                                            var tdt = iw.SelectToken("long_name_fa").ToString();
                                            if (iw.SelectToken("long_name_fa").Contains("باشد"))
                                            {
                                                tdt = iw.SelectToken("long_name_fa").ToString().Replace("باشد", "");
                                            }
                                            sb.AppendLine(tdt);
                                        }
                                    }
                                    sb.AppendLine("macd:");
                                    foreach (var iw in macd)
                                    {
                                        if (iw.SelectToken("long_name_fa").Contains("روند"))
                                        {
                                            continue;
                                        }
                                        if (iw.SelectToken("value").ToString() == "True")
                                        {
                                            var tdt = iw.SelectToken("long_name_fa").ToString();
                                            if (iw.SelectToken("long_name_fa").Contains("باشد"))
                                            {
                                                tdt = iw.SelectToken("long_name_fa").ToString().Replace("باشد", "");
                                            }
                                            sb.AppendLine(tdt);
                                        }
                                    }
                                    sb.AppendLine("stochastic:");
                                    foreach (var iw in so)
                                    {
                                        if (iw.SelectToken("long_name_fa").Contains("روند"))
                                        {
                                            continue;
                                        }
                                        if (iw.SelectToken("value").ToString() == "True" && iw.SelectToken("value").ToString().Contains("و خط") == false)
                                        {
                                            sb.AppendLine(iw.SelectToken("long_name_fa").ToString());
                                        }
                                    }
                                    sb.AppendLine("Ichimoko:");
                                    foreach (var iw in Ichi)
                                    {
                                        if (iw.SelectToken("value").ToString().Contains("روند"))
                                        {
                                            continue;
                                        }
                                        if (iw.SelectToken("value").ToString() == "True")
                                        {
                                            var tdt = iw.SelectToken("long_name_fa").ToString();
                                            if (tdt.Contains("تنکن سن از ابر"))
                                            {
                                                tdt.Replace("تنکن سن", "کیجون سن");
                                            }
                                            else if(tdt.Contains("کیجون سن از ابر"))
                                            {
                                                tdt.Replace("کیجون سن", "تنکن سن");
                                            }
                                            if(iw.SelectToken("long_name_en").ToString() == "Tight Cloud")
                                            {
                                                tdt = "سنکو اسپن A و B برابر";
                                            }
                                            sb.AppendLine(tdt);
                                        }
                                    }
                                    sb.AppendLine("stoch rsi:");
                                    foreach (var iw in strsi)
                                    {
                                        if (iw.SelectToken("long_name_fa").Contains("روند"))
                                        {
                                            continue;
                                        }
                                        if (iw.SelectToken("value").ToString() == "True")
                                        {
                                            sb.AppendLine(iw.SelectToken("long_name_fa").ToString());
                                        }
                                    }
                                    sb.AppendLine("\n");
                                    sb.AppendLine("\n");
                                    sb.AppendLine("\n");
                                    var pInFib = details.SelectToken("pivot_indicators")[1].SelectToken("values");
                                    var pIn = details.SelectToken("pivot_indicators")[3].SelectToken("values");
                                    sb.AppendLine("سطوح حمایت و مقاومت به روش کلاسیک:");
                                    sb.AppendLine(Convert.ToInt32(pIn[5].SelectToken("value")).ToString());
                                    sb.AppendLine(Convert.ToInt32(pIn[3].SelectToken("value")).ToString());
                                    sb.AppendLine(Convert.ToInt32(pIn[1].SelectToken("value")).ToString());
                                    sb.AppendLine(Convert.ToInt32(pIn[0].SelectToken("value")).ToString());
                                    sb.AppendLine(Convert.ToInt32(pIn[2].SelectToken("value")).ToString());
                                    sb.AppendLine(Convert.ToInt32(pIn[4].SelectToken("value")).ToString());
                                    sb.AppendLine(Convert.ToInt32(pIn[6].SelectToken("value")).ToString());
                                    sb.Append("\n");
                                    sb.AppendLine("سطوح حمایت و مقاومت به روش فیبوناچی");
                                    sb.AppendLine(Convert.ToInt32(pInFib[5].SelectToken("value")).ToString());
                                    sb.AppendLine(Convert.ToInt32(pInFib[3].SelectToken("value")).ToString());
                                    sb.AppendLine(Convert.ToInt32(pInFib[1].SelectToken("value")).ToString());
                                    sb.AppendLine(Convert.ToInt32(pInFib[0].SelectToken("value")).ToString());
                                    sb.AppendLine(Convert.ToInt32(pInFib[2].SelectToken("value")).ToString());
                                    sb.AppendLine(Convert.ToInt32(pInFib[4].SelectToken("value")).ToString());
                                    sb.AppendLine(Convert.ToInt32(pInFib[6].SelectToken("value")).ToString());
                                    sb.AppendLine("\n");
                                    sb.AppendLine("\n");
                                    sb.AppendLine("\n");
                                    sb.AppendLine("کندل ها");
                                    sb.AppendLine("\n");
                                    var cans1 = details.SelectToken("indicators").Where(j => j.SelectToken("type").ToString() == "indicator_candlepattern_d_last");
                                    var cans = cans1.SingleOrDefault().SelectToken("values").ToList();
                                    foreach (var iq in cans)
                                    {
                                        if (iq.SelectToken("value").ToString() == "True")
                                        {
                                            sb.AppendLine(iq.SelectToken("long_name_fa").ToString());
                                        }
                                    }
                                }
                            }
                            bot.SendTextMessageAsync(chatID, sb.ToString(), ParseMode.Html, false, false, 0, mainKM);
                        }
                    }
                    ReporterGrid.Invoke(new Action(() =>
                    {
                        ReporterGrid.Rows.Add(u.Message.MessageId, u.Message.From.Username, u.Message.From.Id, date.ToString("yyyy / MM / dd - HH:mm"), u.Message.Text);

                    }));
                }
            }
        }
        private void btnSend_Click(object sender, EventArgs e)
        {
            List<int?> Ids = new List<int?>();
            foreach (var itm in db.Messages.ToList())
            {
                if (!Ids.Contains(itm.MUID))
                {
                    Ids.Add(itm.MUID);
                    bot.SendTextMessageAsync(itm.MUID, this.txtMessage.Text, ParseMode.Html, false, false, 0, null);
                }
            }

        }
        private void btnSelectFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFile = new OpenFileDialog();
            if (openFile.ShowDialog() == DialogResult.OK)
            {
                txtFilePath.Text = openFile.FileName;
            }
        }
        private void btnPhoto_Click(object sender, EventArgs e)
        {
            List<int?> Ids = new List<int?>();
            FileStream imageFile = System.IO.File.Open(txtFilePath.Text, FileMode.Open);
            foreach (var itm in db.Messages.ToList())
            {
                if (!Ids.Contains(itm.MUID))
                {
                    Ids.Add(itm.MUID);
                    bot.SendPhotoAsync(itm.MUID, new Telegram.Bot.Types.InputFiles.InputOnlineFile(imageFile, "1234.jpg"), txtMessage.Text);
                }
            }
        }
        private void btnVideo_Click(object sender, EventArgs e)
        {
            List<int?> Ids = new List<int?>();
            FileStream videoFile = System.IO.File.Open(txtFilePath.Text, FileMode.Open);
            foreach (var itm in db.Messages.ToList())
            {
                Ids.Add(itm.MUID);
                bot.SendVideoAsync(itm.MUID, new Telegram.Bot.Types.InputFiles.InputOnlineFile(videoFile, "1234"), 0, 0, 0, txtMessage.Text);
            }

        }
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            botThread.Abort();
            UpThread.Abort();
            db.Dispose();
        }
    }
}
