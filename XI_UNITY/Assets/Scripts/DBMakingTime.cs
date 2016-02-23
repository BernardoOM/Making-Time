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
        //#IMPORTANT  
		//for Mac build , "/Resources/Data/StreamingAssets/".
		//for iOS build, _constr = "URI=file:" + Application.dataPath + "/Raw/" + p;
		//for Android build Application.persistentDataPath + "/StreamingAssets/" + p;
		//for other build, search for application.datapath for detail path. 
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

	public static void ReadRandomNewCommitment(string event_Type, ref string name, ref int time_Length,
	                                           ref int maxTime, ref int minTime)
	{
		OpenDB("MakingTime.db");
		_dbcm = _dbc.CreateCommand();

		sqlQuery = "select count(*) as NumberOfRegions from Event_Type where Category = '" + event_Type + "'";
		_dbcm.CommandText = sqlQuery;
		Int32 rows = Convert.ToInt32(_dbcm.ExecuteScalar());

		sqlQuery = "select * from Event_Type where Category = '" + event_Type + "'";
		_dbcm.CommandText = sqlQuery;
		_dbr = _dbcm.ExecuteReader();

		int commitmentRow = UnityEngine.Random.Range(0, rows);
		//Question 
		for(int countRows = 0; countRows < commitmentRow + 1; countRows += 1)
		{	
			_dbr.Read();	
		}

		name = _dbr.GetString(0);
		time_Length = _dbr.GetInt32(1);
		maxTime = _dbr.GetInt32(8);
		minTime = _dbr.GetInt32(7);

		_dbr.Close();
		_dbr = null;
		_dbcm.Dispose();
		_dbcm = null;
		_dbc.Close();
		_dbc = null;
	}

	/*
	public static void AddCommitmentToCalendar()
	{
		
	}
	*/

	public static int[] ChangeStatus(string event_Name, int time_Length)
	{
//		string evt = event_Name;
//		string category;
//		int egy_b=-9;
//		int hpy_b;
		int[] values=new int[2]; 
	
		OpenDB("MakingTime.db");
		_dbcm = _dbc.CreateCommand();

		sqlQuery = "select * from Event_Type where pk_Event_Name = '" + event_Name + "' " + "AND pk_Time_Length = '" + time_Length + "'" ;

		//where Event_Name = 'Teach a Class'
		_dbcm.CommandText = sqlQuery;
		_dbr = _dbcm.ExecuteReader();

		while (_dbr.Read())
		{
			values [0] = _dbr.GetInt32 (4);
			values [1] = _dbr.GetInt32 (6); 
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

	public static bool CheckHasScene(string event_Name, int time_Length)
	{
		bool hasScene = false;

		OpenDB("MakingTime.db");
		_dbcm = _dbc.CreateCommand();

		sqlQuery = "select * from Event_Type where pk_Event_Name = '" + event_Name + "' " +"AND pk_Time_Length = '" + time_Length + "'" ;

		//where Event_Name = 'Teach a Class'
		_dbcm.CommandText = sqlQuery;
		_dbr = _dbcm.ExecuteReader();

		while (_dbr.Read())
		{
			if(_dbr.GetInt32(9) == 0)
			{	hasScene = false;	}
			else
			{	hasScene = true;	}
		}

		_dbr.Close();
		_dbr = null;
		_dbcm.Dispose();
		_dbcm = null;
		_dbc.Close();
		_dbc = null;

		return hasScene;
	}
}
