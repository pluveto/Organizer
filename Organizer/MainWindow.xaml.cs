using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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

namespace Organizer
{

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        MainWindowViewModel ViewModel;
        public MainWindow()
        {
            InitializeComponent();
            ViewModel = new MainWindowViewModel();
            this.DataContext = ViewModel;
            this.ViewModel.PropertyChanged += (sender, args) =>
            {
                if(args.PropertyName == "OutputDirectory")
                {
                    this.Title = this.ViewModel.OutputDirectory;
                }
            };
        }

        private void TextBox_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var dialog = new Ookii.Dialogs.Wpf.VistaFolderBrowserDialog();
            if (!(bool)dialog.ShowDialog()) return;
            this.ViewModel.OutputDirectory = dialog.SelectedPath;

        }

        private void DataGrid_Drop(object sender, DragEventArgs e)
        {
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
            if (files == null) { return; }
            for (int i = 0; i < files.Length; i++)
            {
                var file = files[i];
                if(!System.IO.File.Exists(file))
                {
                    continue;
                }
                var fileInfo = new FileInfo(file);
                this.ViewModel.FileInfoList.AddUnique(new FileInfoListItem
                {
                    FullPath = file,
                    FileName = fileInfo.Name,
                    Created = fileInfo.CreationTime,
                    FileInfo = fileInfo
                }) ;
            }
        }

        private void ExecuteButton_Click(object sender, RoutedEventArgs e)
        {
            var outputDir = this.ViewModel.OutputDirectory;
            if (!Directory.Exists(outputDir))
            {
                MessageBox.Show("Please select valid directory.");
                return;
            }
            for (int i = 0; i < this.ViewModel.FileInfoList.Count; )
            {
                var current = this.ViewModel.FileInfoList[i];
                var folder = GetFolderNameByType(this.ViewModel.SortType, current.FileInfo);
                if (folder == null)
                {
                    MessageBox.Show("Please choose way of sorting.");
                    return;
                }
                var destination = outputDir + "/" + folder;
                Directory.CreateDirectory(destination);
                try
                {
                    File.Move(current.FullPath, destination + "/" + current.FileName);
                    this.ViewModel.FileInfoList.RemoveAt(i);                    
                }
                catch (Exception)
                {
                    i++;
                }
                
            }
            System.Diagnostics.Process.Start("explorer", outputDir);
        }

        private string GetFolderNameByType(SortType sortType, FileInfo fileInfo)
        {
            switch (sortType)
            {
                case SortType.YearMonth:
                    return fileInfo.CreationTime.ToString("yyyy-MM");
                case SortType.YearMonthF:
                    return fileInfo.CreationTime.ToString("yyyy-mm");
                default:
                    return null;
            }            
        }
    }
    public enum SortType
    {
        YearMonth,
        YearMonthF
    }

    public class FileInfoListItem : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public FileInfo FileInfo { get; set; }
        public string FileName { get; set; }
        public string FullPath { get; set; }
        public DateTime Created { get; set; }
    }

    public class MainWindowViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        static PropertyChangedEventArgs OutputDirectoryChanged = new PropertyChangedEventArgs("OutputDirectory");

        public ObservableCollection<FileInfoListItem> FileInfoList { get; set; } = new ObservableCollection<FileInfoListItem>();



        string outputDirectory;
        public string OutputDirectory
        {
            get => outputDirectory;
            set
            {
                if (value != outputDirectory)
                {
                    outputDirectory = value;
                    PropertyChanged?.Invoke(this, OutputDirectoryChanged);
                }
            }
        }

        public SortType SortType { get; set; }
    }
    public class EnumBooleanConverter : IValueConverter
    {
        #region IValueConverter Members
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            string parameterString = parameter as string;
            if (parameterString == null)
                return DependencyProperty.UnsetValue;

            if (Enum.IsDefined(value.GetType(), value) == false)
                return DependencyProperty.UnsetValue;

            object parameterValue = Enum.Parse(value.GetType(), parameterString);

            return parameterValue.Equals(value);
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            string parameterString = parameter as string;
            if (parameterString == null)
                return DependencyProperty.UnsetValue;

            return Enum.Parse(targetType, parameterString);
        }
        #endregion
    }

    public static class ExtensionTool
    {
        public static void AddUnique(this ObservableCollection<FileInfoListItem> list, FileInfoListItem item )
        {
            lock (list)
            {
                var found = false;
                foreach (var el in list)
                {
                    if(el.FullPath == item.FullPath)
                    {
                        found = true;
                    }
                }
                if (found) return;
                list.Add(item);
            }
        }
    }
}
