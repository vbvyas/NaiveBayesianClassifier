using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.IO;
using System.Data.SqlClient;
using System.Data;

namespace NaiveBayesianClassifier
{
    class Insert
    {
        public Insert()
        {
            string train = "";
            string test = "";
            try
            {
                train = ConfigurationManager.AppSettings.Get(Constants.train);
                test = ConfigurationManager.AppSettings.Get(Constants.test);
            }
            catch (Exception e)
            {
                Console.WriteLine("Input files not found");
                return;
            }

            try
            {
                using (StreamReader sr = new StreamReader(train))
                {
                    while (!sr.EndOfStream)
                    {
                        string line = sr.ReadLine();
                        InsertIntoDatabase(Constants.train, line);
                    }
                }
                using (StreamReader sr = new StreamReader(test))
                {
                    while (!sr.EndOfStream)
                    {
                        string line = sr.ReadLine();
                        InsertIntoDatabase(Constants.test, line);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Cannot read file: " + train);
                Console.WriteLine(e.Message);
            }
        }

        private void InsertIntoDatabase(string type, string line)
        {
            string existingCheckingAccount = "";
            int duration = 0;
            string creditHistory = "";
            string purpose = "";
            int creditAmount = 0;
            string savingAccount = "";
            string employment = "";
            int installmentRate = 0;
            string status = "";
            string debtors = "";
            int resident = 0;
            string property = "";
            int age = 0;
            string installmentPlan = "";
            string housing = "";
            int existingCredits = 0;
            string job = "";
            int liablePeople = 0;
            string telephone = "";
            string foreignWorker = "";
            int classLabel = 0;

            char[] spliter = { ' ', '\n', '\r', '\t' };
            string[] values = line.Split(spliter);

            existingCheckingAccount = values[0];
            duration = int.Parse(values[1]);
            creditHistory = values[2];
            purpose = values[3];
            creditAmount = int.Parse(values[4]);
            savingAccount = values[5];
            employment = values[6];
            installmentRate = int.Parse(values[7]);
            status = values[8];
            debtors = values[9];
            resident = int.Parse(values[10]);
            property = values[11];
            age = int.Parse(values[12]);
            installmentPlan = values[13];
            housing = values[14];
            existingCredits = int.Parse(values[15]);
            job = values[16];
            liablePeople = int.Parse(values[17]);
            telephone = values[18];
            foreignWorker = values[19];

            if (values.Length > 20)
                classLabel = int.Parse(values[20]);

            try
            {
                string storeProc = "dbo.InsertIntoCreditTable";

                using (SqlCommand command = new SqlCommand())
                {
                    Connection.DataMining.Open();
                    command.Connection = Connection.DataMining;
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = storeProc;
                    command.Parameters.Add("@dataType", SqlDbType.VarChar, 5).Value = type;
                    command.Parameters.Add("@existingCheckingAccount", SqlDbType.VarChar, 3).Value = existingCheckingAccount;
                    command.Parameters.Add("@duration", SqlDbType.Int).Value = duration;
                    command.Parameters.Add("@creditHistory", SqlDbType.VarChar, 3).Value = creditHistory;
                    command.Parameters.Add("@purpose", SqlDbType.VarChar, 4).Value = purpose;
                    command.Parameters.Add("@creditAmount", SqlDbType.Int).Value = creditAmount;
                    command.Parameters.Add("@savingAccount", SqlDbType.VarChar, 3).Value = savingAccount;
                    command.Parameters.Add("@employment", SqlDbType.VarChar, 3).Value = employment;
                    command.Parameters.Add("@installmentRate", SqlDbType.Int).Value = installmentRate;
                    command.Parameters.Add("@status", SqlDbType.VarChar, 3).Value = status;
                    command.Parameters.Add("@debtors", SqlDbType.VarChar, 3).Value = debtors;
                    command.Parameters.Add("@resident", SqlDbType.Int).Value = resident;
                    command.Parameters.Add("@property", SqlDbType.VarChar, 4).Value = property;
                    command.Parameters.Add("@age", SqlDbType.Int).Value = age;
                    command.Parameters.Add("@installmentPlan", SqlDbType.VarChar, 4).Value = installmentPlan;
                    command.Parameters.Add("@housing", SqlDbType.VarChar, 4).Value = housing;
                    command.Parameters.Add("@existingCredits", SqlDbType.Int).Value = existingCredits;
                    command.Parameters.Add("@job", SqlDbType.VarChar, 4).Value = job;
                    command.Parameters.Add("@liablePeople", SqlDbType.Int).Value = liablePeople;
                    command.Parameters.Add("@telephone", SqlDbType.VarChar, 4).Value = telephone;
                    command.Parameters.Add("@foreignWorker", SqlDbType.VarChar, 4).Value = foreignWorker;
                    command.Parameters.Add("@classLabel", SqlDbType.Int).Value = classLabel;
                    command.ExecuteNonQuery();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception during insert");
                Console.WriteLine(e.ToString());
            }
            finally
            {
                if (Connection.DataMining != null)
                    Connection.DataMining.Close();
            }
        }
    }
}
