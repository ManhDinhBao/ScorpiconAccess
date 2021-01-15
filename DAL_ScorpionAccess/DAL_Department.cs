using DTO_ScorpionAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL_ScorpionAccess
{
    public class DAL_Department:DBConnect
    {
        public DataTable GetAllDepartment()
        {
            DataTable dt = new DataTable();

            try
            {
                _conn.Open();

                SqlCommand command = new SqlCommand();
                command.Connection = _conn;
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "spLDepartmentQry";
                command.Parameters.AddWithValue("workType", "Q");
                command.Parameters.AddWithValue("@Id", DBNull.Value);
                command.Parameters.AddWithValue("@Name", DBNull.Value);
                command.Parameters.AddWithValue("@Parent", 0);

                SqlDataAdapter adapter = new SqlDataAdapter();
                adapter.SelectCommand = command;
                adapter.Fill(dt);
            }
            catch (Exception ex)
            {

            }
            finally
            {
                _conn.Close();
            }

            return dt;
        }

        public DataTable GetDepartmentById(string id)
        {
            DataTable dt = new DataTable();

            try
            {
                _conn.Open();

                SqlCommand command = new SqlCommand();
                command.Connection = _conn;
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "spLDepartmentQry";
                command.Parameters.AddWithValue("workType", "Q");
                command.Parameters.AddWithValue("@Id", id);
                command.Parameters.AddWithValue("@Name", DBNull.Value);
                command.Parameters.AddWithValue("@Parent", DBNull.Value);

                SqlDataAdapter adapter = new SqlDataAdapter();
                adapter.SelectCommand = command;
                adapter.Fill(dt);
            }
            catch (Exception ex)
            {

            }
            finally
            {
                _conn.Close();
            }

            return dt;
        }

        public DataTable GetChildByParentId(string parentId)
        {
            DataTable dt = new DataTable();

            try
            {
                _conn.Open();

                SqlCommand command = new SqlCommand();
                command.Connection = _conn;
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "spLDepartmentQry";
                command.Parameters.AddWithValue("workType", "S");
                command.Parameters.AddWithValue("@Id", parentId);
                command.Parameters.AddWithValue("@Name", DBNull.Value);
                command.Parameters.AddWithValue("@Parent", DBNull.Value);

                SqlDataAdapter adapter = new SqlDataAdapter();
                adapter.SelectCommand = command;
                adapter.Fill(dt);
            }
            catch (Exception ex)
            {

            }
            finally
            {
                _conn.Close();
            }

            return dt;
        }

        public SQLResult AddNewDepartment(DTO_Department department)
        {
            DataTable dt = new DataTable();
            SQLResult result = new SQLResult(false, "");

            try
            {
                _conn.Open();

                SqlCommand command = new SqlCommand();
                command.Connection = _conn;
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "spLDepartmentSave";
                command.Parameters.AddWithValue("WorkType", "A");
                command.Parameters.AddWithValue("Id", "");
                command.Parameters.AddWithValue("Name", department.Name);
                command.Parameters.AddWithValue("Parent", department.ParentId);
                if (department.Manager == null)
                {
                    command.Parameters.AddWithValue("Manager", DBNull.Value);
                }
                else
                {
                    command.Parameters.AddWithValue("Manager", department.Manager);
                }

                if (department.Description == null)
                {
                    command.Parameters.AddWithValue("Description", DBNull.Value);
                }
                else
                {
                    command.Parameters.AddWithValue("Description", department.Description);
                }
               

                SqlDataAdapter adapter = new SqlDataAdapter();
                adapter.SelectCommand = command;
                adapter.Fill(dt);

                if (dt.Rows[0]["Result"].ToString() == "OK")
                {
                    result.Result = true;

                }

                result.Detail = dt.Rows[0]["Detail"].ToString();
                result.ExtraData = dt.Rows[0]["ExtraData"].ToString();
            }
            catch (Exception ex)
            {
                result.Detail = ex.Message;
            }
            finally
            {
                _conn.Close();
            }

            return result;
        }

        public SQLResult UpdateDepartment(DTO_Department department)
        {
            DataTable dt = new DataTable();
            SQLResult result = new SQLResult(false, "");

            try
            {
                _conn.Open();

                SqlCommand command = new SqlCommand();
                command.Connection = _conn;
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "spLDepartmentSave";
                command.Parameters.AddWithValue("WorkType", "U");
                command.Parameters.AddWithValue("Id", department.Id);
                command.Parameters.AddWithValue("Name", department.Name);
                command.Parameters.AddWithValue("Parent", department.ParentId);
                if(department.Manager==null)
                {
                    command.Parameters.AddWithValue("Manager", DBNull.Value);
                }
                else
                {
                    command.Parameters.AddWithValue("Manager", department.Manager.Id);
                }
                
                command.Parameters.AddWithValue("Description", department.Description);

                SqlDataAdapter adapter = new SqlDataAdapter();
                adapter.SelectCommand = command;
                adapter.Fill(dt);

                if (dt.Rows[0]["Result"].ToString() == "OK")
                {
                    result.Result = true;

                }

                result.Detail = dt.Rows[0]["Detail"].ToString();
                result.ExtraData = dt.Rows[0]["ExtraData"].ToString();
            }
            catch (Exception ex)
            {
                result.Detail = ex.Message;
            }
            finally
            {
                _conn.Close();
            }

            return result;
        }

        public SQLResult DeleteDepartment(string id)
        {
            DataTable dt = new DataTable();
            SQLResult result = new SQLResult(false, "");

            try
            {
                _conn.Open();

                SqlCommand command = new SqlCommand();
                command.Connection = _conn;
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "spLDepartmentSave";
                command.Parameters.AddWithValue("WorkType", "D");
                command.Parameters.AddWithValue("Id", id);
                command.Parameters.AddWithValue("Name", DBNull.Value);
                command.Parameters.AddWithValue("Parent", DBNull.Value);
                command.Parameters.AddWithValue("Manager", DBNull.Value);
                command.Parameters.AddWithValue("Description", DBNull.Value);


                SqlDataAdapter adapter = new SqlDataAdapter();
                adapter.SelectCommand = command;
                adapter.Fill(dt);

                if (dt.Rows[0]["Result"].ToString() == "OK")
                {
                    result.Result = true;

                }

                result.Detail = dt.Rows[0]["Detail"].ToString();
            }
            catch (Exception ex)
            {
                result.Detail = ex.Message;
            }
            finally
            {
                _conn.Close();
            }

            return result;
        }
    }
}
