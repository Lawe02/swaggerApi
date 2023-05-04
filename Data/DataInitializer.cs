using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using api.Models;

namespace api.Data
{
    public class DataInitializer
    {
        private readonly ApplicationDbContext _dbContext;

        public DataInitializer(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }       

        public void MigrateData()
        {
            _dbContext.Database.Migrate();
            SeedData();
        }

        private void SeedData()
        {
            if(!_dbContext.AdContext.Any(x => x.Id == 1)){

                _dbContext.AdContext.Add(new Ads (){
                    Price = 150,
                    StartDate = DateTime.Now,
                });
            }
            if(!_dbContext.AdContext.Any(x => x.Id == 2)){

                _dbContext.AdContext.Add(new Ads (){
                    Price = 390,
                    StartDate = DateTime.Now.AddDays(3),
                    Description = "Good product"
                });
            }
            _dbContext.SaveChanges();
        }
    }
}