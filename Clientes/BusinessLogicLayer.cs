using System;
using System.Collections.Generic;
using System.Text;

namespace Clientes
{
    //Esta capa lo que tiene que hacer es enviar el modelo y encargarse de enviarla a nuestra capa de acceso a datos
    class BusinessLogicLayer
    {
        private DataAccessLayer _dataAccessLayer;

        public BusinessLogicLayer()
        {
            _dataAccessLayer = new DataAccessLayer();
        }
        public Contact SaveContact(Contact contact)
        {
            if (contact.Id == 0)

                _dataAccessLayer.InsertContact(contact);
            else
                _dataAccessLayer.UpdateContact(contact);

                return contact;

        }
        //Este es el metodo q nos va devolver todos los contactos
        public List<Contact> GetContacts(string searchText = null)
        {
            return _dataAccessLayer.GetContacts( searchText );
        }
        public void DeleteContact(int Id)
        {
            _dataAccessLayer.DeleteContact(Id);
        }
    }
}
