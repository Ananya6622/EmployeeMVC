using Microsoft.Extensions.Configuration;
using ModelLayer;
using RepositoryLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace RepositoryLayer.Services
{
    public class EmployeeRL : IEmployeeRL
    {
        private readonly IConfiguration configuration;

        public EmployeeRL(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        
        public void AddEmployee(EmployeeModel employeeModel) 
        {

            using (SqlConnection con = new SqlConnection(this.configuration.GetConnectionString("EmployeePayRoll")))
            {
                SqlCommand cmd = new SqlCommand("InsertEmployee", con);
                cmd.CommandType = CommandType.StoredProcedure;

                
                cmd.Parameters.AddWithValue("@Name", employeeModel.Name);
                cmd.Parameters.AddWithValue("@ProfileImage", employeeModel.ProfileImage);
                cmd.Parameters.AddWithValue("@Gender", employeeModel.Gender);
                cmd.Parameters.AddWithValue("@Department", employeeModel.Department);
                cmd.Parameters.AddWithValue("@Salary", employeeModel.Salary);
                cmd.Parameters.AddWithValue("@StartDate", employeeModel.StartDate);
                cmd.Parameters.AddWithValue("@Notes", employeeModel.Notes);

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }
        public IEnumerable<EmployeeModel> GetAllEmployees()
        {
            List<EmployeeModel> lstemployee = new List<EmployeeModel>();

            using (SqlConnection con = new SqlConnection(this.configuration.GetConnectionString("EmployeePayRoll")))
            {
                SqlCommand cmd = new SqlCommand("ViewAllEmployees", con);
                cmd.CommandType = CommandType.StoredProcedure;

                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    EmployeeModel employeeModel = new EmployeeModel();

                    employeeModel.EmployeeId = Convert.ToInt32(rdr["EmployeeId"]);
                    employeeModel.Name = rdr["Name"].ToString();
                    employeeModel.ProfileImage = rdr["ProfileImage"].ToString();
                    employeeModel.Gender = rdr["Gender"].ToString();
                    employeeModel.Department = rdr["Department"].ToString();
                    employeeModel.Salary = Convert.ToDecimal(rdr["Salary"]);
                    employeeModel.StartDate = Convert.ToDateTime(rdr["StartDate"]);
                    employeeModel.Notes = rdr["Notes"].ToString();

                    lstemployee.Add(employeeModel);
                }
                con.Close();
            }
            return lstemployee;
        }

        public EmployeeModel UpdateEmployee(EmployeeModel employeeModel)
        {
            using (SqlConnection con = new SqlConnection(this.configuration.GetConnectionString("EmployeePayRoll")))
            {
                SqlCommand cmd = new SqlCommand("AddOrUpdateEmployee", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@EmployeeId", employeeModel.EmployeeId);
                cmd.Parameters.AddWithValue("@Name", employeeModel.Name);
                cmd.Parameters.AddWithValue("@ProfileImage", employeeModel.ProfileImage);
                cmd.Parameters.AddWithValue("@Gender", employeeModel.Gender);
                cmd.Parameters.AddWithValue("@Department", employeeModel.Department);
                cmd.Parameters.AddWithValue("@Salary", employeeModel.Salary);
                cmd.Parameters.AddWithValue("@StartDate", employeeModel.StartDate);
                cmd.Parameters.AddWithValue("@Notes", employeeModel.Notes);

                con.Open();
                cmd.ExecuteNonQuery(); 
                con.Close();
            }
            return employeeModel;
        }
        public bool DeleteEmployee(int? id)
        {

            using (SqlConnection con = new SqlConnection(this.configuration.GetConnectionString("EmployeePayRoll")))
            {
                SqlCommand cmd = new SqlCommand("DeleteEmployee", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@EmployeeId", id);

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
            return true;
        }

        public EmployeeModel GetEmployeeData(int? id)
        {
            EmployeeModel employee = new EmployeeModel();

            using (SqlConnection con = new SqlConnection(this.configuration.GetConnectionString("EmployeePayRoll")))
            {
                SqlCommand cmd = new SqlCommand("GetEmployeeById", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@EmployeeId", id);

                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    employee.EmployeeId = Convert.ToInt32(rdr["EmployeeId"]);
                    employee.Name = rdr["Name"].ToString();
                    employee.ProfileImage = rdr["ProfileImage"].ToString();
                    employee.Gender = rdr["Gender"].ToString();
                    employee.Department = rdr["Department"].ToString();
                    employee.Salary = Convert.ToDecimal(rdr["Salary"]);
                    employee.StartDate = Convert.ToDateTime(rdr["StartDate"]);
                    employee.Notes = rdr["Notes"].ToString();
                }
            }
            return employee;
        }

        public EmployeeModel Login(EmpLogin empLogin)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(this.configuration.GetConnectionString("EmployeePayRoll")))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("EmpLogin", con)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    cmd.Parameters.AddWithValue("EmployeeId", empLogin.EmployeeId);
                    cmd.Parameters.AddWithValue("Name", empLogin.Name);

                    EmployeeModel model = new EmployeeModel();
                    SqlDataReader rd = cmd.ExecuteReader();
                    if (rd.Read())
                    {
                        model.EmployeeId = rd.GetInt32(0);
                        model.Name = rd.GetString(1);
                        model.ProfileImage = rd.GetString(2);
                        model.Gender = rd.GetString(3);
                        model.Department = rd.GetString(4);
                    }
                    return model;
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
            
        }

        public EmployeeModel GetEmployeeByName(string name)
        {
            EmployeeModel model = new EmployeeModel();
            using (SqlConnection con = new SqlConnection(this.configuration.GetConnectionString("EmployeePayRoll")))
            {
                SqlCommand cmd = new SqlCommand("GetEmployeeByName", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("Name", name);
                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                while(rdr.Read())
                {
                    model.EmployeeId = Convert.ToInt32(rdr["EmployeeId"]);
                    model.Name = rdr["Name"].ToString();
                    model.ProfileImage = rdr["ProfileImage"].ToString();
                    model.Gender = rdr["Gender"].ToString();
                    model.Department = rdr["Department"].ToString();
                    model.Salary = Convert.ToDecimal(rdr["Salary"]);
                    model.StartDate = Convert.ToDateTime(rdr["StartDate"]);
                    model.Notes = rdr["Notes"].ToString();
                }
            }
            return model;
        }

    }
}
