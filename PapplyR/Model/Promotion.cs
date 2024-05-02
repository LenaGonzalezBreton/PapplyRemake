    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Data.SqlClient;
    using System.Configuration;

    namespace PapplyR.Model
    {
        public class Promotion
        {
            #region "Déclaration des attributs"
        
            public int Id { get; set; }
            public string Name { get; set; }

            #endregion

            // Liste des étudiants dans cette promotion
            public List<Student> Students { get; set; }

            public Promotion()
            {
                Students = new List<Student>();
            }

            #region "Méthodes"
            // Méthode pour ajouter un étudiant à la promotion
            public void AddStudent(Student student)
            {
                Students.Add(student);
                student.Promotion = this;
            }


            public static void Add(Promotion promotion)
            {
                string connectionString = ConfigurationManager.ConnectionStrings["MaConnexion"].ConnectionString;

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "INSERT INTO Promotion (name) VALUES (@Name)";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@Name", promotion.Name);

                    try
                    {
                        connection.Open();
                        command.ExecuteNonQuery();
                        Console.WriteLine("Promotion ajoutée avec succès.");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Erreur lors de l'ajout de la promotion : " + ex.Message);
                    }
                }
            }

            public static void Delete(Promotion promotion)
            {
                string connectionString = ConfigurationManager.ConnectionStrings["MaConnexion"].ConnectionString;

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "DELETE FROM Promotion WHERE id = @Id";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@Id", promotion.Id);

                    try
                    {
                        connection.Open();
                        int rowsAffected = command.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            Console.WriteLine("Promotion supprimée avec succès.");
                        }
                        else
                        {
                            Console.WriteLine("Aucune promotion trouvée avec cet identifiant.");
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Erreur lors de la suppression de la promotion : " + ex.Message);
                    }
                }
            }

            public static void Update(Promotion promotion)
            {
                string connectionString = ConfigurationManager.ConnectionStrings["MaConnexion"].ConnectionString;

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "UPDATE Promotion SET name = @Name WHERE id = @Id";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@Name", promotion.Name);
                    command.Parameters.AddWithValue("@Id", promotion.Id);

                    try
                    {
                        connection.Open();
                        int rowsAffected = command.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            Console.WriteLine("Informations de la promotion mises à jour avec succès.");
                        }
                        else
                        {
                            Console.WriteLine("Aucune promotion trouvée avec cet identifiant.");
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Erreur lors de la mise à jour des informations de la promotion : " + ex.Message);
                    }
                }
            }

        public static List<Promotion> GetAllPromotions()
        {
            List<Promotion> promotions = new List<Promotion>();

            string connectionString = ConfigurationManager.ConnectionStrings["MaConnexion"].ConnectionString;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT Id, Name FROM Promotion";
                SqlCommand command = new SqlCommand(query, connection);

                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        Promotion promotion = new Promotion();
                        promotion.Id = reader.GetInt32(0);
                        promotion.Name = reader.GetString(1);
                        promotions.Add(promotion);
                    }
                    reader.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Erreur lors de la récupération des promotions : " + ex.Message);
                }
            }

            return promotions;
        
    }
    #endregion
}
    }
