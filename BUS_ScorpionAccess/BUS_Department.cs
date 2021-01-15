using DAL_ScorpionAccess;
using DTO_ScorpionAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BUS_ScorpionAccess
{
    public class BUS_Department
    {
        DAL_Department dal = new DAL_Department();
        BUS_CardHolder bus_holder = new BUS_CardHolder();

        public List<DTO_Department> GetAllDepartment()
        {
            DataTable dt = dal.GetAllDepartment();
            List<DTO_Department> departments = new List<DTO_Department>();

            if (dt.Rows.Count < 0)
            {
                return null;
            }

            try
            {
                foreach (DataRow row in dt.Rows)
                {
                    string id = row["Id"] == DBNull.Value ? null : row["Id"].ToString();
                    DTO_Department department = GetDepartmentById(id);
                    departments.Add(department);
                }

                return departments;
            }
            catch
            {
                return null;
            }
        }

        public DTO_Department GetDepartmentById(string id)
        {
            DataTable dt = dal.GetDepartmentById(id);

            if (dt.Rows.Count < 0)
            {
                return null;
            }

            try
            {
                DataRow row = dt.Rows[0];

                DTO_Department department = new DTO_Department();
                department.Id = row["Id"] == DBNull.Value ? null : row["Id"].ToString();
                department.Name = row["Name"] == DBNull.Value ? null : row["Name"].ToString();
                department.ParentId = row["Parent"] == DBNull.Value ? null : row["Parent"].ToString();
                department.RootDeptId = row["RootParent"] == DBNull.Value ? department.Id : row["RootParent"].ToString();
                department.Description = row["Description"] == DBNull.Value ? null : row["Description"].ToString();

                string mamagerId = row["Manager"] == DBNull.Value ? null : row["Manager"].ToString();
                if (mamagerId == null)
                {
                    mamagerId = "-1";
                }
                department.Manager = bus_holder.GetCardHolderByKey(mamagerId);

                department.Employees = bus_holder.GetCardHolderByDept(department.Id);

                department.Groups = GetChildByParentId(department.Id);

                return department;
            }
            catch
            {
                return null;
            }
        }

        public List<DTO_Department> GetChildByParentId(string parentId)
        {
            DataTable dt = dal.GetChildByParentId(parentId);
            List<DTO_Department> departments = new List<DTO_Department>();

            if (dt.Rows.Count < 0)
            {
                return null;
            }

            try
            {
                foreach(DataRow row in dt.Rows)
                {
                    string id = row["Id"] == DBNull.Value ? null : row["Id"].ToString();
                    DTO_Department department = GetDepartmentById(id);
                    departments.Add(department);
                }

                return departments;
            }
            catch
            {
                return null;
            }
        }

        public SQLResult AddNewGroup(DTO_Department group)
        {
            if (group == null)
            {
                return new SQLResult(false, "Nhóm không có thông tin.");
            }

            return dal.AddNewDepartment(group);
        }

        public SQLResult AddNewDepartment(DTO_Department department)
        {
            if (department == null)
            {
                return new SQLResult(false, "Phòng ban/bộ phận rỗng.");
            }

            return dal.AddNewDepartment(department);
        }

        public SQLResult UpdateDepartment(DTO_Department department)
        {
            if (department == null)
            {
                return new SQLResult(false, "Phòng ban/bộ phận rỗng.");
            }

            return dal.UpdateDepartment(department);
        }

        public SQLResult DeleteDepartment(string id)
        {
            if (id == null)
            {
                return new SQLResult(false, "Không tồn tại phòng ban/bộ phận");
            }

            return dal.DeleteDepartment(id);
        }

    }
}
