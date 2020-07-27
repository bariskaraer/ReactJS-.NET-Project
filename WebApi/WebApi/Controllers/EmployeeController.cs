using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApi.Models;

namespace WebApi.Controllers
{
    public class EmployeeController : ApiController
    {

        public HttpResponseMessage Get()
        {
            DataTable dt = new DataTable();
            string query = @"select EmployeeID, EmployeeName, 
                Department, MailID, convert(varchar(10),DOJ, 120) as DOJ
                from dbo.Employees";

            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["EmployeeAppDB"].ConnectionString))
            using (var cmd = new SqlCommand(query, con))
            using (var da = new SqlDataAdapter(cmd))
            {
                cmd.CommandType = CommandType.Text;
                da.Fill(dt);
            }

            return Request.CreateResponse(HttpStatusCode.OK, dt);

        }

        public string Post(Employee emp)
        {
            try
            {
                DataTable dt = new DataTable();
                string DOJ = emp.DOJ.ToString().Split(' ')[0];
                string query = @"insert into dbo.Employees
                                (EmployeeName,Department, MailID, DOJ) values 
                                ('" +emp.EmployeeName + @"'
                                ,'" + emp.Department + @"'
                                ,'" + emp.MailID + @"'
                                ,'" + DOJ + @"'
                                )";

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

        public string Put(Employee emp)
        {
            try
            {
                DataTable dt = new DataTable();
                string query = @"update dbo.Employees set 
                EmployeeName = '" + emp.EmployeeName + @"' 
                , Department = '" + emp.Department + @"' 
                , MailID = '" + emp.MailID + @"' 
                , DOJ = '" + emp.DOJ + @"'
                where EmployeeID = "+ emp.EmployeeID + @"
                ";

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
                string query = @"delete from dbo.Employees where EmployeeID = " + id;

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
