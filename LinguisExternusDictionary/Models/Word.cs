using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinguisExternusDictionary.Models
{
    public class Word : NotifyProperty
    {
        int id;
        string russian;
        string latin;
        User? user;
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
        public string Russian
        {
            get { return russian; }
            set
            {
                russian = value;
                OnPropertyChanged(nameof(Russian));
            }
        }
        public string Latin
        {
            get { return latin; }
            set
            {
                latin = value;
                OnPropertyChanged(nameof(Latin));
            }
        }
        public User? User
        {
            get { return user; }
            set
            {
                user = value;
                OnPropertyChanged(nameof(User));
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

        [NotMapped]
        public bool IsFavorite { get; set; }
    }
}
