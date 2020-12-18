﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WpfAppLab7
{
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Data.SqlClient;
 

    public partial class Table
    {
        public int Id { get; set; }
        public string FIO { get; set; }
        //public int ServiceSite { get; set; }
        //public int Phonenumber { get; set; }
        //public Nullable<int> ServiceSite { get; set; }
        //public Nullable<int> Phonenumber { get; set; }
        public int? ServiceSite { get; set; }
        public int? Phonenumber { get; set; }
        public string Diagnosis { get; set; }
        public int C_Age { get; set; }
        public string Data_last_visit { get; set; }
        public string Doctor { get; private set; }

        static SqlConnection connection;

        public Table()
        {
            var connString = ConfigurationManager
           .ConnectionStrings["MyLocalDbEntities"]
           .ConnectionString;
            //	Создание объекта подключения
            connection = new SqlConnection(connString);
            //connectionString = "metadata=res://*/MyModel.csdl|res://*/MyModel.ssdl|res://*/MyModel.msl;

        }
        static Table()
        {
            var connString = ConfigurationManager
           .ConnectionStrings["MyLocalDbEntities"]
           .ConnectionString;
            //	Создание объекта подключения
            connection = new SqlConnection(connString);
        }
        public override string ToString()
        {

            return String.Format("Информация о пациенте\nФИО: {0}\nУчасток: {1} \n Возраст:{2} лет\n \n Диагноз: {3}\n Номер телефона: {4}\n Дата последнего визита: {5}\n ", FIO, ServiceSite, C_Age,  Diagnosis, Phonenumber, Data_last_visit);
        }
        //	Получение списка всех пациентов
        public static IEnumerable<Table> GetAllTables()
        {
            var commandString = "SELECT FIO, ServiceSite, C_Age, Phonenumber, Diagnosis, Data_last_visit, FROM Tables ";
            SqlCommand getAllCommand = new SqlCommand(commandString, connection);
            connection.Open();
            var reader = getAllCommand.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    var id = reader.GetInt32(0);
                    var fio = reader.GetString(1);
                    var serviceSite = reader.GetSqlInt16(2);
                    var c_age = reader.GetInt16(3);
                    var phonenumber = reader.GetInt32(4);                   
                    var diagnosis = reader.GetString(5);
                    var data_last_visit = reader.GetString(6);
                   
                   
                    var _table = new Table
                    {
                        Id = id,
                        FIO = fio,
                        ServiceSite = serviceSite != 0 ? serviceSite.Value :0,
                        //ServiceSite = serviceSite.HasValue ? serviceSite.Value : 0,
                        C_Age = c_age,
                        Phonenumber = phonenumber,                      
                        Diagnosis = diagnosis,
                        Data_last_visit = data_last_visit                        
                    };
                    yield return _table;
                }
            };
            connection.Close();
        }
        //Добавление новой записи в базу данных
        public void Insert()
        {
            var commandString = "INSERT INTO Tables (FIO, ServiceSite, С_Age, Phonenumber,Diagnosis, Data_last_visit)" + "VALUES(@)fio, @serviceSite, @с_age, @phonenumber, @diagnosis, @data_last_visit)";
            SqlCommand insertCommand = new SqlCommand(commandString, connection);
            insertCommand.Parameters.AddRange(new SqlParameter[]
            {
                new SqlParameter("fio",FIO),
                new SqlParameter("serviceSite",ServiceSite),
                new SqlParameter("с_age",C_Age),
                new SqlParameter("phonenumber", Phonenumber),
               new SqlParameter("diagnosis",Diagnosis),
                new SqlParameter("data_last_visit ",Data_last_visit)
                
            });
            connection.Open();
            insertCommand.ExecuteNonQuery();
            connection.Close();
        }

        // изменение в текущей базе данных
        public void Update()
        {
            var commandString = "UPDATE Tables SET FIO=@fio, ServiceSite=@serviceSite, С_Age=@с_age, Phonenumber=@phonenumber,Diagnosis=@diagnosis, Data_last_visit=@data_last_visit  WHERE(Id =@id)";

            SqlCommand updateCommand = new SqlCommand(commandString,
            connection);

            updateCommand.Parameters.AddRange(new SqlParameter[] { new SqlParameter("fio", FIO),
             new SqlParameter("serviceSite", ServiceSite),
             new SqlParameter("c_age",C_Age),
             new SqlParameter("phonenumber", Phonenumber),
             new SqlParameter("diagnosis", Diagnosis),
             new SqlParameter("data_last_visit", Data_last_visit),
             new SqlParameter("id",Id), });

            connection.Open();

            updateCommand.ExecuteNonQuery();
            connection.Close();
        }
        //удаление из базы данных
        public static void Delete(int id)
        {

            var commandString = "DELETE FROM Tables WHERE(Id=@id)";

            SqlCommand deleteCommand = new SqlCommand(commandString,
            connection);

            deleteCommand.Parameters.AddWithValue("id", id);
            connection.Open();
            deleteCommand.ExecuteNonQuery();
            connection.Close();
        }
        public static IEnumerable<Table> Search(string _serviceSite)
        {
            var commandString = "SELECT Pacient_ID, FIO, ServiceSite, Age,Phonenumber,  Diagnosis, Data_last_visit FROM Tables WHERE Age = " + _serviceSite;
            Table s = new Table();
            SqlCommand search = new SqlCommand(commandString, Table.connection);
            Table.connection.Open();
            var reader = search.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    var id = reader.GetInt32(0);
                    var fio = reader.GetString(1);
                    var serviceSite = reader.GetInt32(2);
                    var c_age = reader.GetInt16(3);                    
                    var diagnosis = reader.GetString(4);
                    var phonenumber = reader.GetInt32(5);
                    var data_last_visit = reader.GetString(6);
                    
                    var p = new Table
                    {
                        Id = id,
                        FIO = fio,
                        ServiceSite = serviceSite,
                        C_Age = c_age,                       
                        Diagnosis = diagnosis,
                        Phonenumber = phonenumber,
                        Data_last_visit = data_last_visit
                        
                    };
                    yield return p;
                }
            };
            connection.Close();
        }
    }
}
