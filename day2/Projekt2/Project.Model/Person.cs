using System;

namespace Project.Model
{
    public class Person
    {
        public Person()
        {
        }

        public Person(int v1, string v2, string v3, int v4)
        {
            Id = v1;
            Name = v2;
            Surname = v3;
            City = v4;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public int City { get; set; }

    }
}
