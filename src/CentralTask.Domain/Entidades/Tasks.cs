
using CentralTask.Domain.Entidades.Base;
using System;
using System.Collections.Generic;

namespace CentralTask.Domain.Entidades
{
    public class Tasks : Entidade
    {
        public Tasks() { }

        public Tasks(string title, string description, DateTime duedate, Guid userid, DateTime createdat, Users users)
        {
        Title = title;
        Description = description;
        Duedate = duedate;
        Userid = userid;
        Createdat = createdat;
        Users = users;
        }

    public string Title { get; set; }
    public string Description { get; set; }
    public DateTime Duedate { get; set; }
    public Guid Userid { get; set; }
    public DateTime Createdat { get; set; }
    public Users Users { get; set; }
    }
}