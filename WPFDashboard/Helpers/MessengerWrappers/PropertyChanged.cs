using SharedDataTypes;

namespace WPFDashboard.ViewModel.ViewModelMenu
{
    //Transporter for changed properties used by Messanger
    public class PropertyChanged<T>
    {
        public T ChangedProperty { get; }
        public string Message { get;  }
        public int Timer { get;  }



        public PropertyChanged(T value)
        {
            ChangedProperty = value;
        }
        public PropertyChanged(T value, string msg, int i)
        {
            ChangedProperty = value;
            Message = msg;
            Timer = i;
        }
    }
}