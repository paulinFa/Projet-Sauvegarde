using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using System.Threading;
using System.Windows;

namespace Projet_Sauvegarde.Utils
{
    public static class LangUtil
    {
        private static string GetAppName()
        {
            var elType = App.Current.MainWindow.GetType().ToString();
            var elNames = elType.Split('.');
            return elNames[0];
        }

        public static string GetString(string key)
        {
            return App.Current.Resources[key] as string;
        }

        public static void SetupLang()
        {
            SetLang(GetCurrentLang());
        }

        public static string GetCurrentLang()
        {
            RegistryKey curLocInfo = Registry.CurrentUser.OpenSubKey("GsmLib" + @"\" + GetAppName(), false);
            var cultureName = CultureInfo.CurrentUICulture.Name;
            if (curLocInfo != null)
            {
                cultureName = curLocInfo.GetValue("lang", "en-US").ToString();
            }
            return cultureName;
        }

        public static void SetLang(string lang)
        {
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(lang);
            SetLanguageResourceDictionary(GetLocXAMLFilePath(lang));
            // Save new culture info to registry  
            RegistryKey UserPrefs = Registry.CurrentUser.OpenSubKey("GsmLib" + @"\" + GetAppName(), true);
            if (UserPrefs == null)
            {
                // Value does not already exist so create it  
                RegistryKey newKey = Registry.CurrentUser.CreateSubKey("GsmLib");
                UserPrefs = newKey.CreateSubKey(GetAppName());
            }
            UserPrefs.SetValue("lang", lang);
        }

        private static string GetLocXAMLFilePath(string inFiveCharLang)
        {
            string locXamlFile = inFiveCharLang + ".xaml";
            string directory = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            return Path.Combine(directory, "i18N", locXamlFile);
        }

        private static void SetLanguageResourceDictionary(String inFile)
        {
            if (File.Exists(inFile))
            {
                // Read in ResourceDictionary File  
                var languageDictionary = new ResourceDictionary();
                languageDictionary.Source = new Uri(inFile);
                // Remove any previous Localization dictionaries loaded
                int langDictId = -1;
                for (int i = 0; i < App.Current.Resources.MergedDictionaries.Count; i++)
                {
                    var md = App.Current.Resources.MergedDictionaries[i];
                    // Make sure your Localization ResourceDictionarys have the ResourceDictionaryName  
                    // key and that it is set to a value starting with "Loc-".  
                    if (md.Contains("ResourceDictionaryName"))
                    {
                        if (md["ResourceDictionaryName"].ToString().StartsWith("Loc-"))
                        {
                            langDictId = i;
                            break;
                        }
                    }
                }
                if (langDictId == -1)
                {
                    // Add in newly loaded Resource Dictionary  
                    App.Current.Resources.MergedDictionaries.Add(languageDictionary);
                }
                else
                {
                    // Replace the current langage dictionary with the new one  
                    App.Current.Resources.MergedDictionaries[langDictId] = languageDictionary;
                }
            }
            else
            {
                MessageBox.Show("'" + inFile + "' not found.");
            }
        }

    }
}
