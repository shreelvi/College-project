using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ClassWeb.Models
{
    /// <summary>
    /// Created By: Kishor Simkhada
    /// A set of authorizations for each user. 
    /// Each permission can be assigned to one to many user roles.
    /// </summary>
    public class Permission:DatabaseRolePermission
    {
        
    }
}
