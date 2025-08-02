
using CentralTask.Domain.Entidades.Base;
using System;
using System.Collections.Generic;

namespace CentralTask.Domain.Entidades
{
    public class Users : Entidade
    {
        public Users() { }

        public Users(string username, string email, string passwordhash, DateTime createdat)
        {
        Username = username;
        Email = email;
        Passwordhash = passwordhash;
        Createdat = createdat;
        }

    public string Username { get; set; }
    public string Email { get; set; }
    public string Passwordhash { get; set; }
    public DateTime Createdat { get; set; }
    }
}