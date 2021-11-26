using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace DbProject
{
    class DataClass
    {
        MySqlConnectionStringBuilder ConnectrionStr;
        MySqlConnection connection;
        public void CreateStrConnection()
        {
            ConnectrionStr = new MySqlConnectionStringBuilder();
            ConnectrionStr.Server = "localhost";
            ConnectrionStr.Port = 3303;
            ConnectrionStr.UserID = "root";
            ConnectrionStr.Password = "root";
            ConnectrionStr.Database = "database_book";

            connection = new MySqlConnection(ConnectrionStr.ToString());

        }
        public void AddBook(string Title, string Author, string Genre, int Date)
        {
            string CommandText = $"INSERT INTO books (Title,Genre,Author,DateCreate) VALUES ('{Title}','{Genre}','{Author}',{Date});";

            try
            {
                connection.Open();
                MySqlCommand cmd = new MySqlCommand(CommandText, connection);
                cmd.ExecuteNonQuery();

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
            connection.Close();

        }
        public List<Book> ReadBook()
        {
            List<Book> books = new List<Book>();
            string cmdtxt = $"SELECT * FROM books;";
            try
            {
                connection.Open();
                MySqlCommand cmd = new MySqlCommand(cmdtxt, connection);
                MySqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        books.Add(new Book()
                        {
                            idbooks = reader.GetInt32(0),
                            Title = reader.GetString(1),
                            Genre = reader.GetString(2),
                            Author = reader.GetString(3),
                            DateCreate = reader.GetInt32(4)
                        });
                    }
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
            connection.Close();
            return books;

        }

        public void UpdBook(int Id, string newTitle, string newAuthor, string newGenre, int newDate)
        {
            string CommandText = $"UPDATE books SET Title = '{newTitle}', " +
                $"Genre = '{newGenre}', " +
                $"Author = '{newAuthor}', " +
                $"DateCreate = {newDate} WHERE idbooks = {Id};";

            try
            {
                connection.Open();
                MySqlCommand command = new MySqlCommand(CommandText, connection);
                command.ExecuteNonQuery();

            }
            catch (Exception error)
            {
                MessageBox.Show(error.Message);
            }
            connection.Close();
        }

        public void DelBook(int id)
        {
            string cmdtxt = $"DELETE FROM books WHERE idbooks = {id}";
            try
            {
                connection.Open();
                MySqlCommand cmd = new MySqlCommand(cmdtxt, connection);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
            connection.Close();


        }
    }
}
