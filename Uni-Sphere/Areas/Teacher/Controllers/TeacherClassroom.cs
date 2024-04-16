using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Uni_Sphere.Repositories;
using Uni_Sphere.Repositories.IRepositories;

namespace Uni_Sphere.Areas.Teacher.Controllers
{
    [Area("Teacher")]
    [Authorize(Roles = "Teacher")]
    public class TeacherClassroom(ITeacherFunctionality teacherFunctionality, IClassroomRepository classroomRepository) : Controller
    {
        private readonly ITeacherFunctionality _teacherFunctionality = teacherFunctionality;
        private readonly IClassroomRepository _classroomRepository = classroomRepository;

        public IActionResult Add()
        {
            return View();
        }
    }
}
