using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace cliniclibrary
{
    public class Doctordetails
    {

        public long id { get; set; }
        public string fname { get; set; }
        public string lname { get; set; }
        public string sex { get; set; }
        public string specialization { get; set; }
        public int specializationid { get; set; }
        public string starttime { get; set; }

        public string endtime { get; set; }

        public Doctordetails() { }
        public Doctordetails(long id, string fname, string lname, string sex,string specialization, int specializationid, string starttime, string endtime)
        {
            this.id = id;
            this.fname = fname;
            this.lname = lname;
            this.sex = sex;
            this.specialization = specialization;
            this.specializationid = specializationid;
            this.starttime = starttime;
            this.endtime = endtime;
        }
        public Doctordetails(string fname, string specialization, string starttime, string endtime)
        {
            this.fname = fname;
            this.specialization = specialization;
            this.starttime = starttime;
            this.endtime = endtime;
        }
    }
}

