using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SchoolAdmission.Models;

namespace SchoolAdmission.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class SchoolAdmissionController : ControllerBase
    {
        static List<StudentAdmissionDetailModel> studentAdmissionList = new List<StudentAdmissionDetailModel>();

        [HttpGet]
        public async Task<List<StudentAdmissionDetailModel>> Get()
        {
            StudentAdmissionDetailModel admission1 = new StudentAdmissionDetailModel();
            StudentAdmissionDetailModel admission2 = new StudentAdmissionDetailModel();
            StudentAdmissionDetailModel admission3 = new StudentAdmissionDetailModel();

            admission1.StudentID = 101;
            admission1.StudentName = "Varsha JK";
            admission1.StudentClass = ".NET FSD";
            admission1.DateofJoining = DateTime.Now;
            admission2.StudentID = 102;
            admission2.StudentName = "Tarun S";
            admission2.StudentClass = ".NET FSD";
            admission2.DateofJoining = DateTime.Now;
            admission3.StudentID = 103;
            admission3.StudentName = "Prateek";
            admission3.StudentClass = ".NET FSD";
            admission3.DateofJoining = DateTime.Now;


            studentAdmissionList.Add(admission1);
            studentAdmissionList.Add(admission2);
            studentAdmissionList.Add(admission3);
            return studentAdmissionList;
        }


    }
}