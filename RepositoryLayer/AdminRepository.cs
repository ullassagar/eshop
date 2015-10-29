﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer;
using System.Data;

namespace RepositoryLayer
{
    public class AdminRepository
    {
        public static User GetUser(int UserId)
        {
            User user = null;

            var sql = string.Format(@"SELECT UserId, UserName, Password, EmailId FROM users WHERE UserId={0}", UserId);

            using (var reader = MysqlRepository.ExecuteReader(MysqlRepository.ConnectionString_ReadOnly, CommandType.Text, sql))
            {
                if (reader.Read())
                {
                    user = User.Load(reader);
                }
            }

            return user;
        }


        public static User GetUser(string emailAddress, string password)
        {
            User user = null;

            var sql = string.Format(@"SELECT UserId, UserName, Password, EmailId FROM users WHERE EmailId='{0}' And Password='{1}'", emailAddress, password);

            using (var reader = MysqlRepository.ExecuteReader(MysqlRepository.ConnectionString_ReadOnly, CommandType.Text, sql))
            {
                if (reader.Read())
                {
                    user = User.Load(reader);
                }
            }

            return user;
        }

    }
}
