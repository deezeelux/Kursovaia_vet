using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Kursovaiy_vet_DTO.Models
{
    public class ApplicationContext : DbContext
    {
        //Объявление переменной Pets, через которую общаемся к таблице БД
        public DbSet<Pet> Pets { get; set; }

        //Конструктор класса с целью создания (пересоздания) таблицы БД
        public ApplicationContext()
        {
            /*Database.EnsureDeleted();*/
            Database.EnsureCreated();
        }

        //Установка строки подключения к БД
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySql(
                "server=localhost;user=root;password=root;database=usersdb5;",
                new MySqlServerVersion(new Version(8, 0, 11))
            );
        }
    }
}
