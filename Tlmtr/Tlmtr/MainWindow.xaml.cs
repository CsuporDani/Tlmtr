/*
 *  Csupor Dániel - MegaLux Telemetry - Copyright (C) 2016
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Tlmtr
{
    
    public partial class MainWindow : Window
    {
        OpenSaveManager.OpenSaveManager open = new OpenSaveManager.OpenSaveManager();
        public MainWindow()
        {
            InitializeComponent();

        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            // Feliratkozás a filemegnyitásnál az ablak adatainak változásának eseményére a fájlbeolváskor
            // utána a fájlmegnyitás elkezdése
            OpenSaveManager.OpenSaveManager.WindowsSettingsChanged += WindowsSizeChanged;      
            open.OpenFile();

        }

        void WindowsSizeChanged(int width, int height)
        { 
            //az esemény lekezelése 
            this.Width = width;
            this.Height = height;

        }
    }
}
