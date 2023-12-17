using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinguisExternusDictionary.Models
{
    public class User : NotifyProperty
    {
        int id;
        string name;
        string email;
        string password;
        string password2;
        List<Word> words;
        List<Favorite> favorites;


        public int Id
        {
            get { return id; }
            set
            {
                id = value;
                OnPropertyChanged(nameof(Id));
            }
        }
        public string Name
        {
            get { return name; }
            set
            {
                name = value;
                OnPropertyChanged(nameof(Name));
            }
        }
        public string Email
        {
            get { return email; }
            set
            {
                email = value;
                OnPropertyChanged(nameof(Email));
            }
        }
        public string Password
        {
            get { return password; }
            set
            {
                password = value;
                OnPropertyChanged(nameof(Password));
            }
        }
        [NotMapped]
        public string Password2
        {
            get { return password2; }
            set
            {
                password2 = value;
                OnPropertyChanged(nameof(Password2));
            }
        }
        public List<Word> Words
        {
            get { return words; }
            set
            {
                words = value;
                OnPropertyChanged(nameof(Words));
            }
        }
        public List<Favorite> Favorites
        {
            get { return favorites; }
            set
            {
                favorites = value;
                OnPropertyChanged(nameof(Favorites));
            }
        }
    }
}
