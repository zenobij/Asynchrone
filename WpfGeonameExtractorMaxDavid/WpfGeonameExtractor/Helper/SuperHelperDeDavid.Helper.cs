using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WpfGeonameExtractor.Helper
{
    public static class SuperHelperDeDavid
    {
        public static void AjoutDB(string CheminFichier, string txtServ, string txtDB, string txtTable)
        {
            if (txtTable != null && CheminFichier != null && txtServ != null && txtDB != null)
            {
                SqlConnection myCn;
                SqlCommand myCmdSelectTableName;
                string connection = "Server=" + txtServ + ";Database=" + txtDB + ";Trusted_connection=True;";
                string insert = null;
                int nbElem = 0;
                StringBuilder TableName = new StringBuilder();
                using (myCn = new SqlConnection())
                {
                    myCn.ConnectionString = connection;
                    myCn.Open();
                    SqlDataReader myRdr;
                    using (myCmdSelectTableName = new SqlCommand())
                    {

                        myCmdSelectTableName.Connection = myCn;
                        myCmdSelectTableName.CommandText = "select Column_name from INFORMATION_SCHEMA.COLUMNS where TABLE_NAME ='" + txtTable + "';";
                        myRdr = myCmdSelectTableName.ExecuteReader();
                        while (myRdr.Read())
                        {
                            TableName.Append(myRdr.GetString(0) + ",");
                            nbElem++;
                        }
                        TableName.Length -= 1;
                    }
                }

                insert = "INSERT INTO " + txtTable + " (" + TableName + ") VALUES";

                using (myCn = new SqlConnection())
                {
                    myCn.ConnectionString = connection;
                    myCn.Open();
                    SqlCommand myCmd;
                    StringBuilder myStringBuilder = new StringBuilder();
                    using (myCmd = new SqlCommand())
                    {

                        if (CheminFichier != null)
                        {
                            using (StreamReader myStream = new StreamReader(CheminFichier))
                            {
                                String line;
                                int i = 0;
                                myStringBuilder.Append(insert);
                                myCmd.Connection = myCn;
                                while ((line = myStream.ReadLine()) != null)
                                {
                                    i++;
                                    line = line.Replace("'", "''");
                                    String[] stringsValue = line.Split('\t');
                                    BuildValues(myStringBuilder, stringsValue, nbElem);

                                    if (i == 999)//Pas plus de 1000 insert à la fois
                                    {
                                        InsertIntoDB(insert, myCmd, myStringBuilder);
                                        CleanBuffer(insert, myCmd, myStringBuilder);
                                        i = 0;
                                    }

                                }
                                if (i != 0)
                                {
                                    InsertIntoDB(insert, myCmd, myStringBuilder);
                                }
                                MessageBox.Show("Ajout terminé avec succès");
                            }
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Ficher ou table non indiqué !");
            }
        }
        private static void InsertIntoDB(string insert, SqlCommand myCmd, StringBuilder myStringBuilder)
        {
            myStringBuilder.Length -= 1;
            myStringBuilder.Append(";");
            myCmd.CommandText = myStringBuilder.ToString();
            myCmd.ExecuteNonQuery();

        }
        private static void CleanBuffer(string insert, SqlCommand myCmd, StringBuilder myStringBuilder)
        {
            myStringBuilder.Length = 0;
            myStringBuilder.Append(insert);
            myCmd.CommandText = "";
        }

        private static void BuildValues(StringBuilder myStringBuilder, string[] stringsValue, int nbElem)
        {
            myStringBuilder.Append("('" + stringsValue[0] + "'");
            for (int j = 1; j < nbElem; j++)
            {
                myStringBuilder.Append(",'" + stringsValue[j] + "'");
            }
            myStringBuilder.Append("),");
        }

    }
}
