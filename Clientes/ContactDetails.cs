using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Clientes
{
    public partial class ContactDetails : Form
    {
        //LLamamos a la capa de negocio
        private BusinessLogicLayer _businessLogicLayer;
        private Contact _contact;

        public ContactDetails()
        {
            InitializeComponent();
            //Despues q se inicialice los componentes del formulario se cargue la capa de negocios
            _businessLogicLayer = new BusinessLogicLayer();
        }
        #region EVENTOS
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            SaveContact();
            this.Close();
            ((Main)this.Owner).PopulateContacts();
        }
        #endregion

        #region METODOS PUBLICOS
        public void LoadContact(Contact contact)
        {
            _contact = contact;
            if (contact != null)
            {
                ClearForm();

                txtFirstName.Text = contact.FirstName;
                txtLastName.Text = contact.LastName;
                txtPhone.Text = contact.Phone;
                txtAddress.Text = contact.Address;
            }

        }
        #endregion

        #region METODOS PRIVADOS
        private void ClearForm()
        {
            txtFirstName.Text = string.Empty;
            txtLastName.Text = string.Empty;
            txtPhone.Text = string.Empty;
            txtAddress.Text = string.Empty;
        }
        private void SaveContact()
        {
            Contact contact = new Contact();

            contact.FirstName = txtFirstName.Text;
            contact.LastName = txtLastName.Text;
            contact.Phone = txtPhone.Text;
            contact.Address = txtAddress.Text;

            contact.Id = _contact != null ? _contact.Id : 0;

            _businessLogicLayer.SaveContact(contact);
        }
        #endregion




        
       
    }
}
