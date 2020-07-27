using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

using System.Data;
using WebApi.Models;
using System.Data.SqlClient;
using System.Configuration;

namespace WebApi.Controllers
{
    public class DepartmentController : ApiController
    {
        public HttpResponseMessage Get()
        {
            DataTable dt = new DataTable();
            string query = @"select DepartmentID, DepartmentName from dbo.Departments";

            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["EmployeeAppDB"].ConnectionString))
            using (var cmd = new SqlCommand(query,con))
            using (var da = new SqlDataAdapter(cmd))
            {
                cmd.CommandType = CommandType.Text;
                da.Fill(dt);
            }

            return Request.CreateResponse(HttpStatusCode.OK,dt);

        }

        public string Post(Department dep)
        {
            try
            {
                DataTable dt = new DataTable();
                string query = @"insert into dbo.Departments values('" + dep.DepartmentName +@"')";

                using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["EmployeeAppDB"].ConnectionString))
                using (var cmd = new SqlCommand(query, con))
                using (var da = new SqlDataAdapter(cmd))
                {
                    cmd.CommandType = CommandType.Text;
                    da.Fill(dt);
                }

                return "Added succesfully";
            }
            catch (Exception)
            {
                return "Failed to Add";
            }
        }



        public string Put(Department dep)
        {
            try
            {
                DataTable dt = new DataTable();
                string query = @"update dbo.Departments set DepartmentName = '" + dep.DepartmentName + @"'
                                where DepartmentID = "+ dep.DepartmentID +@" ";

                using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["EmployeeAppDB"].ConnectionString))
                using (var cmd = new SqlCommand(query, con))
                using (var da = new SqlDataAdapter(cmd))
                {
                    cmd.CommandType = CommandType.Text;
                    da.Fill(dt);
                }

                return "Changed succesfully";
            }
            catch (Exception)
            {
                return "Failed to Change";
            }
        }


        public string Delete(int id)
        {
            try
            {
                DataTable dt = new DataTable();
                string query = @"delete from dbo.Departments where DepartmentID = " + id;

                using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["EmployeeAppDB"].ConnectionString))
                using (var cmd = new SqlCommand(query, con))
                using (var da = new SqlDataAdapter(cmd))
                {
                    cmd.CommandType = CommandType.Text;
                    da.Fill(dt);
                }

                return "Deleted succesfully";
            }
            catch (Exception)
            {
                return "Failed to delete";
            }
        }





    }
}
