using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using System.Windows.Forms;

namespace LabExerFinals
{
    internal class ClubRegistrationQuery
    {
        private SqlConnection sqlConnect;
        private SqlCommand sqlCommand;
        private SqlDataAdapter sqlAdapter;
        private SqlDataReader sqlReader;

        public DataTable dataTable;
        public BindingSource bindingSource;

        private string connectionString;

        public string _FirstName, _MiddleName, _LastName, _Gender, _Program;
        public int _Age;

        public ClubRegistrationQuery()
        {
            connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\jusxm\Source\Repos\LabExerFinals\LabExerFinals\ClubDB.mdf;Integrated Security=True";

            sqlConnect = new SqlConnection(connectionString);
            dataTable = new DataTable();
            bindingSource = new BindingSource();
        }

        public bool DisplayList()
        {
            string ViewClubMembers = "SELECT StudentId, FirstName, MiddleName, LastName, Age, Gender, Program FROM ClubMembers";

            try
            {
                sqlAdapter = new SqlDataAdapter(ViewClubMembers, sqlConnect);

                dataTable.Clear();
                sqlAdapter.Fill(dataTable);
                bindingSource.DataSource = dataTable;

                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error in DisplayList:\n\n" + ex.Message,
                    "DB Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        public bool RegisterStudent(int ID, long StudentID, string FirstName, string MiddleName, string LastName, int Age, string Gender, string Program)
        {
            
            sqlCommand = new SqlCommand("INSERT INTO ClubMembers VALUES(@ID, @StudentID, @FirstName, @MiddleName, @LastName, @Age, @Gender, @Program)", sqlConnect);
            sqlCommand.Parameters.Add("@ID", SqlDbType.Int).Value = ID;
            sqlCommand.Parameters.Add("@RegistrationID", SqlDbType.BigInt).Value = StudentID; 
            sqlCommand.Parameters.Add("@StudentID", SqlDbType.VarChar).Value = StudentID;
            sqlCommand.Parameters.Add("@FirstName", SqlDbType.VarChar).Value = FirstName;
            sqlCommand.Parameters.Add("@MiddleName", SqlDbType.VarChar).Value = MiddleName;
            sqlCommand.Parameters.Add("@LastName", SqlDbType.VarChar).Value = LastName;
            sqlCommand.Parameters.Add("@Age", SqlDbType.Int).Value = Age;
            sqlCommand.Parameters.Add("@Gender", SqlDbType.VarChar).Value = Gender;
            sqlCommand.Parameters.Add("@Program", SqlDbType.VarChar).Value = Program;

            sqlConnect.Open();
            sqlCommand.ExecuteNonQuery();
            sqlConnect.Close();

            return true;
        }

        public DataTable GetAllStudentIDs()
        {
            string query = "SELECT StudentID FROM ClubMembers";

            SqlDataAdapter da = new SqlDataAdapter(query, sqlConnect);
            DataTable dt = new DataTable();
            da.Fill(dt);
            return dt;
        }
        public DataTable GetStudentByID(long studentID)
        {
            string query = "SELECT * FROM ClubMembers WHERE StudentID = @StudentID";

            SqlCommand cmd = new SqlCommand(query, sqlConnect);
            cmd.Parameters.AddWithValue("@StudentID", studentID);

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);

            return dt;
        }
        public void UpdateStudent(long StudentID, string FirstName, string MiddleName,
                                  string LastName, int Age, string Gender, string Program)
        {
            string query = "UPDATE ClubMembers SET " +
                           "FirstName=@FirstName, MiddleName=@MiddleName, LastName=@LastName, " +
                           "Age=@Age, Gender=@Gender, Program=@Program " +
                           "WHERE StudentID=@StudentID";

            SqlCommand cmd = new SqlCommand(query, sqlConnect);

            cmd.Parameters.AddWithValue("@StudentID", StudentID);
            cmd.Parameters.AddWithValue("@FirstName", FirstName);
            cmd.Parameters.AddWithValue("@MiddleName", MiddleName);
            cmd.Parameters.AddWithValue("@LastName", LastName);
            cmd.Parameters.AddWithValue("@Age", Age);
            cmd.Parameters.AddWithValue("@Gender", Gender);
            cmd.Parameters.AddWithValue("@Program", Program);

            sqlConnect.Open();
            cmd.ExecuteNonQuery();
            sqlConnect.Close();
        }

    }
}
