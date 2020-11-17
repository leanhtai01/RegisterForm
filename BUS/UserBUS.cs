using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO;
using DAO;

namespace BUS
{
    public class UserBUS
    {
        UserDAO userDAO;

        public UserBUS()
        {
            userDAO = new UserDAO();
        }

        public bool AddUser(UserDTO user)
        {
            EncryptPassword encryptPassword = new EncryptPassword();

            user.Password = encryptPassword.GenerateHash(user.Password, encryptPassword.CreateSalt(10));

            return userDAO.AddUser(user);
        }

        public bool IsExistUsername(string username)
        {
            return userDAO.IsExistUsername(username);
        }

        public List<UserDTO> GetUsers()
        {
            return userDAO.GetUsers();
        }

        public int CreateID()
        {
            List<UserDTO> users = GetUsers();

            users.Sort((u1, u2) => -u1.ID.CompareTo(u2.ID));
            int id = users[0].ID;
            ++id;

            return id;
        }
    }
}
