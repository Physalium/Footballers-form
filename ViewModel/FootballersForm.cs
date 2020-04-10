using System.Collections.Generic;

namespace Footballers.ViewModel
{
    using Footballers.Model;
    using Footballers.ViewModel.BaseClass;
    using System.Windows.Input;

    internal class FootballersForm : ViewModelBase
    {
        #region prop

        public List<Footballer> storedFootballers = new List<Footballer>();
        public Footballer selectedFootballer { get; set; }
        public string Forename { get; set; }
        public string Surname { get; set; }
        public double? Age { get; set; }
        public double? Weight { get; set; }

        #endregion prop

        #region commands

        private ICommand add;

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
                            if (!storedFootballers.Contains(footballer))
                            {
                                storedFootballers.Add(footballer);
                                OnPropertyChanged(nameof(storedFootballers));
                            }
                        }
                        , canExecute => FieldsNotNull
                    );
                }
                return add;
            }
        }

        private ICommand delete;

        public ICommand Delete
        {
            get
            {
                if (delete is null)
                {
                    delete = new RelayCommand(execute =>
                    {
                        var footballer = new Footballer(Forename, Surname, (double)Age, (double)Weight);
                        if (storedFootballers.Contains(footballer))
                        {
                            storedFootballers.Remove(footballer);
                            OnPropertyChanged(nameof(storedFootballers));
                        }
                    }, canExecute => FieldsNotNull && selectedFootballer != null);
                }
                return delete;
            }
        }

        private ICommand edit;

        public ICommand Edit
        {
            get
            {
                if (edit is null)
                {
                    edit = new RelayCommand(execute =>
                    {
                        var newFootballer = new Footballer(Forename, Surname, (double)Age, (double)Weight);
                        if (storedFootballers.Contains(selectedFootballer))
                        {
                            storedFootballers.Find(footballer => footballer.Equals(selectedFootballer)).Copy(newFootballer);
                            OnPropertyChanged(nameof(storedFootballers));
                        }
                    }, canExecute => FieldsNotNull && selectedFootballer != null);
                }
                return edit;
            }
        }
        private ICommand clear;
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

        private ICommand copy;
        public ICommand Copy
        {
            get
            {
                if (copy is null)
                {
                    copy = new RelayCommand(
                        execute =>
                        {
                            Forename = selectedFootballer.Forename;
                            Surname = selectedFootballer.Surname;
                            Age = selectedFootballer.Age;
                            Weight = selectedFootballer.Weight;
                        }
                        , canExecute => true
                    );
                }
                return copy;
            }
        }

        #endregion commands

        private bool FieldsNotNull { get { return (Forename != null && Surname != null && Age != null && Weight != null); } }

        public FootballersForm()
        {
            //wczytywanie z json
        }
    }
}