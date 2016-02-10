using UnityEngine;
using System.Collections;
using Mono.Data.Sqlite;
using System.Data;
using System;

public class DBMakingTime : MonoBehaviour
{
	private static string _constr;// = "URI=file:" + Application.persistentDataPath + "/MakingTime.db";//Path to database.
	private static IDbConnection _dbc;
	private static IDbCommand _dbcm;
	private static IDataReader _dbr;
	private static string sqlQuery;

	public int[] current_val=new int[2]{0,0}; 

	public static void OpenDB(string p)
	{
        _constr = "URI=file:" + Application.dataPath + "/StreamingAssets/" + p; // we set the connection to our database
        //Debug.Log(_constr);
       _dbc = new SqliteConnection(_constr);
       _dbc.Open();//Open connection to the database.
    }

    // Use this for initialization
    void Start()
    {


    }
    // Update is called once per frame
    void Update()
    {

    }

	public static int[] Select_DB(string event_name, int Time_Length)
	{
//		string evt = event_Name;
		string category;
		int egy_b=-9;
		int hpy_b;
		int[] values=new int[2]; 
	
		OpenDB("MakingTime.db");
		_dbcm = _dbc.CreateCommand();

		sqlQuery = "select * from Event_Type where pk_Event_Name = '" +event_name+ "' " +"where pk_Time_Length = '"+Time_Length+"'" ;

		//where Event_Name = 'Teach a Class'
		_dbcm.CommandText = sqlQuery;
		_dbr = _dbcm.ExecuteReader();

		while (_dbr.Read())
		{
			values [0] = _dbr.GetInt32 (5);
			values [1] = _dbr.GetInt32 (7); 
		}

		_dbr.Close();
		_dbr = null;
		_dbcm.Dispose();
		_dbcm = null;
		_dbc.Close();
		_dbc = null;
		//return egy_b;

		return values;
	}
}
