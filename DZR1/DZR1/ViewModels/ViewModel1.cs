using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Http;
using Newtonsoft.Json;
using System.Collections.ObjectModel;
using Xamarin.Forms;
using DZR1.Views;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Input;

namespace DZR1.ViewModels
{
    public class SomeJson
    {
        public int userId { get; set; }
        public int id { get; set; }
        public string title { get; set; }
        public bool completed { get; set; }
    }
    public partial class ViewModel1 : INotifyPropertyChanged
    {
        const string url = "https://jsonplaceholder.typicode.com/todos";
        HttpClient client = new HttpClient();
        private ObservableCollection<SomeJson> _someJsons;

        public ObservableCollection<SomeJson> SomeJsons { get => _someJsons; set { _someJsons = value; OnPropertyChanged(); } }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public async Task Output()
        {
            var content = await client.GetStringAsync(url);
            var someJsons = JsonConvert.DeserializeObject<List<SomeJson>>(content);
            SomeJsons = new ObservableCollection<SomeJson>(someJsons);
        }
        public ICommand LoadData => new Command(async value =>
        {
            await Output();
        });
    }
}
