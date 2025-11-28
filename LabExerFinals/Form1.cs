using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LabExerFinals
{
    public partial class FrmClubRegistration : Form
    {
        public FrmClubRegistration()
        {
            InitializeComponent();
            clubRegistrationQuery = new ClubRegistrationQuery();
        }

        private ClubRegistrationQuery clubRegistrationQuery;
        private int ID, Age, count;
        private string FirstName, MiddleName, LastName, Gender, Program;

        private void btnUpd_Click(object sender, EventArgs e)
        {
            using (var frm = new FrmUpdateMember())
            {
                frm.ShowDialog();
            }

            RefreshListOfClubMembers();
        }

        private long StudentID;

        private void FrmClubRegistration_Load(object sender, EventArgs e)
        {
            RefreshListOfClubMembers();
        }

        public  void RefreshListOfClubMembers()
        {
            clubRegistrationQuery.DisplayList();
            dataGridView1.DataSource = clubRegistrationQuery.bindingSource;
        }

        public int RegistrationID()
        {
            count = count + 1;  
            return count;
        }

        private void btnReg_Click(object sender, EventArgs e)
        {
            StudentID = long.Parse(txtStudentID.Text);
            ID = RegistrationID();

            clubRegistrationQuery.RegisterStudent(
                ID,
                StudentID,
                txtFirstName.Text,
                txtMiddleName.Text,
                txtLastName.Text,
                int.Parse(txtAge.Text),
                cmbGender.Text,
                cmbProgram.Text
            );

            RefreshListOfClubMembers();
            MessageBox.Show("Student registered successfully!");
        }
        private void btnRef_Click(object sender, EventArgs e)
        {
            RefreshListOfClubMembers();
        }
    }
}
