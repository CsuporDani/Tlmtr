/*
 *  Csupor Dániel - MegaLux Telemetry - Copyright (C) 2016
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Win32;
using System.IO;
using System.Windows;

namespace Tlmtr.OpenSaveManager
{
    class OpenSaveManager
    {
        string fileLocation;

        public void OpenFile()
        {
            // Fájl megkeresése, majd ha van találat, akkor a tovább adjuk a ReadAndSplitFile() fügvénynek
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";

            if (openFileDialog.ShowDialog() == true)
            {
                fileLocation = openFileDialog.FileName;
                ReadAndSplitFile();
            }
        }

        void ReadAndSplitFile()
        { 
            // egy sztringbe beolvassuk az össze fájlt, és szekciókra bontjuk 
            string line;
            using (StreamReader sr = new StreamReader(fileLocation))
            {           
                 line = sr.ReadToEnd();  
            }
            line = line.Replace("\r\n", string.Empty); // sortörések kiszedése
            string[] Sections = line.Split('#');

            foreach(string section in Sections)
            {
                switch (section.Split('{')[0])
                {
                    case "WindowSettings":
                        WindowSettings(section);
                        break;
                    case "TabControls":
                        TabControlsSettings(section);
                        break;
                }
            }
        }

      

        // A fájlból beolvasott főablak adatainak az eseménye. Kiváltjuk, amint beolvastuk az ablak adatait
        public delegate void EventHandlerDelegate(int Width, int Height);
        public static event EventHandlerDelegate WindowsSettingsChanged;

        void WindowSettings(string section)
        {
             int WindowsWidth = 1200; // alapértelmezett ablakméret adat
             int WindowsHeight = 900;

            string[] propertys = section.Split(';', '{'); // sorokra bontjuk, majd a foreach ciklusban elkülönitjük az adatot a névtől.
            foreach (string property in propertys)
            {
                switch (property.Split(':')[0])
                {
                    case "Width":
                        WindowsWidth = Convert.ToInt32(property.Split(':')[1]);
                        break;
                    case "Height":
                        WindowsHeight = Convert.ToInt32(property.Split(':')[1]);
                        break;

                }
            }
            if (WindowsSettingsChanged != null)  // Megnézi van-e a főablakra változás eseményére felíratkozva valaki. Ha van, akkor kiváltjuk az eseményt.
            {
                WindowsSettingsChanged(WindowsWidth, WindowsHeight);
            }

        }

        void TabControlsSettings(string section)
        {
            string[] propertys = section.Split(';', '{'); // Sorokra bontás, majd az érték, név párok kinyerése.
            List<string> files = new List<string>();
            foreach (string property in propertys)
            {
                switch (property.Split(':')[0])
                {
                    case "TabItemsPath":
                        files.Add(property.Split(':')[1]);
                        break;
                

                }
            }

        }
    }
}
