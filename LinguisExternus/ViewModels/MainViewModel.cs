using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using LinguisExternus.Models;
using LinguisExternus.Views;
using LinguisExternusDictionary;
using LinguisExternusDictionary.Models;

namespace LinguisExternus.ViewModels
{
    public class MainViewModel : NotifyProperty
    {
        Dictionary dict;
        string search = "";

        Word trainWord;
        string trainString;
        int trainTotal;
        List<Word> trainWords;
        static Random rnd = new Random();
        public string TrainInput { get; set; }
        public string DayTip { get; } = Dictionary.Phrases[rnd.Next(Dictionary.Phrases.Count)];
        public MainViewModel(Dictionary dict) 
        { 
            this.dict = dict;
            AddWordCommand = new RelayCommand(AddWord);
            DeleteWordCommand = new RelayCommand(DeleteWord, o=>SelectedWord!=null);
            LogoutCommand = new RelayCommand(Logout);
            CheckFavoriteCommand = new RelayCommand(CheckFavorite);
            TrainCommand = new RelayCommand(Train);
        }
        public ICommand AddWordCommand { get; set; }
        public ICommand DeleteWordCommand { get; set; }
        public ICommand LogoutCommand { get; set; }
        public ICommand CheckFavoriteCommand { get; set; }
        public ICommand TrainCommand { get; set; }
        public Word SelectedWord { get; set; }
        public ObservableCollection<Word> AllWords { get => dict.GetWords(personal:false,search:search); }
        public ObservableCollection<Word> MyWords { get => dict.GetWords(personal:true, search:search); }
        public ObservableCollection<Favorite> Favorites { get => dict.GetFavorites(); }
        void Update()
        {
            OnPropertyChanged(nameof(AllWords));
            OnPropertyChanged(nameof(MyWords));
            OnPropertyChanged(nameof(Favorites));
        }
        void AddWord(object obj)
        {
            var word = new Word();
            if (new WordWindow(word).ShowDialog() == false) return;
            dict.AddWord(word, personal: true);
            Update();
        }
        void DeleteWord(object obj)
        {
            dict.DeleteWord(SelectedWord);
            Update();
        }
        void CheckFavorite(object obj)
        {
            var word = obj is Word? (Word)obj : (obj as Favorite).Word;
            if (obj is Word ^ !word.IsFavorite) dict.AddFavorite(word);
            else dict.DeleteFavorite(word);
            Update();
        }
        void Train(object obj)
        {
            TrainString = "";
            if (trainWord == null)
            {
                if (dict.GetFavorites().Count == 0)
                {
                    MessageBox.Show("У вас нет избранных слов, добавьте их, прежде чем начать тренировку");
                    return;
                }
                trainWords = new();
                foreach (var fav in dict.GetFavorites())
                {
                    trainWords.Add(fav.Word);
                    trainWords.Add(new() { Russian=fav.Word.Latin, Latin=fav.Word.Russian });
                }
                trainTotal = trainWords.Count;
            } 
            else
            {
                if (TrainWord.Latin == TrainInput)
                {
                    trainWords.Remove(TrainWord);
                    TrainString += "Верно\n";
                }
                else TrainString += $"Неправильно, правильный перевод для {trainWord.Russian} это {trainWord.Latin}\n";
                if (trainWords.Count <= 0) 
                {
                    TrainWord = null;
                    MessageBox.Show("Вы всё выучили"); 
                    return;
                }
            }
            TrainString += $"Слово {trainTotal - trainWords.Count + 1}/{trainTotal}";
            TrainWord = trainWords[rnd.Next(trainWords.Count)];
        }
        void Logout(object obj)
        {
            var login = new LoginWindow();
            login.Show();
            App.Current.MainWindow.Close();
            App.Current.MainWindow = login;
        }
        public string Search
        {
            get { return search; }
            set
            {
                search = value;
                OnPropertyChanged(nameof(Search));
                OnPropertyChanged(nameof(AllWords));
                OnPropertyChanged(nameof(MyWords));
            }
        }
        public Word TrainWord
        {
            get => trainWord;
            set
            {
                trainWord = value;
                OnPropertyChanged(nameof(TrainWord));
            }
        }
        public string TrainString
        {
            get => trainString;
            set
            {
                trainString = value;
                OnPropertyChanged(nameof(TrainString));
            }
        }
    }
}
