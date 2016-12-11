using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSV.Repo
{
    public class Database
    {
        string connectionPath = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source = C:\\Users\\VM\\Downloads\\Data\\hq.mdb";
        OleDbConnection accessConnection = null;

        public Database()
        {
            
        }

        private void OpenConnection(string accessConnectionPath)
        {
            try
            {
                accessConnection = new OleDbConnection(accessConnectionPath);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: Failed to create a database connection. \n{0}", ex.Message);
            }
        }

        public void QueryAndBuild()
        {
            string querySelect = "SELECT * FROM ShopStaff";
            
            DataSet data = new DataSet();
            OpenConnection(connectionPath);
            try
            {
                OleDbCommand myAccessCommand = new OleDbCommand(querySelect, accessConnection);
                OleDbDataAdapter myDataAdapter = new OleDbDataAdapter(myAccessCommand);

                myDataAdapter.Fill(data);

                DataTableCollection dta = data.Tables;

                WriteToCsvFile(data.Tables[0], "C:\\Users\\VM\\Downloads\\output.csv");
                myDataAdapter.Dispose();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: Failed to retrieve the required data from the DataBase.\n{0}", ex.Message);
                return;
            }
            finally
            {
                accessConnection.Close();
            }
        }

        private void WriteToCsvFile(DataTable dataTable, string filePath)
        {
            StringBuilder fileContent = new StringBuilder();

            foreach (var col in dataTable.Columns)
            {
                fileContent.Append(col.ToString() + ",");
            }

            fileContent.Replace(",", System.Environment.NewLine, fileContent.Length - 1, 1);

            foreach (DataRow dr in dataTable.Rows)
            {
                foreach (var column in dr.ItemArray)
                {
                    fileContent.Append("\"" + column.ToString() + "\",");
                }

                fileContent.Replace(",", System.Environment.NewLine, fileContent.Length - 1, 1);
            }

            System.IO.File.WriteAllText(filePath, fileContent.ToString());
        }
    }
    
}

