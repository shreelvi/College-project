using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ClassWeb.Models
{
 /// <summary>
 /// Created By: Kishor Simkhada
 /// Role is a designated position for each user.
 /// Each role can be assigned to zero to many users.
 /// Each role user can have one to multiple permissions. 
 /// </summary>
    public class Role:DatabaseRolePermission
    {
      
    }
}
