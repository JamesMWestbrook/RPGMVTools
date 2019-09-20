using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using WinForms = System.Windows.Forms;

namespace RPGMVTools
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static string projectPath;
        public static string BackupTwoPath;
        public static string BackupOnePath;

        public static string RXGCPath;


        public MainWindow()
        {
            InitializeComponent();

            projectPath = Properties.Settings.Default.StoredProjectPath;
            ProjectFolderPath.Text = projectPath;

            BackupOnePath = Properties.Settings.Default.StoredBackupOnePath;
            BackupFolderOnePath.Text = BackupOnePath;

            BackupTwoPath = Properties.Settings.Default.StoredBackupTwoPath;
            BackupFolderTwoPath.Text = BackupTwoPath;

            RXGCPath = Properties.Settings.Default.RXGCFolderPath;
            RXGCFolderPath.Text = RXGCPath;

        }



        private void ProjectFolderButton_Click(object sender, RoutedEventArgs e)
        {
            WinForms.FolderBrowserDialog dialogue = new System.Windows.Forms.FolderBrowserDialog();
            System.Windows.Forms.DialogResult result = dialogue.ShowDialog();
            projectPath = result.ToString();

            if (result == WinForms.DialogResult.OK)
            {
                projectPath = dialogue.SelectedPath;
            }

            ProjectFolderPath.Text = projectPath;
            Properties.Settings.Default.StoredProjectPath = projectPath;

            Properties.Settings.Default.Save();
        }
        private void BackupFolderOneButton_Click(object sender, RoutedEventArgs e)
        {
            //OpenFileDialog openFileDialog = new OpenFileDialog();
            //if (openFileDialog.ShowDialog() == true) ProjectPath = File.ReadAllText(openFileDialog.FileName);

            WinForms.FolderBrowserDialog dialogue = new System.Windows.Forms.FolderBrowserDialog();
            System.Windows.Forms.DialogResult result = dialogue.ShowDialog();
            BackupOnePath = result.ToString();

            if (result == WinForms.DialogResult.OK)
            {
                BackupOnePath = dialogue.SelectedPath;
            }
            BackupFolderOnePath.Text = BackupOnePath;
            Properties.Settings.Default.StoredBackupOnePath = BackupOnePath;

            Properties.Settings.Default.Save();
        }

        private void BackupFolderTwoButton_Click(object sender, RoutedEventArgs e)
        {
            //OpenFileDialog openFileDialog = new OpenFileDialog();
            //if (openFileDialog.ShowDialog() == true) ProjectPath = File.ReadAllText(openFileDialog.FileName);

            WinForms.FolderBrowserDialog dialogue = new System.Windows.Forms.FolderBrowserDialog();
            System.Windows.Forms.DialogResult result = dialogue.ShowDialog();
            BackupTwoPath = result.ToString();

            if (result == WinForms.DialogResult.OK)
            {
                BackupTwoPath = dialogue.SelectedPath;
            }
            BackupFolderTwoPath.Text = BackupTwoPath;
            Properties.Settings.Default.StoredBackupTwoPath = BackupTwoPath;

            Properties.Settings.Default.Save();
        }


        private void BackupProjectButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void RXGCFoldeButton_Click(object sender, RoutedEventArgs e)
        {
            WinForms.FolderBrowserDialog dialogue = new System.Windows.Forms.FolderBrowserDialog();
            System.Windows.Forms.DialogResult result = dialogue.ShowDialog();
            projectPath = result.ToString();

            if (result == WinForms.DialogResult.OK)
            {
                RXGCPath = dialogue.SelectedPath;
            }

            RXGCFolderPath.Text = RXGCPath;
            Properties.Settings.Default.RXGCFolderPath = RXGCPath;
            Properties.Settings.Default.Save();
        }




        private void MoveGraphics_Click(object sender, RoutedEventArgs e)
        {


            DirectoryInfo RXGCFolder = new DirectoryInfo(RXGCPath);
            DirectoryInfo ProjectFolder = new DirectoryInfo(projectPath);

            FileInfo[] Images = RXGCFolder.GetFiles();

            Console.Write(Images);

            List<FileInfo> Characters = new List<FileInfo>();

            for (int i = 0; i < Images.Length; i++)
            {
                MoveImage(Images[i], ProjectFolder);

            }

            List<DirectoryInfo> ContainedFolders = RXGCFolder.GetDirectories().ToList();
            foreach (DirectoryInfo directory in ContainedFolders)
            {
                List<FileInfo> innerCharacters = new List<FileInfo>();
                innerCharacters = directory.GetFiles().ToList();
                foreach (FileInfo file in innerCharacters)
                {
                    MoveImage(file, ProjectFolder);
                }
            }

        }
        private void MoveImage(FileInfo file, DirectoryInfo ProjectFolder)
        {
            if (file.Name.Contains("Character.png"))
            {
                string DestinationFile = ProjectFolder + @"\img\Characters\" + file.Name;
                if (File.Exists(DestinationFile)) File.Delete(DestinationFile);
                file.MoveTo(DestinationFile);
            }
            else if (file.Name.Contains(".png"))
            {
                string DestinationFile = ProjectFolder + @"\img\Faces\" + file.Name;
                if (File.Exists(DestinationFile)) File.Delete(DestinationFile);
                file.MoveTo(DestinationFile);
            }
        }
    }
}
