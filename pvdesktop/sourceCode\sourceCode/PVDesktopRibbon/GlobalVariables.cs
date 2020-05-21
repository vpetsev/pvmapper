using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.OleDb;   

namespace PVDESKTOP
{
    class GlobalVariables
    {
        static string _ProjectDB;
        static string _ODDemandDB;
        static string _NetworkDB;
        static OleDbConnection _ODDemandDBConnection;
        static OleDbConnection _ProjectDBConnection;
        static OleDbConnection _NetworkDBConnection;

        public static string ProjectDB 
        { 
            get{ return _ProjectDB; } 
            set{_ProjectDB=value;}
        }

        public static string ODDemandDB 
        {
            get { return _ODDemandDB; }
            set { _ODDemandDB = value; }
        }
        
        public static string NeworkDB
        {
            get { return _NetworkDB; }
            set { _NetworkDB = value; }
        }

        public static OleDbConnection ProjectDBConnection
        {
            get { return _ProjectDBConnection; }
            set { _ProjectDBConnection = value; }
        }
        public static OleDbConnection ODDemandDBConnection
        {
            get { return _ODDemandDBConnection; }
            set { _ODDemandDBConnection = value; }
        }

        public static OleDbConnection NetworkDBConnection
        {
            get { return _NetworkDBConnection; }
            set { _NetworkDBConnection = value; }
        }
    }
}
