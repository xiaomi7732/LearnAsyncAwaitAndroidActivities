using AndroidX.Lifecycle;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace LearnAsyncExample
{
    public class TextViewModel : ViewModel, INotifyPropertyChanged
    {
        private string _textValue = "Press the button to load data ...";
        public string TextValue
        {
            get { return _textValue; }
            private set
            {
                if (value != _textValue)
                {
                    _textValue = value;
                    RaisePropertyChange();
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public async Task LoadDataAsync()
        {
            TextValue = "Loading data  .... (5s)";
            await Task.Delay(TimeSpan.FromSeconds(5));
            TextValue = "Data is loaded!";
        }

        private void RaisePropertyChange([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}