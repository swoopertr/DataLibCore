using CommonLib.Data;
using DataModel;

namespace DataManager
{
    public class MovieManager : DapperRepo<Movies>
    {
        public MovieManager() 
            : base(@"workstation id=xotixsystem.mssql.somee.com;packet size=4096;user id=swooper_SQLLogin_1;pwd=p4nf5dc7p6;data source=xotixsystem.mssql.somee.com;persist security info=False;initial catalog=xotixsystem")
        {
        }

    }
}
