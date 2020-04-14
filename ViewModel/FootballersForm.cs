namespace Footballers.ViewModel
{
    using System.ComponentModel;
    using System.IO;
    using System.Text.Json;
    using System.Windows.Input;

    using Footballers.Model;
    using Footballers.ViewModel.BaseClass;

    internal class FootballersForm : ViewModelBase
    {
        #region prop

        private string dataPath = "DataFootballers.json";
        private double? age = 25;

        private string forename = null;

        private string surname = null;

        private double? weight = 80;

        public double? Age
        {
            get => age; set
            {
                age = value;
                OnPropertyChanged(nameof(Age));
            }
        }

        public string Forename
        {
            get => forename; set
            {
                forename = value;
                OnPropertyChanged(nameof(Forename));
            }
        }

        private Footballer selectedFootballer = null;

        public Footballer SelectedFootballer
        {
            get => selectedFootballer; set
            {
                selectedFootballer = value;
                OnPropertyChanged(nameof(SelectedFootballer));
                if (Copy.CanExecute(null)) Copy.Execute(null);
            }
        }

        private BindingList<Footballer> storedFootballers = new BindingList<Footballer>();

        public BindingList<Footballer> StoredFootballers
        {
            get => storedFootballers; set
            {
                storedFootballers = value;
                OnPropertyChanged(nameof(StoredFootballers));
            }
        }

        public string Surname
        {
            get => surname; set
            {
                surname = value;
                OnPropertyChanged(nameof(Surname));
            }
        }

        public double? Weight
        {
            get => weight; set
            {
                weight = value;
                OnPropertyChanged(nameof(Weight));
            }
        }

        #endregion prop

        #region commands

        private ICommand add;

        private ICommand clear;

        private ICommand copy;

        private ICommand delete;

        private ICommand edit;

        private ICommand loadData;

        private ICommand saveData;

        public ICommand LoadData
        {
            get
            {
                if (loadData is null)
                {
                    loadData = new RelayCommand(execute =>
                    {
                        var jsonFootballers = File.ReadAllText(dataPath);
                        StoredFootballers = JsonSerializer.Deserialize<BindingList<Footballer>>(jsonFootballers);
                        OnPropertyChanged(nameof(LoadData));
                        StoredFootballers.ResetBindings();
                    }, canExecute => File.Exists(dataPath) && (new FileInfo(dataPath).Length > 0));
                }
                return loadData;
            }
        }

        public ICommand SaveData
        {
            get
            {
                if (saveData is null)
                {
                    saveData = new RelayCommand(execute =>
                    {
                        var jsonFootballers = JsonSerializer.Serialize(StoredFootballers);
                        File.WriteAllText(dataPath, jsonFootballers);
                        OnPropertyChanged(nameof(SaveData));
                    }, canExecute => true);
                }
                return saveData;
            }
        }

        public ICommand Add
        {
            get
            {
                if (add is null)
                {
                    add = new RelayCommand(
                        execute =>
                        {
                            var footballer = new Footballer(Forename, Surname, (double)Age, (double)Weight);
                            if (!StoredFootballers.Contains(footballer))
                            {
                                StoredFootballers.Add(footballer);
                                OnPropertyChanged(nameof(StoredFootballers));
                                Clear.Execute(null);

                            }
                        }
                        , canExecute => FieldsNotNull
                    );
                }
                return add;
            }
        }

        public ICommand Clear
        {
            get
            {
                if (clear is null)
                {
                    clear = new RelayCommand(
                        execute =>
                        {
                            Forename = Surname = null;
                            Weight = Age = null;
                        }
                        , canExecute => true
                    );
                }
                return clear;
            }
        }

        public ICommand Copy
        {
            get
            {
                if (copy is null)
                {
                    copy = new RelayCommand(
                        execute =>
                        {
                            Forename = SelectedFootballer.Forename;
                            Surname = SelectedFootballer.Surname;
                            Age = SelectedFootballer.Age;
                            Weight = SelectedFootballer.Weight;
                        }
                        , canExecute => SelectedFootballer != null
                    );
                }
                return copy;
            }
        }

        public ICommand Delete
        {
            get
            {
                if (delete is null)
                {
                    delete = new RelayCommand(execute =>
                    {
                        var footballer = new Footballer(Forename, Surname, (double)Age, (double)Weight);
                        if (StoredFootballers.Contains(footballer))
                        {
                            StoredFootballers.Remove(footballer);
                            OnPropertyChanged(nameof(StoredFootballers));
                        }
                    }, canExecute => FieldsNotNull && SelectedFootballer != null);
                }
                return delete;
            }
        }

        public ICommand Edit
        {
            get
            {
                if (edit is null)
                {
                    edit = new RelayCommand(execute =>
                    {
                        var newFootballer = new Footballer(Forename, Surname, (double)Age, (double)Weight);
                        if (StoredFootballers.Contains(SelectedFootballer))
                        {
                            var index = StoredFootballers.IndexOf(selectedFootballer);
                            StoredFootballers[index].Copy(newFootballer);
                            StoredFootballers.ResetItem(index);
                            Clear.Execute(null);

                        }
                    }, canExecute => FieldsNotNull && SelectedFootballer != null);
                }
                return edit;
            }
        }

        #endregion commands

        public FootballersForm()
        {
        }

        private bool FieldsNotNull { get { return (!string.IsNullOrEmpty(Forename) && !string.IsNullOrEmpty(Surname) && Age > 0 && Weight > 0); } }
    }
}