using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Asp.NetMVCCodeFirst.Models.Managers
{
    //DataBase işlemleri yapmak için, DbContext sınıfından miras alınır.
    public class DataBaseContext : DbContext
    {
        public DbSet<Kisiler> Kisiler { get; set; }
        public DbSet<Adresler> Adresler { get; set; }

        public DataBaseContext()
        {
            Database.SetInitializer(new VeriTabaniOlusturucu());
        }

    }
    public class VeriTabaniOlusturucu: CreateDatabaseIfNotExists<DataBaseContext>
    {
        protected override void Seed(DataBaseContext context)
        {
            // Kisiler insert ediliyor.
            for (int i = 0; i < 10; i++)
            {
                Kisiler kisi = new Kisiler();
                kisi.Ad = FakeData.NameData.GetFirstName(); // Fake database'dan fake isim getirir.
                kisi.Soyad = FakeData.NameData.GetSurname();
                kisi.Yas = FakeData.NumberData.GetNumber(10,90);

                context.Kisiler.Add(kisi);
            }
            context.SaveChanges();

            // Adresler insert ediliyor.
            List<Kisiler> tumKisiler = context.Kisiler.ToList(); // Select * from Kisiler

            foreach (Kisiler kisi in tumKisiler)
            {
                for (int i = 0; i < FakeData.NumberData.GetNumber(1,5); i++)
                {
                    Adresler adres = new Adresler();
                    adres.AdresTanim = FakeData.PlaceData.GetAddress(); // Fake adres oluşturur.
                    adres.Kisi = kisi;

                    context.Adresler.Add(adres);
                }       
            }
            context.SaveChanges();
        }
    }
}