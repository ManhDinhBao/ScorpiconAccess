using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace DTO_ScorpionAccess
{
    public class DTO_Department
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string ParentId { get; set; }
        public DTO_CardHolder Manager { get; set; }
        public List<DTO_Department> Groups { get; set; }
        public List<DTO_CardHolder> Employees { get; set; }

        public string RootDeptId { get; set; }
        public string Description { get; set; }

        public DTO_Department()
        {
        }

        public DTO_Department(string id, string name, string parentId, DTO_CardHolder manager, List<DTO_Department> groups, List<DTO_CardHolder> employees, string rootDeptId, string description)
        {
            Id = id;
            Name = name;
            ParentId = parentId;
            Manager = manager;
            Groups = groups;
            Employees = employees;
            RootDeptId = rootDeptId;
            Description = description;
        }

        public IList Children
        {
            get
            {
                return new CompositeCollection()
                {
                new CollectionContainer() { Collection = Groups },
                new CollectionContainer() { Collection = Employees }
                };
            }
        }
    }
}
