using System.Data.SqlClient;
using System.Data;
using cliniclibrary;

namespace clinicDB
{
    public class ClinicDatabase
    {

        
        public static SqlConnection con;
        public static SqlCommand cmd;
        public SqlConnection getConnection()
        {
            con = new SqlConnection("Data Source=.;Initial Catalog=ClinicManagement;Integrated Security=true");
            con.Open();
            return con;
        }

        // ---------- retrive and validate staff's username and password ----------
        public string getUserPassword(string username, string password)
        {
            con = getConnection();
            cmd = new SqlCommand("spcheckuserpass", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@username", username);
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                if (reader["UserPassword"].ToString() == password)
                {
                    string fname = reader["Firstname"].ToString();
                    string lname = reader["Lastname"].ToString();
                    return fname + " " + lname;
                }
                else
                    throw new LoginScreen.PasswordException("Incorrect password!");
            }
            else
                return null;
        }

        // ---------- retrive all the details of doctor ----------
        public List<Doctordetails> getDoctorDetails()
        {
            List<Doctordetails> docdetails = new List<Doctordetails> ();
            con = getConnection();
            cmd = new SqlCommand("spdocdetails", con);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {

                long id = long.Parse(reader["doctorid"].ToString());
                string fname = reader["firstname"].ToString();
                string lname = reader["lastname"].ToString();
                string sex = reader["sex"].ToString();
                string specialization = reader["specialization"].ToString();
                int specializationid = int.Parse(reader["specializationid"].ToString());
                string starttime = reader["startTime"].ToString();
                string endtime = reader["endtime"].ToString();
                docdetails.Add(new Doctordetails(id, fname, lname, sex, specialization, specializationid, starttime, endtime));
            }
            return docdetails;
        }

        // ---------- Check if patient exists ----------

        public bool checkPatientIdExists(long patientid)
        {
            con = getConnection();
            cmd = new SqlCommand("sqcheckpatient", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@patientid", patientid);
            SqlDataReader reader = cmd.ExecuteReader();

            if (reader.Read())
                return true;
            return false;

        }

        // ---------- retrive all the Specialization details ----------

        public Dictionary<int, string> getSpecialization()
        {
            Dictionary<int, string> allSpecializations =
                      new Dictionary<int, string>();

            con = getConnection();
            cmd = new SqlCommand("spgetspecializations", con);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                int specid = int.Parse(reader["specializationId"].ToString());
                string spec = reader["specialization"].ToString();
                allSpecializations[specid] = spec;
            }
            return allSpecializations;

        }

        // ---------- Checking appoiintment dates already fixed ----------

        public bool checkAppointmentdates(DateTime date, int specid)
        {
            con = getConnection();
            cmd = new SqlCommand("spcheckAppoint", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@specializationId", specid);
            cmd.Parameters.AddWithValue("@appointDate", date);
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
                return true;
            return false;
        }

        // ---------- Scheduling new appointment ----------

        public Appointments scheduleNewAppointment(long patientid, int specid, DateTime date)
        {
            string specname="";
            string docname="";
            string patientName ="";
            string appointStartTime="";
            string appointEndTime = "";

            string patientname;
            //getting doc name and appointment timing
            con = getConnection();
            cmd = new SqlCommand("spGetDocdetails", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@specializationId", specid);
            SqlDataReader readDocDetails = cmd.ExecuteReader();

            try
            {
                readDocDetails.Read();
                specname = readDocDetails["specialization"].ToString();
                docname = readDocDetails["firstname"].ToString();
                appointStartTime = readDocDetails["startTime"].ToString();
                appointEndTime = readDocDetails["endTime"].ToString();
                readDocDetails.Close();
            }catch(Exception e)
            {
                Console.WriteLine(e.Message);                
            }
            // get patient name
            cmd = new SqlCommand("spPatientname", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@patientId", patientid);
            SqlDataReader readPatient = cmd.ExecuteReader();
            try
            {
                readPatient.Read();
                patientName = readPatient["name"].ToString();
                readPatient.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            // insert data to apointments table 
            long appointid = getId();

            bool success =false;
            try
            {
                cmd = new SqlCommand("spScheduleAppointment", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@appointmentid", appointid);
                cmd.Parameters.AddWithValue("@patientId", patientid);
                cmd.Parameters.AddWithValue("@specializationId", specid);
                cmd.Parameters.AddWithValue("@doctor", docname);
                cmd.Parameters.AddWithValue("@visitDate", date);
                cmd.Parameters.AddWithValue("@appointmentStartTime", appointStartTime);
                cmd.Parameters.AddWithValue("@appointmentEndTime", appointEndTime);
                cmd.ExecuteNonQuery();
                success = true;
            }catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                if (success)
                    Console.WriteLine("***** Appointment fixed successfully *****");
            }
            return new Appointments(appointid, patientName, specname, docname, date, appointStartTime, appointEndTime);

        }

        // ---------- cancel appointment ----------

        public bool cancelAppointment(long appointId)
        {
            con = getConnection();
            cmd = new SqlCommand("spcheckAppointPresent", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@id", appointId);
            SqlDataReader readAppointment = cmd.ExecuteReader();
            if(readAppointment.Read())
            {
                readAppointment.Close();
                bool success=false;
                try
                {
                    cmd = new SqlCommand("spDeleteAppointment", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@id", appointId);
                    cmd.ExecuteNonQuery();
                    success = true;
                }catch(Exception e)
                {
                    Console.WriteLine(e.Message);
                }
                
                return true;

            }
            else
            {
                return false;
            }
        }


        // ---------- Adding new patient ----------

        public Patients addNewPatient(string fname, string lname, DateTime dob, int age, string gender)
        {
            con = getConnection();
            try
            {
                cmd = new SqlCommand("spAddPatient", con);
                cmd.CommandType = CommandType.StoredProcedure;
                long patientid = getId();
                cmd.Parameters.AddWithValue("@patientId", patientid);
                cmd.Parameters.AddWithValue("@firstname", fname);
                cmd.Parameters.AddWithValue("@lastname", lname);
                cmd.Parameters.AddWithValue("@sex", gender);
                cmd.Parameters.AddWithValue("@age", age);
                cmd.Parameters.AddWithValue("@dateofbirth", dob);
                cmd.ExecuteNonQuery();
                return new Patients(patientid, fname, lname, gender, age,dob);
            }catch(Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }

        // Getting random id of 10 digit
        long getId()
        {
            Random rand = new Random();
            Int64 transId = rand.NextInt64(999999999, 10000000000);
            return transId;
        }
    }
}