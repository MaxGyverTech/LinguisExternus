using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinguisExternusDictionary
{
    public class DictionaryException : Exception { }
    public class WrongLoginException : DictionaryException { }
    public class WrongPasswordException : DictionaryException { }
    public class UserAlreadyExistsException : DictionaryException { }
}
