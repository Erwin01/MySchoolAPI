using MySchoolAPI.DTOs;
using MySchoolAPI.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MySchoolAPI.Data.Interfaces
{
    public interface IStudentRepository
    {

        Task<IEnumerable<Student>> GetAll();

        bool Create(StudentCreationDTO studentCreationDTO);

        bool Delete(int id);

        public bool Update(int id, StudentDTO studentDTO);
        
        Task<IEnumerable<Student>> Search(string searchStudent);

    }
}
