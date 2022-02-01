using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CWA.Util
{
    public class RetornoSSOJC
    {
        string _code;
        string _access_token;
        string _refresh_token;

        public string code
        {
            get { return _code; }
            set { _code = value; }
        }

        public string access_token
        {
            get { return _access_token; }
            set { _access_token = value; }
        }

        public string refresh_token
        {
            get { return _refresh_token; }
            set { _refresh_token = value; }
        }

        public profile profile { get; set; }
        public List<userdata> userdata { get; set; }
    }
    public class profile
    {

        string _sub;
        string _email_verified;
        string _preferred_username;
        string _given_name;
        string _family_name;
        string _email;
        string _firstName;
        string _lastName;
        string _fullName;

        public string sub
        {
            get { return _sub; }
            set { _sub = value; }
        }
        public string email_verified
        {
            get { return _email_verified; }
            set { _email_verified = value; }
        }
        public string preferred_username
        {
            get { return _preferred_username; }
            set { _preferred_username = value; }
        }
        public string given_name
        {
            get { return _given_name; }
            set { _given_name = value; }
        }
        public string family_name
        {
            get { return _family_name; }
            set { _family_name = value; }
        }
        public string email
        {
            get { return _email; }
            set { _email = value; }
        }
        public string firstName
        {
            get { return _firstName; }
            set { _firstName = value; }
        }
        public string lastName
        {
            get { return _lastName; }
            set { _lastName = value; }
        }
        public string fullName
        {
            get { return _fullName; }
            set { _fullName = value; }
        }

    }
    public class userdata
    {

        string _NU_CPF;
        string _CD_CONTABIL_PESSOA;
        string _DS_EMAIL;

        public string NU_CPF
        {
            get { return _NU_CPF; }
            set { _NU_CPF = value; }
        }

        public string CD_CONTABIL_PESSOA
        {
            get { return _CD_CONTABIL_PESSOA; }
            set { _CD_CONTABIL_PESSOA = value; }
        }

        public string DS_EMAIL
        {
            get { return _DS_EMAIL; }
            set { _DS_EMAIL = value; }
        }

    }
}
