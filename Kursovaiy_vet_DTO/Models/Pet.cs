using System;
using System.Collections.Generic;
using System.Text;

namespace Kursovaiy_vet_DTO.Models
{ 
    //Объявление модели животного для базы данных и отображения на веб-странице
    public class Pet
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Age { get; set; }
        public string Breed { get; set; }
        public string OwnersName { get; set; }
        public string Diagnosis { get; set; }

    }
}
