using BusinessObject.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public class MemberRepository:IMemberRepository
    {
        public void Delete(Member member) => MemberDAO.Instance.Delete(member);

        public Member GetMemberById(int? v) => MemberDAO.Instance.GetMembersByID(v);

        public List<Member> GetMembers() => MemberDAO.Instance.GetMembers();

        public int GetLatestID() => MemberDAO.Instance.GetLatestID();

        public void Add(Member member) => MemberDAO.Instance.Add(member);

        public void Update(Member member) => MemberDAO.Instance.Update(member);

        public Member login(string email, string pass) => MemberDAO.Instance.login(email, pass);

    }
}
