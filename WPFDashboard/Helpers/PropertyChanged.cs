using SharedDataTypes;

namespace WPFDashboard.ViewModel.ViewModelMenu
{
    //Transporter for changed properties used by Messanger
    public class PropertyChanged<T>
    {
        public T ChangedProperty { get; }

        public PropertyChanged(T value)
        {
            ChangedProperty = value;
        }
    }
}