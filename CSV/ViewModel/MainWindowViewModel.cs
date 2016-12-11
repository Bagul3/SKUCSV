using CSV.Repo;
using CSV.Services;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CSV.ViewModel
{
    public class MainWindowViewModel : BindableBase
    {
        private ImageService _imageService;
        private string _path = @"C:\Users\VM\Pictures";
        public string Path
        {
            get { return _path; }
            set { SetProperty(ref _path, value); }
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
           //_imageService.ReadImageDetails(_path)[0];
            Database db = new Database();
            db.QueryAndBuild();
        }
    }
}
