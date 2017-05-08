using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using System.Resources;

namespace LoginMVC2.Models
{
    public class TamanhoString : StringLengthAttribute
    {
        public TamanhoString() : this(20) { }

        public TamanhoString(int tamanhoMaximo) : base(tamanhoMaximo) {
            ErrorMessageResourceName = "StringLengthMessage";
            ErrorMessageResourceType = typeof(ResourceModel);
        }
    }
}