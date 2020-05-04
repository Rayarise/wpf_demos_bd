using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Text;
using System.Windows;

namespace wpf_demo_phonebook
{
    public class ContactDataService : IDataService<ContactModel>
    {

        readonly List<ContactModel> contacts;
        public ContactDataService()
        {


            var connString = ConfigurationManager.ConnectionStrings["connString"].ConnectionString;


            using (SqlConnection conn = new SqlConnection(connString))
            {


                using (SqlCommand sqlCommand = new SqlCommand("Select * from Contacts", conn))
                {
                    try
                    {
                        conn.Open();
                        contacts = new List<ContactModel>();
                        using (SqlDataReader dataReader = sqlCommand.ExecuteReader())
                        {
                            while (dataReader.Read())
                            {


                                ContactModel cm = new ContactModel();

                                cm.ContactID = Convert.ToInt32(dataReader["ContactID"]);
                                cm.FirstName = dataReader["FirstName"].ToString();
                                cm.LastName = dataReader["LastName"].ToString();
                                cm.Email = dataReader["Email"].ToString();
                                cm.Phone = dataReader["Phone"].ToString();
                                cm.Mobile = dataReader["Mobile"].ToString();

                                contacts.Add(cm);
                                /* ListViewItem item = new ListViewItem();
                                 item.Content = cm;
                                 lvContact.Items.Add(item);
                                 */
                            }
                        }

                    }
                    catch
                    {
                        MessageBox.Show("An error occured!");
                    }
                    finally
                    {
                        conn.Close();
                    }
                }
            }
        }

        public IEnumerable<ContactModel> GetAll()
        {
            foreach (ContactModel c in contacts)
            {
                yield return c;
            }
        }
    } 
}
