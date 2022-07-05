using Dapper;
using MySchoolAPI.Data.Interfaces;
using MySchoolAPI.DTOs;
using MySchoolAPI.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace MySchoolAPI.Data.Repository
{
    public class StudentRepository : BaseRepository, IStudentRepository
    {

        public StudentRepository(string connectionString) : base(connectionString)
        {

        }

        /// <summary>
        /// Get All Students
        /// </summary>
        /// <returns> Students </returns>
        public async Task<IEnumerable<Student>> GetAll() 
        {

            using (IDbConnection dbConnection = GetConnection())
            {

                return await dbConnection.QueryAsync<Student>("GetAllStudents", commandType: CommandType.StoredProcedure);
            }
        }




        /// <summary>
        /// Create New Student
        /// </summary>
        /// <param name="studentCreationDTO"></param>
        /// <returns> New </returns>
        public bool Create(StudentCreationDTO studentCreationDTO) 
        {

            IDbTransaction dbTransaction;

            DynamicParameters parameters = new DynamicParameters();

            parameters.Add("@Name", studentCreationDTO.Name);
            parameters.Add("@FirstName", studentCreationDTO.FirstName);
            parameters.Add("@LastName", studentCreationDTO.LastName);
            parameters.Add("@PhoneNumber", studentCreationDTO.PhoneNumber);
            parameters.Add("@Address", studentCreationDTO.Address);
            parameters.Add("@Career", studentCreationDTO.Career);

            using (IDbConnection db = GetConnection())
            {

                db.Open();
                dbTransaction = db.BeginTransaction();

                try
                {
                    db.ExecuteScalar("CreateStudent", commandType: CommandType.StoredProcedure, param: parameters, transaction: dbTransaction);
                    dbTransaction.Commit();
                    db.Close();

                    return true;

                }
                catch (Exception)
                {

                    dbTransaction.Rollback();
                    db.Close();

                    return false;
                }
            }
        }



        /// <summary>
        /// Delete Student
        /// </summary>
        /// <param name="id"></param>
        /// <returns> True </returns>
        public bool Delete(int id)
        {

            DynamicParameters parameters = new DynamicParameters();

            parameters.Add("@Id", id);

            using (IDbConnection db = GetConnection())
            {

                db.Open();

                try
                {

                    db.ExecuteScalar("DeleteStudent", commandType: CommandType.StoredProcedure, param: parameters);
                    db.Close();

                    return true;

                }
                catch (Exception)
                {

                    db.Close();

                    return false;
                }

            }
           
        }



        /// <summary>
        /// Update Student
        /// </summary>
        /// <param name="id"></param>
        /// <param name="studentDTO"></param>
        /// <returns> True </returns>
        public bool Update(int id, StudentDTO studentDTO)
        {

            IDbTransaction dbTransaction;

            DynamicParameters parameters = new DynamicParameters();

            parameters.Add("@Id", id);
            parameters.Add("@Name", studentDTO.Name);
            parameters.Add("@FirstName", studentDTO.FirstName);
            parameters.Add("@LastName", studentDTO.LastName);
            parameters.Add("@PhoneNumber", studentDTO.PhoneNumber);
            parameters.Add("@Address", studentDTO.Address);
            parameters.Add("@Career", studentDTO.Career);

            using (IDbConnection db = GetConnection())
            {

                db.Open();
                dbTransaction = db.BeginTransaction();

                try
                {
                    db.ExecuteScalar("UpdateStudent", commandType: CommandType.StoredProcedure, param: parameters, transaction: dbTransaction);
                    dbTransaction.Commit();
                    db.Close();

                    return true;

                }
                catch (Exception)
                {

                    dbTransaction.Rollback();
                    db.Close();

                    return false;
                }
            }
        }
    }
}
