namespace WPFDashboard.ViewModel.DetailViewModels
{
    public class ChocolateTestVm
    {
        public ChocolateTestVm(string name, int amount)
        {
            Name = name;
            Amount = amount;
        }

        public string Name { get; set; }
        public int Amount { get; set; }
    }
}