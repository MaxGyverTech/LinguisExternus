using LinguisExternus.Models;
using LinguisExternusDictionary;
using LinguisExternusDictionary.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace LinguisExternus.ViewModels
{
    public class LoginViewModel : NotifyProperty
    {
        bool isLoggining = true;
        string errorString = "";
        //public User User { get; set; } = new User() { Name="Test user",  Email="example@main.com", Password = "12345678", Password2 = "12345678" };
        public User User { get; set; } = new User() { Name="",  Email="", Password = "", Password2 = "" };
        public ICommand LoginCommand { get; set; }
        public ICommand SingUpCommand { get; set; }
        public ICommand SwitchScreenCommand { get; set; }

        public LoginViewModel() 
        {
            Database.getInstance();
            LoginCommand = new RelayCommand(Login);
            SingUpCommand = new RelayCommand(SingUp);
            SwitchScreenCommand = new RelayCommand(SwitchScreens);
        }
        
        void SwitchScreens(object obj) => IsLogining = !IsLogining;
        void Login(object obj)
        {
            try
            {
                var dict = Dictionary.Login(User.Email, User.Password);
                var main = new MainWindow() { DataContext = new MainViewModel(dict) };
                main.Show();
                App.Current.MainWindow.Close();
                App.Current.MainWindow = main;
            }
            catch (WrongLoginException)
            {
                ErrorString = "Пользователь не найден";
            }
            catch (WrongPasswordException)
            {
                ErrorString = "Пароль не верный";
            }
            
        }
        void SingUp(object obj)
        {
            ErrorString = "";
            if (!new EmailAddressAttribute().IsValid(User.Email)) ErrorString += "Некорректный email\n";
            if (User.Name.Length < 2) ErrorString += "Слишком короткое имя\n";
            if (User.Password.Length < 8) ErrorString += "Слишком короткий пароль\n";
            if (User.Password != User.Password2) ErrorString += "Пароли не совпадают\n";
            ErrorString = ErrorString.TrimEnd('\n');
            if (ErrorString=="")
            {
                try
                {
                    var dict = Dictionary.SingUp(User.Name, User.Email, User.Password);
                    var main = new MainWindow() { DataContext = new MainViewModel(dict) };
                    main.Show();
                    App.Current.MainWindow.Close();
                    App.Current.MainWindow = main;
                }
                catch (UserAlreadyExistsException)
                {
                    ErrorString = "Пользователь уже сущестует";
                }
            }
        }
        public bool IsLogining
        {
            get { return isLoggining; }
            set
            {
                isLoggining = value;
                OnPropertyChanged(nameof(IsLogining));
            }
        }
        public string ErrorString
        {
            get => errorString;
            set
            {
                errorString = value;
                OnPropertyChanged(nameof(ErrorString));
            }
        }
    }
}
