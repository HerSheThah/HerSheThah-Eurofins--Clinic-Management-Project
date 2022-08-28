using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cliniclibrary
{
    public  class Patients
    {
        public long id { get; set; }
        public string fname { get; set; }
        public string lname { get; set; }
        public string sex { get; set; }
        public int age { get; set; }
        public DateTime dob { get; set; }

        public Patients() { }
        public Patients(long id, string fname, string lname, string sex, int age, DateTime dob)
        {
            this.id = id;
            this.fname = fname;
            this.lname = lname;
            this.sex = sex;
            this.age = age;
            this.dob = dob;
        }
    }
}
