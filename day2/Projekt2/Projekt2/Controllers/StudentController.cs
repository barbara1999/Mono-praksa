using Projekt2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Projekt2.Controllers
{
    [RoutePrefix("api/student")]
    public class StudentController : ApiController
    {
        static List<Student> students = new List<Student>()
        {
            new Student() { Id=1, Name= "John ", Surname="Smith",Address="4051 County Line Road"},
            new Student() { Id=2, Name="Thomass" ,Surname="Hoss", Address="4972 Werninger Street"},
            new Student() { Id=3, Name="Jay",Surname="Storey",Address="2772 Parkiwe Drive"},
            new Student() { Id=4, Name="Felicia",Surname="Brown",Address="1757 Woodridge Lane"}
        };

        [HttpGet]
        [Route("")]
        public HttpResponseMessage GetAllStudent()
        {
            var response = Request.CreateResponse(HttpStatusCode.OK, students);
            return response;
        }

        [HttpGet]
        [Route("{id:int:min(1)}")]
        public HttpResponseMessage GetStudentById([FromUri]int id)
        {
            var student = students.FirstOrDefault(s => s.Id == id);
            if (student == null)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Student does not exist");
            }
            return Request.CreateResponse(HttpStatusCode.OK, student);
        }
        
        [HttpGet]
        [Route("{name:alpha}")]
        public HttpResponseMessage GetStudentByName(string name)
        {
            var student= students.FirstOrDefault(s => s.Name.ToLower() == name.ToLower());
            if (student == null)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Student does not exist");
            }
            return Request.CreateResponse(HttpStatusCode.OK, student);
        }

        [HttpPost]
        [Route("")]
        public HttpResponseMessage PostNewStudent([FromBody]Student student)
        {
            students.Add(student);
            var response = Request.CreateResponse(HttpStatusCode.Created);
            return response;
        }

        [HttpPut]
        [Route("{Id}")]
        public HttpResponseMessage UpdateStudentAddress(int id, [FromBody] string address)
        {
            var student = students.FirstOrDefault(s => s.Id == id);
            if (student == null)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Student does not exist");
            }
            student.Address = address;

            var response = new HttpResponseMessage();
            response.Headers.Add("Message", "Address is successfuly updated");
            return response;
             
        }

        [HttpDelete]
        [Route("{Id}")]
        public HttpResponseMessage Delete(int id)
        {
            var student = students.FirstOrDefault(s => s.Id == id);

            if (student == null)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Studnet does not exist");
            }

            students.Remove(student);
            var response = new HttpResponseMessage();
            response.Headers.Add("Message", "Student is successfuly deleted");
            return response;
            
        }
       

        

        
    }
}
