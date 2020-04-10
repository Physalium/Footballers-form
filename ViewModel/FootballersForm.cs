using System.Collections.Generic;

namespace Footballers.ViewModel
{
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Windows.Input;

    using Footballers.Model;
    using Footballers.ViewModel.BaseClass;

    internal class FootballersForm : ViewModelBase
    {
        #region prop

        private double? age = null;

        private string forename = null;

        private string surname = null;

        private double? weight = null;

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

        public Footballer SelectedFootballer { get; set; } = null;
        public ObservableCollection<Footballer> StoredFootballers { get; set;} = new ObservableCollection<Footballer>() { new Footballer("janek", "kowalski", 60, 60) };
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
                        , canExecute => true
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
                            StoredFootballers.Single(footballer => footballer.Equals(SelectedFootballer)).Copy(newFootballer);
                            OnPropertyChanged(nameof(StoredFootballers));
                        }
                    }, canExecute => FieldsNotNull && SelectedFootballer != null);
                }
                return edit;
            }
        }

        #endregion commands

        public FootballersForm()
        {
            //wczytywanie z json
        }

        private bool FieldsNotNull { get { return (!string.IsNullOrEmpty(Forename) && !string.IsNullOrEmpty(Surname) && Age > 0 && Weight > 0); } }
    }
}