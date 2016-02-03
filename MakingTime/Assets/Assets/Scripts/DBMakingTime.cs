using UnityEngine;
using System.Collections;
using Mono.Data.SqliteClient;
using System.Data;
using System;

public class DBMakingTime : MonoBehaviour {
    private string _constr = "URI=file:" + Application.dataPath + "/DataBase/MakingTime.db";//Path to database.
    private IDbConnection _dbc;
    private IDbCommand _dbcm;
    private IDataReader _dbr;
    string sqlQuery;

    // Use this for initialization
    void Start()
    {
        _dbc = (IDbConnection)new SqliteConnection(_constr);
        _dbcm = _dbc.CreateCommand();
        _dbc.Open(); //Open connection to the database.

        //Insert data 
        sqlQuery = "INSERT INTO Event (name,current_day,current_time,min_day,max_day,min_time,max_time,scheduled,creator,type) VALUES (Date,1,1,0,3,2,3,1,Robert,Social)";
        _dbcm.CommandText = sqlQuery;
        _dbcm.ExecuteNonQuery();

        sqlQuery = "INSERT INTO Event (name,current_day,current_time,min_day,max_day,min_time,max_time,scheduled,creator,type) VALUES (Party,1,1,0,3,2,3,1,Gina,Social)";
        _dbcm.CommandText = sqlQuery;
        _dbcm.ExecuteNonQuery();

        sqlQuery = "INSERT INTO Event (name,current_day,current_time,min_day,max_day,min_time,max_time,scheduled,creator,type) VALUES (Movie,1,1,0,3,2,3,1,Robert,Social)";
        _dbcm.CommandText = sqlQuery;
        _dbcm.ExecuteNonQuery();

        sqlQuery = "select * from Event";
        _dbcm.CommandText = sqlQuery;
        _dbr = _dbcm.ExecuteReader();

        while (_dbr.Read())
        {
            Debug.Log("****** Data: " + _dbr["name"] + "\tday: " + _dbr["current_day"] + "\tTime: " + _dbr["current_time"] + "\tMin Day: " + _dbr["min_day"]
                + "\tMax Day: " + _dbr["max_day"] + "\tMin Time: " + _dbr["min_time"] + "\tMax Time: " + _dbr["max_time"] + "\tScheduled: " + _dbr["scheduled"]
                + "\tcreator: " + _dbr["creator"] + "\ttype: " + _dbr["type"]);
        }
        _dbr.Close();
        _dbr = null;
        _dbcm.Dispose();
        _dbcm = null;
        _dbc.Close();
        _dbc = null;
    }
    // Update is called once per frame
    void Update()
    {

    }
}
