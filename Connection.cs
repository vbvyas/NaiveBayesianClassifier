using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Configuration;

namespace NaiveBayesianClassifier
{
    public static class Connection
    {
        private static SqlConnection _dataMining = new SqlConnection(ConfigurationManager.ConnectionStrings["DataMining"].ConnectionString);
        public static SqlConnection DataMining { get { return _dataMining; } }
    }
}
