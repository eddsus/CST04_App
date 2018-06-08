namespace WPFDashboard.ViewModel.DetailViewModels
{
    public class IngredientTestVm
    {
        public IngredientTestVm(string name, string type, int amount)
        {
            Name = name;
            Type = type;
            Amount = amount;
        }

        public string  Name { get; set; }
        public string Type { get; set; }
        public int Amount { get; set; }

    }
}