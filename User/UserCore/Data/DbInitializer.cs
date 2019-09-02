using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserCore.Models;

namespace UserCore.Data
{
    public class DbInitializer
    {
        public static void Initialize(SchoolContext context)
        {
            context.Database.EnsureCreated();
            if (context.Oldwhite.Any())
            {
                return;
            }

            var oldwhites = new Oldwhite[]
            {
                new Oldwhite{ ip="127.0.0.1", logname="test1" },
                new Oldwhite{ ip="0.0.0.1",logname="TEST2"},
                new Oldwhite{ ip="19.18.0.15",logname="testgg"},
            };
            foreach (var white in oldwhites)
            {
                context.Oldwhite.Add(white);
            }
            context.SaveChanges();

        }
    }
}
