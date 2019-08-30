namespace WatchEat.Models.Database
{
    public class Activity : EntityBase
    {
        string _name = string.Empty;
        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                OnPropertyChanged(nameof(Name));
            }
        }

        decimal _calories = default(decimal);
        public decimal Calories
        {
            get { return _calories; }
            set
            {
                _calories = value;
                OnPropertyChanged(nameof(Calories));
            }
        }
    }
}
