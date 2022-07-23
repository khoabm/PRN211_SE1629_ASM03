using BusinessObject.DataAccess;
using DataAccess.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace eStore.Controllers
{
    public class MemberController : Controller
    {
        private IMemberRepository _memberRepository;
        public MemberController(IMemberRepository memberRepository)
        {
            this._memberRepository = memberRepository;
        }

        // GET: MemberController
        public ActionResult Index()
        {
            List<Member> members = _memberRepository.GetMembers().ToList();
            return View(members);
        }

        // GET: MemberController/Details/5
        public ActionResult Details(int id)
        {
            Member member = _memberRepository.GetMemberById(id);
            return View(member);
        }

        // GET: MemberController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: MemberController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Member member)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _memberRepository.Add(member);
                }
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.Message=ex.Message;
                return View();
            }
        }

        // GET: MemberController/Edit/5
        public ActionResult Edit(int id)
        {
            var member = _memberRepository.GetMemberById(id);
            if (member != null)
                return View(member);
            else return NotFound();
        }

        // POST: MemberController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Member member)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _memberRepository.Update(member);
                }
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.Message = ex.Message;
                return View();
            }
        }

        // GET: MemberController/Delete/5
        [HttpGet]
        public ActionResult Delete(int id)
        {
            var member = _memberRepository.GetMemberById(id);
            if (member != null)
                return View(member);
            else return NotFound();
        }

        // POST: MemberController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Delete")]
        public ActionResult Del(int id)
        {
            try
            {
                _memberRepository.Delete(_memberRepository.GetMemberById(id));
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
