using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinguisExternusDictionary.Models
{
    public class Favorite : NotifyProperty
    {
        int id;
        User user;
        Word word;

        public int Id
        {
            get { return id; }
            set 
            {
                id = value;
                OnPropertyChanged(nameof(Id));
            }
        }
        public User User
        {
            get { return user; }
            set
            {
                user = value;
                OnPropertyChanged(nameof(User));
            }
        }
        public Word Word
        {
            get { return word; }
            set
            {
                word = value;
                OnPropertyChanged(nameof(Word));
            }
        }
    }
}
