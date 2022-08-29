using System.ComponentModel;

namespace BackEnd
{
    public class Storage : INotifyPropertyChanged
    {
        private string _text = string.Empty;

        public event PropertyChangedEventHandler? PropertyChanged = (sender, e) => { };

        public string Text
        {
            get => this._text;

            set
            {
                if (this._text == value)
                {
                    return;
                }

                this._text = value;

                this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Text"));
            }
        }
    }
}