using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;
using System.Windows.Forms;
using System.IO;



namespace pBat
{
    class Program
    {
        static public Configuration cfg = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

        static string invalidKey = "<>";
        static string szpBatPath = Application.StartupPath + "\\p.bat";
        static string szDotpBatPath = Application.StartupPath + "\\.p.bat";

        static public string GetSingleCfg(string szcfkKey, string szDefault)
        {
            string szRetString = "";
            try
            {
                if (cfg.AppSettings.Settings[szcfkKey] == null)
                {
                    return szDefault;
                }

                szRetString = cfg.AppSettings.Settings[szcfkKey].Value;
                if (szRetString == null)
                {
                    return szDefault;
                }
            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.ToString());
                return szDefault;
            }
            return szRetString;
        }

        static public void CfgSaveSingle(string szKey, string szValue)
        {
            try
            {
                cfg.AppSettings.Settings.Remove(szKey);
                cfg.AppSettings.Settings.Add(szKey, szValue);
                cfg.Save();
            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.ToString());
                return;
            }
        }

        static public void CfgRemoveSingle(string szKey)
        {
            try
            {
                cfg.AppSettings.Settings.Remove(szKey);
                cfg.Save();
            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.ToString());
                return;
            }
        }

        static void SetNewEntry(string szKey)
        {
            string szPWD = Directory.GetCurrentDirectory();
            CfgSaveSingle(szKey, szPWD);
        }

        static void CreatePdotBat()
        {
            if (File.Exists(szpBatPath))
            {
                // it exists
            }
            else
            {
                StreamWriter sw = new StreamWriter(szpBatPath);
                sw.WriteLine("@echo off");
                sw.WriteLine("pBat %*");
                sw.WriteLine(".p.bat %*");
                sw.Close();
            }
        }

        static void OneArg(string szArg)  
        {
            string FilePath = GetSingleCfg(szArg, invalidKey);
            if (FilePath==invalidKey)
            {
                SetNewEntry(szArg);
                StreamWriter sw = new StreamWriter(szDotpBatPath, false);
                sw.WriteLine("@echo off");
                sw.Close();
            }
            else
            {

                //Console.WriteLine("creating .p.bat at " + szDotpBatPath);
                StreamWriter sw = new StreamWriter(szDotpBatPath, false);
                sw.WriteLine("@echo off");
                if (FilePath.Contains(":"))
                {
                    int index = FilePath.IndexOf(":");
                    string szDriveLetter = FilePath.Remove(index + 1);
                    sw.WriteLine(szDriveLetter);

                }
                
                sw.WriteLine("cd " + FilePath);
                sw.Close();
            }

        }

        static void TwoArg(string szArg1, string szArg2)
        {

            //Console.WriteLine("3arg " + szArg1 + " " + szArg2);
            switch (szArg2)
            {
                case "cls":
                    CfgRemoveSingle(szArg1);

                    break;
                default:
                    Console.WriteLine("only valid 2nd arg is cls");
                    break;

            }

        }

        static void Main(string[] args)
        {

            CreatePdotBat();
            if (args.Length == 1)
            {
                OneArg(args[0]);
                return;
            }
            if (args.Length == 2)
            {
                TwoArg(args[0],args[1]);
                return;
            }
            KeyValueConfigurationCollection kvs = cfg.AppSettings.Settings;
            foreach (KeyValueConfigurationElement ke in kvs)
            {
                Console.WriteLine(ke.Key + "\t" + ke.Value);
            }
        }
    }
}
