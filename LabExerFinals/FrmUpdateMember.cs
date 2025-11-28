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
    public partial class FrmUpdateMember : Form

    {
        private ClubRegistrationQuery clubRegistrationQuery;
        public FrmUpdateMember()
        {
            InitializeComponent();
        }

        private void FrmUpdateMember_Load(object sender, EventArgs e)
        {
            clubRegistrationQuery = new ClubRegistrationQuery();
            LoadStudentIDs();
        }

        private void LoadStudentIDs()
        {
            DataTable dt = clubRegistrationQuery.GetAllStudentIDs();
            cmbStudentID.DataSource = dt;
            cmbStudentID.DisplayMember = "StudentID";
            cmbStudentID.ValueMember = "StudentID";
            cmbStudentID.SelectedIndex = -1;
        }
        private void cmbStudentID_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbStudentID.SelectedValue == null) return;

            long studentID;
            if (!long.TryParse(cmbStudentID.SelectedValue.ToString(), out studentID))
                return;
            DataTable dt = clubRegistrationQuery.GetStudentByID(studentID);
            if (dt.Rows.Count == 0)
                return;

            DataRow row = dt.Rows[0];

            txtLastName.Text = row["LastName"].ToString();
            txtFirstName.Text = row["FirstName"].ToString();
            txtMiddleName.Text = row["MiddleName"].ToString();
            txtAge.Text = row["Age"].ToString();
            cmbGender.Text = row["Gender"].ToString();
            cmbProgram.Text = row["Program"].ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (cmbStudentID.SelectedValue == null)
            {
                MessageBox.Show("Please select a Student ID.", "Warning",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            long studentID;
            if (!long.TryParse(cmbStudentID.SelectedValue.ToString(), out studentID))
            {
                MessageBox.Show("Invalid Student ID.", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            int age;
            if (!int.TryParse(txtAge.Text.Trim(), out age))
            {
                MessageBox.Show("Please enter a valid age.", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                clubRegistrationQuery.UpdateStudent(
                    studentID,
                    txtFirstName.Text.Trim(),
                    txtMiddleName.Text.Trim(),
                    txtLastName.Text.Trim(),
                    age,
                    cmbGender.Text,
                    cmbProgram.Text
                );

                MessageBox.Show("Record updated successfully!",
                    "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                this.Close();   
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error updating record:\n" + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

    }
}
