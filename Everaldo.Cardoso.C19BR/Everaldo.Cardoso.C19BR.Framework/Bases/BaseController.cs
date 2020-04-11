using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Everaldo.Cardoso.C19BR.Framework.Exceptions;
using Everaldo.Cardoso.C19BR.Framework.Notification;
using Everaldo.Cardoso.C19BR.Framework.Resources;
using System;
using System.Collections.Generic;

namespace Everaldo.Cardoso.C19BR.Framework.Bases
{
    public class BaseController: ControllerBase
    {
        protected List<Message> Messages { get; set; }

        protected BaseService Service { get; set; }

        public BaseController(BaseService Service)
        {
            this.Service = Service;
        }
        
        protected virtual IActionResult ProcessReturn(object response = null, Exception ex = null)
        {
            if (Service != null && Service.Messages != null && Service.Messages.Count > 0)
            {
                return BadRequest(Service.Messages); //400 - Recurso não encontrado!
            }
            else if (response != null)
            {
                return Ok(response);  //200 - OK!            
            }
            else if (ex != null &&  ex is InternalApiException)
            {
                var error = "Ocorreu um erro: " + ex.Message + ex.InnerException != null ? " - Detalhes:" + ex.InnerException.Message : string.Empty;
                return BadRequest(error); //400 - Recurso não encontrado!          
            }
            else if (ex != null && !(ex is InternalApiException))
            {
                var error = "Ocorreu um erro: " + ex.Message + (ex.InnerException != null ? " - Detalhes:" + ex.InnerException.Message : string.Empty);
                return StatusCode(StatusCodes.Status500InternalServerError, error); //500 - Erro interno no servidor (Deverá ser oculto)
            }
            else
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ReturnMessagesResources.MessageUnknown); //500 - Algum erro desconhecido....
            }
        }
    }
}
