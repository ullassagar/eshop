using System;
using System.Data;
using DataLayer;
using MySql.Data.MySqlClient;

namespace RepositoryLayer
{
    public class MemberRepository
    {
        public static Member GetMember(int MemberId)
        {
            Member member = null;

            var sql = string.Format(@"SELECT MemberId, MemberName, EmailAddress, Password FROM members WHERE MemberId={0}", MemberId);

            using (var reader = MysqlRepository.ExecuteReader(MysqlRepository.ConnectionString_ReadOnly, CommandType.Text, sql))
            {
                if (reader.Read())
                {
                    member = Member.Load(reader);
                }
            }

            return member;
        }

        public static Member GetMember(string emailAddress, string password)
        {
            Member member = null;

            var sql = string.Format(@"SELECT MemberId, MemberName, EmailAddress, Password FROM Members WHERE EmailAddress='{0}' And Password='{1}'", emailAddress, password);

            using (var reader = MysqlRepository.ExecuteReader(MysqlRepository.ConnectionString_ReadOnly, CommandType.Text, sql))
            {
                if (reader.Read())
                {
                    member = Member.Load(reader);
                }
            }

            return member;
        }

        public static ErrorCode AddMember(Member member)
        {
            var sql = string.Format(@"SELECT MemberId FROM Members WHERE EmailAddress='{0}'", member.EmailAddress);
            var id = Convert.ToInt32(MysqlRepository.ExecuteScalar(MysqlRepository.ConnectionString_Writable, sql, null));
            if (id > 0)
            {
                return ErrorCode.ErrorWhileMemberRegistrationEmailAlreadyExist;
            }

            sql = string.Format(@"INSERT INTO Members(MemberName, EmailAddress, Password)
            VALUES('{0}','{1}','{2}');Select last_insert_id();", member.MemberName, member.EmailAddress, member.Password);

            member.MemberId = Convert.ToInt32(MysqlRepository.ExecuteScalar(MysqlRepository.ConnectionString_Writable, sql, null));
            return ErrorCode.None;
        }

        public static void Update(Member member)
        {
            var sql = string.Format(@"UPDATE members SET MemberName='{0}', EmailAddress='{1}', Password='{2}' WHERE MemberId={3}",
                member.MemberName , member.EmailAddress , member.Password , member.MemberId);

            MysqlRepository.ExecuteNonQueryAndCloseConnection(MysqlRepository.ConnectionString_Writable, sql, null);
        }

    }
}
