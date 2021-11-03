using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace instagrambot
{
    class bilgiler
    {
        string username = "Sizin kullanıcı adınız.";
        string sifre = "Sizin Şifreniz";

        public string getpassword()
        {
           return this.sifre;
        }
        public string getusername()
        {
            return this.username;
        }
        
    }
}
