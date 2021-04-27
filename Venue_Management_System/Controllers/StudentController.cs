using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using Venue_Management_System.Models;
using Venue_Management_System.ViewModels;

namespace Venue_Management_System.Controllers
{
    public class StudentController : Controller
    {
        private ApplicationDbContext _context;

        public StudentController()
        {
            _context = new ApplicationDbContext();
        }
        // GET: Student
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult MyGroups()
        {

            var userId = User.Identity.GetUserId();
            var listOfMyGroupsId = _context.GroupMembers.Where(s => s.UserId == userId).ToList();
            var distinctMyGroupId = listOfMyGroupsId.Distinct(new DistinctItemComparer()).ToList();
            var onlyId = distinctMyGroupId.Where(s => s.UserId == userId).Select(g => g.GroupId).ToList();
            var groupMember = _context.GroupMembers.Include(s => s.Student).Include(m => m.Group).Where(g => onlyId.Contains(g.GroupId)).ToList();
            var myGroup = _context.Groups.Include(s => s.Student).Include(m => m.GroupMembers).Where(g => onlyId.Contains(g.Id)).ToList();

            return View(myGroup);
        }

        public ActionResult CreateGroup()
        {
            //var student = _context.Students.ToList();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateGroup(Group group)
        {
            //validate name no need to create two group with the same
            var userId = User.Identity.GetUserId();

            if (userId == null)
                return HttpNotFound();

            if (!ModelState.IsValid)
            {
                return View(group);
            }
            group.UserId = userId;
            _context.Groups.Add(group);
            _context.SaveChanges();

            var groupId = group.Id;
            var groupOwner = new GroupMember
            {
                GroupId = groupId,
                UserId = userId
            };
            //check if the user is not already added on the groupmember table on the same group
            _context.GroupMembers.Add(groupOwner);
            _context.SaveChanges();
            return RedirectToAction("CreateGroup"); // Toast Status succeful changes and redirect to pending applications
        }

        public ActionResult AddMembers()
        {
            var userId = User.Identity.GetUserId();
            var listOfMyGroups = _context.Groups.Where(s => s.UserId == userId).ToList();

            var groupViewModel = new GroupViewModel
            {
                Groups = listOfMyGroups
            };
            return View(groupViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddMembers(GroupViewModel groupViewModel)
        {
            if (!ModelState.IsValid)
            {
                var viewModel = new GroupViewModel
                {
                    Groups = _context.Groups.ToList(),
                    GroupMembers = groupViewModel.GroupMembers,
                    Student = groupViewModel.Student
                };
                return View(viewModel);
            }

            var student = _context.Students.FirstOrDefault(s => s.StudentNumber == groupViewModel.Student.StudentNumber);

            if (student == null)
                return HttpNotFound(); // diplay message student not found

            var groupMember = new GroupMember
            {
                UserId = student.UserId,
                Student = student,
                GroupId = groupViewModel.GroupMembers.Id
            };
            _context.GroupMembers.Add(groupMember);
            _context.SaveChanges();
            return RedirectToAction("MyGroups"); // Toast Status succeful changes and redirect to pending applications
        }

        public ActionResult GetEvent()
        {
            return View("GetEvents");
        }

        public JsonResult GetEvents()
        {
            var events = _context.Events.ToList();
            return new JsonResult { Data = events, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }


    }
}