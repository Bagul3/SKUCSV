using CSV.Repo;
using CSV.Services;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CSV.ViewModel
{
    public class MainWindowViewModel : BindableBase
    {
        private ImageService _imageService;

        private string _path = @"D:\Users\Conor\My Pictures";
        public string Path
        {
            get { return _path; }
            set { SetProperty(ref _path, value); }
        }

        private string _databaseConnectionString = @"D:\Users\Conor\ContractWork\Data\hq.mdb";
        public string Connection
        {
            get { return _path; }
            set { SetProperty(ref _path, value); }
        }

        private string _consoleOutput = "";
        public string ConsoleOutput
        {
            get { return _consoleOutput; }
            set { SetProperty(ref _consoleOutput, value); }
        }

        public ICommand GenerateCSV { get; set; }

        public MainWindowViewModel()
        {
            _imageService = new ImageService();
            GenerateCSV = new DelegateCommand(Execute, CanExecute);
        }

        private bool CanExecute()
        {
            return !string.IsNullOrEmpty(_path);
        }

        private void Execute()
        {
            ConsoleOutput += "Generating CSV File...";
            ConsoleOutput += "";
            var imageDetails = _imageService.ReadImageDetails(_path);
            var db = new Database(_databaseConnectionString);
            if(imageDetails == null)
            {
                ConsoleOutput +="Error: Please check your image path";
                ConsoleOutput += "";
            }
            foreach (string image in imageDetails)
                ConsoleOutput += string.Format(db.QueryAndBuild(image), Environment.NewLine);
        }
    }
}
