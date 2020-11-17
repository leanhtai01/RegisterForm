using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO;

namespace DAO
{
    public class UserDAO
    {
        DataAccess dataAccess;

        public UserDAO()
        {
            dataAccess = new DataAccess();
        }

        public UserDTO Read(SqlDataReader sqlDataReader)
        {
            return new UserDTO
            {
                ID = sqlDataReader.GetInt32(0),
                Username = sqlDataReader.GetString(1),
                Password = sqlDataReader.GetString(2),
                Name = sqlDataReader.GetString(3),
                Email = sqlDataReader.GetString(4),
                BirthDate = sqlDataReader.GetDateTime(5),
                Permission = sqlDataReader.GetInt32(6)
            };
        }

        public List<UserDTO> GetUsers()
        {
            List<UserDTO> users = new List<UserDTO>();
            string queryString = @"SELECT * FROM [QLBH].[dbo].[Users]";
            SqlDataReader sqlDataReader = dataAccess.ExecuteReader(queryString);

            while (sqlDataReader.Read())
            {
                users.Add(Read(sqlDataReader));
            }

            dataAccess.CloseConnection();

            return users;
        }

        /// <summary>
        /// add user to database
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public bool AddUser(UserDTO user)
        {
            string queryString =
                    @"INSERT INTO [QLBH].[dbo].[Users](f_Username, f_Password, f_Name, f_Email, f_DOB, f_Permission)" +
                    @" VALUES (@Username, @Password, @Name, @Email, @BirthDate, @Permission)";
            SqlParameter[] sqlParameters = new SqlParameter[7];

            sqlParameters[0] = new SqlParameter("@ID", SqlDbType.Int);
            sqlParameters[0].Value = user.ID;
            sqlParameters[1] = new SqlParameter("@Username", SqlDbType.NVarChar);
            sqlParameters[1].Value = user.Username;
            sqlParameters[2] = new SqlParameter("@Password", SqlDbType.NVarChar);
            sqlParameters[2].Value = user.Password;
            sqlParameters[3] = new SqlParameter("@Name", SqlDbType.NVarChar);
            sqlParameters[3].Value = user.Name;
            sqlParameters[4] = new SqlParameter("@Email", SqlDbType.NVarChar);
            sqlParameters[4].Value = user.Email;
            sqlParameters[5] = new SqlParameter("@BirthDate", SqlDbType.DateTime);
            sqlParameters[5].Value = user.BirthDate;
            sqlParameters[6] = new SqlParameter("@Permission", SqlDbType.Int);
            sqlParameters[6].Value = user.Permission;

            return dataAccess.ExecuteNonQuery(queryString, sqlParameters);
        }

        /// <summary>
        /// check whether Username is already exist
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public bool IsExistUsername(string username)
        {
            string queryString =
                @"SELECT 1 FROM [QLBH].[dbo].[Users] WHERE f_Username = @Username";
            SqlParameter[] sqlParameters = new SqlParameter[1];

            sqlParameters[0] = new SqlParameter("@Username", SqlDbType.NVarChar);
            sqlParameters[0].Value = username;

            return dataAccess.ExecuteScalar(queryString, sqlParameters) == null ? false : true;
        }
    }
}
