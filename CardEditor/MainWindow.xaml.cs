﻿using CardEditor.Data;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
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

namespace CardEditor
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 
    public partial class MainWindow : Window
    {
        string typeText;
        string filePath;
        //EditorManager manager;

        public MainWindow()
        {
            InitializeComponent();
            //manager = new EditorManager();
            //manager.Init();

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (sender.Equals(btnUpload))
            {
                imageUpload();
            }
            else if (sender.Equals(btnAddType))
            {
                typeText = tbxAddType.Text;
                cboType.Items.Add(typeText);
                tbxAddType.Clear();
            }
        }

        private void imageUpload()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image files (*.jpg)|*.jpg|All Files (*.*)|*.*"; ;
            openFileDialog.RestoreDirectory = true;

            if (openFileDialog.ShowDialog() == true)
            {
                string file = openFileDialog.FileName;
                tbcPath.Text = "Path: " + file;
                filePath = file;
                BitmapImage bitmap = new BitmapImage();
                bitmap.BeginInit();
                bitmap.UriSource = new Uri(file);
                bitmap.EndInit();
                image.Source = bitmap;

            }
        }
    }
}