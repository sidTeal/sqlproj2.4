using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Tisdale_Project_2
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();

            updateComboBoxes();

        }

        private void updateComboBoxes()
        {
            string[] employeeArray = DatabaseManager.getEmployeeNames();
            cbViewEmployee.DataSource = employeeArray;
            cbViewEmployee.SelectedIndex = 0;

            cbDeleteEmployee.DataSource = employeeArray;
            cbDeleteEmployee.SelectedIndex = 0;

            string[] customerArrayView = DatabaseManager.getCustomerNames();
            string[] customerArrayDelete = new string[customerArrayView.Length];
            string[] customerArrayModify = new string[customerArrayView.Length];

            customerArrayView.CopyTo(customerArrayDelete, 0);
            customerArrayView.CopyTo(customerArrayModify, 0);

            cbViewCustomer.DataSource = customerArrayView;
            cbViewCustomer.SelectedIndex = 0;

            cbDeleteCustomer.DataSource = customerArrayDelete;
            cbDeleteCustomer.SelectedIndex = 0;

            cbModifyCustomer.DataSource = customerArrayModify;
            cbModifyCustomer.SelectedIndex = 0;
        }


        private void bnAddCustomer_Click(object sender, EventArgs e)
        {
            Customer temp = null;
            bool legalCustomerValues = false;
            string dateString = "";

            try
            {
                string firstName = tbCustomerFirstName.Text.Replace(" ", "");
                string lastName = tbCustomerLastName.Text.Replace(" ", "");


                temp = new Customer(firstName, lastName, tbCustomerAddress.Text, dtpCustomerDateOfBirth.Value, tbCustomerFavoriteDepartment.Text);
                dateString = dtpCustomerDateOfBirth.Value.ToString("yyyy-MM-dd");
                legalCustomerValues = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Entry Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            if (legalCustomerValues)
            {
                Console.WriteLine(temp.FirstName);
                DatabaseManager.addCustomer(temp.FirstName, temp.LastName, temp.Address, dateString, temp.FavoriteDepartment);
                MessageBox.Show("Customer Added", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                updateComboBoxes();

                tbCustomerFirstName.Text = "";
                tbCustomerLastName.Text = "";
                tbCustomerAddress.Text = "";
                dtpCustomerDateOfBirth.Value = DateTime.Now;
                tbCustomerFavoriteDepartment.Text = "";
            }

            string[] customerArray = DatabaseManager.getCustomerNames();

        }


        private void cbViewEmployee_SelectedIndexChanged(object sender, EventArgs e)
        {


        }

        private void cbDeleteCustomer_SelectedIndexChanged(object sender, EventArgs e)
        {
            string deleteSelection = cbDeleteCustomer.Text;

            if (deleteSelection != "")
            {
                string[] name = new string[1];

                deleteSelection = deleteSelection.Replace(",", "");
                name = deleteSelection.Split(' ');
                //name[0] = last name
                //name[1] = first name

                int personID = DatabaseManager.getPersonIdNumber(name[1], name[0]);
                DatabaseManager.deleteCustomer(personID);
                updateComboBoxes();
                MessageBox.Show("Customer deleted.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);


            }
        }

        private void cbViewCustomer_SelectedIndexChanged(object sender, EventArgs e)
        {

            string viewSelection = cbViewCustomer.Text;
            if (viewSelection != "")
            {
                string[] name = new string[1];

                viewSelection = viewSelection.Replace(",", "");
                name = viewSelection.Split(' ');
                //name[0] = last name
                //name[1] = first name

                dgvViewCustomer.DataSource = DatabaseManager.viewCustomer(name[0], name[1]);
                updateComboBoxes();
            }



        }

        private void cbModifyCustomer_SelectedIndexChanged(object sender, EventArgs e)
        {
            string modifySelection = cbModifyCustomer.Text;

            if (modifySelection != "")
            {
                string[] name = new string[1];

                modifySelection = modifySelection.Replace(",", "");
                name = modifySelection.Split(' ');
                //name[0] = last name
                //name[1] = first name

                int personID = DatabaseManager.getPersonIdNumber(name[1], name[0]);

                pnlModifyCustomer.Visible = true;

                string cusInfo = DatabaseManager.getCustomerToModify(personID);

                string[] cleanCusInfo = new string[3];
                cleanCusInfo = cusInfo.Split('|');

                tbModifyCustomerFirstName.Text = cleanCusInfo[0];
                tbModifyCustomerLastName.Text = cleanCusInfo[1];
                tbModifyCustomerAddress.Text = cleanCusInfo[2];
                dtpModifyCustomerDateOfBirth.Value = Convert.ToDateTime(cleanCusInfo[3]);
                tbModifyCustomerFavoriteDepartment.Text = DatabaseManager.getCustomerFavoriteDepartment(personID);


                updateComboBoxes();

                //MessageBox.Show("Customer modified.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);


            }
        }
    }
}
