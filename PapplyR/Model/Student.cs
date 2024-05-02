using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Configuration;

namespace PapplyR.Model
{
    public class Student
    {
        #region "attributs"
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int PromotionId { get; set; }
        public Promotion Promotion { get; set; }
        #endregion

        #region "Méthodes"
        public static void Add(Student student)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["MaConnexion"].ConnectionString;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "INSERT INTO Student (firstname, lastname, id) VALUES (@FirstName, @LastName, @PromotionId)";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@FirstName", student.FirstName);
                command.Parameters.AddWithValue("@LastName", student.LastName);
                command.Parameters.AddWithValue("@PromotionId", student.PromotionId);

                try
                {
                    connection.Open();
                    command.ExecuteNonQuery();
                    Console.WriteLine("Étudiant ajouté avec succès.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Erreur lors de l'ajout de l'étudiant : " + ex.Message);
                }
            }
        }

        public static void Delete(Student student)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["MaConnexion"].ConnectionString;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "DELETE FROM Student WHERE id = @Id";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Id", student.Id);

                try
                {
                    connection.Open();
                    int rowsAffected = command.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        Console.WriteLine("Étudiant supprimé avec succès.");
                    }
                    else
                    {
                        Console.WriteLine("Aucun étudiant trouvé avec cet identifiant.");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Erreur lors de la suppression de l'étudiant : " + ex.Message);
                }
            }
        }

        public static void Update(Student student)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["MaConnexion"].ConnectionString;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "UPDATE Student SET firstname = @FirstName, lastname = @LastName, id_promotion = @PromotionId WHERE Id = @Id";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@FirstName", student.FirstName);
                command.Parameters.AddWithValue("@LastName", student.LastName);
                command.Parameters.AddWithValue("@PromotionId", student.PromotionId);
                command.Parameters.AddWithValue("@Id", student.Id);

                try
                {
                    connection.Open();
                    int rowsAffected = command.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        Console.WriteLine("Informations de l'étudiant mises à jour avec succès.");
                    }
                    else
                    {
                        Console.WriteLine("Aucun étudiant trouvé avec cet identifiant.");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Erreur lors de la mise à jour des informations de l'étudiant : " + ex.Message);
                }
            }
        }
        public static List<Student> GetAllStudents()
        {
            List<Student> students = new List<Student>();

            string connectionString = ConfigurationManager.ConnectionStrings["MaConnexion"].ConnectionString;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT Id, FirstName, LastName, id_promotion FROM Student";
                SqlCommand command = new SqlCommand(query, connection);

                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        Student student = new Student();
                        student.Id = reader.GetInt32(0);
                        student.FirstName = reader.GetString(1);
                        student.LastName = reader.GetString(2);
                        student.PromotionId = reader.GetInt32(3);
                        students.Add(student);
                    }
                    reader.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Erreur lors de la récupération des étudiants : " + ex.Message);
                }
            }

            return students;
        }

        #endregion
    }
}
