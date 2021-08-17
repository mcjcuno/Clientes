using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Clientes
{
    public partial class Main : Form
    {
        private BusinessLogicLayer _businessLogicLayer;
        public Main()
        {
            InitializeComponent();
            _businessLogicLayer = new BusinessLogicLayer();
        }

        #region EVENTOS
        private void btnAdd_Click(object sender, EventArgs e)
        {
            OpenContactDetailsDiolog();
        }
        private void Main_Load(object sender, EventArgs e)
        {
            PopulateContacts();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewLinkCell cell = (DataGridViewLinkCell)gridContacts.Rows[e.RowIndex].Cells[e.ColumnIndex];

            if (cell.Value.ToString() == "Edit")
            {
                ContactDetails contactDetails = new ContactDetails();

                contactDetails.LoadContact(new Contact
                {
                    Id = int.Parse(gridContacts.Rows[e.RowIndex].Cells[2].Value.ToString()),
                    FirstName = gridContacts.Rows[e.RowIndex].Cells[3].Value.ToString(),
                    LastName = gridContacts.Rows[e.RowIndex].Cells[4].Value.ToString(),
                    Phone = gridContacts.Rows[e.RowIndex].Cells[5].Value.ToString(),
                    Address = gridContacts.Rows[e.RowIndex].Cells[6].Value.ToString(),
                });
                contactDetails.ShowDialog(this);
            }
            else if (cell.Value.ToString() == "Delete")
            {
                DeleteContact(int.Parse(gridContacts.Rows[e.RowIndex].Cells[2].Value.ToString()));
                PopulateContacts();
            }

        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            PopulateContacts(textBox1.Text);
            textBox1.Text = string.Empty;

        }
        #endregion

        #region METODOS PRIVADOS
        private void OpenContactDetailsDiolog()
        {

            //LLamamos al segundo formulario creando el objeto
            ContactDetails contactsDetails = new ContactDetails();

            //Abrimos el segundo formulario por arriva del formulario Main como un Pop-Up
            contactsDetails.ShowDialog(this);
        }

        private void DeleteContact(int Id)
        {
            _businessLogicLayer.DeleteContact(Id);
        }
        #endregion

        #region METODOS PUBLICOS
        //Agregamos un parametro nulo y esto significa q el parametro es opcional
        public void PopulateContacts(string searchText = null)
        {
            List<Contact> contacts = _businessLogicLayer.GetContacts(searchText);
            gridContacts.DataSource = contacts;
        }
        #endregion











    }
}
