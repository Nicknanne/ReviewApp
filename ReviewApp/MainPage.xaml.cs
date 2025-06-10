using System.Diagnostics;

namespace ReviewApp
{
    public partial class MainPage : ContentPage
    {
        int count = 0;
        private readonly List<int> _myList;

        public MainPage(List<int> myList)
        {
            _myList = myList;
            InitializeComponent();
        }

        private void OnCounterClicked(object sender, EventArgs e)
        {
            Debug.WriteLine(string.Join(", ", _myList));

            count++;

            if (count == 1)
                CounterBtn.Text = $"Clicked {count} time";
            else
                CounterBtn.Text = $"Clicked {count} times";

            SemanticScreenReader.Announce(CounterBtn.Text);
        }
    }

}
