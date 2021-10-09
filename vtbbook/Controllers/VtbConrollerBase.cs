using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;

namespace vtbbook.Controllers
{
    public class VtbConrollerBase : ControllerBase
    {
        [NonAction]
        public virtual JsonResult Json(object data)
        {
            return new JsonResult(data);
        }

        [NonAction]
        public virtual JsonResult Json(object data, JsonSerializerSettings serializerSettings)
        {
            if (serializerSettings == null)
            {
                throw new ArgumentNullException(nameof(serializerSettings));
            }

            return new JsonResult(data, serializerSettings);
        }
    }
}
