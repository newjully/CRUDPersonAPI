using CRUDPersonAPI.Models;
using Microsoft.Data.SqlClient;
using System.Data;

namespace CRUDPersonAPI.Repository
{
    public class PersonRepository
    {
        public IList<Person> GetAll()
        {
            IList<Person> list = new List<Person>();

            var connectionString = new ConfigurationBuilder()
                                        .SetBasePath(Directory.GetCurrentDirectory())
                                        .AddJsonFile("appsettings.json")
                                        .Build().GetConnectionString("CRUDPersonAPIConnection");

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                String query =
                    "SELECT ID, NAME, ADDRESS FROM PERSON ";

                SqlCommand command = new SqlCommand(query, connection);
                connection.Open();
                SqlDataReader dataReader = command.ExecuteReader();

                while (dataReader.Read())
                {
                    // Recupera os dados
                    Person person = new Person();
                    person.Id = Convert.ToInt32(dataReader["ID"]);
                    person.Name = dataReader["NAME"].ToString();
                    person.Address = dataReader["ADDRESS"].ToString();

                    // Adiciona o modelo da lista
                    list.Add(person);
                }

                connection.Close();

            } // Finaliza o objeto connection

            // Retorna a lista
            return list;
        }

        public Person GetOne(int id)
        {

            Person person = new Person();

            var connectionString = new ConfigurationBuilder()
                                        .SetBasePath(Directory.GetCurrentDirectory())
                                        .AddJsonFile("appsettings.json")
                                        .Build().GetConnectionString("CRUDPersonAPIConnection");

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                String query =
                    "SELECT ID, NAME, ADDRESS FROM PERSON WHERE ID = @id ";

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.Add("@id", SqlDbType.Int);
                command.Parameters["@id"].Value = id;
                connection.Open();

                SqlDataReader dataReader = command.ExecuteReader();

                while (dataReader.Read())
                {
                    // Recupera os dados
                    person.Id = Convert.ToInt32(dataReader["ID"]);
                    person.Name = dataReader["NAME"].ToString();
                    person.Address = dataReader["ADDRESS"].ToString();
                }

                connection.Close();

            } // Finaliza o objeto connection

            // Retorna a lista
            return person;
        }

        public void Create(Person person)
        {
            var connectionString = new ConfigurationBuilder()
                                        .SetBasePath(Directory.GetCurrentDirectory())
                                        .AddJsonFile("appsettings.json")
                                        .Build().GetConnectionString("CRUDPersonAPIConnection");

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                String query =
                    "INSERT INTO PERSON ( NAME, ADDRESS ) VALUES ( @name, @address ) ";

                SqlCommand command = new SqlCommand(query, connection);

                // Adicionando o valor ao comando
                command.Parameters.Add("@name", SqlDbType.Text);
                command.Parameters["@name"].Value = person.Name;
                command.Parameters.Add("@address", SqlDbType.Text);
                command.Parameters["@address"].Value = person.Address;

                // Abrindo a conexão com  o Banco
                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
            }
        }

        public void Update(Person person)
        {
            var connectionString = new ConfigurationBuilder()
                                        .SetBasePath(Directory.GetCurrentDirectory())
                                        .AddJsonFile("appsettings.json")
                                        .Build().GetConnectionString("CRUDPersonAPIConnection");

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                String query =
                    "UPDATE PERSON SET NAME = @name , ADDRESS = @address WHERE ID = @id  ";

                SqlCommand command = new SqlCommand(query, connection);

                // Adicionando o valor ao comando
                command.Parameters.Add("@name", SqlDbType.Text);
                command.Parameters.Add("@address", SqlDbType.Text);
                command.Parameters.Add("@id", SqlDbType.Int);
                command.Parameters["@name"].Value = person.Name;
                command.Parameters["@address"].Value = person.Address;
                command.Parameters["@id"].Value = person.Id;

                // Abrindo a conexão com  o Banco
                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
            }
        }

        public void Delete(int id)
        {
            var connectionString = new ConfigurationBuilder()
                                        .SetBasePath(Directory.GetCurrentDirectory())
                                        .AddJsonFile("appsettings.json")
                                        .Build().GetConnectionString("CRUDPersonAPIConnection");

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                String query =
                    "DELETE PERSON WHERE ID = @id  ";

                SqlCommand command = new SqlCommand(query, connection);

                // Adicionando o valor ao comando
                command.Parameters.Add("@id", SqlDbType.Int);
                command.Parameters["@id"].Value = id;

                // Abrindo a conexão com  o Banco
                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
            }

        }
    }
}