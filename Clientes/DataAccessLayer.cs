using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace Clientes
{
    class DataAccessLayer
    {
        private SqlConnection conn = new SqlConnection("Integrated Security=SSPI;Persist Security Info=False;Initial Catalog=Clientes;Data Source=0V46ELQ9");

        public void InsertContact(Contact contact)
        {
            try
            {
                //Estos son los cuatro parametros q vamos a pasarle a nuestra funcion
                conn.Open();
                string query = @"
                        INSERT INTO Contacts ([FirstName], [LastName], [Phone], [Address])
                        VALUES (@FirstName, @LastName, @Phone, @Address) ";


                //Escribimos los parametros
                SqlParameter firstName = new SqlParameter("@FirstName", contact.FirstName);
                SqlParameter lastName = new SqlParameter("@LastName", contact.LastName);
                SqlParameter phone = new SqlParameter("@Phone", contact.Phone);
                SqlParameter address = new SqlParameter("@Address", contact.Address);

                SqlCommand command = new SqlCommand(query, conn);
                
                command.Parameters.Add(firstName);
                command.Parameters.Add(lastName);
                command.Parameters.Add(phone);
                command.Parameters.Add(address);

                command.ExecuteNonQuery();
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                conn.Close();
            }
        }

        public void UpdateContact(Contact contact)
        {
            try
            {
                conn.Open();
                string query = @" Update Contacts
                                  Set FirstName = @FirstName, 
                                      LastName  = @LastName,
                                      Phone     = @Phone, 
                                      Address   = @Address 

                                  WHERE Id      = @id";


                SqlParameter id        = new SqlParameter("@Id", contact.Id);
                SqlParameter firstName = new SqlParameter("@FirstName", contact.FirstName);
                SqlParameter lastName  = new SqlParameter("@LastName", contact.LastName);
                SqlParameter phone     = new SqlParameter("@Phone", contact.Phone);
                SqlParameter address   = new SqlParameter("@Address", contact.Address);

                SqlCommand command = new SqlCommand(query, conn);

                command.Parameters.Add(id);
                command.Parameters.Add(firstName);
                command.Parameters.Add(lastName);
                command.Parameters.Add(phone);
                command.Parameters.Add(address);

                command.ExecuteNonQuery();

            }
            catch (Exception)
            {

                throw;
            }
            finally { conn.Close(); }
        }

        public void DeleteContact(int id)
        {
            try
            {
                conn.Open();
                string query = @"DELETE FROM Contacts WHERE Id = @Id";

                SqlCommand command = new SqlCommand(query, conn);
                command.Parameters.Add(new SqlParameter("@Id", id));

                command.ExecuteNonQuery();
            }
            catch (Exception)
            {

                throw;
            }
            finally { conn.Close(); }
        }
         public List<Contact> GetContacts(string search = null)
        {
            //Creamos un lista de contactos bacia
            List<Contact> contacts = new List<Contact>();

            try
            {
                conn.Open();
                string query = @" SELECT Id, FirstName, LastName, Phone, Address
                                FROM Contacts";

                SqlCommand comand = new SqlCommand();

                if (!string.IsNullOrEmpty(search))
                {
                    query += @" WHERE FirstName LIKE  @Search OR LastName LIKE @Search OR Phone LIKE @Search 
                             OR Address LIKE @Search  ";

                    comand.Parameters.Add(new SqlParameter("@Search", $"%{search}%"));
                }

                comand.CommandText = query;
                comand.Connection  = conn;

                //Sqldata reader devuelve todas las filas
                SqlDataReader reader = comand.ExecuteReader();

                while (reader.Read())
                {
                    //Por cada una de las iteraciones lo vamos guardando en esta lista
                    contacts.Add(new Contact
                    {
                        Id        = int.Parse(reader["Id"].ToString()),
                        FirstName = reader["FirstName"].ToString(),
                        LastName  = reader["LastName"].ToString(),
                        Phone     = reader["Phone"].ToString(),
                        Address   = reader["Address"].ToString()
                    });
                }
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                conn.Close();
            }

            return contacts;
        }
    }
}

