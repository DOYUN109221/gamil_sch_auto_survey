using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SCH_Survey
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        public static bool checkboxbool = false;
        public static bool[] working = { false, false, false, false, false, false, false, false, false, false };
        public static int working2 = 0;
        public static int suuuum = 0;

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            checkboxbool = checkBox3.Checked;
            if (checkboxbool == false)
            {
                checkBox2.Checked = true;
                checkboxbool = true;
            }

        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            checkboxbool = checkBox2.Checked;
            if(checkboxbool == false)
            {
                checkBox3.Checked = true;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            trackBar1.Maximum = 10;
            timer1.Start();
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            int thread = trackBar1.Value;

            label4.Text = $"쓰레드 : {thread.ToString()}";
            start(thread + 1);
        }

        public void start(int thread)
        {
            int workingsum = 0;
            foreach (bool a in working)
            {
                if(a)
                {
                    ++workingsum;
                }
            }
            
            if (thread == 0)
            {
                working2 = 0;
                for (int i = 0; i < working.Length; i++)
                {
                    working[i] = false;
                }
            }
            else if (workingsum > thread)
            {
                for (int i = thread; i < working.Length; i++)
                {
                    working[i] = false;
                    working2--;
                }
            }
            else if(workingsum < thread)
            {
                for (int i = workingsum; i < thread; i++)
                {
                    working[i] = true;
                }
            }

            workingsum = 0;
            foreach (bool a in working)
            {
                if (a)
                {
                    ++workingsum;
                }
            }
            if(workingsum > working2)
            {
                
                for(int i = working2 - 1; i < workingsum; i++)
                {
                    Thread thread1 = new Thread(() => Run(i));
                    thread1.Start();
                }
                
            }

        }


        public void Run(int a)
        {
            try
            {
                Random ran = new Random();
                int random = ran.Next(1, 10);
                while (working[a])
                {
                    Console.WriteLine(a + " 작동중 고유번호: " + random);
                    using(IWebDriver driver = new ChromeDriver())
                    {
                        driver.Url = "https://form.office.naver.com/form/responseView.cmd?formkey=NWNjYWU4ODctNWY5Ni00M2Y5LTk5YWItMWJmODAxMmVhYjJj&sourceId=urlshare";
                        driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(3);
                        driver.FindElement(By.XPath("/html/body/div[5]/div[1]/div[1]/div/div[2]/div/div/div[2]/form/div/div/div/div[3]/div/textarea")).SendKeys(getdata());
                        Thread.Sleep(1000);
                        driver.FindElement(By.XPath("/html/body/div[5]/div[1]/div[1]/div/div[2]/div/div/div[2]/div/button[3]")).Click();
                        Thread.Sleep(1000);
                        check();

                    }
                }
            }
            catch 
            { 
            }
        }

        public void check()
        {
            suuuum++;
        }
        public string getdata()
        {
            if(checkBox3.Checked)
            {
                if(textBox1.Text.Length >= 1)
                {
                    return textBox1.Text;
                }
                return "뽀로로 아야 아파요";
            }
            else
            {
                string[] rich = richTextBox1.Text.Split('\n');
                if(rich.Length > suuuum)
                {
                    return rich[suuuum];
                }
                return "뽀로로 아야 아파요";
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            sum.Text = suuuum.ToString();
        }

        private void groupBox3_Enter(object sender, EventArgs e)
        {

        }
    }
}
