using LinguisExternusDictionary.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace LinguisExternusDictionary
{
    public class Dictionary
    {
        User user;
        public User User { get => user; }
        static Database db = Database.getInstance();
        public Dictionary(User user) 
        { 
            this.user = user;
        }
        public static Dictionary SingUp(string name, string email, string password)
        {
            if (db.Users.FirstOrDefault(x => x.Email == email) != null)
                throw new UserAlreadyExistsException();
            using SHA256 hash = SHA256.Create();
            var user = new User()
            {
                Name = name,
                Email = email,
                Password = Convert.ToHexString(hash.ComputeHash(Encoding.ASCII.GetBytes(password)))
            };
            db.Users.Add(user);
            db.SaveChanges();
            return new Dictionary(user);
        }
        public static Dictionary Login(string email,  string password)
        {
            if (db.Users.FirstOrDefault(x => x.Email == email) == null)
                throw new WrongLoginException();
            using SHA256 hash = SHA256.Create();
            var user = db.Users.FirstOrDefault(x => x.Email == email && x.Password == Convert.ToHexString(hash.ComputeHash(Encoding.ASCII.GetBytes(password))));
            if (user == null)
                throw new WrongPasswordException();
            return new Dictionary(user);
        }
        public void AddWord(Word word, bool personal = false)
        {
            if (personal)
            {
                db.Favorites.Add(new Favorite { User = user, Word = word });
                word.User = user;    
            }
            db.Words.Add(word);

            db.SaveChanges();
        }
        public void DeleteWord(Word word) 
        { 
            db.Words.Remove(word);
            db.SaveChanges();
        }
        public void AddFavorite(Word word)
        {
            var favorite = new Favorite() { User = user, Word = word };
            db.Favorites.Add(favorite);
            db.SaveChanges();
        }
        public void DeleteFavorite(Word word)
        {
            DeleteFavorite(db.Favorites.FirstOrDefault(x => x.Word == word && x.User == user));
        }
        public void DeleteFavorite(Favorite favorite)
        {
            db.Favorites.Remove(favorite);
            db.SaveChanges();
        }
        public ObservableCollection<Word> GetWords(bool personal = false, string search = "")
        {
            var res = db.Words.Where(personal ? x => x.User == user : x => x.User == user || x.User == null).Where(x => x.Russian.Contains(search) || x.Latin.Contains(search)).ToList();
            foreach (var word in res)
                word.IsFavorite = db.Favorites.FirstOrDefault(x => x.User == user && x.Word == word) != null;
            return new(res);
        }
        public ObservableCollection<Favorite> GetFavorites()
        {
            return new(db.Favorites.Where(x => x.User == user));
        }
        
        public static List<string> Phrases = new() {
            "Medicamenta heroica in manu imperiti sunt, ut gladius in dextra furiosi — Сильнодействующие лекарства в руке неопытного, как меч в правой руке безумного",
            "Arte et humanitate, labore et scientia — Искусством и человечностью, трудом и знанием!",
            "Edimus, ut vivamus, non vivimus, ut edamus — Мы едим для того, чтобы жить, а не живем для того, чтобы есть.",
            "Non est vivere, sed valere vita — Жизнь не в том, чтобы существовать, а в том, чтобы быть сильным."
        };
    }
}
