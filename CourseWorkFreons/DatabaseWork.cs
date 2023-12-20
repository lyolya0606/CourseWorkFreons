using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace CourseWorkFreons {
    public class DatabaseWork {
        //public string test1;
        private SQLiteConnection _sqlite_conn;

        public DatabaseWork() {
            _sqlite_conn = CreateConnection();//test1 = ReadData(sqlite_conn);
        }

        ~DatabaseWork() {
            //_sqlite_conn.Close();
        }

        static SQLiteConnection CreateConnection() {

            SQLiteConnection sqlite_conn;
            // Create a new database connection:
            sqlite_conn = new SQLiteConnection("Data Source=databaseCourseWork.db");
         // Open the connection:
            try {
                sqlite_conn.Open();
            } catch (Exception) {
            
            }
            return sqlite_conn;
        }

        public List<string> GetMarks() {
            List<string> marks = new List<string>();
            SQLiteDataReader sqlite_datareader;
            SQLiteCommand sqlite_cmd;
            sqlite_cmd = _sqlite_conn.CreateCommand();
            sqlite_cmd.CommandText = $"SELECT designation FROM final_product";
            sqlite_datareader = sqlite_cmd.ExecuteReader();

            while (sqlite_datareader.Read()) {
                string myreader = sqlite_datareader.GetString(0);
                marks.Add(myreader);
            }

            return marks;
        }

        string ReadData(SQLiteConnection conn) {
            string test = "";
            SQLiteDataReader sqlite_datareader;
            SQLiteCommand sqlite_cmd;
            sqlite_cmd = conn.CreateCommand();
            sqlite_cmd.CommandText = "SELECT designation FROM final_product";
            sqlite_datareader = sqlite_cmd.ExecuteReader();

            while (sqlite_datareader.Read()) {
                string myreader = sqlite_datareader.GetString(0);
                test += myreader;
            }
            conn.Close();
            return test;
            
        }

        public string GetNameFreon(string mark) {
            string name = "";
            SQLiteDataReader sqlite_datareader;
            SQLiteCommand sqlite_cmd;
            sqlite_cmd = _sqlite_conn.CreateCommand();
            sqlite_cmd.CommandText = "SELECT name FROM final_product WHERE designation = @designation";
            sqlite_cmd.Parameters.Add(new SQLiteParameter("@designation", mark));
            sqlite_datareader = sqlite_cmd.ExecuteReader();

            while (sqlite_datareader.Read()) {
                string myreader = sqlite_datareader.GetString(0);
                name = myreader;
            }
            
            return name;
        }

        public string GetArea(string mark) {
            string area = "";
            SQLiteDataReader sqlite_datareader;
            SQLiteCommand sqlite_cmd;
            sqlite_cmd = _sqlite_conn.CreateCommand();
            sqlite_cmd.CommandText = "SELECT application_area FROM final_product WHERE designation = @designation";
            sqlite_cmd.Parameters.Add(new SQLiteParameter("@designation", mark));
            sqlite_datareader = sqlite_cmd.ExecuteReader();

            while (sqlite_datareader.Read()) {
                string myreader = sqlite_datareader.GetString(0);
                area = myreader;
            }

            return area;
        }

        public string GetSchemeFreon(string mark) {
            string scheme = "";
            int id = GetIdFreon(mark);
            SQLiteDataReader sqlite_datareader;
            SQLiteCommand sqlite_cmd;
            sqlite_cmd = _sqlite_conn.CreateCommand();
            sqlite_cmd.CommandText = "SELECT scheme FROM recipe WHERE recipe.id_final_product = @id";
            sqlite_cmd.Parameters.Add(new SQLiteParameter("@id", id));
            sqlite_datareader = sqlite_cmd.ExecuteReader();

            while (sqlite_datareader.Read()) {
                string myreader = sqlite_datareader.GetString(0);
                scheme = myreader;
            }

            return scheme;
        }

        private int GetIdFreon(string mark) {
            int id = 0;
            SQLiteDataReader sqlite_datareader;
            SQLiteCommand sqlite_cmd;
            sqlite_cmd = _sqlite_conn.CreateCommand();
            sqlite_cmd.CommandText = "SELECT id_final_product FROM final_product WHERE designation = @designation";
            sqlite_cmd.Parameters.Add(new SQLiteParameter("@designation", mark));
            sqlite_datareader = sqlite_cmd.ExecuteReader();

            while (sqlite_datareader.Read()) {
                int myreader = sqlite_datareader.GetInt32(0);
                id = myreader;
            }

            return id;
        }

        public List<Tuple<string, string>> GetEquipment(string mark) {
            List<Tuple<string, string>> equipment = [];
            int id = GetIdFreon(mark);
            SQLiteDataReader sqlite_datareader;
            SQLiteCommand sqlite_cmd;
            sqlite_cmd = _sqlite_conn.CreateCommand();
            sqlite_cmd.CommandText = "SELECT equipment.name, equipment.designation FROM stage JOIN recipe ON " +
                "recipe.id_stage = stage.id_stage JOIN equipment_for_stage ON equipment_for_stage.id_stage = stage.id_stage " +
                "JOIN equipment ON equipment.id_equipment = equipment_for_stage.id_equipment WHERE recipe.id_final_product = @id";
            sqlite_cmd.Parameters.Add(new SQLiteParameter("@id", id));
            sqlite_datareader = sqlite_cmd.ExecuteReader();

            while (sqlite_datareader.Read()) {
                equipment.Add(new Tuple<string, string>(sqlite_datareader.GetString(0), sqlite_datareader.GetString(1)));
            }

            return equipment;
        }

        public List<List<string>> GetTableFinalProduct() {
            //DataTable dt = new DataTable();
            SQLiteDataReader sqlite_datareader;
            SQLiteCommand sqlite_cmd;
            sqlite_cmd = _sqlite_conn.CreateCommand();
            sqlite_cmd.CommandText = "SELECT * FROM final_product";
            sqlite_datareader = sqlite_cmd.ExecuteReader();

            List<List<string>> table = new List<List<string>>();
            while (sqlite_datareader.Read()) {
                List<string> row = new List<string> { sqlite_datareader.GetInt32(0).ToString(), sqlite_datareader.GetString(1), sqlite_datareader.GetString(2), sqlite_datareader.GetString(3) };

                table.Add(row);
              
                //equipment.Add(new Tuple<string, string>(sqlite_datareader.GetString(0), sqlite_datareader.GetString(1)));
            }

            return table;

        }

        public List<List<string>> GetTableEquipment() {
            //DataTable dt = new DataTable();
            SQLiteDataReader sqlite_datareader;
            SQLiteCommand sqlite_cmd;
            sqlite_cmd = _sqlite_conn.CreateCommand();
            sqlite_cmd.CommandText = "SELECT * FROM equipment";
            sqlite_datareader = sqlite_cmd.ExecuteReader();

            List<List<string>> table = new List<List<string>>();
            while (sqlite_datareader.Read()) {
                List<string> row = new List<string> { sqlite_datareader.GetInt32(0).ToString(), sqlite_datareader.GetString(1), sqlite_datareader.GetString(2) };

                table.Add(row);

                //equipment.Add(new Tuple<string, string>(sqlite_datareader.GetString(0), sqlite_datareader.GetString(1)));
            }

            return table;

        }

        public List<List<string>> GetTableStage() {
            //DataTable dt = new DataTable();
            SQLiteDataReader sqlite_datareader;
            SQLiteCommand sqlite_cmd;
            sqlite_cmd = _sqlite_conn.CreateCommand();
            sqlite_cmd.CommandText = "SELECT * FROM stage";
            sqlite_datareader = sqlite_cmd.ExecuteReader();

            List<List<string>> table = new List<List<string>>();
            while (sqlite_datareader.Read()) {
                List<string> row = new List<string> { sqlite_datareader.GetInt32(0).ToString(), sqlite_datareader.GetString(2) };

                table.Add(row);

                //equipment.Add(new Tuple<string, string>(sqlite_datareader.GetString(0), sqlite_datareader.GetString(1)));
            }

            return table;

        }

        public void UpdateFinalProduct(string id, string name, string designation, string area) {
            //SQLiteDataReader sqlite_datareader;
            SQLiteCommand sqlite_cmd;
            sqlite_cmd = _sqlite_conn.CreateCommand();
            sqlite_cmd.CommandText = "UPDATE final_product SET name = @name, designation = @designation, " +
                "application_area = @area WHERE id_final_product = @id";
            sqlite_cmd.Parameters.Add(new SQLiteParameter("@id", id));
            sqlite_cmd.Parameters.Add(new SQLiteParameter("@name", name));
            sqlite_cmd.Parameters.Add(new SQLiteParameter("@designation", designation));
            sqlite_cmd.Parameters.Add(new SQLiteParameter("@area", area));
            sqlite_cmd.ExecuteNonQuery();
        }
    }
}
